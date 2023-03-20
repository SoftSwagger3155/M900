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

namespace SolveWare_Service_Utility.Heler.Converters
{
    public class String_Converter_DropDown_Motor_Name : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            IResourceProvider provider = SolveWare.Core.MMgr.Get_Single_Tool_Resource(Tool_Resource_Kind.Motor);
            return new StandardValuesCollection(provider.Get_All_Item_Name().ToArray()); //编辑下拉框中的items
        }


        //true: disable text editting.    false: enable text editting;
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}
