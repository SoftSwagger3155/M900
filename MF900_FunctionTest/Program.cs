using MF900_FunctionTest.DataMairPairTest;
using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Utility.Extension;
using System;
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

            /*            
                {
                int X = (int)3 / 5;
                int Y = (int)3 % 5;
                Console.WriteLine($"Array[{X},{Y}]");

                Array_2D_Logic logic = new Array_2D_Logic(12, 10);
                Console.ReadKey();

               }
            */
            {
                Task task1 = Task.Run(() =>
                {
                    Console.WriteLine( "线程-1");
                });

                Task task2 = Task.Run(() =>
                {
                    Console.WriteLine("线程-2");
                });
              
                task1.Wait();
                task2.Wait();

                Console.WriteLine("结束");
                Console.ReadKey();
            }
        }
    }

    
}
