using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.General;
using SolveWare_Service_Tool.Lighting.Base;
using SolveWare_Service_Tool.Lighting.Base.Abstract;
using SolveWare_Service_Utility.Extension;
using SolveWare_Service_Vision.Data;
using SolveWare_Service_Vision.Inspection.JobSheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SolveWare_Service_Vision.Inspection.Business
{
    public class Job_Lighting : JobFundamentalBase, IDataModulePair
    {
        Data_Inspection jobParam;
        public override Mission_Report Do_Job()
        {
            Mission_Report context = new Mission_Report();
            try

            {
                //TODO: Stanley 1.细节实现 MF900的Lighting 是IO 恒开
                jobParam.JobSheet_Lighting_Datas.ForEach(x =>
                {
                    if (x.Is_IO_Controlled)
                    {
                        switch (x.TriggerMode)
                        {
                            case ConstantProperty.ON:
                                x.IO_Name.GetIOBase().On();
                                break;
                            case ConstantProperty.OFF:
                                x.IO_Name.GetIOBase().Off();
                                break;
                        }
                    }
                    // 如果有光源控制器
                    else
                    {
                        LightingBase lighting = x.Lighting_Name.GetLighting();
                        lighting.Set_Intensity((int)x.Intensity);
                    }
                });                
            }
            catch (Exception ex)
            {
                context.Set(ErrorCodes.VisionFailed);
            }


            return context;
        }
        public void Setup(IElement data)
        {
            this.jobParam = data as Data_Inspection;
        }
    }
}
