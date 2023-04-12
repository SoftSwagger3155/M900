using SolveWare_Service_Core;
using SolveWare_Service_Core.Attributes;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.Definition;
using SolveWare_Service_Core.General;
using SolveWare_Service_Tool.Camera.Base.Abstract;
using SolveWare_Service_Tool.Camera.Base.Interface;
using SolveWare_Service_Tool.IO.Base.Abstract;
using SolveWare_Service_Tool.IO.Base.Interface;
using SolveWare_Service_Tool.Lighting.Base.Abstract;
using SolveWare_Service_Tool.Motor.Base.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
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
        public static CameraMediaBase GetCamera(this string name)
        {
            CameraMediaBase camera;
            try
            {
                camera = (CameraMediaBase)SolveWare.Core.MMgr.Get_Single_Element_Form_Tool_Resource(Tool_Resource_Kind.Camera, name);
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
        public static LightingBase GetLighting(this string name)
        {
            LightingBase lighting = null;
            try
            {
                lighting = (LightingBase)SolveWare.Core.MMgr.Get_Single_Element_Form_Tool_Resource(Tool_Resource_Kind.Lighting, name);

            }
            catch (Exception)
            {
                return null;
            }

            return lighting;
        }
        public static double GetUnitPos(this string name)
        {
            return name.GetAxisBase().Get_CurUnitPos();
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
        public static void CopyTo<TFormObj, TToObj>(this TFormObj FromObj, TToObj ToObj)
        {
            Type fromType = FromObj.GetType();
            Type toType = ToObj.GetType();
            object fObject = Activator.CreateInstance(fromType);
            object tObject = Activator.CreateInstance(toType);

            PropertyInfo[] _fPropertyInfo = fromType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty);
            if (_fPropertyInfo.Length > 0)
            {
                PropertyInfo[] _tInfo = toType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty);
                foreach (var fItem in _fPropertyInfo)
                {

                    var value = fItem.GetValue(FromObj);
                    foreach (var tItem in _tInfo)
                    {
                        if (fItem.Name.Equals(tItem.Name) && fItem.CanWrite)
                        {
                            tItem.SetValue(ToObj, value);
                            break;
                        }
                        else if (fItem.Name.Equals(tItem.Name) && fItem.CanRead && !fItem.CanWrite)
                        {
                            tItem.GetValue(ToObj);
                            break;
                        }

                    }
                }
            }

            FieldInfo[] _fFieldInfo = fromType.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty);
            if (_fFieldInfo.Length > 0)
            {
                FieldInfo[] _tInfo = toType.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty);
                foreach (var fItem in _fFieldInfo)
                {
                    var value = fItem.GetValue(FromObj);
                    foreach (var tItem in _tInfo)
                    {


                        if (fItem.Name.Equals(tItem.Name))
                        {
                            tItem.SetValue(ToObj, value);
                        }
                    }
                }
            }



        }
        public static string GetErrorMsg(this int errorCode)
        {
            return ErrorCodes.GetErrorDescription(errorCode);   
        }
      
    }
}
