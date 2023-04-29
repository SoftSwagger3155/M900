using Basler.Pylon;
using HalconDotNet;
using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.Manager.Base.Abstract;
using SolveWare_Service_Core.Manager.Base.Interface;
using SolveWare_Service_Tool.Camera.Data;
using SolveWare_Service_Tool.Camera.Definition;
using SolveWare_Service_Tool.MasterDriver.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Tool.Camera.Business
{
    public class Factory_Camera : IFactory
    {
        int index_Basler = -1;
        //int index_JAI = -1;
        //int index_DaHehng = -1;
        //int index_PointGrey = -1;

        public IElement BuildTool(IElement configData)
        {
            IElement cameraBase = null;
            ConfigData_Camera config = configData as ConfigData_Camera;
            //先盲着把物件实现，然后再从使用者UI来改变绑定的相机名字
            var master = (SolveWare.Core.MMgr as MainManagerBase).MasterDriver as MasterDriverManager;
            bool simulation = false;


            switch (config.MasterDriver)
            {
                case Master_Driver_Camera.Basler:
                    List<Basler.Pylon.ICameraInfo> allCameraInfos = Basler.Pylon.CameraFinder.Enumerate();
                    Basler.Pylon.ICameraInfo iCamInfo = null;
                    Basler.Pylon.Camera camera;

                    if (allCameraInfos.Count > 0)
                    {   
                        //由UerDifine Name 来找 相机配队
                        int index =allCameraInfos.FindIndex(x => x[CameraInfoKey.UserDefinedName] == config.Id_Camera);
                        if(index < 0)
                        {
                            simulation = true;  
                        }
                        else
                        {
                            iCamInfo = allCameraInfos[index];
                            simulation = false; 
                        } 
                    }
                    else
                    {
                        simulation = true;
                    }


                    cameraBase = new Camera_Media_Basler(config.Name, simulation);
                    if (iCamInfo != null)
                    {
                        camera = new Basler.Pylon.Camera(iCamInfo);
                        (cameraBase as Camera_Media_Basler).AssignCameraMedia(camera);
                        (cameraBase as Camera_Media_Basler).Open();
                        (cameraBase as Camera_Media_Basler).Assign_Media_Related_Parameter();
                    }
                    else
                    {
                        (cameraBase as Camera_Media_Basler).AssignCameraMedia(null);
                        (cameraBase as Camera_Media_Basler).Assign_Media_Related_Parameter();
                        //SolveWare.Core.ShowMsg($"相机 : {config.Name}, 开启模拟模式");
                    }
                    break;

                // //TODO: Factory_Camera BuildTool() =>  海康相机尚未建立

                default:
                    break;
            }

            return cameraBase;
        }
    }
}
