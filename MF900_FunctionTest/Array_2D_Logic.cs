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


            if ((arrY +1) % this.X == 0)
            {
                int remainding = num % tool_X;
                double newPtichFactor = tool_X - remainding;

                pitchX = pitch * xFactor* tool_X - pitch * remainding;
            }
            else
            {
                 pitchX = pitch * xFactor* tool_X;
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
