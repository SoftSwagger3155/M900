using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF900_FunctionTest
{
    public class Array_2D_Logic
    {
        //[X, Y]
        public int[,] formation = null;
        int X = 0;
        int Y = 0;

        int tool_X = 3;
        int tool_Y = 2;
        double pitch = 2.0;
        public Array_2D_Logic(int x, int y)
        {
            this.X = x;
            this.Y = y;
            CreateFormation(x, y);
            MoveToTestPos(5);
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
            ////先算是否有超过X数量
            //int x = (int)this.Y / num;
            //x -= 1;

            ////再算Y的位置
            //int y = (int)this.X % num;
            //y -= 1;
            ////计算X Pitch
            ///
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


            if ((arrY +1) % this.X == 0)
            {
                pitchX = pitch * xFactor - pitch / 2;
            }
            else
            {
                 pitchX = pitch * xFactor;
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

            Console.WriteLine($"Pitch [X:{pitchX}, Y:{pitchY}]");

            //计算Y Pitch
        }

        public void CreateFormation(int x, int y)
        {
            formation = new int[x, y];

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    formation[i, j] = (x * i) + (j + 1);
                }
            }
        }

    }
}
