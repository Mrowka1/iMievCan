using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace iMievCan
{
    public partial class Form2 : Form
    {
        System.IO.Ports.SerialPort Serial;
        TcpClientHandler tcpClientHandler;

        public DataTable dataModel;

     
        public Form2()
        {
            InitializeComponent();     
        }



     

      

        private void Form1_Load(object sender, EventArgs e)
        {
                        LoadData();
        }

        private void cbSerialPorts_DropDown(object sender, EventArgs e)
        {
            cbSerialPorts.Items.Clear();
            foreach (string PortName in System.IO.Ports.SerialPort.GetPortNames())
            {
                cbSerialPorts.Items.Add(PortName);
            }
        }

        private void btnSerialConnect_Click(object sender, EventArgs e)
        {

            CloseSerial();

            Serial = new System.IO.Ports.SerialPort(cbSerialPorts.Text, 115200, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
            Serial.Handshake = System.IO.Ports.Handshake.None;

            Serial.DtrEnable = true;
            Serial.DataReceived += Serial_DataReceived;

            Serial.Open();
        }

        private void Serial_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {           
        }

     
        void CloseSerial()
        {
            if (Serial != null)
            {
                Serial.DataReceived -= Serial_DataReceived;
                Serial.DtrEnable = false;
                Serial.DiscardInBuffer();
                if (Serial.IsOpen) { Serial.Close(); }
                Serial = null;
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            tcpClientHandler?.Disconnect();
            CloseSerial();
            SaveData();
        }

        private void dgPids_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
          
       
        }

        private void dgPids_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
        }
        void SaveData()
        {
          
        }
        void LoadData()
        {
           
        }

        private void dgPids_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
          }

        private void dgPids_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        bool tcpStarted = false;
        private async void btnTcpConnect_Click(object sender, EventArgs e)
        {
            tcpStarted = false;
            tcpClientHandler?.Disconnect();
            tcpClientHandler = null;

            tcpClientHandler = new TcpClientHandler(txtTcp.Text, 7777);
            await tcpClientHandler.ConnectAsync();

            tcpClientHandler.DataReceived += TcpClientHandler_DataReceived;

            bool con = true;
            while (con)
            {
                Task.Delay(2000).Wait();
                try
                {
                    await tcpClientHandler.SendDataAsync("ATI");
                    con = false;
                }
                catch
                {
                    con = true;
                }

            }



            string[] cmds =
                {            "ATZ", "STP 0", "ATI", "ATSP6", "STSBR 500000", "ATCER 0","ATH1" , "ATCF 300"   ,"ATCM 700"       };


            for (int i = 0; i < cmds.Length; i++)
            {
                await tcpClientHandler.SendDataAsync(cmds[i]);
                Task.Delay(1000).Wait();
            }




            await tcpClientHandler.SendDataAsync("ATMA");
            tcpStarted = true;
            Task.Delay(2000).Wait();


          /*  _ = Task.Run(async () =>
            {

                while (tcpStarted)
                {
                    Task.Delay(5000).Wait();
                    await tcpClientHandler.SendDataAsync("ATMA");

                }

            });*/


        }

        private void TcpClientHandler_DataReceived(string obj)
        {
            if (!tcpStarted) return;
            Debug.WriteLine(obj);

            if (obj == "STOPPED" || obj == "BUFFER FULL")
            {
                 tcpClientHandler.SendDataAsync("ATMA");
            }

            Task.Run(() =>
            {               

            });

        }
    }
}
