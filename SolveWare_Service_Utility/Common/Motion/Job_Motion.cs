using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.Definition;
using SolveWare_Service_Core.General;
using SolveWare_Service_Tool.Motor.Base.Abstract;
using SolveWare_Service_Utility.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Utility.Common.Motion
{
    public class Job_Motion: DataJobPairFundamentalBase<Data_Motion>
    {
        public Job_Motion(string name) : base(name)
        {
            this.Name = name;
        }
        public override Mission_Report Do_Job()
        {
            Mission_Report context = new Mission_Report();
            try
            {
                context = Execute(Data);
            }
            catch (Exception ex)
            {
                context.Set(ErrorCodes.MotionFunctionError, ex.Message);
            }
            return context;
        }
        private static Mission_Report Execute(Data_Motion data)
        {
            Mission_Report context = new Mission_Report();
            try
            {
                int index = 1;
                int totalCount = data.DetailDatas.Count;
                while(true)
                {
                    var detail = data.DetailDatas.ToList().FindAll(x => x.Priority == index);
                    if (detail.Count == 0) break;
                    if (context.NotPass()) break;

                    if(totalCount ==1)
                    {
                        context = Execute_Single_DetailData(detail[0]);
                    }
                    else
                    {                       
                        List<int> errorCodes = new List<int>();
                        List<Task> tasks = new List<Task>();

                        for (int i = 0; i < detail.Count; i++)
                        {
                            int num = i;
                            DetailData_Motion dData = detail[num];
                            Task task = Task.Factory.StartNew((object obj) =>
                            {
                                Data_Mission_Report mReport  = obj as Data_Mission_Report; 
                                mReport.Context = Execute_Single_DetailData(dData);                               
                            }, new Data_Mission_Report());

                            tasks.Add(task);                            
                        }
                        Task.WaitAll(tasks.ToArray());
                        context = tasks.Converto_Mission_Report();
                    }

                    if(context.NotPass()) break;    
                    totalCount-=detail.Count;
                    if (totalCount == 0) break;
                }
            }
            catch (Exception ex)
            {
                context.Set(ErrorCodes.MotionFunctionError, ex.Message);
            }

            return context;
        }
        private static Mission_Report Execute_Single_DetailData(DetailData_Motion detailData)
        {
            Mission_Report context = new Mission_Report();

            try
            {             
                if(detailData.EnableSlowDown)
                {
                    switch (detailData.SlowDownType)
                    {
                        case ConstantProperty.AllTheWay:
                            break;
                        case ConstantProperty.Start_HalfWay:
                            break;
                        case ConstantProperty.End_HalfWay:
                            break;
                    }
                }
                else
                {
                    AxisBase mtr = detailData.AxisName.GetAxisBase();
                    context = mtr.MoveTo(detailData.Pos);
                }
            }
            catch(Exception ex)
            {
                context.Set( ErrorCodes.MotionFunctionError, ex.Message);
            }

            return context;
        }

    }
}
