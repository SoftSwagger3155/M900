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
using System.Reflection;
using SolveWare_Service_Tool.MasterDriver.Business;
using MF900_SolveWare.WorldCenter.Job;

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
        }

        private void MainManager_On_Data_Resource_Loading_Handler()
        {
            IResourceProvider provider = null;            
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
                                               ResourceKey.Motor_Top_Z,
                                               ResourceKey.Motor_Top_T,
                                               ResourceKey.Motor_Btm_X,
                                               ResourceKey.Motor_Btm_Y,
                                               ResourceKey.Motor_Btm_Z,
                                               ResourceKey.Motor_Btm_T,
                                               ResourceKey.Motor_Table
                                               );
            
            //IO物件
            provider = new Resource_Tool_Manager<ConfigData_IO>(new Factory_IO());
            provider.Initialize();
            provider.Plug_In();
            
            provider.DoubleCheck(ResourceKey.In_Start,
                                               ResourceKey.In_Zero,
                                               ResourceKey.In_Stop,
                                               ResourceKey.In_Reset,
                                               ResourceKey.In_E_Stop,
                                               ResourceKey.In_MoveFornt,
                                               ResourceKey.In_MoveBack,
                                               ResourceKey.In_MoveLeft,
                                               ResourceKey.In_MoveRight,
                                               ResourceKey.In_TopJigInductor,
                                               ResourceKey.In_BtmJigInductor,
                                               ResourceKey.In_ForntBanner,
                                               ResourceKey.In_TopBanner,
                                               ResourceKey.In_XStretchAir1,
                                               ResourceKey.In_XStretchAir2,
                                               ResourceKey.In_YStretchAir1,
                                               ResourceKey.In_YStretchAir2,
                                               ResourceKey.In_TopMarkeInkycapOut,
                                               ResourceKey.In_TopMarkeAirOut,
                                               ResourceKey.In_TopMarkeAirRetract,
                                               ResourceKey.In_BtmPropUpAirOut,
                                               ResourceKey.In_BtmPropUpAirRetract,
                                               ResourceKey.Op_TowerLight_Red,
                                               ResourceKey.Op_TowerLight_Yellow,
                                               ResourceKey.Op_TowerLight_Green,
                                               ResourceKey.OP_Buzzer,
                                               ResourceKey.Op_StartButton,
                                               ResourceKey.Op_ZeroButton,
                                               ResourceKey.Op_StopButton,
                                               ResourceKey.Op_ResetButton,
                                               ResourceKey.Op_LeftX_RailTighten,
                                               ResourceKey.Op_LeftX_RailLoosen,
                                               ResourceKey.Op_RightX_RailTighten,
                                               ResourceKey.Op_RightX_RailLoosen,
                                               ResourceKey.Op_Y_RailTighten,
                                               ResourceKey.Op_Y_RailLoosen,
                                               ResourceKey.Op_PCB_Clamp,
                                               ResourceKey.Op_PCB_Loosen,
                                               ResourceKey.Op_X_StretchAirOn,
                                               ResourceKey.Op_X_StretchAirOff,
                                               ResourceKey.Op_Y_StretchAirOn,
                                               ResourceKey.Op_Y_StretchAirOff,
                                               ResourceKey.Op_MarkeInkBoxOn,
                                               ResourceKey.Op_MarkeInkBoxOff,
                                               ResourceKey.Op_UpMarkeOn,
                                               ResourceKey.Op_DownMarkOn,
                                               ResourceKey.Op_UpJipClampAirOn,
                                               ResourceKey.Op_UpJipClampAirOff,
                                               ResourceKey.Op_DownJipClampAirOn,
                                               ResourceKey.Op_DownJipClampAirOff);


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
            ICommonJobFundamental job = null;
            job  = new Job_MMperPixel_TopCamera(ResourceKey.MMperPixel_TopCamera);
            Resource_DataPair_Center.Add(job);

            job = new Job_MMperPixel_TopCamera(ResourceKey.MMperPixel_BtmCamera);
            Resource_DataPair_Center.Add(job);


            //Inspect            
            job = new Inspect(ResourceKey.InspectKit_Top_Camera_Git_Hole);
            Resource_DataPair_Center.Add(job);

            job = new Inspect(ResourceKey.InspectKit_Btm_Camera_Git_Hole);
            Resource_DataPair_Center.Add(job);


            //Offset
            job = new Job_Offset(ResourceKey.Offset_Top_Camera_Top_Prober, Data_Offset.TopModule);
            Resource_DataPair_Center.Add( job);

            job = new Job_Offset(ResourceKey.Offset_Top_Camera_Top_Pin, Data_Offset.TopModule);
            Resource_DataPair_Center.Add(job);

            job = new Job_Offset(ResourceKey.Offset_Btm_Camera_Btm_Prober, Data_Offset.BtmModule);
            Resource_DataPair_Center.Add(job);

            job = new Job_Offset(ResourceKey.Offset_Btm_Camera_Btm_Pin, Data_Offset.BtmModule);
            Resource_DataPair_Center.Add(job);


            //世界中心
            job = new Job_GlobalWorldCenter(ResourceKey.GlobalWorldCenter);
            Resource_DataPair_Center.Add(job);
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

        public override void AssignFSM()
        {
            var master = (SolveWare.Core.MMgr as MainManagerBase).MasterDriver as MasterDriverManager;
            this.FSM_Home = new FSM_Home_Controller(master.Is_Simulation_FSM);
            //this.FSM_Auto = new FSM_Auto_Controller();
            //this.FSM_Reset = new FSM_Reset_Controller();
        }
        public override void Stop(bool stopMotor = true)
        {
            //设止机器状态为停止
            this.SetStatus(Machine_Status.Stop);

            //设止马达停止
            if (stopMotor)
            {
                var allMotors = SolveWare.Core.MMgr.Get_Single_Tool_Resource(Tool_Resource_Kind.Motor).Get_All_Items().ToList();
                allMotors.ForEach(x => (x as AxisBase).Stop());
            }
        }
    }
}
