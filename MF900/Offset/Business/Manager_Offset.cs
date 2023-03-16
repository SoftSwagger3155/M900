using MF900.Offset.Base.Abstract;
using SolveWare_Service_Core.Manager.Base.Interface;
using SolveWare_Service_Core.Manager.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF900.Offset.Business
{
    public class Manager_Offset
    {
        public const string Top_Camera_To_Top_Prober = "上相机/上治具";
        public const string Top_Camera_To_Btm_Pin = "上相机/下顶针";
        public const string Btm_Camera_To_Btm_Prober = "下相机/下治具";
        IResourceProvider ResourceProvider { get; set; }


        public bool Init()
        {
            ResourceProvider = new Resource_Data_Manager<OffsetDataBase>();
            ResourceProvider.Initialize();




           
            return true;
        }
    }
}
