using SolveWare_Service_Core;
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
            List<string> names = SolveWare.Core.MMgr.Get_Single_Tool_Resource(SolveWare_Service_Core.Definition.Tool_Resource_Kind.IO).Get_All_Item_Name().ToList();
            for (int i = 0; i < names.Count; i++)
            {
                InputIo inputIo = new InputIo();
                inputIo.IoName = names[i];
                inputIo.Size = new Size(120, 35);
                inputIo.Location = new Point(20 + i % 6 * 160, 15 + row * 60);
                inputIo.Status = IoStatus.OFF;
                this.uiGroupBox1.Controls.Add(inputIo);
            }
        }

        private void GenOutputIoControls()
        {

        }
    }
}
