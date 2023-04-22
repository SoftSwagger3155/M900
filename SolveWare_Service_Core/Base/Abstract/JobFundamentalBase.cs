using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.Definition;
using SolveWare_Service_Core.General;
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
        protected DateTime st = DateTime.Now;
        protected JobStatus status = JobStatus.Unknown;
        protected Mission_Report context = new Mission_Report();
        public Mission_Report FinalReport
        {
            get => this.context;
            protected set => this.context = value;
        }

        public int Priority
        {
            get => priority;
            set => UpdateProper(ref priority, value);
        }
        public JobStatus Status
        {
            get => status;
            set => UpdateProper(ref status, value);
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

        public virtual Mission_Report Do_Job() { return new Mission_Report(); }
    }
}
