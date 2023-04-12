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
                List<int> errors = new List<int>();
                List<Task> tasks = new List<Task>();

                foreach (var info in motions.ToList())
                {
                    Task task = Task.Run(() =>
                    {
                        int err = info.Motor_Name.GetAxisBase().MoveTo(info.Pos) ? ErrorCodes.NoError : ErrorCodes.MotionFunctionError;
                        errors.Add(err);
                    });
                    tasks.Add(task);
                }

                Task.WaitAll(tasks.ToArray());

                int foundError = errors.FindIndex(x => x != ErrorCodes.NoError);
                errorCode = foundError >= 0 ? ErrorCodes.MotionFunctionError : ErrorCodes.NoError;
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
                List<int> errors = new List<int>();
                List<Task> tasks = new List<Task>();

                Task task = new Task(() =>
                {
                    int err = motion.Motor_Name.GetAxisBase().MoveTo(motion.Pos) ? ErrorCodes.NoError : ErrorCodes.MotionFunctionError;
                    errors.Add(err);
                });

                tasks.Add(task);
                tasks.ForEach(x => { x.Start(); });
                Task.Factory.ContinueWhenAll(tasks.ToArray(), act =>
                {
                    int foundError = errors.FirstOrDefault(x => x != ErrorCodes.NoError);
                    errorCode = foundError >= 0 ? ErrorCodes.MotionFunctionError : ErrorCodes.NoError;
                });

            }
            catch
            {
                errorCode = ErrorCodes.MotorMoveError;
            }

            return errorCode;
        }
    }
    public class Info_Motion
    {
        public string Motor_Name { get; set; }
        public double Pos { get; set; }
    }
}
