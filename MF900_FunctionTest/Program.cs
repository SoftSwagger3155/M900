﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF900_FunctionTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int X = (int)3 / 5;
            int Y = (int)3 % 5;
            Console.WriteLine($"Array[{X},{Y}]");

            Array_2D_Logic logic = new Array_2D_Logic(12, 10);
            Console.ReadKey();
        }
    }
}