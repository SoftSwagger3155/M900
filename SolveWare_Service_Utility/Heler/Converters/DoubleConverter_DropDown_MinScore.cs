using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.ComponentModel.TypeConverter;

namespace SolveWare_Service_Utility.Heler.Converters
{
    public class DoubleConverter_DropDown_MinScore : DoubleConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            double[] tags = new double[]
           {
                0.5,
                0.6,
                0.7,
                0.8,
                0.9,
                1.0
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
