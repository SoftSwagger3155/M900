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
using SolveWare_Service_Core.Definition;

namespace SolveWare_Service_Utility.Common.IO
{
    public class Job_IO : DataJobPairFundamentalBase<Data_IO>
    {
        public override Mission_Report Do_Job()
        {
            Mission_Report context = new Mission_Report();
            this.Status = JobStatus.Entrance;
            try
            {
                context = Execute(Data);
            }
            catch (Exception ex)
            {
                context.Set(ErrorCodes.IOFunctionError, ex.Message);
            }
            this.Status = context.ErrorCode == ErrorCodes.NoError ? JobStatus.Done : JobStatus.Fail;
            return context;
        }

        private static Mission_Report Execute(Data_IO data)
        {
            Mission_Report context = new Mission_Report();
            try
            {
                int index = 1;
                int totalCount = data.DetailDatas.Count;
                while (true)
                {
                    var detail = data.DetailDatas.ToList().FindAll(x => x.Priority == index);
                    if (detail.Count == 0) break;
                    if (context.NotPass()) break;

                    if (totalCount == 1)
                    {
                        context = Execute_Single_DetailData(detail[0]);
                    }
                    else
                    {
                        bool isError = false;
                        List<Task> tasks = new List<Task>();

                        for (int i = 0; i < detail.Count; i++)
                        {
                            int num = i;
                            Task task = Task.Factory.StartNew((object obj) =>
                            {
                                Data_Mission_Report report = obj as Data_Mission_Report;
                                DetailData_IO dData = detail[num];
                                report.Context = Execute_Single_DetailData(dData);
                            }, new Data_Mission_Report());
                            tasks.Add(task);
                        }
                        Task.WaitAll(tasks.ToArray());
                        context =  tasks.Converto_Mission_Report();

                    }

                    if (context.NotPass()) break;
                    totalCount -= detail.Count;
                    if (totalCount == 0) break;
                }
            }
            catch (Exception ex)
            {
                context.Set(ErrorCodes.IOFunctionError, ex.Message);
            }

            return context;
        }

        private static Mission_Report Execute_Single_DetailData(DetailData_IO detailData)
        {
            Mission_Report context = new Mission_Report();

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
                                if (iO.IsOff()) context.Set(ErrorCodes.IOFunctionError);
                                break;
                            case ConstantProperty.OFF:
                                if (iO.IsOn()) context.Set(ErrorCodes.IOFunctionError);
                                break;
                        }
                        break;
                }
            }
            catch
            {
                context.Set(ErrorCodes.IOFunctionError);
            }

            return context;
        }
    }
}
