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
            private set=> UpdateProper(ref errorCode, value);
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

        protected void LogActionMessage()
        {
            //MainManager.Core.Infohandler.LogActionMessage
        }

        protected void OnEntrance()
        {
            this.st = DateTime.Now;
            this.Status = JobStatus.Entrance;
        }
        public void OnExit()
        {
            this.Status = ErrorCode == 0 ? JobStatus.Done : JobStatus.Fail;
            SolveWare.Core.MMgr.Infohandler.LogActionMessage(this.Name, this.GetType().Name, st, errorCode, errorMsg);
        }

        public virtual int Do_Job() { return 0; }
    }
}
