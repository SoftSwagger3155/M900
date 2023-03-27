using SolveWare_Service_Core.Attributes;
using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Tool.Camera.Base.Abstract;
using SolveWare_Service_Tool.Camera.Base.Interface;
using SolveWare_Service_Utility.Extension;
using SolveWare_Service_Vision.Inspection.Base.Abstract;
using SolveWare_Service_Vision.Inspection.Base.Interface;
using SolveWare_Service_Vision.Inspection.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SolveWare_Service_Vision.Inspection.JobSheet
{
    [PairAttribute(typeof(Job_Brightness))]
    public class JobSheet_Brightness : JobSheetDataBase
    {
        [XmlIgnore]
        public CameraBase Camera { get; private set; }

        public JobSheet_Brightness()
        {
            
        }

        public JobSheet_Brightness(string cameraName)
        {
            this.Camera = cameraName.GetCamera();
        }

        [Category("Brightness 参数")]
        [DisplayName("增益 - Gain")]
        public int Gain { get; set; }

        [Category("Brightness 参数")]
        [DisplayName("曝光时间_ms - Exposure Time")]
        public int ExposureTime { get; set; }
    }
}
