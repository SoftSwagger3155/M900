using MotionCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MF900
{
    public enum MachineState 
    {
        未复位,
        复位中,
        复位完成,
        复位失败,
        测试中,
        暂停测试,
        停止,
        报警,
        急停按下,
        未装治具

        //NoneReset,  //未复位
        //BeingReset,
        //ResetFinish, //复位完成
        //ResetFail,   //复位失败
        //BeingRun,
        //MachinePause,
        //MachineStop,
        //MachineAlarm
    }
    public abstract class BaseTask
    {
        //public event Action<string> StopRun;
        //public ManualResetEvent mrt = new ManualResetEvent(true);
        public CancellationTokenSource cts = new CancellationTokenSource();
        public MotionBase motionCard;
        public Dictionary<string, AxisSpeed> axisSpeed;
        public Dictionary<string, AxisPara> axisNum;
        public event Action<MachineState> SetMachineState;
        public string step = string.Empty;
        public static float vecPercent = 0.1f;
        public bool IsPause { get; set; }
        public Task task { get; set; }
        public TaskStatus taskStatus { get { return task.Status; } }
        public BaseTask(MotionBase motion, Dictionary<string, AxisPara> axisNum)
        {
            motionCard = motion;
            this.axisNum = axisNum;
            task = new Task(new Action(() => TaskMethod()), cts.Token);
        }
        
        /// <summary>
        /// 启动线程
        /// </summary>
        public void StartTask()
        {
            cts = new CancellationTokenSource();
            IsPause = false;
            task = new Task(new Action(() => TaskMethod()), cts.Token);
            task.Start();
        }
        /// <summary>
        /// 运行方法
        /// </summary>
        public abstract void TaskMethod();
        public void PauseTaskMethod()
        {
            while (IsPause)
            {
                Thread.Sleep(20);
            }
        }
        public virtual void SetMachineStateEvent(MachineState machineState)
        {
            SetMachineState(machineState);
        }

        private void EndTask()
        {
            cts?.Cancel();
        }
        public void StopTask(string error)
        {
            EndTask();
            motionCard.resetStop = true;
            Log4NetHepler.WriteError(error);
        }
    }
}
