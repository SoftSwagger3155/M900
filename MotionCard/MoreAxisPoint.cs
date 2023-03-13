using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MotionCard
{
   public partial class MoreAxisPoint : UserControl
    {
        public MoreAxisPoint()
        {
            InitializeComponent();
            SetTableEnable();
            dataGridView1.Rows.Add(new object[7] { 1, 0, 0, 0, 0, "读取", "运动", });
        }
        private void SetTableEnable()
        {
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            Row = e.RowIndex;
            switch (e.ColumnIndex)
            {
                case 5:
                    X = axisX == -1 ? 0 : motion.GetPos(axisX);
                    Y = axisX == -1 ? 0 : motion.GetPos(axisY);
                    Z = axisX == -1 ? 0 : motion.GetPos(axisZ);
                    R = axisX == -1 ? 0 : motion.GetPos(axisR);
                    break;
                case 6:
                    if (!isCanMove)
                        break;
                    Task.Run(new Action(() =>
                    {
                        if (zThenXYU)
                            MoveZXYU();
                        else MoveXYUZ();
                    }));
                    break;
            }
        }
        private void MoveZXYU()
        {
            if (axisZ != -1)
            {
                motion.MoveAbs(axisZ, Z, axisSpeed[AxisZName], 0.3f, 0.2f);
                motion.WaitStop(axisZ);
            }
            if (axisX != -1)
                motion.MoveAbs(axisX, X, axisSpeed[AxisXName], 0.3f, 0.2f);
            if (axisY != -1)
                motion.MoveAbs(axisY, Y, axisSpeed[AxisYName], 0.3f, 0.2f);
            if (axisR != -1)
                motion.MoveAbs(axisR, R, axisSpeed[AxisRName], 0.3f, 0.2f);
           
        }
        private void MoveXYUZ()
        {
            if (axisX != -1)
                motion.MoveAbs(axisX, X, axisSpeed[AxisXName], 0.3f, 0.2f);
            if (axisY != -1)
                motion.MoveAbs(axisY, Y, axisSpeed[AxisYName], 0.3f, 0.2f);
            if (axisR != -1)
                motion.MoveAbs(axisR, R, axisSpeed[AxisRName], 0.3f, 0.2f);
            if (axisX != -1)
                motion.WaitStop(axisX);
            if (axisY != -1)
                motion.WaitStop(axisY);
            if (axisR != -1)
                motion.WaitStop(axisR);
            if (axisZ != -1)
                motion.MoveAbs(axisZ, Z, axisSpeed[AxisZName], 0.3f, 0.2f);
        }

        [Category("自定义属性")]
        public bool zThenXYU { get; set; } = true;

        private MotionBase motion;
        [Category("自定义属性")]
        public MotionBase Motion { set { motion = value; } }

        private int row;
        [Category("自定义属性")]
        public int Row
        {
            get { return row; }
            set { row = value; }
        }
        [Category("自定义属性")]
        public int RowCount
        {   
            get { return dataGridView1.RowCount; }
        }

        [Category("自定义属性")]
        public float X
        {
            get { return Convert.ToSingle(dataGridView1[1, row].Value); }
            set { dataGridView1[1, row].Value = value; }
        }
        [Category("自定义属性")]
        public float Y
        {
            get { return Convert.ToSingle(dataGridView1[2, row].Value); }
            set { dataGridView1[2, row].Value = value; }
        }
        [Category("自定义属性")]
        public float Z
        {
            get { return Convert.ToSingle(dataGridView1[3, row].Value); }
            set { dataGridView1[3, row].Value = value; }
        }
        [Category("自定义属性")]
        public float R
        {
            get { return Convert.ToSingle(dataGridView1[4, row].Value); }
            set { dataGridView1[4, row].Value = value; }
        }

        #region 轴名称属性

        private string axisXName;
        [Category("轴名称")]
        public string AxisXName
        {
            get { return axisXName; }
            set { axisXName = value; }
        }
        private string axisYName;
        [Category("轴名称")]
        public string AxisYName
        {
            get { return axisYName; }
            set { axisYName = value; }
        }
        private string axisZName;
        [Category("轴名称")]
        public string AxisZName
        {
            get { return axisZName; }
            set { axisZName = value; }
        }
        private string axisRName;
        [Category("轴名称")]
        public string AxisRName
        {
            get { return axisRName; }
            set { axisRName = value; }
        }

        #endregion

        private Dictionary<string, AxisSpeed> axisSpeed;
        [Category("自定义属性")]
        public Dictionary<string, AxisSpeed> AxisSpeed
        {
            get { return axisSpeed; }
            set { axisSpeed = value; }
        }
        private bool isCanMove = true;
        [Category("自定义属性")]
        public bool IsCanMove
        {
            get { return isCanMove; }
            set { isCanMove = value; }
        }


        #region 轴号属性
        private short axisX = -1;
        [Category("轴号")]
        public short AxisX
        {
            get { return axisX; }
            set { axisX = value; }
        }
        private short axisY = -1;
        [Category("轴号")]
        public short AxisY
        {
            get { return axisY; }
            set { axisY = value; }
        }
        private short axisZ = -1;
        [Category("轴号")]
        public short AxisZ
        {
            get { return axisZ; }
            set { axisZ = value; }
        }
        private short axisR = -1;
        [Category("轴号")]
        public short AxisR
        {
            get { return axisR; }
            set { axisR = value; }
        }

        #endregion

        public void AddPoint(float[] points)
        {
            dataGridView1.Rows.Add(1);
            dataGridView1[0, dataGridView1.Rows.Count - 1].Value = dataGridView1.Rows.Count;
            for (int i = 1; i < points.Length + 1; i++)
            {
                dataGridView1[i, dataGridView1.Rows.Count - 1].Value = points[i - 1];
            }
            dataGridView1[5, dataGridView1.Rows.Count - 1].Value = "读取";
            dataGridView1[6, dataGridView1.Rows.Count - 1].Value = "运动";
        }
        public void DetelePoint()
        {
            dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 1);
        }
    }
}
