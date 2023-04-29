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
        public static Mission_Report Move_Multiple_Motors(params Info_Motion[] motions)
        {
            Mission_Report context = new Mission_Report();
            try
            {
                do
                {
                    List<Task> tasks = new List<Task>(); ;
                    foreach (var info in motions.ToList())
                    {
                        Task task = new Task((object obj) =>
                        {
                            Data_Mission_Report data = obj as Data_Mission_Report;
                            data.Context = MotionHelper.Move_Motor(info);

                        }, new Data_Mission_Report());
                        tasks.Add(task);
                    }

                    tasks.ForEach(x => x.Start());
                    Task.WaitAll(tasks.ToArray());
                    context = tasks.Converto_Mission_Report();

                } while (false);

            }
            catch (Exception ex)
            {
                context.Set(ErrorCodes.MotorMoveError, ex.Message);
            }

            return context;
        }
        public static Mission_Report Move_Multiple_Motors(double velPct, params Info_Motion[] motions)
        {
            Mission_Report context = new Mission_Report();
            try
            {
                do
                {
                    List<Task> tasks = new List<Task>(); ;
                    foreach (var info in motions.ToList())
                    {
                        Task task = new Task((object obj) =>
                        {
                            Data_Mission_Report data = obj as Data_Mission_Report;
                            data.Context = MotionHelper.Move_Motor(info, velPct);

                        }, new Data_Mission_Report());
                        tasks.Add(task);
                    }

                    tasks.ForEach(x => x.Start());
                    Task.WaitAll(tasks.ToArray());
                    context = tasks.Converto_Mission_Report();

                } while (false);

            }
            catch (Exception ex)
            {
                context.Set(ErrorCodes.MotorMoveError, ex.Message);
            }

            return context;
        }
        public static Mission_Report Move_Motor(Info_Motion motion, double velPct)
        {
            Mission_Report context = new Mission_Report();
            try
            {
                context = motion.Motor_Name.GetAxisBase().MoveTo(motion.Pos, velPct);
            }
            catch (Exception ex)
            {
                context.Set(ErrorCodes.MotorMoveError, ex.Message);
            }

            return context;
        }
        public static Mission_Report Move_Motor(Info_Motion motion)
        {
            Mission_Report context = new Mission_Report();
            try
            {
                context = motion.Motor_Name.GetAxisBase().MoveTo(motion.Pos);
            }
            catch (Exception ex)
            {
                context.Set(ErrorCodes.MotorMoveError, ex.Message);
            }

            return context;
        }
    }
    public struct Info_Motion
    {
        public string Motor_Name { get; set; }
        public double Pos { get; set; }
    }
}
