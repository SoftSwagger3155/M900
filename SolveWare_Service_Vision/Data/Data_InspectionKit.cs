﻿using SolveWare_Service_Core.Attributes;
using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Vision.Inspection.JobSheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Vision.Data
{
    [ResourceBaseAttribute("Data_InspectionKit")]
    public class Data_InspectionKit: ElementBase
    {
        public List<JobSheet_Brightness> JobSheet_Brightness_Datas { get; set; }
        public List<JobSheet_Lighting> JobSheet_Lighting_Datas { get; set; }
        public JobSheet_SearchArea JobSheet_SearchArea_Data { get; set; }
        public JobSheet_PatternMatch JobSheet_PatternMatch_Data { get; set; }
        public JobSheet_Blob JobSheet_Blob_Data { get; set; }

        public Data_InspectionKit()
        {
            JobSheet_Brightness_Datas = new List<JobSheet_Brightness>();
            JobSheet_Lighting_Datas = new List<JobSheet_Lighting>(); ;
            JobSheet_SearchArea_Data = new JobSheet_SearchArea() { InspectKitData_Name = Name };
            JobSheet_PatternMatch_Data = new JobSheet_PatternMatch() { InspectKitData_Name = Name };
            JobSheet_Blob_Data = new JobSheet_Blob() { InspectKitData_Name = Name };
        } 
    }
}
