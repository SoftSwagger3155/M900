using CCWin;
using MotionCard;
using Newtonsoft.Json;
using Sunny.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MF900
{
    public partial class FormParameterSet : Form
    {
        private SocketEx socketEx;
        private IPInfo iPInfo;
        private SocketEx.SocketMessage socketMessage;
        private SerialPortHelper serialPortHelper = new SerialPortHelper();
        public FormParameterSet()
        {
            InitializeComponent();
            cmb_Port.SelectedIndex = 0;
            this.serialPortHelper.SerialPortObject.DataReceived +=
                new SerialDataReceivedEventHandler(SerialPort_DataReceived);
            PreLocationSelect(ProgramParamMange.PreLocationPara.IsRegionPreLocation);
            ReadPreLocation();
        }
        #region 程序预定位选择
        public void PreLocationSelect(bool isRegion)
        {
            int rows = isRegion ? ProgramParamMange.ProductDataPara.ProductXY.X * ProgramParamMange.ProductDataPara.ProductXY.Y :
                ProgramParamMange.ProductDataPara.ProductXY.X * ProgramParamMange.ProductDataPara.ProductXY.Y *
                ProgramParamMange.ProductDataPara.RegionCount.X * ProgramParamMange.ProductDataPara.RegionCount.Y;
            if (dgv_Program.Rows.Count > 0)
                dgv_Program.Rows.Clear();
            dgv_Program.Rows.Add(rows);
            for (int i = 0; i < dgv_Program.RowCount; i++)
            {
                dgv_Program[0, i].Value = i + 1;
            }
        }
       
        private void ReadPreLocation()
        {
            radb_RegionPreLocation.Checked = ProgramParamMange.PreLocationPara.IsRegionPreLocation;
            for (int j = 0; j < ProgramParamMange.PreLocationPara.PreLocationList.Count; j++)
            {
                for (int i = 0; i < dgv_Program.RowCount; i++)
                {
                    if (ProgramParamMange.PreLocationPara.PreLocationList[j] == dgv_Program[0, i].Value.ToString())
                    {
                        dgv_Program[1, i].Value = true;
                    }
                }
            }
        }
        private void SavePreLocationSelect()
        {
            List<string> listPre = new List<string>();
            for (int i = 0; i < dgv_Program.RowCount; i++)
            {
                DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgv_Program[1, i];
                if (Convert.ToBoolean(cell.Value))
                {
                    listPre.Add(dgv_Program[0, i].Value.ToString());
                }
            }
            ProgramParamMange.PreLocationPara = new PreLocationModel()
            {
                IsRegionPreLocation = radb_RegionPreLocation.Checked,
                PreLocationList = listPre
            };
            SerializeHelper.SerializeXml<PreLocationModel>(ProgramParamMange.PreLocationPara, ParaFliePath.ProductPath + $"{ProgramParamMange.ProductManage.NowProgramName}\\PreLocationModel.xml");
        }

        private void radb_RegionPreLocation_CheckedChanged(object sender, EventArgs e)
        {
            PreLocationSelect(true);
        }

        private void radb_BoardPreLocation_CheckedChanged(object sender, EventArgs e)
        {
            PreLocationSelect(false);
        }
        #endregion
       
        private void btn_SaveParaSet_Click(object sender, EventArgs e)
        {
            SavePreLocationSelect();
            //SerializeHelper.SerializeXml<SerialPortPara>(new SerialPortPara()
            //{
            //    Port = cmb_Port.SelectedItem.ToString(),
            //    BaudRrate = 9600,
            //    CheckBit = "EVEN",
            //    DataBit = 8,
            //    StopBit = 1
            //}, ParaFliePath.SystemParaPath + "SerialPortPara.xml");
            SaveCommunicPara();
        }

        public void SaveCommunicPara()
        {
            ProgramParamMange.CommumicPara = new CommumicModel()
            {
                SerialPortModel = new SerialPortPara()
                {
                    Port = cmb_Port.SelectedItem.ToString(),
                    BaudRrate = 9600,
                    CheckBit = "EVEN",
                    DataBit = 8,
                    StopBit = 1
                },
                MotionIP = new TCPModel()
                {
                    Ip = "192.168.0.1",
                    Port = 8000,
                },
                ServerIP = new TCPModel()
                {
                    Ip = "127.0.0.1",
                    Port = 8000
                }
            };
            SerializeHelper.SerializeObject<CommumicModel>(ProgramParamMange.CommumicPara,
                ParaFliePath.SystemParaPath+"CommunicPara.josn");
        }
        #region 串口通讯
        //打开串口
        private void btn_OpenSerial_Click(object sender, EventArgs e)
        {
            try
            {
                if (btn_OpenSerial.Text == "打开串口")
                {
                    serialPortHelper.SerialPortObject.BaudRate = SerializeHelper.DeSerializeXml<SerialPortPara>(ParaFliePath.SystemParaPath + "SerialPortPara.xml").BaudRrate;
                    if (SerializeHelper.DeSerializeXml<SerialPortPara>(ParaFliePath.SystemParaPath + "SerialPortPara.xml").CheckBit == "EVEN")
                        serialPortHelper.SerialPortObject.Parity = Parity.Even;
                    else if (SerializeHelper.DeSerializeXml<SerialPortPara>(ParaFliePath.SystemParaPath + "SerialPortPara.xml").CheckBit == "NONE")
                        serialPortHelper.SerialPortObject.Parity = Parity.None;
                    else if (SerializeHelper.DeSerializeXml<SerialPortPara>(ParaFliePath.SystemParaPath + "SerialPortPara.xml").CheckBit == "0DD")
                        serialPortHelper.SerialPortObject.Parity = Parity.Odd;
                    serialPortHelper.SerialPortObject.DataBits = SerializeHelper.DeSerializeXml<SerialPortPara>(ParaFliePath.SystemParaPath + "SerialPortPara.xml").DataBit;
                    serialPortHelper.SerialPortObject.StopBits = SerializeHelper.DeSerializeXml<SerialPortPara>(ParaFliePath.SystemParaPath + "SerialPortPara.xml").StopBit == 1 ? StopBits.One : StopBits.Two;

                    serialPortHelper.OpenSerialPort(cmb_Port.SelectedItem.ToString(), 1);
                    btn_OpenSerial.FillColor = Color.FromArgb(40, 220, 255);
                    btn_OpenSerial.Text = "关闭串口";
                }
                else
                {
                    serialPortHelper.OpenSerialPort(cmb_Port.SelectedItem.ToString(), 0);
                    btn_OpenSerial.FillColor = Color.FromArgb(80, 160, 255);
                    btn_OpenSerial.Text = "打开串口";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("打开或者关闭串口失败！", "提示");
            }
        }

        private void btn_FindSerial_Click(object sender, EventArgs e)
        {
            this.cmb_Port.Items.Clear();
            if (serialPortHelper.PortNames.Length == 0)
                return;
            this.cmb_Port.Items.AddRange(this.serialPortHelper.PortNames);
            this.cmb_Port.SelectedIndex = 0;
        }

        private void btn_SerialSendMes_Click(object sender, EventArgs e)
        {
            serialValue = string.Empty;
            txt_SerialMes.Text = "";
            Thread.Sleep(200);
            if (serialPortHelper.SerialPortObject.IsOpen)
                serialPortHelper?.SendData("T1\r\n", SendFormat.String);
        }
        string serialValue = string.Empty;
        //接收数据
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            this.Invoke(new Action(() => txt_SerialMes.Text = ""));
            ReceiveData(serialPortHelper.ReceiveData());
        }
        private void ReceiveData(byte[] byteData)
        {
            string data = string.Empty;
            data = this.serialPortHelper.AlgorithmHelperObject.BytesToString(byteData, Enum16Hex.Blank);
            int a = data.Length;
            if (data.Substring(0, 1) != "T" || data.Length < 5 || (data.Length > 3 && data.Substring(data.Length - 2, 2) != "\r\n"))
            {
                serialValue += data;
                if (serialValue.Substring(0, 1) == "T" && (serialValue.Length > 3 && serialValue.Substring(serialValue.Length - 2, 2) == "\r\n"))
                    this.Invoke(new Action(() => txt_SerialMes.Text = serialValue));
            }
            else
            {
                serialValue = data;
                this.Invoke(new Action(() => txt_SerialMes.Text = serialValue));
            }
            //this.txt_SerialMes.Invoke(new Action<string>(s => { this.txt_SerialMes.Text = "" + s; }), serialValue);        }
        }
        #endregion

        #region TCP通讯
        private void btn_StartServer_Click(object sender, EventArgs e)
        {
            iPInfo = new IPInfo(txt_ServerIp.Text.Trim(), int.Parse(txt_ServerPort.Text.Trim()));
            socketEx = new SocketEx(iPInfo);
            socketMessage = new SocketEx.SocketMessage(SocketMes);
            if (socketEx.InitSocket(socketMessage, true))
                btn_CloseServer.Enabled = true;
        }

        private void SocketMes(string str)
        {
            if (listBox1.InvokeRequired)
                listBox1.BeginInvoke(new Action(() => listBox1.Items.Add(str)));
            else
                listBox1.Items.Add(str);
        }
        private void btn_CloseServer_Click(object sender, EventArgs e)
        {
            socketEx?.CloseSocket();
        }
        private void btn_ServerSend_Click(object sender, EventArgs e)
        {
            if (txt_ServerSendMes.Text.Trim() != "")
                socketEx?.Send(txt_ServerSendMes.Text.Trim());
        }
        #endregion

    }
}
