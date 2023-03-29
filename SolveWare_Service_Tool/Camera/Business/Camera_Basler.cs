using Basler.Pylon;
using HalconDotNet;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.General;
using SolveWare_Service_Core;
using SolveWare_Service_Tool.Camera.Base.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SolveWare_Service_Tool.Camera.Data;

namespace SolveWare_Service_Tool.Camera.Business
{
    public class Camera_Basler : CameraBase
    {
        Basler.Pylon.Camera camera_Basler;
        Stopwatch stopwatch;
        private PixelDataConverter converter = new PixelDataConverter();
        private IntPtr latestFrameAddress = IntPtr.Zero;

        /// <summary>
        /// if >= Sfnc2_0_0,说明是ＵＳＢ３的相机
        /// </summary>
        static Version Sfnc2_0_0 = new Version(2, 0, 0);
        //public ConfigData_Camera ConfigData { get; set; }

        public Camera_Basler(IElement configData) : base(configData)
        {
            //this.ConfigData = configData as ConfigData_Camera;
            stopwatch = new Stopwatch();

            if (Id == 0) id = IdentityGenerator.IG.GetIdentity();
        }
        public override void AssingCamera(object obj_Camera)
        {
            this.imagePart_X = this.configData.ImagePart_X;
            this.imagePart_Y = this.configData.ImagePart_Y;

            if (obj_Camera == null) return;

            this.camera_Basler = obj_Camera as Basler.Pylon.Camera;
            string sn = this.camera_Basler.CameraInfo[CameraInfoKey.SerialNumber];
            string modelName = this.camera_Basler.CameraInfo[CameraInfoKey.ModelName];
            string userID = this.camera_Basler.CameraInfo[CameraInfoKey.UserDefinedName];
            this.Id_Camera = $"[{sn}]-{userID}-{modelName}";
            this.ConfigData.Id_Camera = this.Id_Camera;
            this.Name = this.ConfigData.Name;

            OpenCamera();
            BaingEvent();
        }

        private void OpenCamera()
        {

            if (this.configData.IsSimulation) return;
            if(camera_Basler == null) return;

            try
            {
                if(this.camera_Basler.IsOpen) this.camera_Basler.Close();

                this.camera_Basler.Open();
                int width = (int)camera_Basler.Parameters[PLGigECamera.SensorWidth].GetValue();
                int height = (int)camera_Basler.Parameters[PLGigECamera.SensorHeight].GetValue();

                this.configData.ImagePart_X = (int)width;
                this.configData.ImagePart_Y = (int)height;

            }
            catch (Exception)
            {
                OnConnectionLost();
            }          
        }

        public override void CloseCamera()
        {
            if (camera_Basler == null) return;
            if (camera_Basler.IsOpen)
            {
                if (camera_Basler.StreamGrabber.IsGrabbing)
                    StopLive(200);

                camera_Basler.Close();
            }
        }

        public override void GetExposureTime()
        {
            if (camera_Basler == null) return;
            this.ExposureTime = (int)camera_Basler.Parameters[PLCamera.ExposureTime].GetValue();
        }

        public override void GetFrameRate()
        {
            if (camera_Basler == null) return;
            this.FrameRate = (int)camera_Basler.Parameters[PLCamera.AcquisitionFrameRateAbs].GetValue();
        }

        public override int GrabImageOnce()
        {
            int errorCode = ErrorCodes.NoError;
            st = DateTime.Now;
            try
            {
                if (camera_Basler.StreamGrabber.IsGrabbing)
                    StopLive();
                camera_Basler.Parameters[PLCamera.AcquisitionMode].SetValue("SingleFrame");
                camera_Basler.StreamGrabber.Start(1, GrabStrategy.LatestImages, GrabLoop.ProvidedByStreamGrabber);
            }
            catch (Exception ex)
            {
                errorCode = ErrorCodes.VisionFailed;
                SolveWare.Core.MMgr.Infohandler.LogExceptionMessage(ErrorCodes.GetErrorDescription(errorCode), ex, st);
            }

            return errorCode;
        }

        public override int SetBrightness()
        {
            camera_Basler.Parameters[PLCamera.ExposureTime].SetValue(this.ExposureTime);
            camera_Basler.Parameters[PLCamera.Gain].SetValue(this.Gain);

            return 0;
        }

        public override int SetFrameRate()
        {
            throw new NotImplementedException();
        }

        //
        CancellationTokenSource simulateSource;
        AutoResetEvent StopFlag = new AutoResetEvent(false);
        public override int StartLive(int delayTime_ms = 100)
        {
            int errorCode = ErrorCodes.NoError;
            try
            {
                do
                {
                    if (this.configData.IsSimulation || this.camera_Basler == null)
                    {
                        simulateSource = new CancellationTokenSource();
                        Task task = new Task(() =>
                        {
                            while (!simulateSource.IsCancellationRequested)
                            {
                                this.FrameRate = new Random().Next(50, 101);
                                this.GrabTime = new Random().Next(50, 101);
                                OnPropertyChanged(nameof(CameraGrabCapabilityInfo));

                                Thread.Sleep(delayTime_ms);
                            }
                            StopFlag.Set();

                        }, simulateSource.Token, TaskCreationOptions.LongRunning);

                        task.Start();
                        break;
                    }

                    if (camera_Basler == null) break;

                    if(!camera_Basler.IsOpen) camera_Basler.Open(); 
                    if (!camera_Basler.StreamGrabber.IsGrabbing)
                    {
                        camera_Basler.Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.Continuous);
                        camera_Basler.StreamGrabber.Start(GrabStrategy.LatestImages, GrabLoop.ProvidedByStreamGrabber);
                        st = DateTime.Now;
                    }

                } while (false);
            }
            catch (Exception e)
            {
                errorCode = ErrorCodes.VisionFailed;
            }

            if (errorCode != ErrorCodes.NoError)
                SolveWare.Core.MMgr.Infohandler.LogMessage("Error: 相机实时拍摄功能", true, true);

            return errorCode;
        }

        DateTime st = DateTime.Now;
        public override int StopLive(int delayTime__ms = 100)
        {
            int errorCode = ErrorCodes.NoError;
            try
            {
                do
                {
                    if (this.configData.IsSimulation || this.camera_Basler == null)
                    {
                        simulateSource.Cancel();
                        StopFlag.WaitOne(5000);
                        simulateSource = null;
                        break;
                    }

                    if (camera_Basler == null) break;
                    if (camera_Basler.StreamGrabber.IsGrabbing)
                    {
                        camera_Basler.StreamGrabber.Stop();
                        //camera_Basler.Parameters[PLCamera.AcquisitionStop].SetValue();
                        //camera_Basler.StreamGrabber.Start(GrabStrategy.LatestImages, GrabLoop.ProvidedByStreamGrabber);
                   
                    }

                } while (false);
            }
            catch (Exception e)
            {
                errorCode = ErrorCodes.VisionFailed;
            }

            if (errorCode != ErrorCodes.NoError)
                SolveWare.Core.MMgr.Infohandler.LogMessage("Error: 相机停止实时拍摄功能", true, true);

            return errorCode;
        }

        private void BaingEvent()
        {
            camera_Basler.StreamGrabber.ImageGrabbed -= OnImageGrabbed;
            camera_Basler.StreamGrabber.ImageGrabbed += OnImageGrabbed;                      // 注册采集回调函数

            camera_Basler.ConnectionLost -= OnConnectionLost;
            camera_Basler.ConnectionLost += OnConnectionLost;
        }

        int index = 0;
        int count = 1;
        double totalTime = 0;
        private void OnImageGrabbed(Object sender, ImageGrabbedEventArgs e)
        {
            double timeTick = 0;
            try
            {
                IGrabResult grabResult = e.GrabResult;
                HObject ho_Image;
               
                if (grabResult.GrabSucceeded)
                {
                    GrabTime = stopwatch.ElapsedMilliseconds;
                    {
                        if (latestFrameAddress == IntPtr.Zero)
                        {
                            latestFrameAddress = Marshal.AllocHGlobal((Int32)grabResult.PayloadSize);
                        }
                        converter.OutputPixelFormat = PixelType.Mono8;
                        converter.Convert(latestFrameAddress, grabResult.PayloadSize, grabResult);
                        // 转换为Halcon图像显示
                        HOperatorSet.GenImage1(out ho_Image, "byte", (HTuple)grabResult.Width, (HTuple)grabResult.Height, (HTuple)latestFrameAddress);
                        
                        HobjectToHimage(ho_Image, ref this.image);
                        OnPropertyChanged(nameof(Image));
                        
                        if (this.WindowHost != null) HOperatorSet.DispImage(image, WindowHost);
                        TimeSpan ts = DateTime.Now - st;
                        double result = ts.TotalMilliseconds;
                        timeTick = (int)(1000 / result*1000)/1000;
                        st = DateTime.Now;

                        index++;
                        if(index >= 15)
                        {
                            //totalTime += timeTick;
                            //this.GrabTime = (int)totalTime / count;
                            //count++;

                            this.GrabTime = (int)timeTick;
                            OnPropertyChanged(nameof(this.CameraGrabCapabilityInfo));
                            index = 0;
                        }
                       
                    }
                }
            }
            catch (Exception ex)
            {
                SolveWare.Core.MMgr.Infohandler.LogExceptionMessage($"{this.Name} Grab Image Failed", ex, DateTime.Now);
            }
            finally
            {
                e.DisposeGrabResultIfClone();
            }
        }
        private void HobjectToHimage(HObject hobj, ref HImage img)
        {
            HTuple pointer,type,width,height;
            HOperatorSet.GetImagePointer1(hobj, out pointer, out type, out width, out height);
            img.GenImage1(type, width, height, pointer);
        }

        private void OnConnectionLost(Object sender, EventArgs e)
        {
            OnConnectionLost();
        }
        public void OnConnectionLost()
        {
            try
            {
                camera_Basler.Close();

                for (int i = 0; i < 100; i++)
                {
                    try
                    {
                        camera_Basler.Open();
                        if (camera_Basler.IsOpen)
                        {
                            //MessageBox.Show("已重新连接上UserID为“" + strUserID + "”的相机！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                        Thread.Sleep(200);
                    }
                    catch
                    {
                        //MessageBox.Show("请重新连接UserID为“" + strUserID + "”的相机！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                if (camera_Basler == null)
                {
                    //MessageBox.Show("重连超时20s:未识别到UserID为“" + strUserID + "”的相机！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                SetHeartBeatTime(5000);
                //imageWidth = camera.Parameters[PLCamera.Width].GetValue();               // 获取图像宽 
                //imageHeight = camera.Parameters[PLCamera.Height].GetValue();              // 获取图像高
                //camera.StreamGrabber.ImageGrabbed += OnImageGrabbed;                      // 注册采集回调函数
                //camera.ConnectionLost += OnConnectionLost;
                BaingEvent();
            }
            catch (Exception ex)
            {
                SolveWare.Core.MMgr.Infohandler.LogExceptionMessage($"{this.Name} ReConnected Failed", ex, DateTime.Now);
            }
        }

        public void SetHeartBeatTime(long value)
        {
            try
            {
                // 判断是否是网口相机
                if (camera_Basler.GetSfncVersion() < Sfnc2_0_0)
                {
                    camera_Basler.Parameters[PLGigECamera.GevHeartbeatTimeout].SetValue(value);
                }
            }
            catch (Exception ex)
            {
                SolveWare.Core.MMgr.Infohandler.LogExceptionMessage($"{this.Name} Set HeartBeat Time Failed", ex, DateTime.Now);
            }
        }
    }
}
