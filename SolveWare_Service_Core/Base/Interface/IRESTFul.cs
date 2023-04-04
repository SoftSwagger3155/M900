using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Core.Base.Interface
{
    public interface IRESTFul<TData>
    {
        string FilePath { get; set; }
        bool AddSingleData(TData data);
        bool DeleteSingleData(TData data);
        TData GetSingleData(string name);
        TData GetSingleData(TData IElementBase);
        IList<TData> DataBase { get; set; }
        bool SaveSingleData(TData data);
    }
}
