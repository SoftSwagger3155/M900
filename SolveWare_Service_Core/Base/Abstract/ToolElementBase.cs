using SolveWare_Service_Core.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Core.Base.Abstract
{
    public abstract class ToolElementBase : ElementBase, IToolElement
    {
        public virtual void StartStatusReading()
        {
            return;
        }

        public virtual void StopStatusReading()
        {
            return;
        }
    }
}
