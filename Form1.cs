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
    public partial class Form1 : Form
    {
        System.IO.Ports.SerialPort Serial;
        class PID
        {
            public string _PID;

            public static List<PID> _list = new List<PID>();

            public DataGridViewRow row = null;

            public bool CanCalculate = false;
            //  public string[] serialData = { "0", "0", "0", "0", "0", "0", "0", "0" };
            private DateTime[] lastRefresh = { DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now };
            public byte[] CellsValues = { 0, 0, 0, 0, 0, 0, 0, 0 };
            private DateTime GlobalLastRefresh = DateTime.MinValue;
            bool dataReceived = false;
            private long ReceivedInterval = 0;
            private PID(string p_PID)
            {
                _PID = p_PID;
            }

            public static PID Add(string p_PID, DataGridViewRow p_Row)
            {
                PID p = new PID(p_PID);
                _list.Add(p);
                p.row = p_Row;
                p.row.Tag = p;
                p.row.Cells["pid"].Value = p_PID;
                return p;
            }

            public void NewData(byte[] data, bool ignoreRefresh = false)
            {
                if (row != null)
                {

                    DateTime dataReceivedTime = DateTime.Now;
                    for (int i = 0; i <= 7; i++)
                    {
                        if (data[i] != CellsValues[i] || !dataReceived)
                        {
                            row.Cells[i + 2].Value = data[i].ToString();//("X");
                            lastRefresh[i] = DateTime.Now;
                        }
                    }

                    /*  row.Cells[2].Value = data[0];
                      row.Cells[3].Value = data[1];
                      row.Cells[4].Value = data[2];
                      row.Cells[5].Value = data[3];
                      row.Cells[6].Value = data[4];
                      row.Cells[7].Value = data[5];
                      row.Cells[8].Value = data[6];
                      row.Cells[9].Value = data[7];
                      row.Cells[12].Value = DateTime.Now.ToLongTimeString();*/


                    //  row.Cells[12].Value = receivedTimeString;
                    if (!ignoreRefresh)
                    {
                        GlobalLastRefresh = dataReceivedTime;
                        ReceivedInterval = (long)(dataReceivedTime - GlobalLastRefresh).TotalMilliseconds;
                    }

                    CellsValues = data;
                    dataReceived = true;
                    //serialData = data;
                    /*    for (int i = 0; i <= serialData.Length - 1; i++)
                        {
                            serialData[i] = serialData[i].Replace("\r", "");
                        }*/
                    CalculateVal();
                }
            }
            public void RefreshTime()
            {
                DateTime dataReceivedTime = DateTime.Now;
                long receivedTime = (long)(dataReceivedTime - GlobalLastRefresh).TotalMilliseconds;
                string receivedTimeString = receivedTime.ToString() + "ms";
                if (receivedTime > 3600000)
                {
                    receivedTimeString = "---";
                }
                else if (receivedTime > 1000)
                {
                    receivedTimeString = (receivedTime / 1000).ToString() + "s";
                }

                row.Cells[12].Value = receivedTimeString;
            }
            public void CalculateVal()
            {

                string formula = (string)row.Cells[10].Value;
                if (!string.IsNullOrEmpty(formula) && formula.Trim() != "")
                {
                    formula = formula.Replace("b0", CellsValues[0].ToString());
                    formula = formula.Replace("b1", CellsValues[1].ToString());
                    formula = formula.Replace("b2", CellsValues[2].ToString());
                    formula = formula.Replace("b3", CellsValues[3].ToString());
                    formula = formula.Replace("b4", CellsValues[4].ToString());
                    formula = formula.Replace("b5", CellsValues[5].ToString());
                    formula = formula.Replace("b6", CellsValues[6].ToString());
                    formula = formula.Replace("b7", CellsValues[7].ToString());
                    
                    row.Cells[11].Value = Evaluate(formula.Replace("$", string.Empty), formula.StartsWith("$"));
                }

            }

        }

        public Form1()
        {


            InitializeComponent();
        }



        public static string Evaluate(string expression, bool isBoolean = false)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("expression", typeof(string), expression);
                DataRow row = table.NewRow();
                table.Rows.Add(row);
                if (isBoolean)
                {
                    return bool.Parse((string)row["expression"]).ToString();
                }
                else
                {
                    return double.Parse((string)row["expression"]).ToString();
                }
            }
            catch { return "[Error]"; }

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

            try
            {
                String serialString = Serial.ReadLine().Replace("\r", string.Empty);

                string[] serialData = serialString.Split(':');
                if (serialData.Length == 9)
                {
                    if (serialData[0] != "0" && serialData[0].Length < 5)
                    {
                        BeginInvoke((Action)(() =>
                        {
                            evaluateResponse(serialData);
                        }));
                    }
                }
            }
            catch { }
        }

        void evaluateResponse(string[] serialData)
        {
            string senderPID = int.Parse(serialData[0]).ToString("X").ToUpper();
            if (senderPID == "346")
            {
                Debug.WriteLine("");
            }
            List<PID> pids = new List<PID>();
            byte[] Values = { 0, 0,0, 0, 0, 0, 0, 0 };
            for (int i = 0; i <= Values.Length - 1; i++)
            {
               // byte val = 0;
                // byte.TryParse(serialData[i + 1], out val);

                Values[i] = byte.Parse(serialData[i + 1]);// byte.Parse(serialData[i]);
            }

            foreach (PID p in PID._list)
            {
                if (p._PID == senderPID) pids.Add(p);
            }

            if (pids.Count == 0)
            {
                DataGridViewRow newRow = dgPids.Rows[dgPids.Rows.Add()];
                pids.Add(PID.Add(senderPID, newRow));
            }

            foreach (PID pid in pids)
            {
                pid.NewData(Values);
            }

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
            CloseSerial();
            SaveData();
        }

        private void dgPids_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 10)
                {
                    PID p = (PID)dgPids.Rows[e.RowIndex].Tag;
                    p.CanCalculate = p.row.Cells[10].Value.ToString().Trim() != "";
                    p.CalculateVal();
                }
                else if (e.ColumnIndex == 1)
                {
                    DataGridViewRow newRow = dgPids.Rows[e.RowIndex];
                    if (newRow.Tag == null)
                    {
                        if (newRow.Cells[1].Value != null && newRow.Cells[1].Value.ToString().Trim() != "")
                        {
                            string s_newPID = newRow.Cells[1].Value.ToString();

                            byte[] data = { 0, 0, 0, 0, 0, 0, 0, 0 };
                            foreach (PID pid in PID._list)
                            {
                                if (pid._PID == s_newPID)
                                {
                                    /* for (int i = 0; i <= pid.serialData.Length - 1; i++)
                                     {
                                         data[i] = pid.serialData[i].ToString();
                                     }*/
                                    data = pid.CellsValues;
                                    break;
                                }
                            }

                            PID.Add(s_newPID, newRow).NewData(data);
                            SaveData();
                        }
                        else
                        {
                            dgPids.Rows.Remove(newRow);
                        }
                    }

                }
                else if (e.ColumnIndex == 0)
                {
                    SaveData();
                }
            }
            catch { }
        }

        private void dgPids_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 10)
                {
                    PID p = (PID)dgPids.Rows[e.RowIndex].Tag;
                    p.CanCalculate = false;

                }
            }
            catch { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }
        void SaveData()
        {
            string sToSave = "";
            foreach (DataGridViewRow row in dgPids.Rows)
            {
                string sRow = "";
                if (row.Tag == null) continue;
                if (row.Cells["title"].Value == null || row.Cells["title"].Value.ToString() == "") continue;


                for (int i = 0; i <= row.Cells.Count - 1; i++)
                {
                    string sVal = "";
                    if (row.Cells[i].Value != null) sVal = row.Cells[i].Value.ToString();
                    sRow += sVal + ";";
                }
                sToSave += sRow + "\n";

            }
            System.IO.File.WriteAllText(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\data.csv", sToSave);
        }
        void LoadData()
        {
            string sFilePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\data.csv";
            if (System.IO.File.Exists(sFilePath))
            {
                string[] lines = System.IO.File.ReadAllLines(sFilePath);

                foreach (string line in lines)
                {
                    string[] dataValues = line.Split(';');
                    if (dataValues.Length >= 12)
                    {
                        string senderPID = dataValues[1];
                        List<string> lData = new List<string>();
                        lData.AddRange(dataValues);
                        while (lData.Count < dgPids.Columns.Count)
                        {
                            lData.Add("");
                        }
                        dataValues = lData.ToArray();
                        byte[] byteData = new byte[8];
                        for (int i = 0; i <= 7; i++)
                        {
                            try
                            {
                                byteData[i] = Convert.ToByte(dataValues[i + 2], 16);
                            }
                            catch { }
                        }
                        DataGridViewRow newRow = dgPids.Rows[dgPids.Rows.Add(dataValues)];
                        PID pid = PID.Add(senderPID, newRow);

                        pid.NewData(byteData, true);

                        newRow.Cells["lastupdate"].Value = dataValues[12];
                    }
                }
            }
        }

        private void dgPids_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            try
            {



            }
            catch { }

        }

        private void dgPids_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (PID pid in PID._list)
            {
                pid.RefreshTime();
            }
        }
    }
}
