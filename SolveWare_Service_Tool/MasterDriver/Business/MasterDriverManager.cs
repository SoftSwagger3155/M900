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
        public IOMotionCardInfo CardInfo { get; private set; }
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
                            this.CardInfo = new IOMotionCardInfo();
                            int cardNo = Dll_Zmcaux.ZAux_GetMaxPciCards();
                            IntPtr Handle;
                            isOk = cardNo > 0;
                            if (isOk)
                            {
                                for (int i = 0; i < cardNo; i++)
                                {
                                    Dll_Zmcaux.ZAux_OpenPci(Convert.ToUInt32(i), out Handle);
                                    CardInfo.Dic_CardHandler.Add(i, Handle);
                                }
                            }
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
   
    public class IOMotionCardInfo
    {
          public Dictionary<int, IntPtr> Dic_CardHandler { get; set; }
        public IOMotionCardInfo()
        {
            Dic_CardHandler = new Dictionary<int, IntPtr>();
        }
    }
}
