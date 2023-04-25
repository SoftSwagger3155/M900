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
    public abstract class CameraMediaBase : ToolElementBase, ICameraMedia
    {
        protected HWindow hWindow;
        protected HImage image = new HImage();
        protected int maximum_Gain;
        protected int minimum_Gain;
        protected int maximum_ExposoureTime;
        protected int minimum_ExposoureTime;
        protected int grabTime_ms;
        protected byte[] buffer_Image;
        protected int imagePartX;
        protected int imagePartY;
        protected int worldCenterX;
        protected int worldCenterY;
        protected int hWindowSize_Width;
        protected int hWindowSize_Height;
        protected int pointer_Pixel_Row_X;
        protected int pointer_Pixel_Column_Y;
        protected string pointer_GrayValue;
        protected string pointer_Pixel_Info;
        protected bool isSimulation;

        public HImage Image
        {
            get => image;
            private set => UpdateProper(ref image, value);
        }
        public int GrabTime_ms
        {
            get => grabTime_ms; private set => UpdateProper(ref grabTime_ms, value);
        }
        public int Maximum_ExposureTime
        {
            get => maximum_ExposoureTime;
            private set => UpdateProper(ref maximum_ExposoureTime, value);
        }
        public int Minimum_ExposureTime
        {
            get=> minimum_ExposoureTime; 
            private set => UpdateProper(ref minimum_ExposoureTime, value);
        }
        public int Maximum_Gain
        {
            get => maximum_Gain; private set => UpdateProper(ref maximum_Gain, value);
        }
        public int Minimum_Gain
        {
            get=> minimum_Gain; private set => UpdateProper(ref minimum_Gain, value);
        }
        public int WorldCenterX
        {
            get=> worldCenterX; private set => UpdateProper(ref worldCenterX, value);
        }
        public int WorldCenterY
        {
            get => worldCenterY; private set => UpdateProper(ref worldCenterY, value);
        }
        public int Pointer_Pixel_Row_X
        {
            get=> pointer_Pixel_Row_X; private set => UpdateProper(ref pointer_Pixel_Row_X, value);
        }
        public int Pointer_Pixel_Column_Y
        {
            get=> pointer_Pixel_Column_Y; private set => UpdateProper(ref pointer_Pixel_Column_Y, value);
        }
        public string Pointer_GrayValue
        {
            get => pointer_GrayValue; private set => UpdateProper(ref pointer_GrayValue, value);
        }
        public string Pointer_Pixel_Info
        {
            get => pointer_Pixel_Info; private set => UpdateProper(ref pointer_Pixel_Info, value);
        }
        public bool IsSimulation { get => isSimulation; }
        public string GrabTimeInfo { get; set; }


        public abstract bool IsGrabing();
        public abstract void AssignCameraMedia(object obj_Media);

        public abstract bool Close();
        public abstract bool Open();
        public abstract void GrabOneShot();
        public abstract bool SetAutoGain(bool autoGain);
        public abstract bool SetAutoExposure(bool autoExposure);
        public abstract bool SetExposureTime(int exposureTime);
        public abstract bool SetGain(int gain);
        public abstract int StartLive(int delayTime_ms = 100);
        public abstract int StopLive();
    }
}
