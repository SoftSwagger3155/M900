using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MF900
{
    public class GenDgvTable
    {
        //test 


        /// <summary>
        /// 生成表格
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="rows"></param>
        /// <param name="colums"></param>
        public static void GenDataGridTable(DataGridView dataGridView, int rows, int colums)
        {
            dataGridView.Columns.Clear();

            //添加列
            for (int i = 0; i < colums; i++)
            {
                dataGridView.Columns.Add((i + 1).ToString(), (i + 1).ToString());
                dataGridView.Columns[i].ReadOnly = true;
            }

            //添加行
            if (rows > 1)
                dataGridView.Rows.Add(rows);
            int dataGridViewHeiht = dataGridView.Size.Height;
            //设置行高
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                dataGridView.Rows[i].Height = dataGridViewHeiht / (rows - i);
                dataGridViewHeiht = dataGridViewHeiht - dataGridView.Rows[i].Height;
            }
            dataGridView.Size = new Size(dataGridView.Size.Width, dataGridView.RowCount * dataGridView.Rows[0].Height);
            SetDgvStyle(dataGridView);
        }
        /// <summary>
        /// 设置样式
        /// </summary>
        /// <param name="dataGridView"></param>
        public static void SetDgvStyle(DataGridView dataGridView)
        {
            //设置列不可手拉
            for (int i = 0; i < dataGridView.Columns.Count; i++)
            {
                dataGridView.Columns[i].Resizable = DataGridViewTriState.False;
            }
            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                dataGridView.Rows[i].Resizable = DataGridViewTriState.False;
            }
            //设置表头不可点击
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
        /// <summary>
        /// 设置标识
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="oneBoard"></param>
        public static void SetDataGridViewMes(DataGridView dataGridView,OneBoard oneBoard)
        {
            int num = 1;
            switch (oneBoard)
            {
                case OneBoard.左后:
                    for (int j = ProgramParamMange.ProductDataPara.ProductXY.Y * ProgramParamMange.ProductDataPara.RegionCount.Y - 1; j >= 0; j--)
                    {
                        for (int i = 0; i < ProgramParamMange.ProductDataPara.ProductXY.X * ProgramParamMange.ProductDataPara.RegionCount.X; i++)
                        {
                            dataGridView[i, j].Value = num;
                            num++;
                        }
                    }
                    break;
                case OneBoard.右后:
                    for (int j = ProgramParamMange.ProductDataPara.ProductXY.Y * ProgramParamMange.ProductDataPara.RegionCount.Y - 1; j >= 0; j--)
                    {
                        for (int i = ProgramParamMange.ProductDataPara.ProductXY.X * ProgramParamMange.ProductDataPara.RegionCount.X - 1; i >= 0; i--)
                        {
                            dataGridView[i, j].Value = num;
                            num++;
                        }
                    }
                    break;
                case OneBoard.左前:
                    for (int j = 0; j < ProgramParamMange.ProductDataPara.ProductXY.Y * ProgramParamMange.ProductDataPara.RegionCount.Y; j++)
                    {
                        for (int i = 0; i < ProgramParamMange.ProductDataPara.ProductXY.X * ProgramParamMange.ProductDataPara.RegionCount.X; i++)
                        {
                            dataGridView[i, j].Value = num;
                            num++;
                        }
                    }
                    break;
                case OneBoard.右前:
                    for (int j = 0; j < ProgramParamMange.ProductDataPara.ProductXY.Y * ProgramParamMange.ProductDataPara.RegionCount.Y; j++)
                    {
                        for (int i = ProgramParamMange.ProductDataPara.ProductXY.X * ProgramParamMange.ProductDataPara.RegionCount.X - 1; i >= 0; i--)
                        {
                            dataGridView[i, j].Value = num;
                            num++;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public static void SetDataGriaTableColor(DataGridView dataGridView,Color color)
        {
            for (int i = 0; i < dataGridView.ColumnCount; i++)
            {
                for (int j = 0; j < dataGridView.RowCount; j++)
                {
                    dataGridView[i, j].Style.BackColor = color;
                }
            }
        }

    }
}
