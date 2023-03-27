using MF900_SolveWare.Resource;
using SolveWare_Service_Core;
using SolveWare_Service_Tool.Motor.Base.Abstract;
using SolveWare_Service_Tool.Motor.Business;
using SolveWare_Service_Tool.Motor.Data;
using SolveWare_Service_Utility.Extension;
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
    public partial class AxisDebugForm : UIForm
    {
        Dictionary<string, AxisBase> keyAxis;

        public AxisDebugForm()
        {
            InitializeComponent();
            keyAxis = new Dictionary<string, AxisBase>()
            {
                {ResourceKey.Motor_Btm_T, ResourceKey.Motor_Btm_T.GetAxisBase() },
                {"2",new Motor_Zmcaux(new ConfigData_Motor()) },
                {"3",new Motor_Zmcaux(new ConfigData_Motor()) },
                {"4",new Motor_Zmcaux(new ConfigData_Motor()) },
                {"5",new Motor_Zmcaux(new ConfigData_Motor()) },
                {"6",new Motor_Zmcaux(new ConfigData_Motor()) },
                {"7",new Motor_Zmcaux(new ConfigData_Motor()) },
                {"8",new Motor_Zmcaux(new ConfigData_Motor()) },
                {"9",new Motor_Zmcaux(new ConfigData_Motor()) },
            };

            InitalAxisVel();
            ResourceKey.Op_TowerLight_Green.GetAxisBase();
            List<string> names = SolveWare.Core.MMgr.Get_Single_Tool_Resource(SolveWare_Service_Core.Definition.Tool_Resource_Kind.Motor).Get_All_Item_Name().ToList();
        }

        private void InitalAxisMes()
        {
            

        }
        private void InitalAxisVel()
        {
            var axisArry = keyAxis.Keys.ToArray();
            for (int i = 0; i < 4; i++)
            {
                uiDataGridView1.Rows.Add(new object[] {axisArry[i], keyAxis[axisArry[i]].MtrSpeed.Jog_Max_Velocity,
                keyAxis[axisArry[i]].MtrSpeed.Jog_Min_Velocity,keyAxis[axisArry[i]].MtrSpeed.Jog_Acceleration,keyAxis[axisArry[i]].MtrSpeed.Jog_Deceleration});
            }
            for (int i = 4; i < 8; i++)
            {
                uiDataGridView5.Rows.Add(new object[] {axisArry[i], keyAxis[axisArry[i]].MtrSpeed.Jog_Max_Velocity,
                keyAxis[axisArry[i]].MtrSpeed.Jog_Min_Velocity,keyAxis[axisArry[i]].MtrSpeed.Jog_Acceleration,keyAxis[axisArry[i]].MtrSpeed.Jog_Deceleration});
            }
            uiDataGridView6.Rows.Add(new object[] {axisArry[8], keyAxis[axisArry[8]].MtrSpeed.Jog_Max_Velocity,
                keyAxis[axisArry[8]].MtrSpeed.Jog_Min_Velocity,keyAxis[axisArry[8]].MtrSpeed.Jog_Acceleration,keyAxis[axisArry[8]].MtrSpeed.Jog_Deceleration});
        }
        private void InitalAxisJop()
        {

        }
    }
}
