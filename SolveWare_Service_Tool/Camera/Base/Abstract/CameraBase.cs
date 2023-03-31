using HalconDotNet;
using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Tool.Camera.Base.Interface;
using SolveWare_Service_Tool.Camera.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Tool.Camera.Base.Abstract
{
    public abstract class CameraBase : ToolElementBase, ICameraBase
    {
        public CameraBase(IElement configData)
        {
            this.configData = configData as ConfigData_Camera;
            this.MMperPixelData = this.configData?.MMperPixelData;
        }

        public string Id_Camera { get; set; }
        protected HImage image = new HImage();
        public HImage Image
        {
            get => image;
            private set
            {
                image = value;
                OnPropertyChanged(nameof(Image));
            }
        }
        public byte[] image_Buffer { get; }
        public int ExposureTime { get; set; }
        public int Gain { get; set; }
        public int FrameRate { get; set; }
        public long GrabTime { get; set; }
        public Data_MMperPixel MMperPixelData { get; set; }

        protected ConfigData_Camera configData;
        public ConfigData_Camera ConfigData
        {
            get=> configData;
            set => configData = value;
        }

        protected int imagePart_X = 0;
        public int ImagePart_X
        {
            get=> imagePart_X;
        }

        protected int imagePart_Y = 0;
        public int ImagePart_Y
        {
            get => imagePart_Y;
        }

        public string CameraGrabCapabilityInfo
        {
            get => $"{GrabTime} fps";
        }
        

        public HWindow WindowHost { get; private set; }
        public void SetWindowHost(HWindow win)
        {
            this.WindowHost = win;
        }

        public abstract void GetExposureTime();

        public abstract void GetFrameRate();

        public abstract int GrabImageOnce();

        public abstract int SetBrightness();

        public abstract int SetFrameRate();

        public abstract int StartLive(int delayTime_ms = 100);

        public abstract int StopLive(int delayTime__ms = 100);

        public abstract void CloseCamera();

        public abstract void AssingCameraMedia(object obj_Camera);

        public override void StartStatusReading()
        {
            return;
        }
        public override void StopStatusReading() { return;}
    }
}
