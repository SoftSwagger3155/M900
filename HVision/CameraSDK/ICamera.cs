using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVision
{
    interface ICamera
    {
        [Category("相机SN")]
        string SN { get; }
        [Category("相机曝光")]
        double ExposureTime { get; set; }
        [Category("相机增益")]
        double Gain { get; set; }

        void Open();
        void Close();
    }
}

