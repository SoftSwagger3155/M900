using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class IndicatorResourceAttribute : Attribute
    {
        public string Name { get; set; }
        public IndicatorResourceAttribute(string name)
        {
            Name = name;
        }
    }
}
