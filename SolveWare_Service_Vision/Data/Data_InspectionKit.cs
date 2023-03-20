using SolveWare_Service_Core.Attributes;
using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Vision.JobSheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Vision.Data
{
    [ResourceBaseAttribute("Data_InspectionKit")]
    public class Data_InspectionKit: ElementBase
    {
        public List<JobSheet_Brightness> JobSheet_Brightness_Datas { get; set; }
        public List<JobSheet_Brightness> JobSheet_Lighting_Datas { get; set; }
        public JobSheet_SearchArea JobSheet_SearchArea_Data { get; set; }
        public JobSheet_PatternMatch JobSheet_PatternMatch_Data { get; set; }
        public JobSheet_Blob JobSheet_Blob_Data { get; set; }
    }
}
