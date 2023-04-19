using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Core.Base.Abstract
{
    public abstract class JobFundamentalBase : ElementBase, IJobFundamental
    {
        protected int priority;
        protected int errorCode = 0;
        protected string errorMsg = string.Empty;
        protected DateTime st = DateTime.Now;
        protected JobStatus status = JobStatus.Unknown;


        public int Priority
        {
            get => priority;
            set => UpdateProper(ref priority, value);
        }
        public int ErrorCode
        {
            get => errorCode;
            set=> UpdateProper(ref errorCode, value);
        }

        public JobStatus Status
        {
            get => status;
            set => UpdateProper(ref status, value);
        }

        public string ErrorMsg
        {
            get=> errorMsg;
            set => UpdateProper(ref errorMsg, value);
        }
        protected string info;
        public string Info
        {
            get => info;
            set => UpdateProper(ref info, value);
        }

        protected void LogActionMessage()
        {
            //MainManager.Core.Infohandler.LogActionMessage
        }

        protected void OnEntrance()
        {
            this.st = DateTime.Now;
            this.Status = JobStatus.Entrance;
            this.errorMsg = string.Empty;
            //SolveWare.Core.MMgr.SetStatus(Machine_Status.Busy);
        }
        public void OnExit()
        {
            this.Status = ErrorCode == 0 ? JobStatus.Done : JobStatus.Fail;
            //Machine_Status mStatus = errorCode == 0 ? Machine_Status.Idle : Machine_Status.Error;
            //SolveWare.Core.MMgr.SetStatus(mStatus);
            SolveWare.Core.MMgr.Infohandler.LogActionMessage(this.Name, info, st, errorCode, errorMsg);
        }

        public virtual int Do_Job() { return 0; }
    }
}
