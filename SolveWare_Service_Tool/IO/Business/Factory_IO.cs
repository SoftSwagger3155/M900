using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.Manager.Base.Abstract;
using SolveWare_Service_Core.Manager.Base.Interface;
using SolveWare_Service_Tool.IO.Base.Abstract;
using SolveWare_Service_Tool.IO.Data;
using SolveWare_Service_Tool.MasterDriver.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Tool.IO.Business
{
    public class Factory_IO : IFactory
    {
        public IElement BuildTool(IElement configData)
        {
            IElement iO = null;
            ConfigData_IO data = configData as ConfigData_IO;

            switch (data.IOMasterDriver)
            {
                case Definition.Master_Driver_IO.ZMCAUX:
                    var master = (SolveWare.Core.MMgr as MainManagerBase).MasterDriver as MasterDriverManager;
                    bool simulation = false;

                    //驱动模拟 马达模拟
                    if (master.Config.Is_Simulation_Motor || data.Simulation)
                    {
                        simulation = true;
                    }
                    //驱动正常 马达摸拟
                    else if (!master.Config.Is_Simulation_Motor && data.Simulation)
                    {
                        simulation = true;
                    }

                    iO = new IO_Zmcaux(data, simulation);
                    (iO as IOBase).Init();
                    break;
                case Definition.Master_Driver_IO.LEADSIDE_DMC3600:
                    break;
                case Definition.Master_Driver_IO.YANKONG_MCC800:
                    break;
            }

            return iO;
        }
    }
}
