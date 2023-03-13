using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVision
{
    public class MvsCamera : ICamera
    {
        public string SN { get => throw new NotImplementedException(); }
        public double ExposureTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Gain { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Open()
        {
            throw new NotImplementedException();
        }
    }
}
