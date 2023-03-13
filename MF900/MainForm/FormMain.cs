using CCWin;
using HVision;
using MotionCard;
using Newtonsoft.Json;
using Sunny.UI;
using System;
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
using Timer = System.Windows.Forms.Timer;

namespace MF900
{
    public partial class FormMain : Form  //CCSkinMain
    {
        private Form formCurrent;
        private FormButtonMain formButton;
        private FormProgramSet formProgramSet;
        private FormRunUI formRun;
        private FormDebug formDebug;
        private FormParameterSet formParameterSet;
        private FormPanel formPanel;
        private FormMachineState formMachineState;
        private FormMaintaining formMaintaining;
        private FormSetProcess formSetProcess = new FormSetProcess();

        private MotionBase zMotion;
        private readonly DetectionStartPro detectionStart;
        private static MachineState machineState = MachineState.未复位;
        private static SerialPortHelper serialPort = new SerialPortHelper();
        private Dictionary<MachineState, Color> stateColor = new Dictionary<MachineState, Color>();
        public FormMain()
        {
            InitializeComponent();
            ProgramParamMange.ReadAxisPara();
            ProgramParamMange.ReadBasePara();
            OpenMotionCar(); //连接控制器
            NewForm();
            OpenForm(formButton, panelButtonShow);
            OpenForm(formRun, panelMainShow);
            formCurrent = formRun;
            SetCurrentProduct(ProgramParamMange.ProductManage.NowProgramName);
            detectionStart = new DetectionStartPro(zMotion, ProgramParamMange.AxisPara, ProgramParamMange.AxisSpeed);

        }
        private void FormMain_Shown(object sender, EventArgs e)
        {
            InitalCamera();
            EventBing();
            AddStateColor();
            TimerUpdataUI();
            listBox_VelSelect.SelectedIndex = 1;
            BaseTask.vecPercent = ProgramParamMange.RunFuncPara.VelPrecent / 100;
            detectionStart.StartTask();
        }
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("是否关闭软件？", "提示", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                e.Cancel = true;
                return;
            }
            zMotion?.CloseCard();
            this.Controls.Clear();
            ProgramParamMange.MyCameras?["上相机"].Close();
            ProgramParamMange.MyCameras?["下相机"].Close();
            System.Diagnostics.Process.GetCurrentProcess().Kill();  //杀死所有线程
            #region 轴参数
            //axisPara.Add("上模旋转轴", new AxisPara() { AxisNum = 0, homeIo = 66, Fwd = 65, Rev = 64 });
            //axisPara.Add("下模旋转轴", new AxisPara() { AxisNum = 1, homeIo = 82, Fwd = 81, Rev = 80 });
            //axisPara.Add("托板升降轴", new AxisPara() { AxisNum = 2, homeIo = 98, Fwd = 97, Rev = 96 });
            //axisPara.Add("上模Z轴", new AxisPara() { AxisNum = 3, homeIo = 114, Fwd = 113, Rev = 112 });
            //axisPara.Add("下模Z轴", new AxisPara() { AxisNum = 4, homeIo = 130, Fwd = 129, Rev = 128 });
            //axisPara.Add("上模Y轴", new AxisPara() { AxisNum = 5, homeIo = 146, Fwd = 145, Rev = 144 });
            //axisPara.Add("下模Y轴", new AxisPara() { AxisNum = 6, homeIo = 162, Fwd = 161, Rev = 160 });
            //axisPara.Add("上模X轴", new AxisPara() { AxisNum = 7, homeIo = 178, Fwd = 177, Rev = 176 });
            //axisPara.Add("下模X轴", new AxisPara() { AxisNum = 8, homeIo = 194, Fwd = 193, Rev = 192 });
            //SerializeHelper.SerializeDictionary(axisPara, ParaFliePath.SystemPath + "MF900_AxisPara.xml");
            #endregion

        }
        public void SetCurrentProduct(string name)
        {
            this.lbl_CurrentPro.Text = name;
        }
        /// <summary>
        /// 初始化相机
        /// </summary>
        private void InitalCamera()
        {
            ProgramParamMange.MyCameras = new Dictionary<string, BaslerCamera>();
            ProgramParamMange.MyCameras.Add("上相机", new BaslerCamera("上相机"));
            ProgramParamMange.MyCameras.Add("下相机", new BaslerCamera("下相机"));
            ProgramParamMange.MyCameras["上相机"]?.Open();
            ProgramParamMange.MyCameras["下相机"]?.Open();
        }
        private void NewForm()
        {
            formProgramSet = new FormProgramSet();
            formRun = new FormRunUI();
            formButton = new FormButtonMain();
            formMachineState = new FormMachineState();
            formParameterSet = new FormParameterSet();
            formDebug = new FormDebug();
            formPanel = new FormPanel(formProgramSet);
            formMaintaining = new FormMaintaining();
            formPanel.ParaUpdateEvent += formRun.UpdateDataGridEvent;
        }
        
        /// <summary>
        /// 添加状态颜色显示
        /// </summary>
        private void AddStateColor()
        {
            foreach (MachineState e in Enum.GetValues(typeof(MachineState)))
            {
                if (e == MachineState.报警 || e == MachineState.复位失败 || e == MachineState.停止)
                    stateColor.Add(e, Color.FromArgb(255, 0, 0));
                else if (e == MachineState.测试中 || e == MachineState.复位完成)
                    stateColor.Add(e, Color.FromArgb(0, 255, 0));
                else stateColor.Add(e, Color.Yellow);
            }
        }
        /// <summary>
        /// 连接控制卡
        /// </summary>
        /// <returns></returns>
        private bool OpenMotionCar()
        {
            zMotion = new Zmotion("192.168.0.25");
            zMotion = new Zmotion("127.0.0.1");
            if (!zMotion.InitCard().IsSuccess)
            {
                MessageBox.Show("连接控制器失败,请检查网线或者IP设置!", "提示");
                return false;
            }
            for (int i = 0; i < 9; i++)
            {
                zMotion.ClearAlarm((uint)i, 0);
            }
            zMotion.SetAllAxisEnable(9, 1);
            zMotion.SetPulseEquival(ProgramParamMange.AxisPulse);   //设置脉冲
            MotionCommons.SetMotion(zMotion);
            return true;
        }

        #region 串口通信
        private bool OpenOrCloseSerialPort(int isOpen)
        {
            if (isOpen == 0)
            {
                serialPort.SerialPortObject.DataReceived -= new SerialDataReceivedEventHandler(SerialPort_DataReceived);
                return serialPort.OpenSerialPort(SerializeHelper.DeSerializeXml<SerialPortPara>(ParaFliePath.SystemParaPath + "SerialPortPara.xml").Port, 0);
            }
            serialPort.SerialPortObject.BaudRate = SerializeHelper.DeSerializeXml<SerialPortPara>(ParaFliePath.SystemParaPath + "SerialPortPara.xml").BaudRrate;
            if (SerializeHelper.DeSerializeXml<SerialPortPara>(ParaFliePath.SystemParaPath + "SerialPortPara.xml").CheckBit == "EVEN")
                serialPort.SerialPortObject.Parity = System.IO.Ports.Parity.Even;
            else if (SerializeHelper.DeSerializeXml<SerialPortPara>(ParaFliePath.SystemParaPath + "SerialPortPara.xml").CheckBit == "NONE")
                serialPort.SerialPortObject.Parity = System.IO.Ports.Parity.None;
            else if (SerializeHelper.DeSerializeXml<SerialPortPara>(ParaFliePath.SystemParaPath + "SerialPortPara.xml").CheckBit == "0DD")
                serialPort.SerialPortObject.Parity = System.IO.Ports.Parity.Odd;
            serialPort.SerialPortObject.DataBits = SerializeHelper.DeSerializeXml<SerialPortPara>(ParaFliePath.SystemParaPath + "SerialPortPara.xml").DataBit;
            serialPort.SerialPortObject.StopBits = SerializeHelper.DeSerializeXml<SerialPortPara>(ParaFliePath.SystemParaPath + "SerialPortPara.xml").StopBit == 1 ? StopBits.One : StopBits.Two;

            serialPort.SerialPortObject.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
            return serialPort.OpenSerialPort(SerializeHelper.DeSerializeXml<SerialPortPara>(ParaFliePath.SystemParaPath + "SerialPortPara.xml").Port, 1);
        }
        public static void SerialProtSendMes(string mes)
        {
            serialValue = string.Empty;
            serialPort.SendData(mes, SendFormat.String);
        }
        //接收数据
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            ReceiveData(serialPort.ReceiveData());
        }
        private static string serialValue = string.Empty;
        private void ReceiveData(byte[] byteData)
        {
            string data = string.Empty;
            data = serialPort.AlgorithmHelperObject.BytesToString(byteData, Enum16Hex.None);
            if (data.Substring(0, 1) != "T" || data.Length < 5 ||(data.Length > 3 && data.Substring(data.Length - 2, 2) != "\r\n"))
            {
                serialValue += data;
                if (serialValue.Substring(0, 1) == "T" && (serialValue.Length > 3 && serialValue.Substring(serialValue.Length - 2, 2) == "\r\n"))
                    VisionDataDisop(serialValue);
            }
            else
            {
                serialValue = data;
                VisionDataDisop(serialValue);
            }
        }

        /// <summary>
        /// 视觉数据处理
        /// </summary>
        /// <param name="data"></param>
        private void VisionDataDisop(string data)
        {
            //if (data.Contains("NG"))
            //{
            //    feedStocketPro.visionResult = false;
            //    DevicePauseRun("CCD拍照失败,设备暂停运行");
            //    SetMachinState(MachineState.报警);
            //}
            //switch (data.Substring(0, 2))
            //{
            //    case "T0": //T0,0,0,0,#
            //        feedStocketPro.reviceT0 = true;
            //        break;
            //    case "T1":
            //        if (!feedStocketPro.visionResult) break;
            //        feedStocketPro.upX = float.Parse(data.Split(',')[1]);
            //        feedStocketPro.upY = float.Parse(data.Split(',')[2]);
            //        feedStocketPro.upR = float.Parse(data.Split(',')[3]);
            //        feedStocketPro.reviceT0 = true;
            //        break;
            //    case "T2": //T0,0,0,0,#
            //        feedStocketPro.reviceT1 = true;
            //        break;
            //    case "T3":
            //        if (!feedStocketPro.visionResult) break;
            //        feedStocketPro.downX = float.Parse(data.Split(',')[1]);
            //        feedStocketPro.downY = float.Parse(data.Split(',')[2]);
            //        feedStocketPro.downR = float.Parse(data.Split(',')[3]);
            //        feedStocketPro.reviceT1 = true;
            //        break;
            //}
        }
        #endregion

        /// <summary>
        /// 事件绑定
        /// </summary>
        private void EventBing()
        {
            //窗体切换绑定
            formButton.ShowsubformAction += this.Showsubform; 
            formProgramSet.ShowOperationformAction += this.ShowOperationForm;
            formProgramSet.ShowMainformAction += this.Showsubform;
            formProgramSet.ShowProductForm += formPanel.Showsubform;
            formPanel.FormButtonChange+= this.ShowOperationForm;
            formPanel.FormMainChange += this.Showsubform;
            formSetProcess.FormSwitch += formPanel.Showsubform;
            //状态设置绑定
            formMaintaining.deviceReset.SetMachineState += this.SetMachinState;
            detectionStart.SetMachineState += this.SetMachinState;
            formProgramSet.SetMainFormProName += SetCurrentProduct;

        }

        #region 界面切换
        //关闭panel中的窗体
        private void CloseFrm(Panel panel)
        {
            foreach (Control item in panel.Controls)
            {
                if (item is Form)
                {
                    Form form = (Form)item;
                    form.Close();
                    panel.Controls.Remove(form);
                }
            }
        }

        /// <summary>
        /// 主界面切换
        /// </summary>
        /// <param name="name"></param>
        private void Showsubform(string name)
        {
            Task.Run(new Action(() =>
            {
                this.Invoke(new Action(() =>
                {
                    formParameterSet = new FormParameterSet();
                    foreach (Form f in new Form[] { formRun, formDebug,formProgramSet, new FormFunc(),
                     formMachineState,formParameterSet,formPanel,formMaintaining})
                    {
                        if (f.Name as string == name)//&& formCurrent.Name != f.Name
                        {
                            formCurrent = f;
                            OpenForm(f, panelMainShow);
                        }
                    }
                }));
            }));
        }
        /// <summary>
        /// 操作界面切换
        /// </summary>
        /// <param name="name"></param>
        private void ShowOperationForm(string name)
        {
            foreach (Form f in new Form[] { formButton, formSetProcess })
            {
                if (f.Name as string == name)
                    OpenForm(f, panelButtonShow);
            }
        }
        /// <summary>
        /// 窗体嵌入
        /// </summary>
        /// <param name="form"></param>
        public void OpenForm(Form childForm, Panel panel)
        {
            try
            {
                foreach (Control item in panel.Controls)
                {
                    if (item is Form)
                    {
                        ((Form)item).Visible = false;
                    }
                }
                if (childForm != null)
                {
                    childForm.TopLevel = false; //将子窗体设置成非顶级控件
                    childForm.Parent = panel;
                    childForm.Dock = DockStyle.Fill;   //随着容S器大小自动调整窗体大小
                    childForm.BringToFront();
                    childForm.Show();
                    this.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("窗体切换错误:" + ex.Message);
            }
        }

        #endregion
        /// <summary>
        /// 设置设备状态
        /// </summary>
        /// <param name="machineState"></param>
        private void SetMachinState(MachineState machineStates)
        {
            machineState = machineStates;
        }
        private void StateLable()
        {
            if(lbl_MachineState.Text != machineState.ToString())
            {
                lbl_MachineState.BackColor = stateColor[machineState];
                lbl_MachineState.Text = machineState.ToString();
            }
        }
        /// <summary>
        /// 更新设备状态
        /// </summary>
        private void UpdataMachineState()
        {
            this.Invoke(new Action(() =>
            {
                if (machineState == MachineState.未复位)
                {
                    StateLable();
                }
                else if (machineState == MachineState.复位中)
                {
                    //黄灯闪烁
                    SetTricolourLight(0, 1, 0, 0);
                    Thread.Sleep(500);
                    SetTricolourLight(0, 0, 0, 0);
                    Thread.Sleep(500);
                    StateLable();
                }
                else if (machineState == MachineState.复位完成)
                {
                    SetTricolourLight(1, 0, 0, 0);
                    StateLable();
                }
                else if (machineState == MachineState.复位失败)
                {
                    SetTricolourLight(0, 0, 1, 0);
                    StateLable();
                }
                else if (machineState == MachineState.测试中)
                {
                    SetTricolourLight(1, 0, 0, 0);
                    StateLable();
                }
                else if (machineState == MachineState.暂停测试)
                {
                    SetTricolourLight(0, 1, 0, 0);
                    StateLable();
                }
                else if (machineState == MachineState.停止)
                {
                    SetTricolourLight(0, 0, 1, 0);
                    StateLable();
                }
                else if (machineState == MachineState.报警)
                {
                    lbl_MachineState.BackColor = stateColor[machineState];
                    SetTricolourLight(0, 0, 1, 1);
                    Thread.Sleep(500);
                    SetTricolourLight(0, 0, 0, 0);
                    Thread.Sleep(500);
                }
                else if (machineState == MachineState.急停按下)
                {
                    SetTricolourLight(0, 0, 1, 0);
                    StateLable();
                }
                else if (machineState == MachineState.未装治具)
                {
                    SetTricolourLight(0, 0, 1, 0);
                    StateLable();
                }
            }));
        }
        /// <summary>
        /// 三色灯设置
        /// </summary>
        /// <param name="green"></param>
        /// <param name="yellow"></param>
        /// <param name="red"></param>
        /// <param name="buzzer"></param>
        private void SetTricolourLight(uint green,uint yellow,uint red,uint buzzer)
        {
            zMotion.SetOutSignal(20, red);
            zMotion.SetOutSignal(21, green);
            zMotion.SetOutSignal(22, yellow);
            zMotion.SetOutSignal(23, buzzer);
        }
       
        /// <summary>
        /// 线程更新 IO和轴状态
        /// </summary>
        private void TimerUpdataUI()
        {
            Task.Factory.StartNew(new Action(() =>
            {
                while (true)
                {
                    Thread.Sleep(20);
                    UpdateAxisState();
                    Thread.Sleep(20);
                    UpdateInIo();
                    Thread.Sleep(20);
                    UpdataMachineState();
                    ReadTimeMotionBtton();
                }
            }));
        }
        public void ReadTimeMotionBtton()
        {
            string UpOrDown = string.Empty;
            bool inIo1 = false;
            bool inIo2 = false;
            bool inIo3 = false;
            bool inIo4 = false;
            bool direction = true;
            zMotion.GetInSignal(ProgramParamMange.InIo["前"], ref inIo1);
            zMotion.GetInSignal(ProgramParamMange.InIo["后"], ref inIo2);
            zMotion.GetInSignal(ProgramParamMange.InIo["左"], ref inIo3);
            zMotion.GetInSignal(ProgramParamMange.InIo["右"], ref inIo4);

            if (inIo1)
            {
                if (!MotionCommons.RunCkeckSin())
                    return;
                UpOrDown = radb_UpTestHead.Checked ? "上模" : "下模";
                zMotion.VMove(ProgramParamMange.AxisPara[UpOrDown+"Y轴"].AxisNum, 10, false, 0, 100, 100, 0);
            }
            else if (inIo2)
            {
                if (!MotionCommons.RunCkeckSin())
                    return;
                UpOrDown = radb_UpTestHead.Checked ? "上模" : "下模";
                zMotion.VMove(ProgramParamMange.AxisPara[UpOrDown + "Y轴"].AxisNum, 10, true, 0, 100, 100, 0);
            }
            else if (inIo3)
            {
                if (!MotionCommons.RunCkeckSin())
                    return;
                UpOrDown = radb_UpTestHead.Checked ? "上模" : "下模";
                direction = UpOrDown == "上模" ? false : true;
                zMotion.VMove(ProgramParamMange.AxisPara[UpOrDown + "X轴"].AxisNum, 10, direction, 0, 100, 100, 0);
            }
            else if (inIo4)
            {
                if (!MotionCommons.RunCkeckSin())
                    return;
                UpOrDown = radb_UpTestHead.Checked ? "上模" : "下模";
                direction = UpOrDown == "上模" ? true : false;
                zMotion.VMove(ProgramParamMange.AxisPara[UpOrDown + "X轴"].AxisNum, 10, direction, 0, 100, 100, 0);
            }
        }
        private void UpdateInIo()
        {
            bool inIo1 = false;
            bool inIo2 = false;
            bool inIo3 = false;
            zMotion.GetInSignal(ProgramParamMange.InIo["急停1"], ref inIo1);
            zMotion.GetInSignal(ProgramParamMange.InIo["上模治具到位感应"], ref inIo2);
            zMotion.GetInSignal(ProgramParamMange.InIo["下模治具到位感应"], ref inIo3);
            if (inIo1 && machineState != MachineState.急停按下)
            {
                DeviceAbnormal("急停按下", MachineState.急停按下);
            }
            else if ((!inIo2 || !inIo3) && machineState != MachineState.未装治具)
            {
                DeviceAbnormal("治具拆下", MachineState.未装治具);
            }
            else if (machineState == MachineState.急停按下)
            {
                zMotion.GetInSignal(ProgramParamMange.InIo["急停1"], ref inIo1);
                if (!inIo1)
                {
                    SetMachinState(MachineState.停止);
                    this.BeginInvoke(new Action(() => StateLable()));
                }
            }
            else if (machineState == MachineState.未装治具)
            {
                zMotion.GetInSignal(ProgramParamMange.InIo["上模治具到位感应"], ref inIo2);
                zMotion.GetInSignal(ProgramParamMange.InIo["下模治具到位感应"], ref inIo3);
                if (inIo2 && inIo3)
                {
                    SetMachinState(MachineState.停止);
                    this.BeginInvoke(new Action(() => StateLable()));
                }
            }
            else if (detectionStart.TestTaskStatus == TaskStatus.Running)
            {
                zMotion.GetInSignal(ProgramParamMange.InIo["停止"], ref inIo1);
                if (inIo1) DevicePauseRun("“停止”按钮按下,测试中停");
                zMotion.GetInSignal(ProgramParamMange.InIo["正前方安全光栅"], ref inIo1);
                if (!inIo1) DevicePauseRun("正前方安全光栅触发,测试中停"); 
                zMotion.GetInSignal(ProgramParamMange.InIo["上方安全光栅"], ref inIo1);
                if (!inIo1) DevicePauseRun("上方安全光栅触发,测试中停");
            }
            else if(machineState == MachineState.复位中)
            {
                zMotion.GetInSignal(ProgramParamMange.InIo["停止"], ref inIo1);
                if (inIo1) EndReset("“停止”按钮按下,复位停止");
                zMotion.GetInSignal(ProgramParamMange.InIo["正前方安全光栅"], ref inIo1);
                if (!inIo1) EndReset("正前方安全光栅触发,复位停止");
                zMotion.GetInSignal(ProgramParamMange.InIo["上方安全光栅"], ref inIo1);
                if (!inIo1) EndReset("上方安全光栅触发,复位停止");
            }
        }

        /// <summary>
        /// 结束复位
        /// </summary>
        /// <param name="error"></param>
        private void EndReset(string error)
        {
            formMaintaining.deviceReset.StopTask(error);
        }
        #region 遍历轴状态
        private void UpdateAxisState()
        {
            AxisStatus axisStatus = new AxisStatus();
            string[] axisNames = ProgramParamMange.AxisPara.Keys.ToArray();
            for (int i = 0; i < ProgramParamMange.AxisPara.Count; i++)
            {
                zMotion.GetAxisStatus2(i, ProgramParamMange.AxisPara[axisNames[i]].Fwd, ProgramParamMange.AxisPara[axisNames[i]].Rev, ref axisStatus);
                SetAxisStatus(axisNames[i], axisStatus);
            }

        }
        private void SetAxisStatus(string axis, AxisStatus axisStatus)
        {
            if (axisStatus.servoError == 1)
            {
                DeviceAbnormal(axis + "轴伺服报警", MachineState.报警);
                this.Invoke(new Action(()=> lbl_MachineState.Text = axis + "轴伺服报警"));
            }
        }
        #endregion

        private void listBox_VelSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BaseTask.vecPercent = float.Parse(listBox_VelSelect.SelectedItem.ToString().Replace("%", "")) / 100;

        }

        #region 暂停
        /// <summary>
        /// 设备异常
        /// </summary>
        private void DeviceAbnormal(string error, MachineState machineState)
        {
            zMotion.StopAllAxis(2);
            detectionStart.EndTest(error);
            SetMachinState(machineState);
            Log4NetHepler.WriteError(error);
            //MessageBox.Show(error);
        }
        //暂停测试
        private void DevicePauseRun(string error)
        {
            detectionStart.PauseTest();
            SetMachinState(MachineState.暂停测试);
            Log4NetHepler.WriteError(error);
        }
       
        #endregion
        private void btn_ClosePrograms_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
