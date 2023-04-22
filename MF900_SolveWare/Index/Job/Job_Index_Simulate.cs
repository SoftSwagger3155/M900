using MF900_SolveWare.Index.Data;
using MF900_SolveWare.Offset.Job;
using MF900_SolveWare.Resource;
using MF900_SolveWare.Safe;
using MF900_SolveWare.WorldCenter.Job;
using SolveWare_Service_Core;
using SolveWare_Service_Core.Attributes;
using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.General;
using SolveWare_Service_Tool.Motor.Data;
using SolveWare_Service_Utility.Common;
using SolveWare_Service_Utility.Extension;
using SolveWare_Service_Utility.Index.Base.Interface;
using SolveWare_Service_Vision.MMperPixel.Base.Interface;
using Sunny.UI;
using Sunny.UI.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MF900_SolveWare.Index.Job
{
    //TODO: 杨工 UI / Stanley 实现细节，利用 2D Logic 来实现相关功能
    [ResourceBaseAttribute(ConstantProperty.ResourceKey_Index)]
    public class Job_Index_Simulate : DataJobPairFundamentalBase<Data_Index>, IIndex
    {
        public double PosX_Top { get; protected set; }
        public double PosY_Top { get; protected set; }
        public double PosX_Btm { get; protected set; }
        public double PosY_Btm { get; protected set; }
        public int Current_Row { get; protected set; }
        public int Current_Col { get; protected set; }
        public int Current_No { get; protected set; }

        public Job_Index_Simulate()
        {
            
        }
        public Job_Index_Simulate(string name): base(name) { }
        
        /// <summary>
        /// 安全措施
        /// </summary>
        /// <returns></returns>
        public Mission_Report Do_Save_Prevention()
        {         
            Mission_Report mReport = new Mission_Report();

            try
            {
                Job_Safe.Do_Safe_Proection(this.Data.SafeData);


            }
            catch (Exception ex)
            {
               
            }

            return mReport;
        }

        public Mission_Report Go(int number)
        {
            Mission_Report context = new Mission_Report();
            string errMsg = string.Empty;
            PosX_Top = 0;
            PosY_Top = 0;
            PosX_Btm = 0;
            PosY_Btm = 0;


            try
            {
                do
                {
                    if (Get_Position(number, ref errMsg) == false)
                    {
                        break;
                    }

                    context = Do_Save_Prevention();
                    if (context.NotPass()) break;

                    context = Go_Multiple_PosXY(PosX_Top, PosY_Top, PosX_Btm, PosY_Btm);
                    if (context.NotPass()) break;

                    this.Data.Data_Display.Current_No = number;
     
                } while (false);
            }
            catch (Exception ex)
            {
                context.Set(ErrorCodes.ActionFailed, ex.Message);
            }
       
            return context;
        }
        /// <summary>
        /// 走到指定产品数
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public Mission_Report Go_Offset_Press(int number)
        {
            Mission_Report context = new Mission_Report();
            PosX_Top = 0;
            PosY_Top = 0;
            PosX_Btm = 0;
            PosY_Btm = 0;

            try
            {
                do
                {
                   
                    context = Go(number);
                    if (context.NotPass()) break;
                    Thread.Sleep(1000);

                    context = Go_Offset();
                    if (context.NotPass()) break;
                    Thread.Sleep(1000);


                    context = Go_Multiple_PosZ(15, 15);
                    if (context.NotPass()) break;
                    Thread.Sleep(1000);

                    context = Go_Multiple_PosZ(0, 0);
                    if (context.NotPass()) break;
                    Thread.Sleep(1000);

                } while (false);
            }
            catch (Exception ex)
            {
                context.Set(ErrorCodes.ActionFailed, ex.Message);
            }

            return context;
        }

        /// <summary>
        /// 走到下一个
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Mission_Report GoNext()
        {
            return new Mission_Report();
        }

        /// <summary>
        /// 走到上一个
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Mission_Report GoPrevious()
        {
            return new Mission_Report();
        }

        public void Save_First_Pos()
        {
            string msg = string.Empty;

            try
            {
                do
                {
                    Job_GlobalWorldCenter job = (Job_GlobalWorldCenter)SolveWare.Core.MMgr.Get_PairJob(ResourceKey.GlobalWorldCenter);
                    if (job == null)
                    {
                        msg += "无世界中心物件";
                        break;
                    }

                    double offsetX = ResourceKey.Motor_Top_X.GetUnitPos() - job.Data.Top_WorldCenter_PosX;
                    double offsetY = ResourceKey.Motor_Top_Y.GetUnitPos() - job.Data.Top_WorldCenter_PosY;

                    this.Data.Data_FirstPos.Top_PosX = ResourceKey.Motor_Top_X.GetUnitPos();
                    this.Data.Data_FirstPos.Top_PosY = ResourceKey.Motor_Top_Y.GetUnitPos();
                    this.Data.Data_FirstPos.Top_PosZ = ResourceKey.Motor_Top_Z.GetUnitPos();
                    this.Data.Data_FirstPos.Top_PosT = ResourceKey.Motor_Top_T.GetUnitPos();

                    this.Data.Data_FirstPos.Btm_PosX = job.Data.Btm_WorldCenter_PosX + (offsetX * -1);
                    this.Data.Data_FirstPos.Btm_PosY = job.Data.Btm_WorldCenter_PosY + (offsetY * 1);
                    this.Data.Data_FirstPos.Btm_PosZ = ResourceKey.Motor_Btm_Z.GetUnitPos();
                    this.Data.Data_FirstPos.Btm_PosT = ResourceKey.Motor_Btm_T.GetUnitPos();

                } while (false);

            }
            catch (Exception ex)
            {
                msg += ex.Message;
            }

            SolveWare.Core.ShowMsg(msg);    
        }

        public override Mission_Report Do_Job()
        {
            return new Mission_Report();
        }

        #region 新的index方法 
        public Mission_Report Go_Offset()
        {
            Mission_Report context = new Mission_Report();
            double topPosX = 0;
            double topPosY = 0;
            double btmPosX = 0;
            double btmPosY = 0;

            try
            {
                do
                {
                    topPosX = ResourceKey.Motor_Top_X.GetUnitPos();
                    topPosY = ResourceKey.Motor_Top_Y.GetUnitPos();
                    btmPosX = ResourceKey.Motor_Btm_X.GetUnitPos();
                    btmPosY = ResourceKey.Motor_Btm_Y.GetUnitPos();

                    Job_Offset topOffset = (Job_Offset)SolveWare.Core.MMgr.Get_PairJob(ResourceKey.Offset_Top_Camera_Top_Prober);
                    Job_Offset btmOffset = (Job_Offset)SolveWare.Core.MMgr.Get_PairJob(ResourceKey.Offset_Btm_Camera_Btm_Prober);

                    topPosX += topOffset.Data.OffsetX;
                    topPosY += topOffset.Data.OffsetY;
                    btmPosX += btmOffset.Data.OffsetX;
                    btmPosY += btmOffset.Data.OffsetY;

                    if(Is_Safe_To_Go(topPosX, topPosY, btmPosX, btmPosY))
                    {
                       context = Go_Multiple_PosXY(topPosX, topPosY, btmPosX, btmPosY);
                        if (context.NotPass()) break;
                    }
                    else
                    {
                        context.Set(ErrorCodes.OffsetMoveError, ErrorCodes.GetErrorDescription(ErrorCodes.OffsetMoveError));
                    }

                } while (false);
            }
            catch (Exception ex)
            {
                context.Set(ErrorCodes.MotorMoveError, ex.Message);
            }

            return context;
        }
        public Mission_Report Return_Offset()
        {
          Mission_Report context = new Mission_Report();
            double topPosX = 0;
            double topPosY = 0;
            double btmPosX = 0;
            double btmPosY = 0;

            try
            {
                do
                {
                    topPosX = ResourceKey.Motor_Top_X.GetUnitPos();
                    topPosY = ResourceKey.Motor_Top_Y.GetUnitPos();
                    btmPosX = ResourceKey.Motor_Btm_X.GetUnitPos();
                    btmPosY = ResourceKey.Motor_Btm_Y.GetUnitPos();

                    Job_Offset topOffset = (Job_Offset)SolveWare.Core.MMgr.Get_PairJob(ResourceKey.Offset_Top_Camera_Top_Prober);
                    Job_Offset btmOffset = (Job_Offset)SolveWare.Core.MMgr.Get_PairJob(ResourceKey.Offset_Btm_Camera_Btm_Prober);

                    topPosX -= topOffset.Data.OffsetX;
                    topPosY -= topOffset.Data.OffsetY;
                    btmPosX -= btmOffset.Data.OffsetX;
                    btmPosY -= btmOffset.Data.OffsetY;

                    if (Is_Safe_To_Go(topPosX, topPosY, btmPosX, btmPosY))
                    {
                        context = Go_Multiple_PosXY(topPosX, topPosY, btmPosX, btmPosY);
                        if (context.NotPass()) break;
                    }
                    else
                    {
                        context.Set(ErrorCodes.OffsetMoveError, ErrorCodes.GetErrorDescription(ErrorCodes.OffsetMoveError));
                    }

                } while (false);
            }
            catch (Exception ex)
            {
                context.Set(ErrorCodes.MotorMoveError, ex.Message);
            }

            return context;
        }
        public bool Get_Position(int no, ref string msg)
        {
            bool isOk = false;
            double topPosX = 0;
            double topPosY = 0;
            double btmPosX = 0;
            double btmPosY = 0;

            double offsetX = 0;
            double offsetY = 0;

            do
            {
                //判断是否可以行
                if (no > Data.Data_Setup.Total_Nos_Of_X * Data.Data_Setup.Total_Nos_Of_Y)
                {
                    msg += "超过总数";
                    break;
                }

                Convert_No_To_Matrix(no, ref topPosX, ref topPosY);


                //根剧世界中心位置来换算, 如果是以上模具为主定位
                Job_GlobalWorldCenter job = (Job_GlobalWorldCenter)SolveWare.Core.MMgr.Get_PairJob(ResourceKey.GlobalWorldCenter);
                offsetX = topPosX - job.Data.Top_WorldCenter_PosX;
                offsetY = topPosY - job.Data.Top_WorldCenter_PosY;
                btmPosX = job.Data.Btm_WorldCenter_PosX + (offsetX * -1);
                btmPosY = job.Data.Btm_WorldCenter_PosY + (offsetY * 1);

                isOk = Is_Safe_To_Go(topPosX, topPosY, btmPosX, btmPosY);
                if (isOk)
                {
                    this.PosX_Top = topPosX;
                    this.PosY_Top = topPosY;
                    this.PosX_Btm = btmPosX;
                    this.PosY_Btm = btmPosY;
                }
                else
                {
                    msg += "不安全位置";
                    break;  
                }

            } while (false);

            return isOk;
        }
        public void Convert_No_To_Matrix(int targetNo, ref double posX, ref double posY)
        {
            int target = targetNo - 1;
            int row = 1;
            int col = 1;
            double startBaseX = Data.Data_FirstPos.Top_PosX;
            double startBaseY = Data.Data_FirstPos.Top_PosY;
            double pitchX = Data.Data_Setup.Pitch_X;
            double pitchY = Data.Data_Setup.Pitch_Y;
            
            switch (Data.Data_Setup.Move_Priority)
            {
                case IndexPriority.RowFirst:
                    int totalRow = this.Data.Data_Setup.Total_Nos_Of_Y;
                    col = target / totalRow;
                    row = target % totalRow;

                    bool is_Odd_Col = (col + 1) % 2 == 1;
                    startBaseY = is_Odd_Col ? startBaseY : (startBaseY + (pitchY * (totalRow - 1)));

                    posY = is_Odd_Col ? startBaseY + pitchY * row : startBaseY - pitchY * row;
                    posX = (startBaseX + col * pitchX);

                    Data.Data_Display.Current_X = col + 1;
                    Data.Data_Display.Current_Y = row + 1;
                    break;
                
                case IndexPriority.ColumnFirst:
                    int totalCol = this.Data.Data_Setup.Total_Nos_Of_X;
                    row = target / totalCol;
                    col = target % totalCol;
                    
                    bool is_Odd_Row = (row+1) % 2 == 1;
                    startBaseX = is_Odd_Row ? startBaseX : (startBaseX + (pitchX * (totalCol - 1)));

                    posX = is_Odd_Row ? startBaseX + pitchX * col : startBaseX - pitchX * col;
                    posY = (startBaseY + row * pitchY * -1);

                    Data.Data_Display.Current_X = col + 1;
                    Data.Data_Display.Current_Y = row + 1;
                    Data.Data_Display.Current_No = target + 1;
                    break;
            }
            
        }
        public bool Is_Safe_To_Go(double topPosX, double topPosY, double btmPosX, double btmPosY)
        {
            MtrTable topX = ResourceKey.Motor_Top_X.GetAxisBase().MtrTable;
            MtrTable topY = ResourceKey.Motor_Top_Y.GetAxisBase().MtrTable;
            MtrTable btmX = ResourceKey.Motor_Btm_X.GetAxisBase().MtrTable;
            MtrTable btmY = ResourceKey.Motor_Btm_Y.GetAxisBase().MtrTable;

            bool isDangerous = topPosX < topX.MinDistance_SoftLimit || topPosX > topX.MaxDistance_SoftLimit ||
                                             topPosY < topY.MinDistance_SoftLimit || topPosY > topY.MaxDistance_SoftLimit ||
                                             btmPosX < btmX.MinDistance_SoftLimit || btmPosX > btmX.MaxDistance_SoftLimit ||
                                             btmPosY < btmY.MinDistance_SoftLimit || btmPosY > btmY.MaxDistance_SoftLimit;

            return isDangerous ==false;
        }
        private Mission_Report Go_Multiple_PosXY(double topPosX, double topPosY, double btmPosX, double btmPosY)
        {
            Mission_Report context = new Mission_Report();
            try
            {
                do
                {
                    List<Task> tasks = new List<Task>();
                    List<Info_Motion> jobs = new List<Info_Motion>
                    {
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Top_X, Pos = topPosX },
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Top_Y, Pos = topPosY },
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Btm_X, Pos = btmPosX },
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Btm_Y, Pos = btmPosY }
                    };

                    Info_Motion currentJob;
                    foreach (var item in jobs)
                    {
                        currentJob = item;
                        Task task = Task.Factory.StartNew((object obj) =>
                        {
                            Data_Mission_Report data = obj as Data_Mission_Report;
                            data.Context = MotionHelper.Move_Motor(currentJob);

                        }, new Data_Mission_Report());
                        tasks.Add(task);
                    }
                    Task.WaitAll(tasks.ToArray());
                    context = tasks.Converto_Mission_Report();


                } while (false);
            }
            catch (Exception ex)
            {
                context.ErrorCode = ErrorCodes.ActionFailed;
                context.Message += ex.Message;
            }

            return context;
        }
        private Mission_Report Go_Multiple_PosZ(double posZ1, double posZ2)
        {
            Mission_Report context = new Mission_Report();
            try
            {
                do
                {
                    List<Task> tasks = new List<Task>();
                    List<Info_Motion> jobs = new List<Info_Motion>
                    {
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Top_Z, Pos = posZ1 },
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Btm_Z, Pos = posZ2 }
                    };

                    Info_Motion currentJob;
                    foreach (var job in jobs)
                    {
                        currentJob = job;   
                        Task task = Task.Factory.StartNew((object obj) =>
                        {
                            var data = obj as Data_Mission_Report;
                            data.Context = MotionHelper.Move_Motor(currentJob);

                        }, new Data_Mission_Report());
                    }
                    context = tasks.Converto_Mission_Report();

                } while (false);
            }
            catch (Exception ex)
            {
                context.ErrorCode = ErrorCodes.ActionFailed;
                context.Message = ex.Message;   
            }

            return context;
        }


        #endregion

    }

    public class Array_2D_Logic
    {
        //[X, Y]
        public int[,] formation = null;
        int X = 0;
        int Y = 0;

        int tool_X = 3;
        int tool_Y = 2;
        double pitch = 1.0;
        public Array_2D_Logic(int x, int y)
        {
            this.X = x;
            this.Y = y;

            CreateFormation(x, y);
            MoveToTestPos(10);
        }

        public void Show_Array_Location_Msg(int num)
        {
            //先算是否有超过X数量
            int x = (int)this.X / num;

            //再算Y的位置
            int y = (int)this.X % num;

            Console.WriteLine($"Array[{X},{Y}]");
        }

        public void MoveToTestPos(int num)
        {
            bool found = false;
            int arrX = 0, arrY = 0;
            for (int i = 0; i < formation.GetLength(0); i++)
            {
                for (int j = 0; j < formation.GetLength(1); j++)
                {
                    if (formation[i, j] == num)
                    {
                        found = true;
                        arrX = i;
                        arrY = j;
                        break;
                    }
                }

                if (found) break;
            }


            Console.WriteLine($"Array[{arrX},{arrY}]");

            int xFactor = (int)arrY / tool_X;
            double pitchX = 0;
            double pitchY = 0;


            if ((arrY + 1) % this.X == 0)
            {
                int remainding = num % tool_X;
                double newPtichFactor = tool_X - remainding;

                pitchX = pitch * xFactor * tool_X - pitch * remainding;
            }
            else
            {
                pitchX = pitch * xFactor * tool_X;
            }
            int yFactor = (int)arrX / tool_Y;

            if ((arrX + 1) % this.Y == 0)
            {
                pitchY = pitch * yFactor - pitch / 2;
            }
            else
            {
                pitchY = pitch * yFactor;
            }

            Console.WriteLine($"Pitch [X: {this.pitch} * {xFactor} = {pitchX}, Y:{pitch} * {yFactor} = {pitchY}]");
        }

        public void CreateFormation(int x, int y)
        {
            formation = new int[y, x];

            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    formation[i, j] = (this.X * i) + (j + 1);
                }
            }
        }

    }
}
