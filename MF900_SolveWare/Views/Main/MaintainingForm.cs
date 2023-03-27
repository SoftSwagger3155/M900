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
    public partial class MaintainingForm : Form,IView
    {
        private IODebugForm ioDebug;
        private AxisDebugForm axisDebug;
        public MaintainingForm()
        {
            InitializeComponent();
            
        }

        public void Setup<TData>(TData data)
        {
            throw new NotImplementedException();
        }

        private void btn_IoShow_Click(object sender, EventArgs e)
        {
            ioDebug = new IODebugForm();
            ioDebug.Show();
        }

        private void btn_AxisDebugShow_Click(object sender, EventArgs e)
        {
            axisDebug = new AxisDebugForm();
            axisDebug.Show();
        }
    }
}
