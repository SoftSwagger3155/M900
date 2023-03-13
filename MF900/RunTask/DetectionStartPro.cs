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
    public class DetectionStartPro : BaseTask
    {
        public TestTask testTaskPro;
        public bool TestTaskIsPause { get { return testTaskPro.IsPause; } }
        public TaskStatus TestTaskStatus { get { return testTaskPro.taskStatus; } }
        public DetectionStartPro(MotionBase motion, Dictionary<string, AxisPara> axisNum,Dictionary<string, AxisSpeed> axisSpeed)
            : base(motion, axisNum)
        {
            this.axisSpeed = axisSpeed;
            testTaskPro = new TestTask(motion, axisNum, axisSpeed);
            testTaskPro.SetMachineState += SetMachineStateEvent;
        }
        
        public override void TaskMethod()
        {
            bool sin1 = false;
            
            while (true)
            {
                motionCard.GetInSignal(ProgramParamMange.InIo["启动"], ref sin1);
                if (testTaskPro.taskStatus == TaskStatus.Running && !TestTaskIsPause && PauseCondition())//暂停
                    PauseTest();
                else if (testTaskPro.taskStatus == TaskStatus.Running && TestTaskIsPause && sin1)//继续
                    ConniunTest();
                else if (testTaskPro.taskStatus != TaskStatus.Running && sin1)//启动测试
                {
                    if (!MotionCommons.RunCkeckSin())
                        continue;
                    SetMachineStateEvent(MachineState.测试中);
                    testTaskPro.StartTask();
                }
                else Thread.Sleep(50);
            }
        }

        /// <summary>
        /// 暂停条件
        /// </summary>
        /// <returns></returns>
        private bool PauseCondition()
        {
            bool sin1 = false;
            motionCard.GetInSignal(ProgramParamMange.InIo["正前方安全光栅"], ref sin1);
            if (!sin1) return true;
            motionCard.GetInSignal(ProgramParamMange.InIo["上方安全光栅"], ref sin1);
            if (!sin1) return true;
            motionCard.GetInSignal(ProgramParamMange.InIo["停止"], ref sin1);
            if (sin1) return true;
            return false;
        }

        //运行准备
        public void RunPrepare()
        {
            //气缸松开
            MotionCommons.AirClamp(false);

            //轴移动到避让位
            testTaskPro.MoveAvoidZ();
            testTaskPro.MoveAvoidXY();

            //平台上升

        }
       
        /// <summary>
        /// 暂停测试
        /// </summary>
        public void PauseTest()
        {
            testTaskPro.IsPause = true;
            SetMachineStateEvent(MachineState.暂停测试);
        }
        /// <summary>
        /// 继续测试
        /// </summary>
        public void ConniunTest()
        {
            if (!MotionCommons.RunCkeckSin())
            {
                return;
            }
            testTaskPro.IsPause = false;
            SetMachineStateEvent(MachineState.测试中);
        }
        /// <summary>
        /// 结束测试
        /// </summary>
        public void EndTest(string error)
        {
            if(testTaskPro.taskStatus == TaskStatus.Running)
            {
                testTaskPro.StopTask(error);
                SetMachineStateEvent(MachineState.停止);
            }
        }
        public override void SetMachineStateEvent(MachineState machineState)
        {
            base.SetMachineStateEvent(machineState);
        }
    }
}
