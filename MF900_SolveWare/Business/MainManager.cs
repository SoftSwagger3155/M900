﻿using MF900_SolveWare.Resource;
using SolveWare_Service_Core.FSM.Base.Interface;
using SolveWare_Service_Core.Info.Base.Interface;
using SolveWare_Service_Core.Manager.Base.Abstract;
using SolveWare_Service_Core.Manager.Base.Interface;
using SolveWare_Service_Core.Manager.Business;
using SolveWare_Service_Tool.IO.Business;
using SolveWare_Service_Tool.IO.Data;
using SolveWare_Service_Tool.Motor.Business;
using SolveWare_Service_Tool.Motor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF900_SolveWare.Business
{
    public class MainManager : MainManagerBase
    {
        public MainManager():base()
        {

        }
        public MainManager(IInfoHandler infoHandler) : base(infoHandler)
        {
           
        }

        public override void Setup()
        {
            this.On_Tool_IO_Resource_Loading_Handler -= MainManager_On_Tool_IO_Resource_Loading_Handler;
            this.On_Tool_IO_Resource_Loading_Handler += MainManager_On_Tool_IO_Resource_Loading_Handler;

            this.On_Tool_Motor_Resource_Loading_Handler -= MainManager_On_Tool_Motor_Resource_Loading_Handler;
            this.On_Tool_Motor_Resource_Loading_Handler += MainManager_On_Tool_Motor_Resource_Loading_Handler;
        }

        private void MainManager_On_Tool_Motor_Resource_Loading_Handler()
        {
            IResourceProvider provider = new Resource_Tool_Manager<ConfigData_Motor>(new Factory_Motor());
            provider.Initialize();
            provider.DoubleCheck(ResourceKey.Motor_Top_X,
                                               ResourceKey.Motor_Top_Y,
                                               ResourceKey.Motor_Top_T,
                                               ResourceKey.Motor_Btm_X,
                                               ResourceKey.Motor_Btm_Y,
                                               ResourceKey.Motor_Btm_Z, 
                                               ResourceKey.Motor_Btm_T);

        }
        private void MainManager_On_Tool_IO_Resource_Loading_Handler()
        {
            IResourceProvider provider = new Resource_Tool_Manager<ConfigData_IO>(new Factory_IO());
            provider.Initialize();
            provider.DoubleCheck(ResourceKey.TowerLight_Green, 
                                               ResourceKey.TowerLight_Yellow,
                                               ResourceKey.TowerLight_Red);

        }

        public override void CloseAll()
        {
            //底层已有基础关闭运作
            base.CloseAll();

        }

        public override bool HasIdenticalWindow()
        {
            return false;
        }

    }
}
