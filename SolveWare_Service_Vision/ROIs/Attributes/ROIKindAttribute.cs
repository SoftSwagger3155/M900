using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Vision.ROIs.Attributes
{
    public class ROIKindAttribute: Attribute
    {
        public Type ROI_ResourceKey { get; set; }
        public ROIKindAttribute()
        {

        }
        public ROIKindAttribute(Type rOI_ResourceKey)
        {
            ROI_ResourceKey = rOI_ResourceKey;
        }
    }
}
