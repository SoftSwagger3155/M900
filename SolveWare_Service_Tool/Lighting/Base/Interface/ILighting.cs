using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.Communication.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Tool.Lighting.Base.Interface
{
    public interface ILighting: IElement, IJobFundamental
    {
        void SetChassis(IInstrumentChassis chassis);
        void Set_Intensity(int  intensity);
        void On();
        void Off();  
    }
}
