using MF900_SolveWare.Offset.Data;
using MF900_SolveWare.Resource;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.FSM.Base.Interface;
using SolveWare_Service_Core.Info.Base.Interface;
using SolveWare_Service_Core.Manager.Base.Abstract;
using SolveWare_Service_Core.Manager.Base.Interface;
using SolveWare_Service_Core.Manager.Business;
using SolveWare_Service_Tool.Camera.Business;
using SolveWare_Service_Tool.Camera.Data;
using SolveWare_Service_Tool.IO.Business;
using SolveWare_Service_Tool.IO.Data;
using SolveWare_Service_Tool.Motor.Business;
using SolveWare_Service_Tool.Motor.Data;
using SolveWare_Service_Vision.Data;
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
            //硬件
            this.On_Tool_Resource_Loading_Handler -= MainManager_On_Tool_Resource_Loading_Handler;
            this.On_Tool_Resource_Loading_Handler += MainManager_On_Tool_Resource_Loading_Handler;

            //Data
            this.On_Data_Resource_Loading_Handler -= MainManager_On_Data_Resource_Loading_Handler;
            this.On_Data_Resource_Loading_Handler += MainManager_On_Data_Resource_Loading_Handler;

            //机器其他所需资源
            this.On_Machine_Resource_Loading_Handler -= MainManager_On_Machine_Resource_Loading_Handler;
            this.On_Machine_Resource_Loading_Handler += MainManager_On_Machine_Resource_Loading_Handler;
        }

        private void MainManager_On_Data_Resource_Loading_Handler()
        {
            IResourceProvider provider = null;
            //Offset Data
            provider = new Resource_Data_Manager<MF900_OffsetDataBase>();
            provider.Initialize();
            provider.DoubleCheck(ResourceKey.Top_Camera_Top_Prober_OffsetData);

            //视物Data
            provider = new Resource_Data_Manager<Data_InspectionKit>();
            provider.Initialize();
            provider.DoubleCheck(ResourceKey.Top_Camera_Btm_Prober_Mark_Point_InspectKit,
                                               ResourceKey.Top_Camera_Git_Hole_InspectKit,
                                               ResourceKey.Btm_Camera_Top_Prober_Mark_Point_InspectKit,
                                               ResourceKey.Btm_Camera_Git_Hole_InspectKit);
        }

        private void MainManager_On_Tool_Resource_Loading_Handler()
        {
            IResourceProvider provider = null;
            //马达物件
            provider = new Resource_Tool_Manager<ConfigData_Motor>(new Factory_Motor());
            provider.Initialize();
            provider.DoubleCheck(ResourceKey.Motor_Top_X,
                                               ResourceKey.Motor_Top_Y,
                                               ResourceKey.Motor_Top_T,
                                               ResourceKey.Motor_Btm_X,
                                               ResourceKey.Motor_Btm_Y,
                                               ResourceKey.Motor_Btm_Z,
                                               ResourceKey.Motor_Btm_T);

            //IO物件
            provider = new Resource_Tool_Manager<ConfigData_IO>(new Factory_IO());
            provider.Initialize();
            provider.DoubleCheck(ResourceKey.TowerLight_Green,
                                               ResourceKey.TowerLight_Yellow,
                                               ResourceKey.TowerLight_Red);


            //相机物件
            provider = new Resource_Tool_Manager<ConfigData_Camera>(new Factory_Camera());
            provider.Initialize();
            provider.DoubleCheck(ResourceKey.Top_Camera,
                                               ResourceKey.Btm_Camera);

        }

        private void MainManager_On_Machine_Resource_Loading_Handler()
        {

           

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
