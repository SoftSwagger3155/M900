using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MF900
{
    public partial class ResultCountDgv : UserControl
    {

        public ResultCountDgv()
        {
            InitializeComponent();
            PrepareName = new string[9] { "GOOD", "NG", "OPEN", "4WNG", "SHORT", "SHORT+", "C", "AUC", "ERROR/SKIP" };
            PrepareColor = new Color[9] { GoodCloor, NGCloor, OPENCloor, WNG4Cloor, SHORTCloor, SHORTADDCloor, CCloor, AUCCloor, SKIPCloor };
            AddPerpare();
            //for (int i = 0; i < 9; i++)
            //{
            //    DicPrepareColor.Add(PrepareName[i], PrepareColor[i]);
            //}
            SetColor();
            dataGridView1.ClearSelection();
        }
        public int Count { get; set; } = 0;
        public string[] PrepareName { get; set; }
        public Color[] PrepareColor { get; set; }
        public Dictionary<string, Color> DicPrepareColor { get; set; }

        public Color GoodCloor { get; set; } = Color.Green;
        public Color NGCloor { get; set; } = Color.Gray;
        public Color OPENCloor { get; set; } = Color.Red;
        public Color WNG4Cloor { get; set; } = Color.Blue;
        public Color SHORTCloor { get; set; } = Color.Yellow;
        public Color SHORTADDCloor { get; set; } = Color.YellowGreen;
        public Color CCloor { get; set; } = Color.FromArgb(135, 142, 197);
        public Color AUCCloor { get; set; } = Color.FromArgb(255, 192, 192);
        public Color SKIPCloor { get; set; } = Color.Gainsboro;

        public DataGridView dataGridView { get { return dataGridView1; } }

        public void SetCount(string name,int count)
        {
            Count += count;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if(name == dataGridView1[0, i].Value.ToString())
                {
                    dataGridView1[1, i].Value = count;
                    dataGridView1[2, i].Value = $"{Convert.ToInt32(dataGridView1[1, i].Value) / Count}%";
                }
            }
        }
        private void AddPerpare()
        {
            for (int i = 0; i < PrepareName.Length; i++)
            {
                dataGridView1.Rows.Add(new object[3] { PrepareName[i], 0, "0%" });
                dataGridView1.Rows[i].Height = 35;
            }
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
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
        public void SetColor()
        {
            for (int i = 0; i < 9; i++)
            {
                dataGridView1[1, i].Style.BackColor = PrepareColor[i];
                dataGridView1[2, i].Style.BackColor = PrepareColor[i];
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.ClearSelection();
        }
    }
}
