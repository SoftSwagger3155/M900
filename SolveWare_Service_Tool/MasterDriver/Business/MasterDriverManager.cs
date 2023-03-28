﻿using Basler.Pylon;
using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.General;
using SolveWare_Service_Core.Manager.Base.Interface;
using SolveWare_Service_Core.Manager.Business;
using SolveWare_Service_Tool.Dlls;
using SolveWare_Service_Tool.MasterDriver.Data;
using SolveWare_Service_Tool.MasterDriver.Definition;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SolveWare_Service_Tool.MasterDriver.Business
{
    public class MasterDriverManager : IMasterDriver
    {
        IResourceProvider provider = null;
        ConfigData_MasterDriver config = null;
        static string FileName = "开机驱动档案";
        public IOMotionCardInfo CardInfo { get; private set; }
        public bool IsSimulation { get; set; } = true;

        public IList<ICameraInfo> Basler_Camera_Infos { get; private set; }

        public void Setup(IElement configData)
        {
            config = configData as ConfigData_MasterDriver;
        }

        public MasterDriverManager()
        {
            provider = new Resource_Data_Manager<ConfigData_MasterDriver>();
            provider.Initialize();
            provider.DoubleCheck(FileName);
            
           config = (ConfigData_MasterDriver)provider.Get_Single_Item(FileName);
        }

        public bool Init()
        {
            bool isOk = false;
            try
            {
                if (this.config.Is_Simulation_Motor && this.config.Is_Simulation_IO) return true;
                //同样的卡
                if(config.Master_Driver_Motor == config.Master_Driver_IO)
                {
                    switch(config.Master_Driver_Motor)
                    {
                        case Master_Driver_Kind.Zmcaux:
                            #region 连接控制器  ---杨工
                            IntPtr Handle;
                            this.CardInfo = new IOMotionCardInfo();
                            isOk = Dll_Zmcaux.ZAux_OpenEth("127.0.0.1", out Handle) == 0;
                            if (isOk)
                            {
                                CardInfo.Dic_CardHandler.Add(0, Handle);
                            }
                           
                            #endregion


                            #region PCIE控制卡卡
                            //this.CardInfo = new IOMotionCardInfo();
                            //int cardNo = Dll_Zmcaux.ZAux_GetMaxPciCards();
                            //IntPtr Handle;
                            //isOk = cardNo > 0;
                            //if (isOk)
                            //{
                            //    for (int i = 0; i < cardNo; i++)
                            //    {
                            //        Dll_Zmcaux.ZAux_OpenPci(Convert.ToUInt32(i), out Handle);
                            //        CardInfo.Dic_CardHandler.Add(i, Handle);
                            //    }
                            //}
                            #endregion
                            break;
                    }
                }
                else // TODO: Master Driver Init() => 如果不是正运动的话
                {

                }

           

                isOk = true;
            }
            catch (Exception ex)
            {
                SolveWare.Core.MMgr.Infohandler.LogMessage($"Master Driver 初始化 失败\r\n{ex.Message}"); 
            }

            return isOk;
        }

        public void Close()
        {
            switch (config.Master_Driver_Motor)
            {
                case Master_Driver_Kind.Zmcaux:
                    for (int i = 0; i < CardInfo.Dic_CardHandler.Count; i++)
                    {
                        Dll_Zmcaux.ZAux_Close(CardInfo.Dic_CardHandler[i]);
                    }
                break;
            }
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
