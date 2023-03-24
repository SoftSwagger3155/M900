using SolveWare_Service_Core;
using SolveWare_Service_Core.Attributes;
using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.Manager.Base.Interface;
using SolveWare_Service_Utility.Business.Index.Data;
using SolveWare_Service_Utility.Business.IO.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Utility.Business.IO.Job
{
    [PairAttribute(typeof(MF900_Data_IO))]
    public class MF900_Job_IO : JobFundamentalBase, IDataModulePair
    {
        MF900_Data_IO iO_Data = null;

        public void Setup(IElement data)
        {
            this.iO_Data = data as MF900_Data_IO;
        }
        
        public void Save(bool isShowWindow = false)
        {
           IResourceProvider provider =   SolveWare.Core.MMgr.Get_Single_Data_Resource(typeof(MF900_Job_IO).Name);
           bool isSucessful = (provider as IRESTFul).SaveSingleData(this.iO_Data);
           
            if(isShowWindow)
            {

            }
         
        }

        public override int Do_Job()
        {
            return base.Do_Job();   
        }
    }
}
