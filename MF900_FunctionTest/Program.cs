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

            #region Array 2D Logic 测试
            /*            
                {
                int X = (int)3 / 5;
                int Y = (int)3 % 5;
                Console.WriteLine($"Array[{X},{Y}]");

                Array_2D_Logic logic = new Array_2D_Logic(12, 10);
                Console.ReadKey();

               }
            */
            #endregion
            #region Task 多线程同步等待
            {
                //Task task1 = Task.Run(() =>
                //{
                //    Console.WriteLine( "线程-1");
                //});

                //Task task2 = Task.Run(() =>
                //{
                //    Console.WriteLine("线程-2");
                //});

                //task1.Wait();
                //task2.Wait();

                //Console.WriteLine("结束");
                //Console.ReadKey();
            }
            #endregion
            #region Remaining | Divided Factor 计算 Column 和 Row
            {
                //int target = 41;
                //int totalColumn =20;
                //int index_Col = 1;
                //int index_Row = 0;
                //int factor = 1;

                //for (int i = 1; i < target; i++)
                //{
                //    if ((i % totalColumn) != 0)
                //    {
                //        index_Col += factor;
                //    }
                //    else
                //    {
                //        factor *= -1;
                //        index_Row++;
                //    }
                //}

                //Console.WriteLine($"目标 {target} Column {index_Col} Row {index_Row}");
                //Console.ReadKey();

            }

            {
                //int target = 21;
                //int totalColumn =20;
                //int index_Col = 0;
                //int index_Row = 1;
                //int factor = 1;

                //for (int i = 1; i < target; i++)
                //{
                //    if ((i % totalColumn) != 0)
                //    {
                //        index_Row += factor;
                //    }
                //    else
                //    {
                //        factor *= -1;
                //        index_Col++;
                //    }
                //}

                //Console.WriteLine($"目标 {target} Column {index_Col} Row {index_Row}");
                //Console.ReadKey();
            }
            {
                //target 40

                //int target = 20 -1;
                //int totalColumn =20;
                //int row = target / totalColumn;
                //int col = target % totalColumn;
                //int pitchX = 1;
                //int pitchY = 1;


                ////判断单双
                //bool is_Odd_Row = (row + 1) % 2 == 1;
                //double baseX = 0;
                //double baseY = 0;
                //int displayCol = 0, displayRow = 0;

                //if (is_Odd_Row == false)
                //{
                //    double startBaseX =baseX + (pitchX * (totalColumn - 1));
                //    baseX = startBaseX - (pitchX * col);
                //    baseY = baseY + (pitchY * row);
                //    displayCol = totalColumn - col;
                //    displayRow = row+1;

                //}   
                //else
                //{
                //    baseX = baseX + (pitchX * col);
                //    baseY = (baseY + (pitchY * row));

                //    displayCol = col + 1;
                //    displayRow = row+1;
                //}

                //Console.WriteLine($"Column First 目标 {target+1} PosX {baseX} PosY{baseY}, Column {displayCol} Row {displayRow}");
                //Console.ReadKey();

            }
            {
                // int target = 20 -1;
                // int totalRow = 20;
                // int col = target / totalRow;
                // int row = target % totalRow;
                // int pitchX = 1;
                // int pitchY = 1;
                // double startBaseX = 0;
                // double startBaseY = 0;

                // bool is_Odd_Col = (col + 1) % 2 == 1;
                // startBaseY = is_Odd_Col ? startBaseY : (startBaseY + (pitchY * (totalRow - 1)));

                //double posY = is_Odd_Col ? startBaseY + pitchY * row : startBaseY - pitchY * row;
                //double posX = (startBaseX + col * pitchX);

                // int displayCol = col + 1;
                // int displayRow = row + 1;


                // Console.WriteLine($"Row First 目标 {target + 1} PosX {posX} PosY{posY}, Column {displayCol} Row {displayRow}");
                // Console.ReadKey();
            }
            #endregion
            #region TaskFactory
            Task task = Task.Factory.StartNew(() =>
            {
                Task[] tasks = new Task[10];
                for (int i = 0; i < tasks.Length; i++)
                {
                    int id = i;
                    tasks[i] = Task.Factory.StartNew((object obj) =>
                    {
                        Report_Info info = obj as Report_Info;
                        info.ErrorCode = id;
                        info.ErrorMsg = $"错误讯息 {id}";

                    }, new Report_Info());
                }
                Task.WaitAll(tasks);
                foreach (var item in tasks)
                {
                    var info = item.AsyncState as Report_Info;
                    Console.WriteLine($"顺序 {info.ErrorCode}，讯息 {info.ErrorMsg}");
                }
            });
            Task.WaitAll(task);
            Console.ReadKey();
            #endregion

        }

        public class Report_Info
        {
            public int ErrorCode { get; set; }
            public string ErrorMsg { get; set; }
        }
    }
    
}
