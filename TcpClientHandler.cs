using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class TcpClientHandler
{
    private readonly string _serverIp;
    private readonly int _serverPort;
    private TcpClient _tcpClient;
    private NetworkStream _networkStream;
    private CancellationTokenSource _cancellationTokenSource;

    public event Action<string> DataReceived;

    public TcpClientHandler(string serverIp, int serverPort)
    {
        _serverIp = serverIp;
        _serverPort = serverPort;
    }

    public async Task ConnectAsync()
    {
        _tcpClient = new TcpClient();
        await _tcpClient.ConnectAsync(_serverIp, _serverPort);
        _networkStream = _tcpClient.GetStream();
        _cancellationTokenSource = new CancellationTokenSource();

        _ = Task.Run(() => ReceiveDataAsync(_cancellationTokenSource.Token));
    }

    public async Task SendDataAsync(string data)
    {
        if (_networkStream == null || !_networkStream.CanWrite)
            throw new InvalidOperationException("Połączenie nie jest aktywne.");
        data = data + '\r';
        byte[] buffer = Encoding.UTF8.GetBytes(data);
        Debug.WriteLine("Send: " + data);
        await _networkStream.WriteAsync(buffer, 0, buffer.Length);
   
    }

    private async Task ReceiveDataAsync(CancellationToken cancellationToken)
    {
        byte[] buffer = new byte[1024];
        StringBuilder lineBuffer = new StringBuilder();
        TimeSpan timeout = TimeSpan.FromSeconds(5); // Timeout na linię
        DateTime lineStartTime = DateTime.UtcNow;

        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (_networkStream.DataAvailable)
                {
                    int bytesRead = await _networkStream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);
                    if (bytesRead > 0)
                    {
                        for (int i = 0; i < bytesRead; i++)
                        {
                            char c = (char)buffer[i];
                            if (c == '\r') // Koniec linii
                            {
                                string line = lineBuffer.ToString();
                                lineBuffer.Clear();
                                DataReceived?.Invoke(line); // Wywołanie zdarzenia z pełną linią
                                lineStartTime = DateTime.UtcNow; // Reset czasu dla nowej linii
                            }
                            else
                            {
                                lineBuffer.Append(c);
                            }
                        }
                    }
                }

                // Sprawdzanie timeoutu
                if (DateTime.UtcNow - lineStartTime > timeout && lineBuffer.Length > 0)
                {
                    string line = lineBuffer.ToString();
                    lineBuffer.Clear();
                    DataReceived?.Invoke(line); // Wywołanie zdarzenia z niepełną linią
                    lineStartTime = DateTime.UtcNow; // Reset czasu dla nowej linii
                }

                await Task.Delay(10, cancellationToken); // Krótka pauza, by nie obciążać CPU
            }
        }
        catch (Exception ex) when (ex is OperationCanceledException || ex is ObjectDisposedException)
        {
            // Obsługa przerwania lub zamknięcia połączenia
        }
    }


    public void Disconnect()
    {
        _cancellationTokenSource?.Cancel();
        _networkStream?.Close();
        _tcpClient?.Close();
    }
}
