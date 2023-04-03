using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.FSM.Base.Interface;
using SolveWare_Service_Core.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Core.FSM.Base.Abstract
{
    public abstract class FSMBase : JobFundamentalBase, IFSM
    {

        public IList<IFSMStation> Stations { get; set; }

        public FSMBase()
        {
            Stations = new List<IFSMStation>();
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
        public int Run_Auto_Cycle()
        {
            OnEntrance();
            int errorCode = ErrorCodes.NoError;
            string errMsg = string.Empty;

            try
            {
                List<int> errors = new List<int>();
                List<Task> tasks = new List<Task>();

                Stations.ToList().ForEach(stn =>
                {
                    Task task = new Task(() =>
                    {
                        int err = stn.RunAutoCycle();
                        errors.Add(err);
                    });
                    tasks.Add(task);
                });

                tasks.ForEach(task => { task.Start(); });
                Task.Factory.ContinueWhenAll(tasks.ToArray(), act =>
                {
                    int index = errors.FirstOrDefault(x => x != ErrorCodes.NoError);
                    errorCode = index >= 0 ? ErrorCodes.CyclingFailed : ErrorCodes.NoError;

                    errMsg += ErrorCodes.GetErrorDescription(errorCode);
                });


            }
            catch (Exception ex)
            {
                errMsg += ex.Message;
            }

            Get_Result(nameof(this.Run_One_Cycle), errMsg);
            OnExit();
            return errorCode;
        }

        /// <summary>
        /// 一次自动循环
        /// </summary>
        /// <returns></returns>
        public int Run_One_Cycle()
        {
            SolveWare.Core.MMgr.SetStatus(Definition.Machine_Status.Auto);
            OnEntrance();
            int errorCode = ErrorCodes.NoError;
            string errMsg = string.Empty;

            try
            {
                List<int> errors = new List<int>();
                List<Task> tasks = new List<Task>();

                Stations.ToList().ForEach(stn =>
                {
                    Task task = new Task(() =>
                    {
                        int err = stn.RunSingleCycle();
                        errors.Add(err);
                    });
                    tasks.Add(task);
                });

                tasks.ForEach(task => { task.Start(); });
                Task.Factory.ContinueWhenAll(tasks.ToArray(), act =>
                {
                    int index = errors.FirstOrDefault(x => x != ErrorCodes.NoError);
                    errorCode = index >= 0 ? ErrorCodes.CyclingFailed : ErrorCodes.NoError;

                    errMsg += ErrorCodes.GetErrorDescription(errorCode);
                });


            }
            catch (Exception ex)
            {
                errMsg += ex.Message;
            }

            Get_Result(nameof(this.Run_One_Cycle), errMsg);
            OnExit();
            return errorCode;
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
