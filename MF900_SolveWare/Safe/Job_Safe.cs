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
        
        public static int Do_Safe_Proection(Data_Safe data, ref string errorMsg)
        {
            int errorCode = ErrorCodes.NoError;
            errorMsg = string.Empty;    
            try
            {
                do
                {
                    if (data.SafeDetailDatas.Count == 0) break;

                    var tempOrders = data.SafeDetailDatas.AsEnumerable().OrderBy(x => x.Priority);
                    if (CheckPriorityOrder(tempOrders.ToList(), ref errorMsg) == false)
                    {
                        errorCode = ErrorCodes.SafetyViolation;
                        break;
                    }


                    int index = 1;
                    int totalDetailCount = data.SafeDetailDatas.Count;

                    while(true)
                    {
                        var detail = data.SafeDetailDatas.FindAll(x => x.Priority == index);
                        if(detail.Count == 0) break;
                        if (errorCode.NotPass(ref errorMsg)) break;

                        if (detail.Count == 1)
                        {
                            errorCode = ExecuteDetailData(detail[0]);
                        }
                        else
                        {
                            List<Task> tasks = new List<Task>();
                            List<int> errors = new List<int>();
                            int id = 0;

                            foreach (var item in detail)
                            {
                                Task task = Task.Run(() =>
                                {
                                    int err = ExecuteDetailData(item);
                                    errors.Add(err);
                                });
                                tasks.Add(task);
                            }

                            Task.WaitAll(tasks.ToArray());

                            int errorFound = errors.FindIndex(x => x != ErrorCodes.NoError);
                            errorCode = errorFound > 0 ? errors[errorFound] : ErrorCodes.NoError;

                            if (errorCode.NotPass(ref errorMsg)) break;
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
                errorMsg += ex.Message;
            }

            return errorCode;
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

        public static int ExecuteDetailData(SafeDetailDataBase data)
        {
            int errorCode = ErrorCodes.NoError;
            if(data is DetailData_Safe_Pos)
            {
                string mtr = (data as DetailData_Safe_Pos).MotorName;
                double pos = (data as DetailData_Safe_Pos).Pos;

                if (mtr.GetUnitPos() > pos)
                {
                    errorCode = MotionHelper.Move_Motor(new Info_Motion { Motor_Name = mtr, Pos = pos });
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
                            if (iO.IsOff()) errorCode = ErrorCodes.IOFunctionError;
                        }
                        else
                        {
                            iO.On();
                            Thread.Sleep(delayTime);
                            if(iO.IsOff()) errorCode = ErrorCodes.IOFunctionError;
                        }
                        break;
                    case ConstantProperty.OFF:
                       iO.Off();
                        if (iOType == ConstantProperty.InPut)
                        {
                            Thread.Sleep(delayTime);
                            if (iO.IsOn()) errorCode = ErrorCodes.IOFunctionError;
                        }
                        else
                        {
                            iO.Off();
                            Thread.Sleep(delayTime);
                            if (iO.IsOn()) errorCode = ErrorCodes.IOFunctionError;
                        }
                        break;

                }

            }

            return errorCode;
        }
    }
}
