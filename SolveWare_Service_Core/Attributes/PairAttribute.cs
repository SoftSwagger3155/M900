using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Core.Attributes
{
    public class PairAttribute: Attribute
    {
        public Type PartnerType { get; set; }
        public PairAttribute(Type partnerType)
        {
            this.PartnerType = partnerType;
        }

    }
}
