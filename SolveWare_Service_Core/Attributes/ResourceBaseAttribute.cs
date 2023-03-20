using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Core.Attributes
{
    public class ResourceBaseAttribute: Attribute
    {
        public string ResourceKey { get; set; }
        public ResourceBaseAttribute(string resourceKey)
        {
            ResourceKey = resourceKey;
        }
    }
}
