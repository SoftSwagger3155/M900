using SolveWare_Service_Core.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Core.Index.Base.Interface
{
    public interface IIndex
    {
        int GoNext();
        int GoPrevisou();
        int Go(int number);
    }
}
