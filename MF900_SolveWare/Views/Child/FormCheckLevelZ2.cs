﻿using SolveWare_Service_Core.Base.Interface;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MF900_SolveWare
{
    public partial class FormCheckLevelZ2 : Form, IView
    {
        public FormCheckLevelZ2()
        {
            InitializeComponent();
        }

        public void Setup<TObj>(TObj obj)
        {
            throw new NotImplementedException();
        }

        //左前限位
        private void uiButton3_Click(object sender, EventArgs e)
        {
            
        }
        //左后限位
        private void uiButton4_Click(object sender, EventArgs e)
        {

        }
        //右前限位
        private void uiButton5_Click(object sender, EventArgs e)
        {

        }
        //右后限位
        private void uiButton6_Click(object sender, EventArgs e)
        {

        }
    }
}
