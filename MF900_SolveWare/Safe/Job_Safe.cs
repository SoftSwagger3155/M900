using SolveWare_Service_Core;
using SolveWare_Service_Core.General;
using SolveWare_Service_Tool.IO.Base.Abstract;
using SolveWare_Service_Tool.MasterDriver.Business;
using SolveWare_Service_Tool.Motor.Base.Abstract;
using SolveWare_Service_Utility.Common;
using SolveWare_Service_Utility.Extension;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.ModelBinding;

namespace MF900_SolveWare.Safe
{
    public class Job_Safe
    {
        
        public static Mission_Report Do_Safe_Proection(Data_Safe data)
        {
            Mission_Report mContext = new Mission_Report();
            string errorMsg = string.Empty;
            try
            {
                do
                {
                    if (data.SafeDetailDatas.Count == 0) break;

                    var tempOrders = data.SafeDetailDatas.AsEnumerable().OrderBy(x => x.Priority);
                    if (CheckPriorityOrder(tempOrders.ToList(), ref errorMsg) == false)
                    {
                        mContext.ErrorCode = ErrorCodes.SafetyViolation;
                        mContext.Message += errorMsg;
                        break;
                    }


                    int index = 1;
                    int totalDetailCount = data.SafeDetailDatas.Count;

                    while(true)
                    {
                        var detail = data.SafeDetailDatas.FindAll(x => x.Priority == index);
                        if(detail.Count == 0) break;
                        if (mContext.NotPass()) break;

                        if (detail.Count == 1)
                        {
                            mContext = ExecuteDetailData(detail[0]);
                        }
                        else
                        {
                            List<Task> tasks = new List<Task>();
                            List<int> errors = new List<int>();
                            int id = 0;

                            foreach (var item in detail)
                            {
                                Task task = new Task((object obj) =>
                                {
                                    Data_Mission_Report report = obj as Data_Mission_Report;
                                    report.Context = ExecuteDetailData(item);

                                }, new Data_Mission_Report());
                                tasks.Add(task);
                            }

                            tasks.ForEach(x=> x.Start());   
                            Task.WaitAll(tasks.ToArray());

                            mContext = tasks.Converto_Mission_Report();
                            if(mContext.NotPass()) break;
                        }

                        totalDetailCount -= detail.Count;
                        if (totalDetailCount == 0) break;

                        index++;

                        if ((SolveWare.Core.MMgr.MasterDriver as MasterDriverManager).IsSimulation) Thread.Sleep(1000);
                    }


                } while (false);
            }
            catch (Exception ex)
            {
                mContext.ErrorCode = ErrorCodes.ActionFailed;
                mContext.Message += ex.Message;
            }

            return mContext;
        }

        public static Mission_Report Is_Task_Worked_Correctly(Task[] tasks)
        {
            Mission_Report context= new Mission_Report();
            if (tasks.Length == 0) return context;
            foreach (var task in tasks)
            {
                var report = task.AsyncState as Data_Mission_Report;
                if (report.Context.ErrorCode != ErrorCodes.NoError)
                {
                    context = report.Context;
                    break;
                }
            }

           return context;

        }

        public static bool CheckPriorityOrder(List<SafeDetailDataBase> details, ref string errorMsg)
        {
            bool isPass = false;
            List<int> checkList = new List<int>();
            details.ForEach(x => checkList.Add(x.Priority));

            int baseNo = 1;
            int noOfObj = checkList.Count;
            while (true)
            {
                var num = checkList.FindAll(x => x == baseNo);
                if (noOfObj > 0 && num.Count == 0)
                {
                    isPass = false;
                    break;
                }
                noOfObj -= num.Count;
                if (noOfObj == 0)
                {
                    isPass = true;
                    break;
                }
                baseNo++;
            }

            if(!isPass) { errorMsg += "顺序安排错误"; }
            return isPass;
        }
        public static void SortDetailDatas(Data_Safe data)
        {
            var tempOrders = data.SafeDetailDatas.AsEnumerable().OrderBy(x => x.Priority);
            var temps = tempOrders.ToList();
            data.SafeDetailDatas.Clear();
            data.SafeDetailDatas.AddRange(temps);          
         }

        public static Mission_Report ExecuteDetailData(SafeDetailDataBase data)
        {
            Mission_Report context= new Mission_Report();
            if(data is DetailData_Safe_Pos)
            {
                string mtr = (data as DetailData_Safe_Pos).MotorName;
                double pos = (data as DetailData_Safe_Pos).Pos;

                if (mtr.GetUnitPos() > pos)
                {
                    context = MotionHelper.Move_Motor(new Info_Motion { Motor_Name = mtr, Pos = pos });
                }
            }
            else
            {
                IOBase iO = (data as DetailData_Safe_IO).IOName.GetIOBase();
                string iOType = (data as DetailData_Safe_IO).IOType;
                string triggerMode = (data as DetailData_Safe_IO).TriggerMode;
                int delayTime =(data as DetailData_Safe_IO).DelayTime;

                switch(triggerMode)
                {
                    case ConstantProperty.ON:
                        if(iOType == ConstantProperty.InPut) {
                            Thread.Sleep(delayTime);
                            if (iO.IsOff())
                            {
                                context.ErrorCode = ErrorCodes.IOFunctionError;
                                context.Message = $"IO: {iO.Name} {ErrorCodes.GetErrorDescription(ErrorCodes.IOFunctionError)}";
                            }
                        }
                        else
                        {
                            iO.On();
                            Thread.Sleep(delayTime);
                            if(iO.IsOff())
                            {
                                context.ErrorCode = ErrorCodes.IOFunctionError;
                                context.Message = $"IO: {iO.Name} {ErrorCodes.GetErrorDescription(ErrorCodes.IOFunctionError)}";
                            }
                        }
                        break;
                    case ConstantProperty.OFF:
                       iO.Off();
                        if (iOType == ConstantProperty.InPut)
                        {
                            Thread.Sleep(delayTime);
                            {
                                context.ErrorCode = ErrorCodes.IOFunctionError;
                                context.Message = $"IO: {iO.Name} {ErrorCodes.GetErrorDescription(ErrorCodes.IOFunctionError)}";
                            }
                        }
                        else
                        {
                            iO.Off();
                            Thread.Sleep(delayTime);
                            {
                                context.ErrorCode = ErrorCodes.IOFunctionError;
                                context.Message = $"IO: {iO.Name} {ErrorCodes.GetErrorDescription(ErrorCodes.IOFunctionError)}";
                            }
                        }
                        break;

                }

            }

            return context;
        }
    }
}
