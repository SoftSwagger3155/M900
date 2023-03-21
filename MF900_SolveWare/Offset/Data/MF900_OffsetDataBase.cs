using SolveWare_Service_Offset.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MF900_SolveWare.Offset.Data
{
    [XmlInclude(typeof(Data_Top_Camera_Top_Prober_Offset))]
    public abstract class MF900_OffsetDataBase: Data_Offset_Base
    {
    }
}
