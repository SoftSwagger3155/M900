using Basler.Pylon;
using HalconDotNet;
using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.General;
using SolveWare_Service_Tool.Camera.Base.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SolveWare_Service_Tool.Camera.Business
{
    public class Camera_Media_Basler : CameraMediaBase
    {
        private Basler.Pylon.Camera media;
        private Stopwatch sWatch;
        private PixelDataConverter converter;
        private IntPtr latestFrameAddress = IntPtr.Zero;
        private static Version Sfnc2_0_0 = new Version(2, 0, 0);
        private CancellationTokenSource simulateSource;
        private AutoResetEvent StopFlag = new AutoResetEvent(false);

        public Camera_Media_Basler(string name, bool isSimulation)
        {
            this.Name = name;
            this.isSimulation = isSimulation;
            sWatch = new Stopwatch();
            converter = new PixelDataConverter();
        }
        public override void AssignCameraMedia(object obj_Media)
        {
            this.media = obj_Media as Basler.Pylon.Camera;
            BeginEvent();
        }
        public void Assign_Media_Related_Parameter()
        {
            //判断
            if(this.media == null && !isSimulation)
            {
                SolveWare.Core.MMgr.Infohandler.LogMessage("无相机物件", true, true);
                return;
            }

            //实行工作
            minimum_ExposoureTime = isSimulation ? 0: (int)media.Parameters[PLGigECamera.AutoExposureTimeAbsLowerLimit].GetMinimum();
            maximum_ExposoureTime = isSimulation ? 100000 : (int)media.Parameters[PLGigECamera.AutoExposureTimeAbsUpperLimit].GetMaximum();
            minimum_Gain = isSimulation? 0: (int)media.Parameters[PLGigECamera.AutoGainRawLowerLimit].GetMinimum();
            maximum_Gain = isSimulation ? 100000 : (int)media.Parameters[PLGigECamera.AutoGainRawUpperLimit].GetMaximum();
            imagePartX =isSimulation ? 1920:  (int)media.Parameters[PLGigECamera.SensorWidth].GetValue();
            imagePartY = isSimulation ? 1080: (int)media.Parameters[PLGigECamera.SensorHeight].GetValue();
            worldCenterX = imagePartX / 2;
            worldCenterY = imagePartY / 2;
        }
        private void BeginEvent()
        {
            if (media == null) return;
            media.StreamGrabber.ImageGrabbed -= OnImageGrabbed;
            media.StreamGrabber.ImageGrabbed += OnImageGrabbed;                      // 注册采集回调函数

            media.ConnectionLost -= OnConnectionLost;
            media.ConnectionLost += OnConnectionLost;
        }
        private void OnConnectionLost(object sender, EventArgs e)
        {
            try
            {
                //停止连接
                media.StreamGrabber.Stop();
                //释放相机资源
                DestroyCamera();
            }
            catch (Exception)
            {
                return;
            }


        }
        public void DestroyCamera()
        {
            if (media != null)
            {
                media.Close();
                media.Dispose();
                media = null;
            }
            else
            {
                return;
            }
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
                        if(image != null) 
                        {
                            OnPropertyChanged(nameof(Image));
                            this.grabTime_ms = (int)media.Parameters[PLGigECamera.ResultingFrameRateAbs].GetValue();
                            OnPropertyChanged(nameof(GrabTime_ms));
                        }

                        
                        //if (hWindow != null)
                        //    hWindow.DispImage(image);

                        //if (this.WindowHost != null) HOperatorSet.DispImage(image, WindowHost);
                        //TimeSpan ts = DateTime.Now - st;
                        
                        //double result = sWatch.ElapsedMilliseconds;
                        //timeTick = (int)(1000 / result * 1000) / 1000;
                        //this.GrabTimeInfo = timeTick.ToString("F2");
                        //sWatch.Restart();

                        //index++;
                        //if (index >= 15)
                        //{
                        //    this.grabTime_ms = (int)timeTick;
                        //    OnPropertyChanged(nameof(this.GrabTime_ms));
                        //    index = 0;
                        //}

                    }
                }
            }
            catch (Exception ex)
            {
                SolveWare.Core.MMgr.Infohandler.LogExceptionMessage($"{this.Name} Grab Image Failed", ex, DateTime.Now);
            }
            finally
            {
                //e.DisposeGrabResultIfClone();
            }
        }
        private void HobjectToHimage(HObject hobj, ref HImage img)
        {
            HTuple pointer, type, width, height;
            HOperatorSet.GetImagePointer1(hobj, out pointer, out type, out width, out height);
            img.GenImage1(type, width, height, pointer);
        }
        public override bool Close()
        {
            string errMsg = string.Empty;
            try
            {
                do
                {
                    //判断
                    if (isSimulation) { break; }
                    if (media == null)
                    {
                        errMsg += "无相机物件\n";
                        break;
                    }
                    if (this.media.IsOpen == false) { break; }

                    //执行
                    if (this.media.StreamGrabber.IsGrabbing)
                        this.media.StreamGrabber.Stop();

                    this.media.Close();
                    Thread.Sleep(20);

                } while (false);
            }
            catch (Exception ex)
            {
                errMsg += ex.Message;
            }

            return Get_Result(nameof(this.Open), errMsg);
        }
        public override bool Open()
        {
            string errMsg = string.Empty; 
            try
            {
                do
                {
                    //判断
                    if (isSimulation) { break; }
                    if (media == null)
                    {
                        errMsg += "无相机物件\n";
                        break;
                    }
                    if (this.media.IsOpen) {break; }

                    //执行
                    this.media.Open();
                    Thread.Sleep(20);

                } while (false);
            }
            catch (Exception ex)
            {
                errMsg += ex.Message;
            }

            return Get_Result(nameof(this.Open), errMsg);
        }
        public override bool SetAutoGain(bool autoGain)

        {
            string errMsg = string.Empty;
            try
            {
                do
                {
                    //判断
                    if (isSimulation) { break; }
                    if (media == null)
                    {
                        errMsg += "无相机物件\n";
                        break;
                    }

                    //执行
                    string action = autoGain ? PLCamera.GainAuto.Continuous : PLCamera.GainAuto.Off;
                    media.Parameters[PLCamera.ExposureAuto].SetValue(action);
                    Thread.Sleep(20);

                } while (false);
            }
            catch (Exception ex)
            {
                errMsg += ex.Message;
            }

            return Get_Result(nameof(this.SetAutoGain), errMsg);
        }
        public override bool SetExposureTime(int exposureTime)
        {
            string errMsg = string.Empty;
            try
            {
                do
                {
                    //判断
                    if (isSimulation) { break; }
                    if (media == null)
                    {
                        errMsg += "无相机物件\n";
                        break;
                    }

                    //执行
                    SetAutoExposure(false);
                    media.Parameters[PLGigECamera.ExposureTimeAbs].SetValue(1.0*exposureTime);
                    Thread.Sleep(20);

                } while (false);
            }
            catch (Exception ex)
            {
                errMsg += ex.Message;
            }

            return Get_Result(nameof(this.SetExposureTime), errMsg);
        }
        public override bool SetGain(int gain)
        {
            string errMsg = string.Empty;
            try
            {
                do
                {
                    //判断
                    if (isSimulation) { break; }
                    if (media == null)
                    {
                        errMsg += "无相机物件\n";
                        break;
                    }

                    //执行
                    SetAutoGain(false);
                    media.Parameters[PLGigECamera.GainRaw].SetValue(gain);
                    Thread.Sleep(20);

                } while (false);
            }
            catch (Exception ex)
            {
                errMsg += ex.Message;
            }

            return Get_Result(nameof(this.SetGain), errMsg);
        }
        public override int StartLive(int delayTime_ms = 100)
        {
            int errorCode = ErrorCodes.NoError;
            string errMsg = string.Empty;
            try
            {
                do
                {
                    //判断
                    if (isSimulation)
                    {
                        simulateSource = new CancellationTokenSource();
                        Task task = new Task(() =>
                        {
                            while (!simulateSource.IsCancellationRequested)
                            {
                                this.grabTime_ms = new Random().Next(50, 101);
                                OnPropertyChanged(nameof(GrabTime_ms));

                                Thread.Sleep(delayTime_ms);
                            }
                            StopFlag.Set();

                        }, simulateSource.Token, TaskCreationOptions.LongRunning);

                        task.Start();
                        break;
                    }
                    if (media == null)
                    {
                        errMsg += "无相机物件\n";
                        break;
                    }

                    //执行
                    if (!media.IsOpen) media.Open();
                    if (media.StreamGrabber.IsGrabbing) break;

                    media.Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.Continuous);
                    media.StreamGrabber.Start(GrabStrategy.LatestImages, GrabLoop.ProvidedByStreamGrabber);
                    Thread.Sleep(20);

                    sWatch.Restart();

                } while (false);
            }
            catch (Exception ex)
            {
                errMsg += ex.Message;
            }

            errorCode = Get_Result(nameof(this.StartLive), errMsg) ? ErrorCodes.NoError : ErrorCodes.ActionFailed;
            return errorCode;
        }
        public override int StopLive()
        {
            int errorCode = ErrorCodes.NoError;
            string errMsg = string.Empty;
            try
            {
                do
                {
                    //判断
                    if (isSimulation) 
                    {
                        simulateSource.Cancel();
                        StopFlag.WaitOne(5000);
                        simulateSource = null;
                        break; 
                    }
                    if (media == null)
                    {
                        errMsg += "无相机物件\n";
                        break;
                    }

                    //执行
                    if (!media.IsOpen) media.Open();
                    if (media.StreamGrabber.IsGrabbing)
                    {
                       media.StreamGrabber.Stop();
                    }
                    
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    while(true)
                    {
                        if (media.StreamGrabber.IsGrabbing == false)
                        {
                            break;
                        }
                        Thread.Sleep(20);

                        if(sw.ElapsedMilliseconds > 5000)
                        {
                            errMsg += "停止超时\n";
                            break;
                        }
                    }

                    media.Parameters[PLGigECamera.AcquisitionMode].SetValue(PLGigECamera.AcquisitionMode.SingleFrame);
                    media.StreamGrabber.Start(1, GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);


                } while (false);
            }
            catch (Exception ex)
            {
                errMsg += ex.Message;
            }

            errorCode = Get_Result(nameof(this.StopLive), errMsg) ? ErrorCodes.NoError : ErrorCodes.ActionFailed;
            return errorCode;
        }
        public override bool SetAutoExposure(bool autoExposure)
        {
            string errMsg = string.Empty;
            try
            {
                do
                {
                    //判断
                    if (isSimulation) { break; }
                    if (media == null)
                    {
                        errMsg += "无相机物件\n";
                        break;
                    }

                    //执行
                    string action = autoExposure ? PLGigECamera.ExposureAuto.Continuous : PLGigECamera.ExposureAuto.Off;
                    media.Parameters[PLGigECamera.ExposureAuto].SetValue(action);
                    Thread.Sleep(20);

                } while (false);
            }
            catch (Exception ex)
            {
                errMsg += ex.Message;
            }

            return Get_Result(nameof(this.SetAutoExposure), errMsg);
        }
        public override void GrabOneShot()
        {
            string errMsg = string.Empty;
            try
            {
                do
                {
                    //判断
                    if (isSimulation) { break; }
                    if (media == null)
                    {
                        errMsg += "无相机物件\n";
                        break;
                    }

                    if (this.media.StreamGrabber.IsGrabbing)
                    {
                        int errorCode = ErrorCodes.NoError;
                        errorCode = StopLive();

                        if (errorCode != ErrorCodes.NoError) break;
                    }

                    sWatch.Restart();
                    media.Parameters[PLGigECamera.AcquisitionMode].SetValue(PLGigECamera.AcquisitionMode.SingleFrame);
                    media.StreamGrabber.Start(1, GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);

                } while (false);
            }
            catch (Exception ex)
            {
                errMsg += ex.Message;
            }
            Get_Result(nameof(this.GrabOneShot), errMsg);
        }
        public override bool IsGrabing()
        {
            if (IsSimulation) return false;
            if (media == null) return false;

            return this.media.StreamGrabber.IsGrabbing;
        }
    }
}
