using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.Manager.Base.Interface;
using SolveWare_Service_Tool.IO.Data;
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
                    iO = new IO_Zmcaux(data);
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
