using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Tool.Motor.Base.Abstract;
using SolveWare_Service_Tool.Motor.Business;
using SolveWare_Service_Tool.Motor.Data;
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
    public enum AxisDirection
    {
        None,
        Left,
        Up,
        Right,
        Down,
        LeftRotate,
        RightRotate
    }
    public partial class AxisJop : UserControl
    {
        Dictionary<AxisDirection, Image> DicAxisDirection;
        AxisBase mtr = null;
        public AxisJop()
        {
            InitializeComponent();
            DicAxisDirection = new Dictionary<AxisDirection, Image>()
            {
                {AxisDirection.None,null},
                {AxisDirection.Left,Properties.Resources.Left},
                {AxisDirection.Right,Properties.Resources.Right},
                {AxisDirection.Up,Properties.Resources.Up},
                {AxisDirection.Down,Properties.Resources.Down},
                {AxisDirection.LeftRotate,Properties.Resources.LeftRotate},
                {AxisDirection.RightRotate,Properties.Resources.RightRotate}
            };
 
            if(!string.IsNullOrEmpty(this.AxisName))
            {
                mtr = (AxisBase)SolveWare.Core.MMgr.Get_Single_Element_Form_Tool_Resource(SolveWare_Service_Core.Definition.Tool_Resource_Kind.Motor, this.AxisName);

            }
            uiSymbolButton_Forward.MouseDown += UiSymbolButton_Forward_MouseDown;
            uiSymbolButton_Forward.MouseUp += UiSymbolButton_Forward_MouseUp;
            uiSymbolButton_Backward.MouseDown += UiSymbolButton_Backward_MouseDown;
            uiSymbolButton_Backward.MouseUp += UiSymbolButton_Backward_MouseUp;
        }

        #region 点动

        private void UiSymbolButton_Backward_MouseUp(object sender, MouseEventArgs e)
        {
            mtr.Stop();
        }

        private void UiSymbolButton_Backward_MouseDown(object sender, MouseEventArgs e)
        {
            mtr.Jog(false);
        }

        private void UiSymbolButton_Forward_MouseUp(object sender, MouseEventArgs e)
        {
            mtr.Stop();
        }

        private void UiSymbolButton_Forward_MouseDown(object sender, MouseEventArgs e)
        {
            mtr.Jog(true);
        }

        #endregion

        [Description("轴名称"),Category("自定属性")]
        public string AxisName
        {
            get { return uiTitlePanel1.Text; }
            set { this.uiTitlePanel1.Text = value; }
        }

        private short axisNum;
        [Description("轴号"), Category("自定属性")]
        public short AxisNum
        {
            set { axisNum = value; }
        }

        private float vel;
        [Description("速度"), Category("自定属性")]
        public float Vel
        {
            get { return vel; }
            set { vel = value; }
        }


        private AxisDirection imageForward;
        [Description("图标方向1"), Category("自定义属性")]
        public AxisDirection ImageBtnForward
        {
            get { return imageForward; }
            set
            {
                imageForward = value;
                this.uiSymbolButton_Forward.Image = DicAxisDirection[imageForward];
            }
        }

        private AxisDirection imageBackward;
        [Description("图标方向2"), Category("自定义属性")]
        public AxisDirection ImageBtnBackward
        {
            get { return imageBackward; }
            set
            {
                imageBackward = value;
                this.uiSymbolButton_Backward.Image = DicAxisDirection[imageForward];
            }
        }
    }
}
