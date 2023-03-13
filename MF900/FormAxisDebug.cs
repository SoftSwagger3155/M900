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
    public partial class FormAxisDebug : UIForm
    {
        private string[] axisName = new string[4] { "X轴", "Y轴", "Z轴", "R轴" };
        private bool start;
        public FormAxisDebug()
        {
            InitializeComponent();
            start = true;
            SetDgvTableStyle();
            ReadAxisVelPara();
            ReadAxisParaToDgv("上模", uiDataGridView4);
            ReadAxisParaToDgv("下模", uiDataGridView5);
            ReadAxisParaToDgv2();
            GenMovingDvg("上模", uiDataGridView7);
            GenMovingDvg("下模", uiDataGridView8);
            Task.Run(new Action(() => TimeReadLimitIO()));
        }
        private void SetDgvTableStyle()
        {
            GenDgvTable.SetDgvStyle(uiDataGridView1);
            GenDgvTable.SetDgvStyle(uiDataGridView2);
            GenDgvTable.SetDgvStyle(uiDataGridView3);
            GenDgvTable.SetDgvStyle(uiDataGridView4);
            GenDgvTable.SetDgvStyle(uiDataGridView5);
            GenDgvTable.SetDgvStyle(uiDataGridView6);
        }
       
        private void SaveAxisSpeed()
        {
            ProgramParamMange.AxisSpeed.Clear();
            DgvAxisVelToFile(uiDataGridView1);
            DgvAxisVelToFile(uiDataGridView2);
            ProgramParamMange.AxisSpeed.Add(uiDataGridView3[0, 0].Value.ToString(), new AxisSpeed()
            {
                vel = Convert.ToSingle(uiDataGridView3[1, 0].Value),
                minVel = Convert.ToSingle(uiDataGridView3[2, 0].Value),
                acc = Convert.ToSingle(uiDataGridView3[3, 0].Value),
                dec = Convert.ToSingle(uiDataGridView3[4, 0].Value),
            });
            SerializeHelper.SerializeDictionary(ProgramParamMange.AxisSpeed, ParaFliePath.AxisParaPath + "AxisSpeedPara.xml");
        }
        private void DgvAxisVelToFile(DataGridView dataGridView)
        {
            for (int i = 0; i < 4; i++)
            {
                ProgramParamMange.AxisSpeed.Add(dataGridView[0, i].Value.ToString(), new AxisSpeed()
                {
                    vel = Convert.ToSingle(dataGridView[1, i].Value),
                    minVel = Convert.ToSingle(dataGridView[2, i].Value),
                    acc = Convert.ToSingle(dataGridView[3, i].Value),
                    dec = Convert.ToSingle(dataGridView[4, i].Value),
                });
            }
        }
        private void btn_SaveAxisPara_Click(object sender, EventArgs e)
        {
            if (!UIMessageBox.Show("是否保存轴参数？", "提示", UIStyle.Blue, UIMessageBoxButtons.OKCancel))
                return;
            SaveAxisSpeed();
            SaveAxisPara();
        }

        private void ReadAxisVelPara()
        {
            string[] axisName = ProgramParamMange.AxisSpeed.Keys.ToArray();
            uiDataGridView1.Rows.Add(4);
            uiDataGridView2.Rows.Add(4);
            uiDataGridView3.Rows.Add(1);
            for (int i = 0; i < 4; i++)
            {
                uiDataGridView1[0, i].Value = axisName[i];
                uiDataGridView1[1, i].Value = ProgramParamMange.AxisSpeed[axisName[i]].vel;
                uiDataGridView1[2, i].Value = ProgramParamMange.AxisSpeed[axisName[i]].minVel;
                uiDataGridView1[3, i].Value = ProgramParamMange.AxisSpeed[axisName[i]].acc;
                uiDataGridView1[4, i].Value = ProgramParamMange.AxisSpeed[axisName[i]].dec;
            }
            for (int i = 4; i < 8; i++)
            {
                uiDataGridView2[0, i - 4].Value = axisName[i];
                uiDataGridView2[1, i - 4].Value = ProgramParamMange.AxisSpeed[axisName[i]].vel;
                uiDataGridView2[2, i - 4].Value = ProgramParamMange.AxisSpeed[axisName[i]].minVel;
                uiDataGridView2[3, i - 4].Value = ProgramParamMange.AxisSpeed[axisName[i]].acc;
                uiDataGridView2[4, i - 4].Value = ProgramParamMange.AxisSpeed[axisName[i]].dec;
            }
            uiDataGridView3[0, 0].Value = axisName[8];
            uiDataGridView3[1, 0].Value = ProgramParamMange.AxisSpeed[axisName[8]].vel;
            uiDataGridView3[2, 0].Value = ProgramParamMange.AxisSpeed[axisName[8]].minVel;
            uiDataGridView3[3, 0].Value = ProgramParamMange.AxisSpeed[axisName[8]].acc;
            uiDataGridView3[4, 0].Value = ProgramParamMange.AxisSpeed[axisName[8]].dec;
        }
        private void ReadAxisParaToDgv(string direct, DataGridView dataGridView)
        {
            dataGridView.Rows.Add(4);
            dataGridView[0, 0].Value = $"{direct}X轴";
            dataGridView[0, 1].Value = $"{direct}Y轴";
            dataGridView[0, 2].Value = $"{direct}Z轴";
            dataGridView[0, 3].Value = $"{direct}R轴";

            for (int i = 0; i < 4; i++)
            {
                dataGridView[1, i].Value = ProgramParamMange.AxisPara[dataGridView[0, i].
                    Value.ToString()].AxisNum;
                dataGridView[2, i].Value = ProgramParamMange.AxisPara[dataGridView[0, i].
                    Value.ToString()].homeIo;
                dataGridView[3, i].Value = ProgramParamMange.AxisPara[dataGridView[0, i].
                    Value.ToString()].Fwd;
                dataGridView[4, i].Value = ProgramParamMange.AxisPara[dataGridView[0, i].
                    Value.ToString()].Rev;
            }
        }
        private void ReadAxisParaToDgv2()
        {
            uiDataGridView6.Rows.Add(1);
            uiDataGridView6[0, 0].Value = "托板升降轴";
            uiDataGridView6[1, 0].Value = ProgramParamMange.AxisPara["托板升降轴"].AxisNum;
            uiDataGridView6[2, 0].Value = ProgramParamMange.AxisPara["托板升降轴"].homeIo;
            uiDataGridView6[3, 0].Value = ProgramParamMange.AxisPara["托板升降轴"].Fwd;
            uiDataGridView6[4, 0].Value = ProgramParamMange.AxisPara["托板升降轴"].Rev;
        }

        #region 上模复位
        bool result = false;
        private void uiButton1_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(new Action(() =>
            {
                result = MotionCommons.motion.ZeroAxis2(ProgramParamMange.AxisPara["上模X轴"], 10, 1, 5, 2, 2000, 2000, 0);
                if(!result)
                    MessageBox.Show("上模X轴复位失败!");
            }));
        }

        private void uiButton2_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(new Action(() =>
            {
                result = MotionCommons.motion.ZeroAxis2(ProgramParamMange.AxisPara["上模Y轴"], 10, 1, 5, 2, 2000, 2000, 0);
                if (!result)
                    MessageBox.Show("上模Y轴复位失败!");
            }));
        }

        private void uiButton3_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(new Action(() =>
            {
                result = MotionCommons.motion.ZeroAxis2(ProgramParamMange.AxisPara["上模Z轴"], 10, 1, 5, 2, 2000, 2000, 0);
                if (!result)
                    MessageBox.Show("上模Z轴复位失败!");
            }));
        }

        private void uiButton4_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(new Action(() =>
            {
                result = MotionCommons.motion.ZeroAxis2(ProgramParamMange.AxisPara["上模R轴"], 10, 1, 5, 2, 2000, 2000, 0);
                if (!result)
                    MessageBox.Show("上模R轴复位失败!");
            }));
        }

        #endregion

        #region 下模复位

        private void uiButton5_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(new Action(() =>
            {
                result = MotionCommons.motion.ZeroAxis2(ProgramParamMange.AxisPara["下模X轴"], 10, 1, 5, 2, 2000, 2000, 0);
                if (!result)
                    MessageBox.Show("下模X轴复位失败!");
            }));
        }

        private void uiButton7_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(new Action(() =>
            {
                result = MotionCommons.motion.ZeroAxis2(ProgramParamMange.AxisPara["下模Y轴"], 10, 1, 5, 2, 2000, 2000, 0);
                if (!result)
                    MessageBox.Show("下模Y轴复位失败!");
            }));
        }

        private void uiButton6_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(new Action(() =>
            {
                result = MotionCommons.motion.ZeroAxis2(ProgramParamMange.AxisPara["下模Z轴"], 10, 1, 5, 2, 2000, 2000, 0);
                if (!result)
                    MessageBox.Show("下模Z轴复位失败!");
            }));
        }

        private void uiButton8_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(new Action(() =>
            {
                result = MotionCommons.motion.ZeroAxis2(ProgramParamMange.AxisPara["下模R轴"], 10, 1, 5, 2, 2000, 2000, 0);
                if (!result)
                    MessageBox.Show("下模R轴复位失败!");
            }));
        }

        #endregion

        #region 托板升降Z轴复位,Jop

        private void uiButton11_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(new Action(() =>
            {
                result = MotionCommons.motion.ZeroAxis2(ProgramParamMange.AxisPara["托板升降轴"], 10, 1, 5, 2, 2000, 2000, 0);
                if (!result)
                    MessageBox.Show("托板升降轴复位失败!");
            }));
        }

        private void uiButton10_Click(object sender, EventArgs e)
        {
            MotionCommons.motion.VMove(ProgramParamMange.AxisPara["托板升降轴"].AxisNum, float.Parse(uiTextBox2.Text),
                false, 1, 1000, 1000, 0);
        }

        private void uiButton12_Click(object sender, EventArgs e)
        {
            MotionCommons.motion.VMove(ProgramParamMange.AxisPara["托板升降轴"].AxisNum, float.Parse(uiTextBox2.Text),
                true, 1, 1000, 1000, 0);
        }

        #endregion

        #region 限位IO读取
        private void TimeReadLimitIO()
        {
            while (start)
            {
                ShowLimitIO(uiDataGridView4);
                ShowLimitIO(uiDataGridView5);
                ShowLimitIO(uiDataGridView6);
            }
        }
        private void ShowLimitIO(DataGridView dataGridView)
        {
            AxisStatus axisStatus = new AxisStatus();
            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                MotionCommons.motion.GetAxisStatus2(Convert.ToInt32(dataGridView[1, i].Value),
                    Convert.ToInt32(dataGridView[3, i].Value), Convert.ToInt32(dataGridView[4, i].Value), ref axisStatus);
                dataGridView[2, i].Style.BackColor = axisStatus.origin == 1 ? Color.Red : Color.White;
                dataGridView[3, i].Style.BackColor = axisStatus.positLimit == 1 ? Color.Red : Color.White;
                dataGridView[4, i].Style.BackColor = axisStatus.minusLimit == 1 ? Color.Red : Color.White;
            }
            Thread.Sleep(10);
        }

        #endregion
        private void SaveAxisPara()
        {
            ProgramParamMange.AxisPara.Clear();
            DgvAxisParaToFile(uiDataGridView4);
            DgvAxisParaToFile(uiDataGridView5);
            DgvAxisParaToFile(uiDataGridView6);
            SerializeHelper.SerializeDictionary(ProgramParamMange.AxisPara, ParaFliePath.AxisParaPath + "MF900_AxisPara.xml");
        }
        private void DgvAxisParaToFile(DataGridView dataGridView)
        {
            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                ProgramParamMange.AxisPara.Add(dataGridView[0, i].Value.ToString(),
                    new AxisPara()
                    {
                        AxisNum = Convert.ToInt16(dataGridView[1, i].Value),
                        homeIo = Convert.ToInt16(dataGridView[2, i].Value),
                        Fwd = Convert.ToInt16(dataGridView[3, i].Value),
                        Rev = Convert.ToInt16(dataGridView[4, i].Value)
                    });
            }
        }

        private void GenMovingDvg(string name, DataGridView dataGridView)
        {
            dataGridView.Rows.Add(new object[6] { name + "X轴", 0, "移动", "10", "左", "右" });
            dataGridView.Rows.Add(new object[6] { name + "Y轴", 0, "移动", "10", "左", "右" });
            dataGridView.Rows.Add(new object[6] { name + "Z轴", 0, "移动", "10", "左", "右" });
            dataGridView.Rows.Add(new object[6] { name + "R轴", 0, "移动", "10", "左", "右" });
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                dataGridView.Rows[i].Height = 30;
            }
        }
        private void FormAxisDebug_FormClosing(object sender, FormClosingEventArgs e)
        {
            start = false;
        }
        //上模移动
        private void uiDataGridView7_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            int col = e.ColumnIndex;
            string axisName = uiDataGridView7[0, row].Value.ToString();
            float vel = Convert.ToSingle(uiDataGridView7[3, row].Value);
            float distance= Convert.ToSingle(uiDataGridView7[1, row].Value);
            switch (col)
            {
                case 2:
                    MotionCommons.motion.MoveRelative(ProgramParamMange.AxisPara[axisName].AxisNum, vel,
                        distance, 1, 1000, 1000, 0);

                    break;
                case 4:
                    MotionCommons.motion.VMove(ProgramParamMange.AxisPara[axisName].AxisNum, vel,
                        false, 1, 1000, 1000, 0);
                    break;
                case 5:
                    MotionCommons.motion.VMove(ProgramParamMange.AxisPara[axisName].AxisNum, vel,
                        true, 1, 1000, 1000, 0);
                    break;
            }
        }
        //下模移动
        private void uiDataGridView8_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            int col = e.ColumnIndex;
            string axisName = uiDataGridView8[0, row].Value.ToString();
            float vel = Convert.ToSingle(uiDataGridView8[3, row].Value);
            float distance = Convert.ToSingle(uiDataGridView8[1, row].Value);
            switch (col)
            {
                case 2:
                    MotionCommons.motion.MoveRelative(ProgramParamMange.AxisPara[axisName].AxisNum, vel,
                        distance, 1, 1000, 1000, 0);

                    break;
                case 4:
                    MotionCommons.motion.VMove(ProgramParamMange.AxisPara[axisName].AxisNum, vel,
                        false, 1, 1000, 1000, 0);
                    break;
                case 5:
                    MotionCommons.motion.VMove(ProgramParamMange.AxisPara[axisName].AxisNum, vel,
                        true, 1, 1000, 1000, 0);
                    break;
            }
        }

    }
}
