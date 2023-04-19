using MF900_SolveWare.Index.Data;
using MF900_SolveWare.Offset.Job;
using MF900_SolveWare.Resource;
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
        public int Do_Save_Prevention()
        {         
            return 0;
        }

        public int Go(int number)
        {
            int errorCode = ErrorCodes.NoError;
            string errMsg = string.Empty;
            PosX_Top = 0;
            PosY_Top = 0;
            PosX_Btm = 0;
            PosY_Btm = 0;


            try
            {
                do
                {
                    if (Get_Position(number) == false)
                    {
                        errMsg += "获取产品座标位置失败";
                        break;
                    }

                    errorCode = Do_Save_Prevention();
                    if (errorCode.NotPass(ref errMsg)) break;

                    errorCode = Go_Multiple_PosXY(PosX_Top, PosY_Top, PosX_Btm, PosY_Btm);
                    if (errorCode.NotPass(ref errMsg)) break;

                    this.Data.Data_Display.Current_No = number;
     
                } while (false);
            }
            catch (Exception ex)
            {
                errMsg += ex.Message;
            }
       
            return errorCode;
        }
        /// <summary>
        /// 走到指定产品数
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public int Go_Offset_Press(int number)
        {
            int errorCode = ErrorCodes.NoError;
            string errMsg = string.Empty;
            PosX_Top = 0;
            PosY_Top = 0;
            PosX_Btm = 0;
            PosY_Btm = 0;


            try
            {
                do
                {
                   
                    errorCode = Go(number);
                    if (errorCode.NotPass(ref errMsg)) break;
                    Thread.Sleep(1000);

                    errorCode = Go_Offset();
                    if (errorCode.NotPass(ref errMsg)) break;
                    Thread.Sleep(1000);


                    errorCode = Go_Multiple_PosZ(15, 15);
                    if (errorCode.NotPass(ref errMsg)) break;
                    Thread.Sleep(1000);

                    errorCode = Go_Multiple_PosZ(0, 0);
                    if (errorCode.NotPass(ref errMsg)) break;
                    Thread.Sleep(1000);



                } while (false);
            }
            catch (Exception ex)
            {
                errMsg += ex.Message;
            }

            return errorCode;
        }

        /// <summary>
        /// 走到下一个
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int GoNext()
        {
            return 0;
        }

        /// <summary>
        /// 走到上一个
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int GoPrevious()
        {
            return 0;
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

        public override int Do_Job()
        {
            return ErrorCode;
        }

        #region 新的index方法 
        public int Go_Offset()
        {
            errorCode = ErrorCodes.NoError;
            errorMsg = string.Empty;
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
                       errorCode = Go_Multiple_PosXY(topPosX, topPosY, btmPosX, btmPosY);
                        if (errorCode.NotPass(ref errorMsg)) break;
                    }
                    else
                    {
                        errorCode = ErrorCodes.OffsetMoveError;
                    }

                } while (false);
            }
            catch (Exception ex)
            {
                errorCode = ErrorCodes.MotorMoveError;
            }

            return errorCode;
        }
        public int Return_Offset()
        {
            errorCode = ErrorCodes.NoError;
            errorMsg = string.Empty;
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
                        errorCode = Go_Multiple_PosXY(topPosX, topPosY, btmPosX, btmPosY);
                        if (errorCode.NotPass(ref errorMsg)) break;
                    }
                    else
                    {
                        errorCode = ErrorCodes.OffsetMoveError;
                    }

                } while (false);
            }
            catch (Exception ex)
            {
                errorCode = ErrorCodes.MotorMoveError;
            }

            return errorCode;
        }
        public bool Get_Position(int no)
        {
            bool isOk = false;
            errorMsg = string.Empty;
            errorCode = ErrorCodes.NoError;
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
                    errorMsg += "超过总数";
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
        private int Go_Multiple_PosXY(double topPosX, double topPosY, double btmPosX, double btmPosY)
        {
            try
            {
                do
                {
                    string errMsg = string.Empty;
                    int errorCode1 = ErrorCodes.NoError;
                    int errorCode2 = ErrorCodes.NoError;
                    int errorCode3 = ErrorCodes.NoError;
                    int errorCode4 = ErrorCodes.NoError;

                    //List<Task> tasks = new List<Task>();
                    AutoResetEvent endFlag_1 = new AutoResetEvent(false);
                    AutoResetEvent endFlag_2 = new AutoResetEvent(false);
                    AutoResetEvent endFlag_3 = new AutoResetEvent(false);
                    AutoResetEvent endFlag_4 = new AutoResetEvent(false);
                    Task task1 = new Task(() =>
                    {
                        do
                        {
                            errorCode1 = MotionHelper.Move_Motor(new Info_Motion { Motor_Name = ResourceKey.Motor_Top_X, Pos = topPosX });
                            if (errorCode1.NotPass(ref errMsg))
                            {
                                break;
                            }
                            endFlag_1.Set();
                        } while (false);
                    });
                    Task task2 = new Task(() =>
                    {
                        do
                        {
                          
                            errorCode2 = MotionHelper.Move_Motor(new Info_Motion { Motor_Name = ResourceKey.Motor_Top_Y, Pos = topPosY });
                            if (errorCode2.NotPass(ref errMsg))
                            {
                                break;
                            }
                            endFlag_2.Set();
                        } while (false);
                    });
                    Task task3 = new Task(() =>
                    {
                        do
                        {
                            errorCode3 = MotionHelper.Move_Motor(new Info_Motion { Motor_Name = ResourceKey.Motor_Btm_X, Pos = btmPosX });
                            if (errorCode3.NotPass(ref errMsg))
                            {
                                break;
                            }
                            endFlag_3.Set();

                        } while (false);
                    });
                    Task task4 = new Task(() =>
                    {
                        do
                        {
                            errorCode4 = MotionHelper.Move_Motor(new Info_Motion { Motor_Name = ResourceKey.Motor_Btm_Y, Pos = btmPosY });
                            if (errorCode4.NotPass(ref errMsg))
                            {
                                break;
                            }
                            endFlag_4.Set();

                        } while (false);
                    });
                    task1.Start();
                    task2.Start();
                    task3.Start();
                    task4.Start();
                    Stopwatch sw = Stopwatch.StartNew();
                    sw.Restart();
                    bool isTimeOut = false;
                    bool isOk = false;
                    while (true)
                    {

                        isOk = endFlag_1.WaitOne() && endFlag_2.WaitOne() && endFlag_3.WaitOne() && endFlag_4.WaitOne();
                        if (isOk)
                            break;

                        if (sw.ElapsedMilliseconds > 600000)
                        {
                            isTimeOut = true;
                            break;
                        }
                    }

                    if (isTimeOut || errorCode1 != ErrorCodes.NoError || errorCode2 != ErrorCodes.NoError || errorCode3 != ErrorCodes.NoError || errorCode4 != ErrorCodes.NoError)
                    {
                        errorCode = ErrorCodes.ActionFailed;
                        break;
                    }

                } while (false);
            }
            catch (Exception ex)
            {
                errorCode = ErrorCodes.ActionFailed;
            }

            return errorCode;
        }
        private int Go_Multiple_PosZ(double posZ1, double posZ2)
        {
            try
            {
                do
                {
                    string errMsg = string.Empty;
                    int errorCode1 = ErrorCodes.NoError;
                    int errorCode2 = ErrorCodes.NoError;

                    //List<Task> tasks = new List<Task>();
                    AutoResetEvent endFlag_1 = new AutoResetEvent(false);
                    AutoResetEvent endFlag_2 = new AutoResetEvent(false);
                    Task task1 = new Task(() =>
                    {
                        do
                        {
                            errorCode1 = MotionHelper.Move_Motor(new Info_Motion { Motor_Name = ResourceKey.Motor_Top_Z, Pos = posZ1 });
                            if (errorCode1.NotPass(ref errMsg))
                            {
                                endFlag_1.Set();
                                break;
                            }
                            endFlag_1.Set();
                        } while (false);
                    });
                    Task task2 = new Task(() =>
                    {
                        do
                        {
                            errorCode2 = MotionHelper.Move_Motor(new Info_Motion { Motor_Name = ResourceKey.Motor_Btm_Z, Pos = posZ2 });
                            if (errorCode2.NotPass(ref errMsg))
                            {
                                endFlag_2.Set();
                                break;
                            }
                            endFlag_2.Set();

                        } while (false);
                    });
                    task1.Start();
                    task2.Start();
                    Stopwatch sw = Stopwatch.StartNew();
                    sw.Restart();
                    bool isTimeOut = false;
                    bool isOk = false;
                    while (true)
                    {

                        isOk = endFlag_1.WaitOne() && endFlag_2.WaitOne();
                        if (isOk)
                            break;

                        if (sw.ElapsedMilliseconds > 600000)
                        {
                            isTimeOut = true;
                            break;
                        }
                    }

                    if (isTimeOut || errorCode1 != ErrorCodes.NoError || errorCode2 != ErrorCodes.NoError)
                    {
                        errorCode = ErrorCodes.ActionFailed;
                        break;
                    }

                } while (false);
            }
            catch (Exception ex)
            {
                errorCode = ErrorCodes.ActionFailed;
            }

            return errorCode;
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
