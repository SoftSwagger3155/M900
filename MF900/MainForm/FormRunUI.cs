using CCWin;
using SqlSugar;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MF900
{
    public partial class FormRunUI : Form
    {
        private SqlSugarHelper<Login> loginSql = new SqlSugarHelper<Login>();
        private ColorDialog colorDialog = new ColorDialog();
        public FormRunUI()
        {
            InitializeComponent();
            Log4NetHepler.BingListView(listView1);
            loginSql.SetConectionConfigs = loginSql.SetConnectionConfig("SqlData\\Login", SqlSugar.DbType.Sqlite);
            //loginSql.CodeFirst();
        }
        private void FormRunUI_Shown(object sender, EventArgs e)
        {
            UpdateDataGridEvent();
        }
        public void UpdateDataGridEvent()
        {
            GenDgvTable.GenDataGridTable(dgv_TestData, ProgramParamMange.ProductDataPara.ProductXY.Y * ProgramParamMange.ProductDataPara.RegionCount.Y,
                ProgramParamMange.ProductDataPara.ProductXY.X * ProgramParamMange.ProductDataPara.RegionCount.X);
            SetDataGridNumber();
            GenDgvTable.SetDataGridViewMes(dgv_TestData, ProgramParamMange.BoardMessagePara.OneBoardSelect);
        }

        #region DataGridView表格创建及控件重绘
        private Lines[] lines;
        /// <summary>
        /// 设置表格序号及粗线条
        /// </summary>
        public void SetDataGridNumber()
        {
            int num = 1;
            for (int j = 0; j < ProgramParamMange.ProductDataPara.ProductXY.Y * ProgramParamMange.ProductDataPara.RegionCount.Y; j++)
            {
                for (int i = 0; i < ProgramParamMange.ProductDataPara.ProductXY.X * ProgramParamMange.ProductDataPara.RegionCount.X; i++)
                {
                    dgv_TestData[i, j].Value = num;
                    num++;
                }
            }
            this.Visible = true;
            lines = new Lines[ProgramParamMange.ProductDataPara.RegionCount.X - 1 + ProgramParamMange.ProductDataPara.RegionCount.Y - 1];
            for (int i = 0; i < ProgramParamMange.ProductDataPara.RegionCount.X - 1; i++)
            {
                lines[i] = new Lines()
                {
                    StartPoint = new Point()
                    {
                        X = dgv_TestData.GetCellDisplayRectangle(ProgramParamMange.ProductDataPara.ProductXY.X * (i + 1),
                        ProgramParamMange.ProductDataPara.ProductXY.Y, false).X,
                        Y = 0
                    },
                    EndPoint = new Point()
                    {
                        X = dgv_TestData.GetCellDisplayRectangle(ProgramParamMange.ProductDataPara.ProductXY.X * (i + 1),
                        ProgramParamMange.ProductDataPara.ProductXY.Y, false).X,
                        Y = dgv_TestData.Height
                    }
                };
            }
            for (int i = 0; i < ProgramParamMange.ProductDataPara.RegionCount.Y - 1; i++)
            {
                lines[i + ProgramParamMange.ProductDataPara.RegionCount.X - 1] = new Lines()
                {
                    StartPoint = new Point()
                    {
                        X = 0,
                        Y = dgv_TestData.GetCellDisplayRectangle(ProgramParamMange.ProductDataPara.ProductXY.X,
                        ProgramParamMange.ProductDataPara.ProductXY.Y * (i + 1), false).Y
                    },
                    EndPoint = new Point()
                    {
                        X = dgv_TestData.Width,
                        Y = dgv_TestData.GetCellDisplayRectangle(ProgramParamMange.ProductDataPara.ProductXY.X,
                        ProgramParamMange.ProductDataPara.ProductXY.Y * (i + 1), false).Y
                    }
                };
            }

            dgv_TestData.Invalidate();
        }

        private void DrawStepLine(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            for (int i = 0; i < lines?.Length; i++)
            {
                graphics.DrawLine(new Pen(new SolidBrush(Color.FromArgb(80, 160, 255)), 15), lines[i].StartPoint.X, lines[i].StartPoint.Y,
                    lines[i].EndPoint.X, lines[i].EndPoint.Y);
            }
        }

        private void dgv_TestData_Paint(object sender, PaintEventArgs e)
        {
            DrawStepLine(e);
            //DataGridViewCellStyle style = tgv.Nodes[i].DefaultCellStyle;  //字体设置
            //style.Font = new Font(tgv.Font, FontStyle.Bold);
        }

        #endregion

        private void btn_DataSave_Click(object sender, EventArgs e)
        {
            
            Log4NetHepler.WriteInfo("点击“保存”按钮");
            Log4NetHepler.WriteDebug("点击“保存”按钮");
            Log4NetHepler.WriteError("点击“保存”按钮");
        }

        #region SqlSugar操作
        private async void uiButton2_Click(object sender, EventArgs e)
        {
            bool s = false;
            s = await loginSql.Add(new Login() { User = "test2", Password = "12345", ID = 3 });
            if (s)
            {

            }
        }

        private async void uiButton3_Click(object sender, EventArgs e)
        {
            var s = await loginSql.GetByWhere(t=>t.User=="123");
            if (s != null)
            {
                //string use = s[0].User;
                //string passd = s[1].Password;
            }
        }

        private void uiButton4_Click(object sender, EventArgs e)
        {
            var s = loginSql.DelelteInSingle(new Login() { User = "test2" });
            if (s != null)
            {
            }
            //var s = loginSql.DelelteByWhere(t => t.User == "123");
            //if (s != null)
            //{
            //    int a = s.Result;
            //}
        }
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private async Task<Login> GetByWheres()
        {
            return await loginSql.GetByWhere(t => t.ID > 0);
            //return s.Password;
        }
        private async void uiButton5_Click(object sender, EventArgs e)
        {
            bool s = await loginSql.UpdateDate(t => new Login() { User = "3356",Password = "125333" }, t => t.User == "36635");
        }
        //导入DataTable
        private void uiButton6_Click(object sender, EventArgs e)
        {
            ClearDgv();
            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();
            Task<DataTable> d = loginSql.GetToDataTable(t => t.ID > 0);
            for (int i = 0; i < d.Result.Columns.Count; i++)
            {
                for (int j = 0; j < d.Result.Rows.Count; j++)
                {
                    dgv_TestData[i, j].Value = d.Result.Rows[j][i];
                }
            }
        }
        #endregion
        private void ClearDgv()
        {
            for (int x = 0; x < dgv_TestData.Columns.Count; x++)
            {
                for (int y = 0; y < dgv_TestData.Rows.Count; y++)
                {
                    dgv_TestData[y, x].Value = "";
                }
            }
        }
        
    }

    public class Lines
    {
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
    }
}
