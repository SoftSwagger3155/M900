using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.Communication.Base.Abstract;
using SolveWare_Service_Core.Communication.Base.Interface;
using SolveWare_Service_Tool.Lighting.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Tool.Lighting.Base.Abstract
{
    public abstract class LightingBase : JobFundamentalBase, ILighting
    {
        InstrumentChassisBase chassis;
        public virtual void SetChassis(IInstrumentChassis chassis)
        {
            this.chassis = chassis as InstrumentChassisBase;
        }

        public abstract void Off();

        public abstract void On();

        public abstract void Set_Intensity(int intensity);
    }
}
