using SolveWare_Service_Core.Attributes;
using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.General;
using SolveWare_Service_Tool.Camera.Definition;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Tool.Camera.Data
{
    [ResourceBaseAttribute(ConstantProperty.ResourceKey_Camera)]
    public class ConfigData_Camera: ElementBase
    {
        public string Id_Camera { get; set; } = "使用者定义";
        public string Camera_Name { get; set; }
        public int ImagePart_X { get; set; } = 1280;
        public int ImagePart_Y { get; set; } = 1080;
        public bool IsSimulation { get; set; } = true;
        public string Resource { get; set; }
        public Master_Driver_Camera MasterDriver { get; set; } = Master_Driver_Camera.Basler;
        public double MMperPixel_X { get; set; }
        public double MMperPixel_Y { get; set; }
        public double Average_MMperPixel { get; set; }
    }
}
