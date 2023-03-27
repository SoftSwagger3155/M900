using MF900_SolveWare.FSM.Auto;
using MF900_SolveWare.FSM.Home;
using MF900_SolveWare.FSM.Reset;
using MF900_SolveWare.MMperPixel.Job;
using MF900_SolveWare.Offset.Data;
using MF900_SolveWare.Offset.Job;
using MF900_SolveWare.Resource;
using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.Definition;
using SolveWare_Service_Core.FSM.Base.Interface;
using SolveWare_Service_Core.General;
using SolveWare_Service_Core.Info.Base.Interface;
using SolveWare_Service_Core.Manager.Base.Abstract;
using SolveWare_Service_Core.Manager.Base.Interface;
using SolveWare_Service_Core.Manager.Business;
using SolveWare_Service_Tool.Camera.Base.Abstract;
using SolveWare_Service_Tool.Camera.Business;
using SolveWare_Service_Tool.Camera.Data;
using SolveWare_Service_Tool.IO.Base.Abstract;
using SolveWare_Service_Tool.IO.Base.Interface;
using SolveWare_Service_Tool.IO.Business;
using SolveWare_Service_Tool.IO.Data;
using SolveWare_Service_Tool.Motor.Base.Abstract;
using SolveWare_Service_Tool.Motor.Business;
using SolveWare_Service_Tool.Motor.Data;
using SolveWare_Service_Utility.Common.Motion;
using SolveWare_Service_Utility.Extension;
using SolveWare_Service_Vision.Data;
using SolveWare_Service_Vision.Inspection.Business;
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

            //其他初始化任务
            Init();
            //AssignFSM();
        }

        private void MainManager_On_Data_Resource_Loading_Handler()
        {
            IResourceProvider provider = null;
          
            //视物Data
            //provider = new Resource_Data_Manager<Data_InspectionKit>();
            //provider.Initialize();
            //provider.Plug_In();
            //provider.DoubleCheck(ResourceKey.InspectKit_Top_Camera_Btm_Prober_Mark_Point,
            //                                   ResourceKey.InspectKit_Top_Camera_Git_Hole,
            //                                   ResourceKey.InspectKit_Btm_Camera_Top_Prober_Mark_Point,
            //                                   ResourceKey.InspectKit_Btm_Camera_Git_Hole);               
        }

        private void MainManager_On_Tool_Resource_Loading_Handler()
        {
            IResourceProvider provider = null;
            //马达物件
            provider = new Resource_Tool_Manager<ConfigData_Motor>(new Factory_Motor());
            provider.Initialize();
            provider.Plug_In();
            provider.DoubleCheck(ResourceKey.Motor_Top_X,
                                               ResourceKey.Motor_Top_Y,
                                               ResourceKey.Motor_Top_T,
                                               ResourceKey.Motor_Btm_X,
                                               ResourceKey.Motor_Btm_Y,
                                               ResourceKey.Motor_Btm_Z,
                                               ResourceKey.Motor_Btm_T);

            //AxisBase mtrA = ResourceKey.Motor_Top_X.GetAxisBase();
            //IOBase ioA = ResourceKey.Op_TowerLight_Green.GetIOBase();
            //CameraBase camA = ResourceKey.Btm_Camera.GetCamera();

          
            //IO物件
            provider = new Resource_Tool_Manager<ConfigData_IO>(new Factory_IO());
            provider.Initialize();
            provider.Plug_In();
            provider.DoubleCheck(ResourceKey.Op_TowerLight_Green,
                                               ResourceKey.Op_TowerLight_Yellow,
                                               ResourceKey.Op_TowerLight_Red);


            //相机物件
            provider = new Resource_Tool_Manager<ConfigData_Camera>(new Factory_Camera());
            provider.Initialize();
            provider.Plug_In();
            provider.DoubleCheck(ResourceKey.Top_Camera,
                                               ResourceKey.Btm_Camera);

        }

        private void MainManager_On_Machine_Resource_Loading_Handler()
        {
            //MMperPixel 
            ICommonJobFundamental MMperPixel_TopCamera = new Job_MMperPixel_TopCamera(ResourceKey.MMperPixel_TopCamera);
            ICommonJobFundamental MMperPixel_BtmCamera = new Job_MMperPixel_TopCamera(ResourceKey.MMperPixel_BtmCamera);
            Resource_DataPair_Center.Add(MMperPixel_TopCamera);
            Resource_DataPair_Center.Add(MMperPixel_BtmCamera);


            //Pos
            Job_Motion Pos_TopCameraInspectGitHole = new Job_Motion(ResourceKey.Pos_WorldCenter_TopCamera);
            List<DetailData_Motion> top_MotionDetails = new List<DetailData_Motion>
            {
                new DetailData_Motion{ AxisName = ResourceKey.Motor_Top_X, Pos =0 },
                new DetailData_Motion{ AxisName = ResourceKey.Motor_Top_Y, Pos =0 }
            };
            Pos_TopCameraInspectGitHole.Data.DetailDatas = top_MotionDetails;
            Pos_TopCameraInspectGitHole.Save();
           
            ICommonJobFundamental Pos_btmCameraInspectGitHole = new Job_Motion(ResourceKey.Pos_WorldCenter_BtmCamera);
            Resource_DataPair_Center.Add(Pos_TopCameraInspectGitHole);
            Resource_DataPair_Center.Add(Pos_btmCameraInspectGitHole);

           

            //Offset
            ICommonJobFundamental Offset_TopCamera_TopProber = new Job_Offset_TopCamera_TopProber(ResourceKey.OffsetData_Top_Camera_Top_Prober);
            ICommonJobFundamental Offset_BtmCamera_BtmProber = new Job_Offset_TopCamera_TopProber(ResourceKey.OffsetData_Btm_Camera_Btm_Prober);
            ICommonJobFundamental Offset_TopCamera_BtmPin = new Job_Offset_TopCamera_TopProber(ResourceKey.OffsetData_Top_Camera_Btm_Pin);
            Resource_DataPair_Center.Add(Offset_TopCamera_TopProber);
            Resource_DataPair_Center.Add(Offset_BtmCamera_BtmProber);
            Resource_DataPair_Center.Add(Offset_TopCamera_BtmPin);

            //Inspect
            ICommonJobFundamental inspect_1 = new Inspect(ResourceKey.InspectKit_Top_Camera_Git_Hole);
            Resource_DataPair_Center.Add(inspect_1);

           
        }

        private void AssignFSM()
        {
            this.FSM_Home = new FSM_Home_Controller();
            this.FSM_Auto = new FSM_Auto_Controller();
            this.FSM_Reset = new FSM_Reset_Controller();
        }

        private void Init()
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
