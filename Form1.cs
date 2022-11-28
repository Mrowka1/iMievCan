using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            public string[] serialData = { "0", "0", "0", "0", "0", "0", "0", "0" };
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

            public void NewData(string[] data)
            {
                if (row != null)
                {
                    row.Cells[2].Value = data[0];
                    row.Cells[3].Value = data[1];
                    row.Cells[4].Value = data[2];
                    row.Cells[5].Value = data[3];
                    row.Cells[6].Value = data[4];
                    row.Cells[7].Value = data[5];
                    row.Cells[8].Value = data[6];
                    row.Cells[9].Value = data[7];
                    row.Cells[12].Value = DateTime.Now.ToLongTimeString();

                    serialData = data;
                    for (int i = 0; i <= serialData.Length - 1; i++)
                    {
                        serialData[i] = serialData[i].Replace("\r", "");
                    }
                    CalculateVal();
                }
            }

            public void CalculateVal()
            {
                if (serialData != null && serialData.Length >= 8)
                {
                    string formula = (string)row.Cells[10].Value;
                    if (!string.IsNullOrEmpty(formula) && formula.Trim() != "")
                    {
                        formula = formula.Replace("b0", Convert.ToByte(serialData[0], 16).ToString());
                        formula = formula.Replace("b1", Convert.ToByte(serialData[1], 16).ToString());
                        formula = formula.Replace("b2", Convert.ToByte(serialData[2], 16).ToString());
                        formula = formula.Replace("b3", Convert.ToByte(serialData[3], 16).ToString());
                        formula = formula.Replace("b4", Convert.ToByte(serialData[4], 16).ToString());
                        formula = formula.Replace("b5", Convert.ToByte(serialData[5], 16).ToString());
                        formula = formula.Replace("b6", Convert.ToByte(serialData[6], 16).ToString());
                        formula = formula.Replace("b7", Convert.ToByte(serialData[7], 16).ToString());
                        row.Cells[11].Value = Evaluate(formula);
                    }
                }
            }

        }

        public Form1()
        {


            InitializeComponent();
        }



        public static double Evaluate(string expression)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("expression", typeof(string), expression);
                DataRow row = table.NewRow();
                table.Rows.Add(row);
                return double.Parse((string)row["expression"]);
            }
            catch { return -9999; }

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

            Serial = new System.IO.Ports.SerialPort(cbSerialPorts.Text, 9600, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
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
                    if (serialData[0] != "0" && serialData[0].Length == 3)
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
            string senderPID = serialData[0];

            List<PID> pids = new List<PID>();

            for (int i = 0; i < serialData.Length - 1; i++)
            {
                serialData[i] = serialData[i];
            }

            foreach (PID p in PID._list)
            {
                if (p._PID == serialData[0]) pids.Add(p);
            }

            if (pids.Count == 0)
            {
                DataGridViewRow newRow = dgPids.Rows[dgPids.Rows.Add()];
                pids.Add(PID.Add(senderPID, newRow));
            }

            foreach (PID pid in pids)
            {
                pid.NewData(new string[] { serialData[1], serialData[2], serialData[3], serialData[4], serialData[5], serialData[6], serialData[7], serialData[8] });
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

                            string[] data = { "0", "0", "0", "0", "0", "0", "0", "0" };
                            foreach (PID pid in PID._list)
                            {
                                if (pid._PID == s_newPID)
                                {
                                    for (int i = 0; i <= pid.serialData.Length - 1; i++)
                                    {
                                        data[i] = pid.serialData[i].ToString();
                                    }
                                }
                            }

                            PID.Add(s_newPID, newRow).NewData(data);
                        }
                        else
                        {
                            dgPids.Rows.Remove(newRow);
                        }
                    }

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
                    string[] data = line.Split(';');
                    if (data.Length >= 12)
                    {
                        string senderPID = data[1];
                        List<string> lData = new List<string>();
                        lData.AddRange(data);
                        while (lData.Count < dgPids.Columns.Count)
                        {
                            lData.Add("");
                        }
                        data = lData.ToArray();
                        DataGridViewRow newRow = dgPids.Rows[dgPids.Rows.Add(data)];
                        PID pid = PID.Add(senderPID, newRow);

                        pid.NewData(new string[] { data[2], data[3], data[4], data[5], data[6], data[7], data[8], data[9] });

                        newRow.Cells["lastupdate"].Value = data[12];
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
    }
}
