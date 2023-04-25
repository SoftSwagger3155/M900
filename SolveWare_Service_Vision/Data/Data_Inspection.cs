using SolveWare_Service_Core.Attributes;
using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.General;
using SolveWare_Service_Vision.Inspection.JobSheet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;

namespace SolveWare_Service_Vision.Data
{
    [ResourceBaseAttribute(ConstantProperty.ResourceKey_Inspect)]
    public class Data_Inspection: ElementBase
    {
        [XmlIgnore]
        public string ErrorMsg { get; set; }
        //照顺序来设定视觉
        public JobSheet_Brightness JobSheet_Brightness_Data { get; set; }
        public List<JobSheet_Lighting> JobSheet_Lighting_Datas { get; set; }
        public JobSheet_SearchArea JobSheet_SearchArea_Data { get; set; }
        public JobSheet_PatternMatch JobSheet_PatternMatch_Data { get; set; }
        public JobSheet_Blob JobSheet_Blob_Data { get; set; }
        public double CenterX { get; set; }
        public double CenterY { get; set; }
        public double Angle { get; set; }
        public string CameraName { get; set; }

        public Data_Inspection(string cameraName)
        {
            this.CameraName = cameraName;
        }

        public Data_Inspection()
        {
            JobSheet_Brightness_Data = new JobSheet_Brightness();
            JobSheet_Lighting_Datas = new List<JobSheet_Lighting>(); ;
            JobSheet_SearchArea_Data = new JobSheet_SearchArea() { InspectKitData_Name = Name };
            JobSheet_PatternMatch_Data = new JobSheet_PatternMatch() { InspectKitData_Name = Name };
            JobSheet_Blob_Data = new JobSheet_Blob() { InspectKitData_Name = Name };
        } 
    }
}
