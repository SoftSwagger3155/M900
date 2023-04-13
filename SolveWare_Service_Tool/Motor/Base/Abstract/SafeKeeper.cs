using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.Definition;
using SolveWare_Service_Core.General;
using SolveWare_Service_Tool.IO.Base.Abstract;
using SolveWare_Service_Tool.IO.Base.Interface;
using SolveWare_Service_Tool.Motor.Base.Interface;
using SolveWare_Service_Tool.Motor.Data;
using SolveWare_Service_Tool.Motor.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Tool.Motor.Base.Abstract
{
    public class SafeKeeper: JobFundamentalBase, ISafeKeeper
    {
        public bool Is_Safe_To_Move(MtrSafe mtrSafe)
        {
            errorMsg = string.Empty;
            bool isDangerous = false;
            try
			{
                do
                {
                    if (mtrSafe.Data_Pos_Safetys.Count == 0l) return true;

                  
                    foreach (var safeItem in mtrSafe.Data_Pos_Safetys)
                    {
                        AxisBase axis = (AxisBase)SolveWare.Core.MMgr.Get_Single_Element_Form_Tool_Resource(Tool_Resource_Kind.Motor, safeItem.MotorName);

                        if (safeItem.Operand == Safety_Operand.大于等于.ToString())
                        {
                            isDangerous = axis.Get_CurUnitPos() >= safeItem.Pos;
                        }
                        else
                        {
                            isDangerous = axis.Get_CurUnitPos() <= safeItem.Pos;
                        }

                        if (isDangerous)
                        {
                            errorMsg += $"马达 {safeItem.MotorName} 现在位置 {axis.Get_CurUnitPos()} mm  {safeItem.Operand} {safeItem.Pos} mm";
                            break;
                        }
                    }
                    if (isDangerous) break;

                    foreach (var safeItem in mtrSafe.Data_IO_Safetys)
                    {
                        IOBase iO = (IOBase)SolveWare.Core.MMgr.Get_Single_Element_Form_Tool_Resource(Tool_Resource_Kind.IO, safeItem.IOName);
                        if (safeItem.TriggerMode == ConstantProperty.ON)
                            isDangerous = iO.IsOn();
                        else if (safeItem.TriggerMode == ConstantProperty.OFF)
                            isDangerous = iO.IsOff();

                        if (isDangerous)
                        {
                            errorMsg += $"IO {safeItem.IOName} 在危险触发模式";
                            break;
                        }
                    }

                } while (false);
			}
			catch (Exception ex)
			{
                errorMsg += ex.Message;
			}

            return isDangerous == false ? true : false; 
        }
    }

}
