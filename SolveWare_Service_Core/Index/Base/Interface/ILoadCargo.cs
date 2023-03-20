using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Core.Index.Base.Interface
{
    public interface ILoadCargo
    {
        int Go_Load_Pos();
        int Vision_Align_Load_Cargo();
        int Move_Vision_To_PickTool_Offset();
        int Pick_Cargo_From_Tray();
        int Put_Back_Cargo_To_Tray();
        int Go_UnLoad_Pos();
        int Vision_Align_UnLoad_Cargo(); 
    }
}
