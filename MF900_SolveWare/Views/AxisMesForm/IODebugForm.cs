using SolveWare_Service_Core;
using SolveWare_Service_Core.Definition;
using SolveWare_Service_Tool.IO.Base.Abstract;
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

namespace MF900_SolveWare
{
    public partial class IODebugForm : UIForm
    {
        public IODebugForm()
        {
            InitializeComponent();
            GenInputIoControls();
        }

        private void GenInputIoControls()
        {
            int row = 0;
            var pro = SolveWare.Core.MMgr.Get_Single_Tool_Resource(Tool_Resource_Kind.IO);
            var ips = pro.Get_All_Items().ToList().FindAll(x => (x as IOBase).IOType == SolveWare_Service_Tool.IO.Definition.IO_Type.Input);

            List<string> names = SolveWare.Core.MMgr.Get_Single_Tool_Resource(Tool_Resource_Kind.IO).Get_All_Item_Name().ToList();
            for (int i = 0; i < names.Count; i++)
            {
                InputIo inputIo = new InputIo();
                inputIo.IoName = names[i];
                inputIo.Size = new Size(120, 35);
                inputIo.Location = new Point(20 + i % 6 * 160, 30 + row * 60);
                inputIo.Status = IoStatus.OFF;
                this.uiGroupBox1.Controls.Add(inputIo);
            }
            Task.Run(new Action(() => RefreshInputIo()));
        }
        public void RefreshInputIo()
        {
            while (true)
            {
                if (!this.IsHandleCreated)
                    continue;
                Thread.Sleep(50);
                foreach (Control control in uiGroupBox1.Controls)
                {
                    if (control is InputIo)
                        control.Refresh();
                }
               
            }
        }
        private void GenOutputIoControls()
        {

        }
    }
}
