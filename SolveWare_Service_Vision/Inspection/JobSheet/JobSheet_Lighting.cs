using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Vision.Inspection.Base.Abstract;
using SolveWare_Service_Vision.Inspection.Base.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Vision.Inspection.JobSheet
{
    public class JobSheet_Lighting : JobSheetDataBase
    {
        [Category("Lighting 参数")]
        [DisplayName("相机名称-Camera Name")]
        public double Camera_Name { get; set; }

        [Category("Lighting 参数")]
        [DisplayName("强度-Intensity")]
        public double Intensity { get; set; }
    }
}
