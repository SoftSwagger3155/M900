using MF900_SolveWare.Safe;
using SolveWare_Service_Core.Attributes;
using SolveWare_Service_Core.General;
using SolveWare_Service_Utility.Index.Base.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF900_SolveWare.Index.Data
{
    //TODO: 需结合总表 来分配 Index Data
    [ResourceBaseAttribute(ConstantProperty.ResourceKey_Index)]
    public class Data_Index: Data_IndexBase
    {
        public Data_Index()
        {
            SafeData = new Data_Safe();
        }
        public  Data_Safe SafeData { get; set; }
    }
}
