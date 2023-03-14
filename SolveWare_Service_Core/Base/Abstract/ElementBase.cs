using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Core.Base.Abstract
{
    public class ElementBase : IElement
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public long Id { get; private set; }

        public ElementBase()
        {
            if(Id == 0)
                Id = IdentityGenerator.IG.GetIdentity();
        }

        public virtual void UpdateContent()
        {

        }
    }
}
