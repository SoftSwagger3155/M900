using SolveWare_Service_Core.Attributes;
using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Vision.Data;
using SolveWare_Service_Vision.Inspection.Base.Abstract;
using SolveWare_Service_Vision.Inspection.Base.Interface;
using SolveWare_Service_Vision.Inspection.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Vision.Inspection.JobSheet
{
    [PairAttribute(typeof(Job_SearchArea))]
    public class JobSheet_SearchArea: JobSheetDataBase
    {
        [Category("Lighting 参数")]
        [DisplayName("相机名称-Camera Name")]
        public double Camera_Name { get; set; }

        public Data_ROI ROI_Data { get; set; }

        public double Row_Y1 { get; set; }
        public double Column_X1 { get; set; }
        public double Row_Y2 { get; set; }
        public double Column_X2 { get; set; }
    }
}
