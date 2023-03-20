using SolveWare_Service_Core.Attributes;
using SolveWare_Service_Core.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Vision.Helper
{
    public static class HelperMethod
    {
        public static IDataModulePair GetModule<TData>(this TData data)
        {
            IDataModulePair pair = null;
            var attr = (PairAttribute)data.GetType().GetCustomAttribute(typeof(PairAttribute));

            pair = (IDataModulePair)Activator.CreateInstance(attr.PartnerType);
            pair.Setup(data as IElement);
            return pair;
        }
    }
}
