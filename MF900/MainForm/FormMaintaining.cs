using Halcon.Functions;
using HalconDotNet;
using HVision;
using MotionCard;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MF900
{
    public partial class FormMaintaining : Form
    {
        private FormAxisDebug formAxisDebug;
        private InSignalLed signalLed;
        private OutSwitch outSwitch;
        public DeviceReset deviceReset;
        public FormMaintaining()
        {
            InitializeComponent();
            GenInIoControls(MotionCommons.motion);
            GenOutIoControls(MotionCommons.motion);
            GenAxisIoControls(MotionCommons.motion);
            InitalControls(MotionCommons.motion);
            ReadDeviceBasePara();
            GenDgvTable.SetDgvStyle(dgv_Caltab);
            GenFormDgvTable();
            deviceReset = new DeviceReset(MotionCommons.motion, ProgramParamMange.AxisPara);
            Task.Run(new Action(() => RefreshIo()));
            Task.Run(new Action(() => ReadAxisPointsTask()));
        }
        private void GenFormDgvTable()
        {
            for (int i = 0; i < 9; i++)
            {
                dgv_Caltab.AddRow(new object[] { 0, 0, "Get", 0, 0, "Draw" });
            }
        }
        private void ReadDeviceBasePara()
        {
            xktTextSet1.VarValue = ProgramParamMange.DeviceBasePara.BasePointXY.X.ToString();
            xktTextSet2.VarValue = ProgramParamMange.DeviceBasePara.BasePointXY.Y.ToString();
            value_PlatHeight.VarValue= ProgramParamMange.DeviceBasePara.PlatformHeight.ToString();
            moreAxisPoint1.X= ProgramParamMange.DeviceBasePara.UpAvoidPoints.X;
            moreAxisPoint1.Y = ProgramParamMange.DeviceBasePara.UpAvoidPoints.Y;
            moreAxisPoint1.Z = ProgramParamMange.DeviceBasePara.UpAvoidPoints.Z;
            moreAxisPoint1.R = ProgramParamMange.DeviceBasePara.UpAvoidPoints.R;
            moreAxisPoint2.X = ProgramParamMange.DeviceBasePara.DownAvoidPoints.X;
            moreAxisPoint2.Y = ProgramParamMange.DeviceBasePara.DownAvoidPoints.Y;
            moreAxisPoint2.Z = ProgramParamMange.DeviceBasePara.DownAvoidPoints.Z;
            moreAxisPoint2.R = ProgramParamMange.DeviceBasePara.DownAvoidPoints.R;
        }

        #region 初始化Io控件
        private void GenInIoControls(MotionBase motion)
        {
            int row = 0;
            for (int i = 0; i < ProgramParamMange.InIo.Count; i++)
            {
                if (i % 6 == 0 && i != 0)
                {
                    row++;
                }
                InSignalLed inSignalLed = new InSignalLed();
                inSignalLed.Name = $"inSignalLed{i}";
                inSignalLed.Size = new Size(120, 35);
                inSignalLed.Location = new Point(20 + i % 6 * 160, 15 + row * 60);
                inSignalLed.ImageSelect = SignalImageList.Gray;
                this.panel3.Controls.Add(inSignalLed);
            }
            SetIoInMotion(motion);
        }
        private void GenOutIoControls(MotionBase motion)
        {
            int row = 0;
            for (int i = 0; i < ProgramParamMange.OutIo.Count; i++)
            {
                if (i % 7 == 0 && i != 0)
                {
                    row++;
                }
                OutSwitch outSwitch = new OutSwitch();
                outSwitch.Name = $"outSwitch{i}";
                outSwitch.Size = new Size(150, 60);
                outSwitch.Location = new Point(10 + i % 7 * 160, 15 + row * 70);
                this.panel2.Controls.Add(outSwitch);
            }
            SetOutInMotion(motion);
        }
        /// <summary>
        /// 初始化输入IO
        /// </summary>
        /// <param name="motion"></param>
        private void SetIoInMotion(MotionBase motion)
        {
            string[] ioNames = ProgramParamMange.InIo.Keys.ToArray();
            int index = 0;
            foreach (Control item in panel3.Controls)
            {
                if (item is InSignalLed)
                {
                    signalLed = (InSignalLed)item;
                    signalLed.Motion = motion;
                    signalLed.SignalName = ioNames[index];
                    signalLed.IoNum = ProgramParamMange.InIo[signalLed.SignalName];
                    index++;
                }
            }
        }
        /// <summary>
        /// 初始化输出IO
        /// </summary>
        /// <param name="motion"></param>
        private void SetOutInMotion(MotionBase motion)
        {
            string[] ioNames = ProgramParamMange.OutIo.Keys.ToArray();
            int index = 0;
            foreach (Control item in panel2.Controls)
            {
                if (item is OutSwitch)
                {
                    outSwitch = (OutSwitch)item;
                    outSwitch.Motion = motion;
                    outSwitch.IoNmae = ioNames[index];
                    outSwitch.IoNum = ProgramParamMange.OutIo[outSwitch.IoNmae];
                    outSwitch.SinglChecked = false;
                    index++;
                }
            }
        }
        /// <summary>
        /// 轴限位IO
        /// </summary>
        private void GenAxisIoControls(MotionBase motion)
        {
            int row = 0;
            for (int i = 0; i < 21; i++)
            {
                if (i % 5 == 0 && i != 0)
                {
                    row++;
                }
                InSignalLed inSignalLed = new InSignalLed();
                inSignalLed.Name = $"inSignalLed{i}";
                inSignalLed.Size = new Size(170, 35);
                inSignalLed.Location = new Point(20 + i % 5 * 180, 15 + row * 60);
                inSignalLed.ImageSelect = SignalImageList.Gray;
                this.tabPage8.Controls.Add(inSignalLed);
            }
            SetAxisIoControls(motion);
        }
        private void SetAxisIoControls(MotionBase motion)
        {
            string[] ioNames = ProgramParamMange.AxisPara.Keys.ToArray();
            int index = 2;
            string[] limit = new string[3] { "原点", "正限", "负限" };
            int limitIndex = 0;
            foreach (Control item in tabPage8.Controls)
            {
                if (item is InSignalLed)
                {
                    signalLed = (InSignalLed)item;
                    signalLed.Motion = motion;
                    signalLed.SignalName = ioNames[index] + limit[limitIndex];
                    if(limitIndex==0)
                    {
                        signalLed.IoNum = ProgramParamMange.AxisPara[ioNames[index]].homeIo;
                    }
                    else if(limitIndex == 1)
                    {
                        signalLed.IoNum = ProgramParamMange.AxisPara[ioNames[index]].Fwd;
                    }
                    else if (limitIndex == 2)
                    {
                        signalLed.IoNum = ProgramParamMange.AxisPara[ioNames[index]].Rev;
                    }
                    limitIndex++;
                    if (limitIndex == 3)
                    {
                        limitIndex = 0;
                        index++;
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// 线程更新轴点位
        /// </summary>
        private void ReadAxisPointsTask()
        {
            while (true)
            {
                Thread.Sleep(60);
                ReadTimeAxisPoints(uiTableLayoutPanel9);
                ReadTimeAxisPoints(uiTableLayoutPanel11);
            }
        }
        private void ReadTimeAxisPoints(TableLayoutPanel tableLayoutPanel)
        {
            AxisControls axisControls;
            foreach (Control item in tableLayoutPanel.Controls)
            {
                if (item is AxisControls)
                {
                    axisControls = (AxisControls)item;
                    axisControls.ReadAxisPoints();
                }
            }
        }

        public void InitalControls(MotionBase motion)
        {
            InitalMotionControls(motion, uiTableLayoutPanel9);
            InitalMotionControls(motion, uiTableLayoutPanel11);
        }
        private void InitalMotionControls(MotionBase motion, TableLayoutPanel tableLayoutPanel)
        {
            AxisControls axisControls;
            foreach (Control item in tableLayoutPanel.Controls)
            {
                if(item is AxisControls)
                {
                    axisControls = (AxisControls)item;
                    axisControls.Motion = motion;
                }
            }
        }
        private void RefreshIo()
        {
            while (true)
            {
                if (!this.IsHandleCreated)
                    continue;
                Thread.Sleep(50);
                foreach (Control item in panel3.Controls)
                {
                    if (item is InSignalLed)
                        item.Refresh();
                }
                //轴限位IO
                foreach (Control item in tabPage8.Controls)
                {
                    if (item is InSignalLed)
                        item.Refresh();
                }
            }
        }

       //基准点查找
        private void uiButton7_Click(object sender, EventArgs e)
        {
            HTuple row = new HTuple();
            HTuple col = new HTuple();
            HTuple radius = new HTuple();
            bool isOk = false;
            cameraHWControls1.userHWControls.NewHObject();
            isOk = HlCommonsFunction.FindBaseLocation(cameraHWControls1, cameraHWControls1.userHWControls.roiCircle.RoiCircleDatas.Row, cameraHWControls1.userHWControls.roiCircle.RoiCircleDatas.Column,
                           cameraHWControls1.userHWControls.roiCircle.RoiCircleDatas.Radius, ref row, ref col, ref radius);
           
            //cameraHWControls1.userHWControls.WriteString("台面基准定位完成");
        }

        private void uiRadioButton8_CheckedChanged(object sender, EventArgs e)
        {
            if (uiRadioButton8.Checked)
                uiTabControl2.SelectedIndex = 0;
        }

        private void uiRadioButton9_CheckedChanged(object sender, EventArgs e)
        {
            if (uiRadioButton9.Checked)
                uiTabControl2.SelectedIndex = 1;
        }
        private void uiRadioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (uiRadioButton9.Checked)
                uiTabControl2.SelectedIndex = 2;
        }
        private void radb_UpCamera_CheckedChanged(object sender, EventArgs e)
        {
            //ProgramParamMange.MyCameras["下相机"].StopGrabbing();
            //ProgramParamMange.MyCameras["上相机"].eventProcessImage += userHWControls1.ShowSourceImage;
            //ProgramParamMange.MyCameras["上相机"].HWindows = userHWControls1.HWindows;
            //ProgramParamMange.MyCameras["上相机"].GrabOne();
        }

        private void radb_DownCamera_CheckedChanged(object sender, EventArgs e)
        {
            //ProgramParamMange.MyCameras["上相机"].StopGrabbing();
            //ProgramParamMange.MyCameras["下相机"].HWindows = userHWControls1.HWindows;
            //ProgramParamMange.MyCameras["下相机"].StartGrabbing();
        }

        private void btn_SetBaseLocation_Click(object sender, EventArgs e)
        {
            xktTextSet1.VarValue = "110";
            xktTextSet2.VarValue = "60";
            SaveDeviceBasePara();
        }

        private void btn_SaveAvoidPoints_Click(object sender, EventArgs e)
        {
            SaveDeviceBasePara();
        }
        /// <summary>
        /// 保存设备基础参数
        /// </summary>
        public void SaveDeviceBasePara()
        {
            ProgramParamMange.DeviceBasePara = new DeviceBaseParaModel()
            {
                BasePointXY = new Pofloat()
                {
                    X = float.Parse(xktTextSet1.VarValue),
                    Y = float.Parse(xktTextSet2.VarValue)
                },
                UpAvoidPoints = new AxisPoints()
                {
                    X = moreAxisPoint1.X,
                    Y = moreAxisPoint1.Y,
                    Z = moreAxisPoint1.Z,
                    R = moreAxisPoint1.R,
                },
                DownAvoidPoints = new AxisPoints()
                {
                    X = moreAxisPoint2.X,
                    Y = moreAxisPoint2.Y,
                    Z = moreAxisPoint2.Z,
                    R = moreAxisPoint2.R,
                },
                PlatformHeight = float.Parse(value_PlatHeight.VarValue),
            };
            SerializeHelper.SerializeXml<DeviceBaseParaModel>(ProgramParamMange.DeviceBasePara, ParaFliePath.SystemParaPath + "DeviceBasePara.xml");
        }

        private void btn_DeviceReset_Click(object sender, EventArgs e)
        {
            if (!MotionCommons.RunCkeckSin())
                return;
            if (!UIMessageBox.Show("是否确认设备初始化？", "提示", UIStyle.Blue, UIMessageBoxButtons.OKCancel))
                return;

            deviceReset.StartTask();
        }

        private void uiButton5_Click(object sender, EventArgs e)
        {
            formAxisDebug = new FormAxisDebug();
            formAxisDebug.ShowDialog();

        }

    }
}
