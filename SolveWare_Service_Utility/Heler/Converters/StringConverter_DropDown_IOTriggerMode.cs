using SolveWare_Service_Core.Definition;
using SolveWare_Service_Core.Manager.Base.Interface;
using SolveWare_Service_Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.ComponentModel.TypeConverter;
using SolveWare_Service_Core.General;

namespace SolveWare_Service_Utility.Heler.Converters
{
    public class StringConverter_DropDown_IOTriggerMode : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            string[] triggerModes = new string[]
            {
                ConstantProperty.ON,
                ConstantProperty.OFF
            };
            return new StandardValuesCollection(triggerModes); //编辑下拉框中的items
        }


        //true: disable text editting.    false: enable text editting;
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}
