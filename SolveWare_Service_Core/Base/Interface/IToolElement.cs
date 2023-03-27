using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Core.Base.Interface
{
    public interface IToolElement: IElement
    {
        void StartStatusReading();
        void StopStatusReading();
    }
}
