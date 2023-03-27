using SolveWare_Service_Core.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Tool.Lighting.Base
{
    public interface ILighting: IElement
    {
        void Set_Intensity(int  intensity);
        void On();
        void Off();  
    }
}
