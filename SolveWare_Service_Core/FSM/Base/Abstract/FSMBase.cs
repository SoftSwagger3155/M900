using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.FSM.Base.Interface;
using SolveWare_Service_Core.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolveWare_Service_Core.FSM.Base.Abstract
{
  
    public abstract class FSMBase : JobFundamentalBase, IFSM
    {
        protected  bool isSimulation;
        public IList<IFSMStation> Stations { get; set; }

        public FSMBase(bool isSimulation)
        {
            Stations = new List<IFSMStation>();
            this.isSimulation = isSimulation;

        }

        /// <summary>
        /// 目前没用
        /// </summary>
        public void Build_Resource()
        {
            return;
        }

        /// <summary>
        /// 持续自动循环
        /// </summary>
        /// <returns></returns>
        public Mission_Report Run_Auto_Cycle()
        {
            if (isSimulation)
            {
                var result = MessageBox.Show("因有硬件相关系统设为模拟状态，FSM 会以模拟状态运行\r\n继续按 Yes", "提示", MessageBoxButtons.YesNo);
                if (result == DialogResult.No) { return new Mission_Report { ErrorCode = ErrorCodes.NoError }; }
            }

            Mission_Report mContext = new Mission_Report();
            SolveWare.Core.MMgr.SetStatus(Definition.Machine_Status.Auto);
            string errMsg = string.Empty;
            List<Task> tasks = new List<Task>();

            try
            {
                Stations.ToList().ForEach(stn =>
                {
                    Task task = Task.Factory.StartNew((object obj) =>
                    {
                        Data_Mission_Report fsm = obj as Data_Mission_Report;
                        fsm.Context = stn.RunSingleCycle();
                    }, new Data_Mission_Report());
                    tasks.Add(task);
                });

                Task.WaitAll(tasks.ToArray());


                tasks.ForEach(task =>
                {
                    var taskReport = task.AsyncState as Data_Mission_Report;
                    mContext.Message += taskReport.Context.Message + "\r\n";
                });


            }
            catch (Exception ex)
            {
                mContext.Message += ex.Message;
            }
            finally
            {
                if (string.IsNullOrEmpty(mContext.Message) == false)
                {
                    mContext.ErrorCode = ErrorCodes.FSMRunningFailed;
                    SolveWare.Core.ShowMsg($"任务 执行 失败, 原因如下\r\n{mContext.Message}", true);
                }
                else
                {
                    mContext.ErrorCode = ErrorCodes.NoError;
                    SolveWare.Core.ShowMsg("任务完成");
                }

                this.Status = mContext.ErrorCode == ErrorCodes.NoError ? Definition.JobStatus.Done : Definition.JobStatus.Fail;
            }

            return mContext;
        }

        /// <summary>
        /// 一次自动循环
        /// </summary>
        /// <returns></returns>
        public Mission_Report Run_One_Cycle()
        {
            if (isSimulation)
            {
                var result = MessageBox.Show("因有硬件相关系统设为模拟状态，FSM 会以模拟状态运行\r\n继续按 Yes", "提示", MessageBoxButtons.YesNo);
                if (result == DialogResult.No) { return new Mission_Report { ErrorCode = ErrorCodes.NoError}; }
            }

            Mission_Report mContext = new Mission_Report();
            SolveWare.Core.MMgr.SetStatus(Definition.Machine_Status.Auto);
            string errMsg = string.Empty;
            List<Task> tasks = new List<Task>();

            try
            {              
                Stations.ToList().ForEach(stn =>
                {
                    Task task = Task.Factory.StartNew((object obj) =>
                    {
                        Data_Mission_Report fsm = obj as Data_Mission_Report;
                        fsm.Context = stn.RunSingleCycle();
                    }, new Data_Mission_Report());
                    tasks.Add(task);
                });

                Task.WaitAll(tasks.ToArray());


                tasks.ForEach(task =>
                {
                    var taskReport = task.AsyncState as Data_Mission_Report;
                    mContext.Message += taskReport.Context.Message + "\r\n";
                });
               

            }
            catch (Exception ex)
            {
                mContext.Message += ex.Message;
            }
            finally
            {
                if (string.IsNullOrEmpty(mContext.Message) == false)
                {
                    mContext.ErrorCode = ErrorCodes.FSMRunningFailed;
                    SolveWare.Core.ShowMsg($"任务 执行 失败, 原因如下\r\n{mContext.Message}", true);
                }
                else
                {
                    mContext.ErrorCode = ErrorCodes.NoError;
                    SolveWare.Core.ShowMsg("任务完成");
                }

                this.Status = mContext.ErrorCode == ErrorCodes.NoError ? Definition.JobStatus.Done : Definition.JobStatus.Fail;
            }

            return mContext;
        }

        /// <summary>
        /// 暂停
        /// </summary>
        public void Stop()
        {
            SolveWare.Core.MMgr.SetStatus(Definition.Machine_Status.Stop);
        }

        /// <summary>
        /// 增加工作站
        /// </summary>
        /// <param name="stations"></param>
        public void Add_Station(params IFSMStation[] stations)
        {
            stations.ToList().ForEach(station => { this.Stations.Add(station); });  
        }
    }
}
