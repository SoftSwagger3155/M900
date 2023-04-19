using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.General;
using SolveWare_Service_Utility.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Utility.Common
{
    public class MotionHelper
    { 
        public static int Move_Multiple_Motors(params Info_Motion[] motions)
        {
            int errorCode = ErrorCodes.NoError;
            try
            {
                do
                {
                    List<Report_Info> infos = new List<Report_Info>();
                    List<Task> tasks = new List<Task>();

                    foreach (var info in motions.ToList())
                    {
                        Task task = Task.Run(() =>
                        {
                            int err = info.Motor_Name.GetAxisBase().MoveTo(info.Pos) ? ErrorCodes.NoError : ErrorCodes.MotionFunctionError;
                            infos.Add(new Report_Info { ErrorCode = err, ErrorMsg = $"{ErrorCodes.GetErrorDescription(err)} +{info.Motor_Name.GetAxisBase().ErrorReport}" });
                        });
                        tasks.Add(task);
                    }
                 
                    Task.Factory.ContinueWhenAll(tasks.ToArray(), act =>
                    {
                        string msg = string.Empty;
                        infos.ForEach(x =>
                        {
                            if (x.ErrorCode != ErrorCodes.NoError)
                                msg += x.ErrorMsg + "\r\n";
                        });

                        if(msg != string.Empty)
                        {
                            SolveWare.Core.ShowMsg(msg);
                        }
                    });

                } while (false);

            }
            catch 
            {
                errorCode = ErrorCodes.MotorMoveError;
            }

            return errorCode;
        }
        public static int Move_Motor(Info_Motion motion)
        {
            int errorCode = ErrorCodes.NoError;
            try
            {
                errorCode = motion.Motor_Name.GetAxisBase().MoveTo(motion.Pos) ? ErrorCodes.NoError : ErrorCodes.MotionFunctionError;
            }
            catch
            {
                errorCode = ErrorCodes.MotorMoveError;
            }

            return errorCode;
        }
    }
    public struct Info_Motion
    {
        public string Motor_Name { get; set; }
        public double Pos { get; set; }
    }
}
