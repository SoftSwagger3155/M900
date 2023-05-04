using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.ComponentModel.TypeConverter;

namespace SolveWare_Service_Utility.Heler.Converters
{
    public class IntConverter_DropDown_HorizontalMeasureLength : Int32Converter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            int[] tags = new int[]
           {
               1,2,3,4,5,10,15,20
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
