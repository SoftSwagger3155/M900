using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Abstract;
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
        public override int Do_Job()
        {
            OnEntrance();
            try
            {
                errorCode = Execute(Data);
            }
            catch (Exception ex)
            {
                this.errorMsg += ex.Message;
                errorCode = ErrorCodes.MotionFunctionError;
            }
            OnExit();
            return ErrorCode;
        }

        private static int Execute(Data_Motion data)
        {
            int errorCode = ErrorCodes.NoError;
            try
            {
                int index = 1;
                int totalCount = data.DetailDatas.Count;
                while(true)
                {
                    var detail = data.DetailDatas.ToList().FindAll(x => x.Priority == index);
                    if (detail.Count == 0) break;
                    if (SolveWare.Core.MMgr.IsStop) return ErrorCodes.MachineStopCall;

                    if(totalCount ==1)
                    {
                        errorCode = Execute_Single_DetailData(detail[0]);
                    }
                    else
                    {
                        bool isError = false;
                        List<int> errorCodes = new List<int>();
                        List<Task> tasks = new List<Task>();

                        for (int i = 0; i < detail.Count; i++)
                        {
                            int num = i;
                            Task task = new Task(() =>
                            {
                                DetailData_Motion dData= detail[num];
                                errorCode = Execute_Single_DetailData(dData);
                                errorCodes.Add(errorCode);
                            });
                            tasks.Add(task);                            
                        }
                        tasks.ForEach(x => x.Start());
                        Task.WaitAll(tasks.ToArray());
                        isError = errorCodes.FirstOrDefault(x => x != ErrorCodes.NoError) > 0;
                        errorCode = isError ? ErrorCodes.MotionFunctionError : ErrorCodes.NoError;
                    }

                    if (errorCode != ErrorCodes.NoError) break;
                    totalCount-=detail.Count;
                    if (totalCount == 0) break;
                }
            }
            catch (Exception ex)
            {
                errorCode = ErrorCodes.MotionFunctionError;
            }

            return errorCode;
        }

        private static int Execute_Single_DetailData(DetailData_Motion detailData)
        {
            int errorCode = ErrorCodes.NoError;

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
                    errorCode = mtr.MoveTo(detailData.Pos) ? ErrorCodes.NoError : ErrorCodes.NoError;
                }
            }
            catch 
            {
                errorCode = ErrorCodes.MotionFunctionError;
            }

            return errorCode;
        }

    }
}
