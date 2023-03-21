using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Tool.Camera.Definition;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Tool.Camera.Data
{
    public class ConfigData_Camera: ElementBase
    {
        public string Id_Camera { get; set; }
        public string Camera_Name { get; set; }
        public bool IsSimulation { get; set; }
        public string Resource { get; set; }
        public Master_Driver_Camera MasterDriver { get; set; } = Master_Driver_Camera.Basler;

        public Data_MMperPixel MMperPixelData { get; set; }
    }
}
