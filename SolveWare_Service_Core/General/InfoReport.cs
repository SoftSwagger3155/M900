using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Core.General
{
    public struct Mission_Report
    {
        public int ErrorCode { get; set; } 
        public string Message { get; set; } 
        public void Set(int errorCode, string msg)
        {
            this.ErrorCode = errorCode;
            this.Message = msg;
        }
        public void Set(int errorCode)
        {
            this.ErrorCode = errorCode;
            this.Message = ErrorCodes.GetErrorDescription(errorCode);
        }
    }
    public class Data_Mission_Report
    {
        public Data_Mission_Report()
        {
            
        }
        public Mission_Report Context = new Mission_Report();
    }
}
