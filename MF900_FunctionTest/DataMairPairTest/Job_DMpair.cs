using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF900_FunctionTest.DataMairPairTest
{
    public class Job_DMpair: JobFundamentalBase, IDataModulePair
    {
        Data_DMpair Data;
        public void Save(bool isShowWindow = false)
        {
            throw new NotImplementedException();
        }

        public void Say()
        {
            Console.WriteLine($"名字 {Data.Name}");
        }

        public void Setup(IElement data)
        {
            Data = data as Data_DMpair;
        }
    }
}
