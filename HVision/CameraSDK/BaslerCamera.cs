using Basler.Pylon;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HVision
{
    public class BaslerCamera:ICamera
    {

        private Camera camera;
        private Stopwatch stopWatch = new Stopwatch();
        private PixelDataConverter converter = new PixelDataConverter();
        private IntPtr latestFrameAddress = IntPtr.Zero;
        private HObject hPylonImage = null;
        private long grabTime = 0;          // 采集图像时间
        public long imageWidth = 0;         // 图像宽
        public long imageHeight = 0;        // 图像高

        /// <summary>
        /// 图像处理委托事件
        /// </summary>
        public event Action<HObject> eventProcessImage;
        public BaslerCamera(string UserID)
        {
            try
            {
                strUserID = UserID;
                // 枚举相机列表
                List<ICameraInfo> allCameraInfos = CameraFinder.Enumerate();

                foreach (ICameraInfo cameraInfo in allCameraInfos)
                {
                    if (strUserID == cameraInfo[CameraInfoKey.UserDefinedName])
                    {
                        camera = new Basler.Pylon.Camera(cameraInfo);
                    }
                }

                if (camera == null)
                {
                    MessageBox.Show("未识别到UserID为“" + strUserID + "”的相机！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception e)
            {
                ShowException(e);
            }
        }

        public BaslerCamera(string UserID, HWindow hWindow)
        {
            try
            {
                strUserID = UserID;
                // 枚举相机列表
                List<ICameraInfo> allCameraInfos = CameraFinder.Enumerate();

                foreach (ICameraInfo cameraInfo in allCameraInfos)
                {
                    if (strUserID == cameraInfo[CameraInfoKey.UserDefinedName])
                    {
                        camera = new Basler.Pylon.Camera(cameraInfo);
                    }
                }

                if (camera == null)
                {
                    MessageBox.Show("未识别到UserID为“" + strUserID + "”的相机！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else HWindows = hWindow;

            }
            catch (Exception e)
            {
                ShowException(e);
            }
        }
        
        public HWindow HWindows { get; set; }

        
        public HObject HPylonImage
        {
            get { return hPylonImage; }
            set { hPylonImage = value; }
        }

        /// <summary>
        /// if >= Sfnc2_0_0,说明是ＵＳＢ３的相机
        /// </summary>
        static Version Sfnc2_0_0 = new Version(2, 0, 0);
        public string strUserID { get; set; }
        [Category("相机数量")]
        public int CameraCount
        {
            get { return CameraFinder.Enumerate(DeviceType.GigE).Count; }
        }
        [Category("相机SN")]
        public string SN
        {
            get
            {
                return camera != null ? camera.CameraInfo[CameraInfoKey.DriverKeyName] : "";
            }
        }

        [Category("相机曝光")]
        public double ExposureTime
        {
            set
            {
                if (camera != null)
                {
                    camera.Parameters[PLCamera.ExposureTimeAbs].SetValue(value);
                }
            }
            get
            {
                return camera != null ? camera.Parameters[PLCamera.ExposureTimeAbs].GetValue() : 0;
            }
        }

        [Category("相机增益")]
        public double Gain
        {
            set
            {
                if (camera != null)
                {
                    camera.Parameters[PLCamera.Gain].SetValue(value);
                }
            }
            get
            {
                return camera != null ? camera.Parameters[PLCamera.Gain].GetValue() : 0;
            }
        }

        public bool ReverseY
        {
            set
            {
                if (camera != null)
                {
                    camera.Parameters[PLCamera.ReverseY].SetValue(value);
                }
            }
            get
            {
                return camera != null ? camera.Parameters[PLCamera.ReverseY].GetValue() : false;
            }
        }

        public bool ReverseX
        {
            set
            {
                if (camera != null)
                {
                    camera.Parameters[PLCamera.ReverseX].SetValue(value);
                }
            }
            get
            {
                return camera != null ? camera.Parameters[PLCamera.ReverseX].GetValue() : false;
            }
        }

        public void Open()
        {
            if (camera != null && !camera.IsOpen)
            {
                BaingEvent();
                camera.CameraOpened += Configuration.AcquireContinuous;
                camera.Open();
                camera.Parameters[PLTransportLayer.HeartbeatTimeout].SetValue(10000);
                
            }
        }

        public void Close()
        {
            if (camera != null && camera.IsOpen)
            {
                if (camera.StreamGrabber.IsGrabbing)
                    camera.StreamGrabber.Stop();
                camera.CameraOpened -= Configuration.AcquireContinuous;
                camera.Close();
            }
        }
        /// <summary>
        /// 开始连续采集
        /// </summary>
        public bool StartGrabbing()
        {
            try
            {
                if (camera.StreamGrabber.IsGrabbing)
                {
                    MessageBox.Show("Camera is now Continue Grabing Images", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else
                {
                    camera.Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.Continuous);
                    camera.StreamGrabber.Start(GrabStrategy.LatestImages, GrabLoop.ProvidedByStreamGrabber);
                    stopWatch.Restart();
                    return true;
                }
            }
            catch (Exception e)
            {
                ShowException(e);
                return false;
            }
        }

        /// <summary>
        /// 停止连续采集
        /// </summary>
        public void StopGrabbing()
        {
            try
            {
                if (camera.StreamGrabber.IsGrabbing)
                {
                    camera.StreamGrabber.Stop();
                }
            }
            catch (Exception e)
            {
                ShowException(e);
            }
        }
        /// <summary>
        /// 单张采集
        /// </summary>
        public void GrabOne()
        {
            try
            {
                if (camera.StreamGrabber.IsGrabbing)
                    StopGrabbing();
                camera.Parameters[PLCamera.AcquisitionMode].SetValue("SingleFrame");
                camera.StreamGrabber.Start(1, GrabStrategy.LatestImages, GrabLoop.ProvidedByStreamGrabber);
                stopWatch.Restart();
            }
            catch (Exception e)
            {
                ShowException(e);
            }
        }

        public void ShowImage(HObject hObject)
        {
            HTuple hv_Height = null, hv_Width = null;
            HPylonImage = hObject.Clone();

            if (hObject != null)
            {
                HOperatorSet.GetImageSize(hObject, out hv_Width, out hv_Height);
                HOperatorSet.SetPart(HWindows, 0, 0, hv_Height, hv_Width);
                HOperatorSet.DispObj(hObject, HWindows);
            }
        }

        /// <summary>
        /// 设置相机垂直方向镜像 Unchanged/Top Down/Bottom Up
        /// </summary>
        public void OutputOrientation(string str)
        {
            if (camera != null && !camera.IsOpen)
            {
                camera.Parameters[PLPixelDataConverter.OutputOrientation].SetValue(str);
            }
        }
        // 相机取像回调函数.
            private void OnImageGrabbed(Object sender, ImageGrabbedEventArgs e)
            {
                try
                {
                    IGrabResult grabResult = e.GrabResult;
                    if (grabResult.GrabSucceeded)
                    {
                        grabTime = stopWatch.ElapsedMilliseconds;
                        {
                            if (latestFrameAddress == IntPtr.Zero)
                            {
                                latestFrameAddress = Marshal.AllocHGlobal((Int32)grabResult.PayloadSize);
                            }
                            converter.OutputPixelFormat = PixelType.Mono8;
                            converter.Convert(latestFrameAddress, grabResult.PayloadSize, grabResult);
                            // 转换为Halcon图像显示
                            HOperatorSet.GenImage1(out hPylonImage, "byte", (HTuple)grabResult.Width, (HTuple)grabResult.Height, (HTuple)latestFrameAddress);
                            // 抛出图像处理事件
                            eventProcessImage(hPylonImage);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Grab faild!\n" + grabResult.ErrorDescription, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception exception)
                {
                    ShowException(exception);
                }
                finally
                {
                    e.DisposeGrabResultIfClone();
                }
            }

        /// <summary>
        /// 设置Gige相机心跳时间
        /// </summary>
        /// <param name="value"></param>
        public void SetHeartBeatTime(long value)
        {
            try
            {
                // 判断是否是网口相机
                if (camera.GetSfncVersion() < Sfnc2_0_0)
                {
                    camera.Parameters[PLGigECamera.GevHeartbeatTimeout].SetValue(value);
                }
            }
            catch (Exception e)
            {
                ShowException(e);
            }
        }

        /// <summary>
        /// 掉线重连回调函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnConnectionLost(Object sender, EventArgs e)
        {
            try
            {
                camera.Close();

                for (int i = 0; i < 100; i++)
                {
                    try
                    {
                        camera.Open();
                        if (camera.IsOpen)
                        {
                            MessageBox.Show("已重新连接上UserID为“" + strUserID + "”的相机！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                        Thread.Sleep(200);
                    }
                    catch
                    {
                        MessageBox.Show("请重新连接UserID为“" + strUserID + "”的相机！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                if (camera == null)
                {
                    MessageBox.Show("重连超时20s:未识别到UserID为“" + strUserID + "”的相机！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                SetHeartBeatTime(5000);
                imageWidth = camera.Parameters[PLCamera.Width].GetValue();               // 获取图像宽 
                imageHeight = camera.Parameters[PLCamera.Height].GetValue();              // 获取图像高
                //camera.StreamGrabber.ImageGrabbed += OnImageGrabbed;                      // 注册采集回调函数
                //camera.ConnectionLost += OnConnectionLost;
                BaingEvent();
            }
            catch (Exception exception)
            {
                ShowException(exception);
            }
        }
        private void BaingEvent()
        {
            camera.StreamGrabber.ImageGrabbed += OnImageGrabbed;                      // 注册采集回调函数
            camera.ConnectionLost += OnConnectionLost;
        }
        private void ShowException(Exception exception)
        {
            MessageBox.Show("Exception caught:\n" + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
