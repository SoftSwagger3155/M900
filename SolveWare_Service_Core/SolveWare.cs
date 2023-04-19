using SolveWare_Service_Core.Manager.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolveWare_Service_Core
{
    public class SolveWare
    {
        static SolveWare core;
        static object mutex = new object();
        IMainManager mmgr = null;

        public IMainManager MMgr => mmgr;

        private SolveWare()
        {

        }

        public static SolveWare Core
        {
            get
            {
                if (core == null)
                {
                    lock (mutex)
                    {
                        if (core == null)
                            core = new SolveWare();
                    }
                }

                return core;
            }
        }

        public void SetMMgr(IMainManager mmgr)
        {
            this.mmgr = mmgr;
        }
        public bool Is_Machine_Already_Homing()
        {
            if(this.mmgr.Is_Ready_Home == false)
            {
                var result = MessageBox.Show("机器尚未复位，是否继续执行?\r\n继续，按是\r\n结束，按否","提问", MessageBoxButtons.YesNo);
                return result == DialogResult.Yes;
            }

            return true;
        }
        public void ShowMsg(string msg, bool isError = false)
        {
            bool showMsg = !string.IsNullOrEmpty(msg);
            this.mmgr.Infohandler.LogMessage(msg, showMsg, isError);
        }
    }
}
