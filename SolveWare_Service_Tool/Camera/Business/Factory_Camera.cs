using Basler.Pylon;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.Manager.Base.Interface;
using SolveWare_Service_Tool.Camera.Data;
using SolveWare_Service_Tool.Camera.Definition;
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
            switch (config.MasterDriver)
            {
                case Master_Driver_Camera.Basler:
                    if (config.IsSimulation) return new Camera_Media_Basler(config.Camera_Name, config.IsSimulation);
                    List<Basler.Pylon.ICameraInfo> allCameraInfos = Basler.Pylon.CameraFinder.Enumerate();
                    Basler.Pylon.ICameraInfo iCamInfo = null;
                    if (allCameraInfos.Count > 0)
                    {
                        index_Basler++;
                        iCamInfo = allCameraInfos[index_Basler];
                    }

                    Basler.Pylon.Camera camera;
                    cameraBase = new Camera_Media_Basler(config.Camera_Name, config.IsSimulation);
                   

                    if(iCamInfo != null)
                    {
                        camera = new Basler.Pylon.Camera(iCamInfo);
                        (cameraBase as Camera_Media_Basler).AssignCameraMedia(camera);
                                 
                    }
                    else
                    {
                        (cameraBase as Camera_Media_Basler).AssignCameraMedia(null);
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
