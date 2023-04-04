using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.General;
using SolveWare_Service_Core.Manager.Base.Interface;
using SolveWare_Service_Tool.IO.Business;
using SolveWare_Service_Tool.IO.Data;
using SolveWare_Service_Tool.Motor.Base.Abstract;
using SolveWare_Service_Tool.Motor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Tool.Motor.Business
{
    public class Factory_Motor : IFactory
    {
        public IElement BuildTool(IElement configData)
        {
            IElement mtr = null;
            ConfigData_Motor data = configData as ConfigData_Motor;

            switch (data.Driver)
            {
                case Definition.Master_Driver_Motor.Zmcaux:
                    mtr = new Motor_Zmcaux(data);
                    if (data.MtrSpeed.SpeedSettings.Count == 0)
                    {
                        data.MtrSpeed.SpeedSettings.Add(new SpeedSeting() { Name = ConstantProperty.SpeedSetting_Home });
                        data.MtrSpeed.SpeedSettings.Add(new SpeedSeting() { Name = ConstantProperty.SpeedSetting_Jog });
                        data.MtrSpeed.SpeedSettings.Add(new SpeedSeting() { Name = ConstantProperty.SpeedSetting_TestHome });
                        data.MtrSpeed.SpeedSettings.Add(new SpeedSeting() { Name = ConstantProperty.SpeedSetting_TestJog });
                    }


                    (mtr as AxisBase).Init();
                    break;
                case Definition.Master_Driver_Motor.ACS:
                    break;
                case Definition.Master_Driver_Motor.LeadSide_DMC3600:
                    break;
                case Definition.Master_Driver_Motor.YangKong_MCC800P:
                    break;
            }

            return mtr;
        }
    
    }
}
