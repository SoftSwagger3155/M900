using MF900_SolveWare.Index.Data;
using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.General;
using SolveWare_Service_Utility.Index.Base.Interface;
using SolveWare_Service_Vision.MMperPixel.Base.Interface;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF900_SolveWare.Index.Job
{
    //TODO: 杨工 UI / Stanley 实现细节，利用 2D Logic 来实现相关功能
    public class Job_Index : DataJobPairFundamentalBase<Data_Index>, IIndex
    {
        /// <summary>
        /// 安全措施
        /// </summary>
        /// <returns></returns>
        public int Do_Save_Prevention()
        {         
            return 0;
        }

        /// <summary>
        /// 走到指定产品数
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public int Go(int number)
        {
            int errorCode = ErrorCodes.NoError;
            string errMsg = string.Empty;
            double posX =0, posY = 0;
            try
            {
                do
                {
                   if(GetPosition(number, ref posX, ref posY) == false)
                    {
                        errMsg += "获取产品座标位置失败";
                        break;
                    }

                    errorCode = Do_Save_Prevention();
                    if(errorCode != ErrorCodes.NoError)
                    {
                        errorMsg += ErrorCodes.GetErrorDescription(errorCode);
                        break;
                    }

                     

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


        public override int Do_Job()
        {
            return ErrorCode;
        }


        #region 本地方法
        private bool GetPosition(int number, ref double posX, ref double posY)
        {
            int col = 0, row = 0;

            if (ConvertToRowAndColumn(number, Data.Total_Column, Data.Total_Row, ref col, ref row) == false)
                return false;

            if(GetPosition(row, col, Data.BasePoint.X, Data.BasePoint.Y, Data.MoveGap_Row_Y, Data.MoveGap_Column_X, 0, 0, ref posX ,ref posY) == false)
                return false;
            return true;
        }

        private bool GetPosition(int row, int col, double basePosX, double basePosY, double rowPitch, double colPitch, double colOffset, double rowOffset, ref double posX, ref double posY)
        {
            if (rowPitch == 0 || colPitch == 0) return false;

            posY = (col - 1) * colPitch + colOffset;
            posX = (row - 1) * rowPitch + rowOffset;
            posX += basePosX;
            posY += basePosY;

            return true;
        }

        private bool IsInValidRange(int row, int col, int totalRow, int totalCol)
        {
            if (col < 1 || col > totalCol) return false;
            if (row < 1 || row > totalRow) return false;

            return true;
        }
        private bool IndexNext(int row, int col, int totalRow, int totalCol, ref int nextRow, ref int nextCol)
        {
            bool haveNextIndex = false;
            if (col >= totalCol && row >= totalRow) return false;

            col++;

            if (col >= totalCol)
            {
                col = 1;
                row++;
            }

            nextRow = row;
            nextCol = col;

            haveNextIndex = (row != 1 || col != 1);

            return haveNextIndex;
        }
        private bool IndexPrevious(int row, int col, int totalRow, int totalCol, ref int nextRow, ref int nextCol)
        {
            bool haveNextIndex = false;

            if (row <= 1 || col <= 1) return false;
            col--;

            if (col <= 1)
            {
                col = totalCol;
                row--;
            }

            nextRow = row;
            nextCol = col;
            haveNextIndex = (row != 0 && col != 0);

            return haveNextIndex;
        }
        private bool ConvertToRowAndColumn(int noOfUnit, int totalCol, int totalRow, ref int col, ref int row)
        {
            if (noOfUnit > (totalCol * totalRow)) return false;

            //TotalCol: 10, TotalRow: 3,  NoOfUnit:11 => 11 % 10 =1;
            int reminding = noOfUnit % totalCol;
            if (reminding == 0)
                col = totalCol;
            else
                col = reminding;



            int temp = (int)Math.Ceiling((double)noOfUnit / totalCol);
            row = temp;

            return true;
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
