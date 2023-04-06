using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.Definition;
using SolveWare_Service_Core.General;
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
            string errMsg = string.Empty;
			try
			{
                if (mtrSafe.Data_Safetys.Count == 0l) return true;

                bool isDangerous = false;
                foreach (var safeItem in mtrSafe.Data_Safetys)
                {
                    AxisBase axis = (AxisBase)SolveWare.Core.MMgr.Get_Single_Element_Form_Tool_Resource(Tool_Resource_Kind.Motor, safeItem.MotorName);

                    if(safeItem.Operand == Safety_Operand.大于等于.ToString())
                    {
                        isDangerous = axis.Get_CurUnitPos() >= safeItem.Pos;
                    }
                    else
                    {
                        isDangerous = axis.Get_CurUnitPos() <= safeItem.Pos;
                    }

                    if (isDangerous)
                    {
                        errMsg += $"马达 {safeItem.MotorName} 现在位置 {axis.Get_CurUnitPos()} mm  {safeItem.Operand} {safeItem.Pos} mm";
                        break;
                    }
                }

			}
			catch (Exception ex)
			{
                errMsg += ex.Message;
			}

            return Get_Result(nameof(Is_Safe_To_Move), errMsg); 
        }
    }

}
