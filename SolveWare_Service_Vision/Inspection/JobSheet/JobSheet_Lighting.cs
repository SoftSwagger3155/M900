using SolveWare_Service_Core.Attributes;
using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Utility.Heler.Converters;
using SolveWare_Service_Vision.Inspection.Base.Abstract;
using SolveWare_Service_Vision.Inspection.Base.Interface;
using SolveWare_Service_Vision.Inspection.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Vision.Inspection.JobSheet
{
    [PairAttribute(typeof(Job_Lighting))]
    public class JobSheet_Lighting : JobSheetDataBase
    {
        [Category("Lighting 参数")]
        [DisplayName("相机名称-Camera Name")]
        [TypeConverter(typeof(StringConverter_DropDown_Camera_Name))]
        public double Camera_Name { get; set; }

        [Category("Lighting 参数")]
        [DisplayName("IO 控制 光源- IO Controlled")]
        public bool Is_IO_Controlled { get; set; }

        [Category("Lighting 参数")]
        [DisplayName("IO 光源- IO Name")]
        [TypeConverter(typeof(StringConverter_DropDown_IO_Name))]
        public string IO_Name { get; set; }

        [Category("Lighting 参数")]
        [DisplayName("IO 光源 触发模式- TriggerMode")]
        [TypeConverter(typeof(StringConverter_DropDown_IOTriggerMode))]
        public string TriggerMode { get; set; }


        [Category("Lighting 参数")]
        [DisplayName("光源名称-Lighting Name")]
        [TypeConverter(typeof(StringConverter_DropDown_Lighting_Name))]
        public double Lighting_Name { get; set; }


        [Category("Lighting 参数")]
        [DisplayName("强度-Intensity")]
        public double Intensity { get; set; }
    }
}
