using MotionCard;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MF900
{
    public class TestTask : BaseTask
    {
        public bool reviceT0 = false;
        public bool reviceT1 = false;
        public float upX = 0, upY = 0, upR = 0;
        public float downX = 0, downY = 0, downR = 0;
        public bool visionResult = true;
        public TestTask(MotionBase motion, Dictionary<string, AxisPara> axisNum, Dictionary<string, AxisSpeed> axisSpeed)
            : base(motion, axisNum)
        {
            base.axisSpeed = axisSpeed;
        }
        
        public static int stepX; //每区内跳步数量X
        public static int stepY; //每区内跳步数量Y
        public static void UpdataSetpXY()
        {
            stepX = ProgramParamMange.ProductDataPara.ProductXY.X / ProgramParamMange.DownJipDataPara.JipStepXY.X;
            stepY = ProgramParamMange.ProductDataPara.ProductXY.Y / ProgramParamMange.DownJipDataPara.JipStepXY.Y;

        }

        /// <summary>
        /// 测试流程循环
        /// </summary>
        private void TestProcess()
        {
            string stepStr = string.Empty;
            float baseVisX = 0;
            float baseVisY = 0;
            UpdataSetpXY();
            for (int i = 0; i < ProgramParamMange.ProductDataPara.RegionCount.Y; i++)
            {
                for (int j = 0; j < ProgramParamMange.ProductDataPara.RegionCount.X; j++)
                {
                    //区定位

                    for (int col = 0; col < stepY; col++)
                    {
                        for (int row = 0; row < stepX; row++)
                        {
                            stepStr = "拍照位";
                            //拍照位计算
                            baseVisX = ProgramParamMange.DeviceBasePara.BasePointXY.X +
                                ProgramParamMange.ProductDataPara.ReferencePositionXY.X + 
                                row* ProgramParamMange.ProductDataPara.CqtFaceStepXY.X +  //跳模距离
                                j* ProgramParamMange.ProductDataPara.RegionSetp.X; //跳区距离
                            baseVisY = ProgramParamMange.DeviceBasePara.BasePointXY.Y +
                                ProgramParamMange.ProductDataPara.ReferencePositionXY.Y +
                                col * ProgramParamMange.ProductDataPara.CqtFaceStepXY.Y +
                                i * ProgramParamMange.ProductDataPara.RegionSetp.Y;

                            while (!cts.IsCancellationRequested)
                            {
                                PauseTaskMethod();
                                if (cts.IsCancellationRequested) break;
                                switch (stepStr)
                                {
                                    case "拍照位":
                                        motionCard.MoveAbs(axisNum["上模X轴"].AxisNum, baseVisX, axisSpeed["上模X轴"], vecPercent, 0.2f);
                                        motionCard.MoveAbs(axisNum["上模Y轴"].AxisNum, baseVisY, axisSpeed["上模Y轴"], vecPercent, 0.2f);
                                        if (!motionCard.WaitInPlace(axisNum["上模X轴"].AxisNum, baseVisX))
                                            StopTask("上模X轴移动到拍照位异常 " + baseVisX.ToString());
                                        if (!motionCard.WaitInPlace(axisNum["上模Y轴"].AxisNum, baseVisY))
                                            StopTask("上模X轴移动到拍照位异常 " + baseVisX.ToString());
                                        break;
                                    case "偏位计算":

                                        break;
                                    case "测试位":

                                        break;
                                    case "测试":

                                        break;
                                    case "等待测试结果":

                                        break;
                                    case "测试完成":

                                        break;
                                    case "跳出":

                                        break;
                                    default:
                                        break;
                                }
                                if (stepStr == "跳出")
                                    break;
                            }
                        }
                    }
                }
            }
        }

        public override void TaskMethod()
        {
            step = "气缸夹紧拉伸";
            float offPos = 0;
            try
            {
                while (!cts.IsCancellationRequested)
                {
                    PauseTaskMethod();
                    if (cts.IsCancellationRequested) break;
                    switch (step)
                    {
                        case "气缸夹紧拉伸":
                            MotionCommons.AirClamp(true);
                            step = "托盘下降";
                            break;
                        case "托盘下降":
                            motionCard.MoveAbs(axisNum["托板升降轴"].AxisNum, 0, axisSpeed["托板升降轴"], vecPercent, 0.2f);
                            if (!motionCard.WaitInPlace(axisNum["托板升降轴"].AxisNum, 0))
                                StopTask("托板升降轴移动到避让位异常" + offPos.ToString());
                            //MessageBox.Show("托板下降完成");
                            step = "开始测试";
                            break;
                        case "开始测试":
                            TestProcess();
                            step = "上下模XYU轴移动到避让位";
                            break;
                        case "上下模XYU轴移动到避让位":
                            MoveAvoidXY();
                            step = "上下模XY是否在避让位";
                            break;
                        case "上下模XY是否在避让位":
                           
                            //MessageBox.Show("上下模XY在避让位");
                            step = "托板上来";
                            break;
                        case "托板上来":  
                            motionCard.MoveAbs(axisNum["托板升降轴"].AxisNum, ProgramParamMange.DeviceBasePara.PlatformHeight, axisSpeed["托板升降轴"], vecPercent, 0.2f);
                            if (!motionCard.WaitInPlace(axisNum["托板升降轴"].AxisNum, ProgramParamMange.DeviceBasePara.PlatformHeight))
                                StopTask("托板升降轴移动到避让位异常" + offPos.ToString());

                            cts.Cancel();//测试完成
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                StopTask(ex.Message);
                SetMachineStateEvent(MachineState.报警);
            }
        }

        /// <summary>
        /// 预定位流程
        /// </summary>
        public void PreLocationPro()
        {

        }

        /// <summary>
        /// 上下模Z轴移动到避让位
        /// </summary>
        public void MoveAvoidZ()
        {
            float offPos = 0;
            motionCard.MoveAbs(axisNum["上模Z轴"].AxisNum, ProgramParamMange.DeviceBasePara.UpAvoidPoints.Z, axisSpeed["上模Z轴"], vecPercent, 0.2f);
            motionCard.MoveAbs(axisNum["下模Z轴"].AxisNum, ProgramParamMange.DeviceBasePara.DownAvoidPoints.Z, axisSpeed["下模Z轴"], vecPercent, 0.2f);
            if (!motionCard.WaitInPlace(axisNum["上模Z轴"].AxisNum, ProgramParamMange.DeviceBasePara.UpAvoidPoints.Z))
                StopTask("上模X轴移动到避让位异常" + offPos.ToString());
            if (!motionCard.WaitInPlace(axisNum["下模Z轴"].AxisNum, ProgramParamMange.DeviceBasePara.UpAvoidPoints.Z))
                StopTask("上模Y轴移动到避让位异常" + offPos.ToString());
        }
        /// <summary>
        /// 上下模XY移动到避让位
        /// </summary>
        public void MoveAvoidXY()
        {
            float offPos = 0;
            motionCard.MoveAbs(axisNum["上模X轴"].AxisNum, ProgramParamMange.DeviceBasePara.UpAvoidPoints.X, axisSpeed["上模X轴"], vecPercent, 0.2f);
            motionCard.MoveAbs(axisNum["上模Y轴"].AxisNum, ProgramParamMange.DeviceBasePara.UpAvoidPoints.Y, axisSpeed["上模Y轴"], vecPercent, 0.2f);
            motionCard.MoveAbs(axisNum["下模X轴"].AxisNum, ProgramParamMange.DeviceBasePara.DownAvoidPoints.X, axisSpeed["下模X轴"], vecPercent, 0.2f);
            motionCard.MoveAbs(axisNum["下模Y轴"].AxisNum, ProgramParamMange.DeviceBasePara.DownAvoidPoints.Y, axisSpeed["下模Y轴"], vecPercent, 0.2f);
            if (!motionCard.WaitInPlace(axisNum["上模X轴"].AxisNum, 0))
                StopTask("上模X轴移动到避让位异常" + offPos.ToString());
            if (!motionCard.WaitInPlace(axisNum["上模Y轴"].AxisNum, 0))
                StopTask("上模Y轴移动到避让位异常" + offPos.ToString());
            if (!motionCard.WaitInPlace(axisNum["下模X轴"].AxisNum, 0))
                StopTask("下模X轴移动到避让位异常" + offPos.ToString());
            if (!motionCard.WaitInPlace(axisNum["下模Y轴"].AxisNum, 340))
                StopTask("下模Y轴移动到避让位异常" + offPos.ToString());
        }
        /// <summary>
        /// 上下模旋转轴移动到避让位
        /// </summary>
        public void MoveAvoidU()
        {

        }

        public override void SetMachineStateEvent(MachineState machineState)
        {
            base.SetMachineStateEvent(machineState);
        }
    }
}
