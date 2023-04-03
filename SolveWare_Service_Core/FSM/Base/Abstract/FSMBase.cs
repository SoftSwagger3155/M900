using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.FSM.Base.Interface;
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

        public void Build_Resource()
        {
            return;
        }

        public int Run_Auto_Cycle()
        {
            return 0;
        }

        public int Run_One_Cycle()
        {

            //List<Task> tasks = new List<Task>();
           
            //foreach (IFSMStation station in this.Stations)
            //{
            //    station.States.ToList().ForEach(x => x.IsSimulation = false);
            //    station.States.ToList().ForEach(x => x.RunningState = StateRunningStatus.Idle);
            //    station.States.ToList().ForEach(x => x.ResultStatus = StateResultStatus.Unknown);
            //    station.SetCurrentState(station.FirstState);
            //}

            //foreach (IFSMStationBase station in this.FSMStations)
            //{
            //    tasks.Add(Task.Run(() =>
            //    {
            //        station.RunStateChain(FSMRunMode.OneRun);
            //    }));
            //}
            //Task.Factory.ContinueWhenAny(tasks.ToArray(), act =>
            //{

            //});
            //Task.Factory.ContinueWhenAll(tasks.ToArray(), act =>
            //{
            //    do
            //    {
            //        switch (Manage.Core.MMgr.Status)
            //        {
            //            case MachineStatusType.UnInitialised:
            //                break;
            //            case MachineStatusType.Initialising:
            //                Manage.Core.MMgr.SetStatus(MachineStatusType.Idle);
            //                Manage.Core.MMgr.Infohandler.LogMessage("复位成功", true);
            //                break;
            //            case MachineStatusType.Idle:
            //                break;
            //            case MachineStatusType.Auto:
            //                break;
            //            case MachineStatusType.Busy:
            //                break;
            //            case MachineStatusType.Error:
            //                //Manage.Control.MMgr.SetStatus(MachineStatusType.Error);
            //                Manage.Core.MMgr.Infohandler.LogMessage("复位失败", true);
            //                break;
            //            case MachineStatusType.Stop:
            //                Manage.Core.MMgr.Infohandler.LogMessage("复位执行未完成", true);
            //                break;
            //            case MachineStatusType.Reset:
            //                break;
            //            default:
            //                break;
            //        }

                    //if (Manage.Control.MMgr.Status == MachineStatusType.Stop) break;


                    ////bool isSuccessFul = true;
                    ////foreach (var station in this.FSMStations)
                    ////{
                    ////    if (station.ErrorCode == ErrorCodes.NoError) continue;
                    ////    isSuccessFul = false;
                    ////    break;
                    ////}
                    //if (isSuccessFul)
                    //{
                    //    MachineStatusType status = isSuccessFul ? MachineStatusType.Idle : MachineStatusType.Error;

                    //}
                    //else
                    //{

                    //}

            ////    } while (false);
            ////});
            
            return 0;
        }

        public void Stop()
        {
            SolveWare.Core.MMgr.SetStatus(Definition.Machine_Status.Stop);
        }
    }
}
