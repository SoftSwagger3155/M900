using MotionCard;
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
    public partial class FormMachineState : Form
    {
        private InSignalLed signalLed;
        public FormMachineState()
        {
            InitializeComponent();
            SetMotion(MotionCommons.motion);
            //Task.Run(new Action(() => RefreshIo()));
        }
        /// <summary>
        /// 初始化输入IO
        /// </summary>
        /// <param name="motion"></param>
        private void SetMotion(MotionBase motion)
        {
            string[] ioNames = ProgramParamMange.InIo.Keys.ToArray();
            int index = 0;
            foreach (Control item in panel1.Controls)
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
        private void RefreshIo()
        {
            while (true)
            {
                if (!this.IsHandleCreated)
                    continue;
                Thread.Sleep(80);
                foreach (Control item in panel1.Controls)
                {
                    if(item is InSignalLed)
                        item.Refresh();
                }
            }
        }
    }
}
