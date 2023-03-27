using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.Manager.Base.Interface;
using SolveWare_Service_Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolveWare_Service_Core.General;
using SolveWare_Service_Utility.Common.Motion;
using SolveWare_Service_Tool.Motor.Base.Abstract;
using SolveWare_Service_Utility.Extension;
using SolveWare_Service_Tool.IO.Definition;
using SolveWare_Service_Tool.IO.Base.Interface;
using System.Threading;

namespace SolveWare_Service_Utility.Common.IO
{
    public class Job_IO : DataJobPairFundamentalBase<Data_IO>
    {
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

        private static int Execute(Data_IO data)
        {
            int errorCode = ErrorCodes.NoError;
            try
            {
                int index = 1;
                int totalCount = data.DetailDatas.Count;
                while (true)
                {
                    var detail = data.DetailDatas.ToList().FindAll(x => x.Priority == index);
                    if (detail.Count == 0) break;
                    if (SolveWare.Core.MMgr.IsStop) return ErrorCodes.MachineStopCall;

                    if (totalCount == 1)
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
                                DetailData_IO dData = detail[num];
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
                    totalCount -= detail.Count;
                    if (totalCount == 0) break;
                }
            }
            catch (Exception ex)
            {
                errorCode = ErrorCodes.MotionFunctionError;
            }

            return errorCode;
        }

        private static int Execute_Single_DetailData(DetailData_IO detailData)
        {
            int errorCode = ErrorCodes.NoError;

            try
            {
                IIOBase iO = detailData.IOName.GetIOBase();
                switch (detailData.IOType)
                {
                    case IO_Type.Output:
                        switch (detailData.TriggerMode)
                        {
                            case ConstantProperty.ON:                               
                                iO.On();
                                break;
                            case ConstantProperty.OFF:
                                iO.Off();
                               break;
                        }
                        Thread.Sleep(detailData.DelayTime);
                        break;
                    case IO_Type.Input:
                        Thread.Sleep(detailData.DelayTime);
                        switch (detailData.TriggerMode)
                        {
                            case ConstantProperty.ON:
                                errorCode = iO.IsOff()? ErrorCodes.IOFunctionError : ErrorCodes.NoError;    
                                break;
                            case ConstantProperty.OFF:
                                errorCode = iO.IsOn() ? ErrorCodes.IOFunctionError : ErrorCodes.NoError;
                                break;
                        }
                        break;
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
