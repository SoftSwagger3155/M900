using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF900
{
    [Serializable]
    public class ProductManage
    {
        //public ProductManage() { }
        public string NowProgramName { get; set; }
        public List<string> ProgramList { get; set; }
    }
}
