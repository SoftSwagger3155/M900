using SolveWare_Service_Core;
using SolveWare_Service_Core.Attributes;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.Definition;
using SolveWare_Service_Core.General;
using SolveWare_Service_Tool.Camera.Base.Abstract;
using SolveWare_Service_Tool.Camera.Base.Interface;
using SolveWare_Service_Tool.IO.Base.Abstract;
using SolveWare_Service_Tool.IO.Base.Interface;
using SolveWare_Service_Tool.Motor.Base.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Utility.Extension
{
    public static class ExtensionMethod
    {
        public static void OneNotchUp<TObj>(TObj obj, ref IList<TObj> objs, ref int selectedIndex)
        {
            int index = objs.ToList().FindIndex(x => x.Equals(obj));

            if (index <= 0)
            {
                selectedIndex = 0;
                return;
            }


            var temp = objs[index - 1];

            objs[index - 1] = objs[index];
            objs[index] = temp;
            selectedIndex = index - 1;
        }
        public static void OneNotchDown<TObj>(TObj obj, ref IList<TObj> objs, ref int selectedIndex)
        {
            int index = objs.ToList().FindIndex(x => x.Equals(obj));

            if (index >= objs.Count - 1)
            {
                selectedIndex = objs.Count - 1;
                return;
            }


            var temp = objs[index + 1];

            objs[index + 1] = objs[index];
            objs[index] = temp;
            selectedIndex = index + 1;
        }
        public static CameraBase GetCamera(this string name)
        {
            CameraBase camera;
            try
            {
                camera = (CameraBase)SolveWare.Core.MMgr.Get_Single_Element_Form_Tool_Resource(Tool_Resource_Kind.Camera, name);
            }
            catch (Exception)
            {
                return null;
            }

            return camera;
        }
        public static AxisBase GetAxisBase(this string name)
        {
            AxisBase mtr = null;
            try
            {
               mtr = (AxisBase)SolveWare.Core.MMgr.Get_Single_Element_Form_Tool_Resource(Tool_Resource_Kind.Motor, name);

            }
            catch (Exception)
            {
                return null;
            }
            
            return mtr;
        }
        public static IOBase GetIOBase(this string name)
        {
            IOBase io = null;
            try
            {
                io = (IOBase)SolveWare.Core.MMgr.Get_Single_Element_Form_Tool_Resource(Tool_Resource_Kind.IO, name);

            }
            catch (Exception)
            {
                return null;
            }
            
            return io;
        }
        public static ICommonJobFundamental GetJob(this string name)
        {
           return  SolveWare.Core.MMgr.Resource_DataPair_Center.FirstOrDefault(x => (x as IElement).Name == name);
        }
        public static string GetResourceKey<TData>(this TData data)
        {
            string key = "";
            var resource = (ResourceBaseAttribute)data.GetType().GetCustomAttribute(typeof(ResourceBaseAttribute));
            key = resource.ResourceKey;

            return key;
        }
    }
}
