using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Tool.Motor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MF900_SolveWare.Business
{
    public class SafeProtector
    {
        MtrSafe data_Safe;
       public string ErrorMsg { get; protected set; }

        public SafeProtector(MtrSafe dataSafe)
        {
            data_Safe = dataSafe;   
        }
    }
}
