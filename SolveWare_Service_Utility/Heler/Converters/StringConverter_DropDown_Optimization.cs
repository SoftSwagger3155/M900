using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.ComponentModel.TypeConverter;

namespace SolveWare_Service_Utility.Heler.Converters
{
    public class StringConverter_DropDown_Optimization : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            string[] tags = new string[]
           {
                "auto",
                "no_pregeneration",
                "none",
                "point_reduction_high",
                "point_reduction_low",
                "point_reduction_medium",
                "pregeneration"
           };
            return new StandardValuesCollection(tags);
        }


        //true: disable text editting.    false: enable text editting;
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}
