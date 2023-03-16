using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.Manager.Base.Interface;
using SolveWare_Service_Tool.Dlls;
using SolveWare_Service_Tool.MasterDriver.Data;
using SolveWare_Service_Tool.MasterDriver.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Tool.MasterDriver.Business
{
    public class MasterDriverManager : IMasterDriver
    {
        ConfigData_MasterDriver config = null;
        public void Setup(IElement configData)
        {
            config = configData as ConfigData_MasterDriver;
        }

        public bool Init()
        {
            bool isOk = false;
            try
            {
                //同样的卡
                if(config.Master_Driver_Motor == config.Master_Driver_IO)
                {
                    switch(config.Master_Driver_Motor)
                    {
                        case Master_Driver_Kind.Zmcaux:
                            int cardNo = Dll_Zmcaux.ZAux_GetMaxPciCards();
                            isOk = cardNo > 0;
                            break;
                    }  
                }


                isOk = true;
            }
            catch (Exception ex)
            {
                SolveWare.Core.MMgr.Infohandler.LogMessage($"Master Driver 初始化 失败\r\n{ex.Message}"); 
            }

            return isOk;
        }
    }
}
