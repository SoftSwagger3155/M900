using MotionCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MF900
{
    public class DeviceReset : BaseTask
    {
        public DeviceReset(MotionBase motion, Dictionary<string, AxisPara> axisNum)
            : base(motion, axisNum)
        {

        }

        public override void TaskMethod()
        {
            Task task1;
            Task task2;
            bool result1 = false;
            bool result2 = false;
            motionCard.resetStop = false;
            SetMachineStateEvent(MachineState.复位中);
            step = "输出信号复位";
            while (!cts.IsCancellationRequested)
            {
                //mrt.WaitOne();
                switch (step)
                {
                    case "输出信号复位":
                        Thread.Sleep(100);

                        step = "托板升降平台回零";
                        break;
                    case "托板升降平台回零":
                        task1 = Task.Factory.StartNew(new Action(() =>
                        {
                            result1 = motionCard.ZeroAxis(axisNum["托板升降轴"].AxisNum, 10, 1, axisNum["托板升降轴"].homeIo,
                                      axisNum["托板升降轴"].Fwd, axisNum["托板升降轴"].Rev, 5, 2, 2000, 2000, 1);

                        }));
                        Task.WaitAll(task1);
                        Thread.Sleep(20);
                        //MessageBox.Show("托板升降平台回零完成");
                        step = result1 ? "上下模Z轴回零" : "回零失败";
                        break;
                    case "上下模Z轴回零":
                        task1 = Task.Factory.StartNew(new Action(() =>
                        {
                            result1 = motionCard.ZeroAxis(axisNum["上模Z轴"].AxisNum, 20, 1, axisNum["上模Z轴"].homeIo,
                                                axisNum["上模Z轴"].Fwd, axisNum["上模Z轴"].Rev, 5, 2, 1000, 1000, 0.2f);

                        }));
                        task2 = Task.Factory.StartNew(new Action(() =>
                        {
                            result2 = motionCard.ZeroAxis(axisNum["下模Z轴"].AxisNum, 20, 1, axisNum["下模Z轴"].homeIo,
                                               axisNum["下模Z轴"].Fwd, axisNum["下模Z轴"].homeIo, 5, 1, 1000, 1000, 0.2f);

                        }));
                        Task.WaitAll(task1, task2);
                        Thread.Sleep(20);
                        step = (result1 && result2) ? "上下模XR轴回零" : "回零失败";
                        break;
                    case "上下模XR轴回零":
                        //MessageBox.Show("上下模Z轴回零完成");
                        task1 = Task.Factory.StartNew(new Action(() =>
                        {
                            result1 = motionCard.ZeroAxis(axisNum["上模X轴"].AxisNum, 30, 1, axisNum["上模X轴"].homeIo,
                                                axisNum["上模X轴"].Fwd, axisNum["上模X轴"].Rev, 5, 1, 1000, 1000, 0.2f);

                        }));
                        task2 = Task.Factory.StartNew(new Action(() =>
                        {
                            result2 = motionCard.ZeroAxis(axisNum["下模X轴"].AxisNum, 30, 1, axisNum["下模X轴"].homeIo,
                                               axisNum["下模X轴"].Fwd, axisNum["下模X轴"].Rev, 5, 1, 1000, 1000, 0.2f);

                        }));
                        Task.WaitAll(task1, task2);
                        Thread.Sleep(20);
                        step = (result1 && result2) ? "上下模Y轴回零" : "回零失败";
                        break;
                    case "上下模Y轴回零":
                        //MessageBox.Show("上下模Y轴回零");
                        task1 = Task.Factory.StartNew(new Action(() =>
                        {
                            result1 = motionCard.ZeroAxis(axisNum["上模Y轴"].AxisNum, 20, 1, axisNum["上模Y轴"].homeIo,
                                                axisNum["上模Y轴"].Fwd, axisNum["上模Y轴"].Rev, 5, 1, 1000, 1000, 0);

                        }));
                        task2 = Task.Factory.StartNew(new Action(() =>
                        {
                            result2 = motionCard.ZeroAxis(axisNum["下模Y轴"].AxisNum, 10, 1, axisNum["下模Y轴"].homeIo,
                                               axisNum["下模Y轴"].Fwd, axisNum["下模Y轴"].Rev, 5, 1, 1000, 1000, 0);

                        }));
                        Task.WaitAll(task1, task2);
                        Thread.Sleep(20);
                        step = (result1 && result2) ? "回零完成" : "回零失败";
                        break;
                    case "回零完成":
                        //motionCard.SetOutSignal(23, 1);
                        SetMachineStateEvent(MachineState.复位完成);
                        //Thread.Sleep(200);
                        //motionCard.SetOutSignal(23, 0);
                        step = "回零结束";
                        break;
                    case "回零失败":
                        SetMachineStateEvent(MachineState.复位失败);
                        step = "回零终止";
                        break;
                    default:
                        break;
                }
                if (step == "回零结束" || step == "回零终止")
                    break;
            }
        }

        public override void SetMachineStateEvent(MachineState machineState)
        {
            base.SetMachineStateEvent(machineState);
        }
    }
}
