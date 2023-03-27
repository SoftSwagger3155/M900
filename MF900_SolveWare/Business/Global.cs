using MF900_SolveWare.Resource;
using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.General;
using SolveWare_Service_Core.Manager.Base.Interface;
using SolveWare_Service_Core.Manager.Business;
using SolveWare_Service_Utility.Common.Motion;
using Sunny.UI.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF900_SolveWare.Business
{
    public class Global
    {
        private static Global instance;
        private static object mutex = new object();
        IResourceProvider provider = null;
        public Job_Motion Pos_TopCamera { get; set; }
        public Job_Motion Pos_BtmCamera { get; set; }
        private Global() 
        {
            provider = new Resource_Data_Manager<Data_GlobalCenter>();
            provider.Initialize();
            provider.DoubleCheck(ResourceKey.Data_GlobalWorldCenter);
            CreateJobMotion();
       
        }
        public static Global Instance
        {
            get
            {
                if(instance == null)
                {
                    lock(mutex)
                    {
                        instance = new Global();
                    }
                }
                return instance;
            }
        }
     
        public Data_GlobalCenter Data_Center { get => (Data_GlobalCenter)provider.Get_Single_Item(ResourceKey.Data_GlobalWorldCenter); }

        /// <summary>
        /// TODO Run Global Center Process : Stanley  实现细节
        /// </summary>
        public void Run_GlobalCenter_Process()
        {

        }

        private void CreateJobMotion()
        {
            Pos_TopCamera = new Job_Motion(ResourceKey.Pos_WorldCenter_TopCamera);
            List<DetailData_Motion> top_MotionDetails = new List<DetailData_Motion>
            {
                new DetailData_Motion{ AxisName = ResourceKey.Motor_Top_X, Pos =0 },
                new DetailData_Motion{ AxisName = ResourceKey.Motor_Top_Y, Pos =0 }
            };
            Pos_TopCamera.Data.DetailDatas = top_MotionDetails;

            Pos_BtmCamera = new Job_Motion(ResourceKey.Pos_WorldCenter_BtmCamera);
            List<DetailData_Motion> btm_MotionDetails = new List<DetailData_Motion>
            {
                new DetailData_Motion{ AxisName = ResourceKey.Motor_Btm_X, Pos =0 },
                new DetailData_Motion{ AxisName = ResourceKey.Motor_Btm_Y, Pos =0 }
            };
            Pos_BtmCamera.Data.DetailDatas = btm_MotionDetails;
        }
    }

    public class Data_GlobalCenter: ElementBase
    {   
        public POINT WorldCenter_TopCamera { get; set; }
        public POINT WorldCenter_BtmCamera { get; set; }


    }
}
