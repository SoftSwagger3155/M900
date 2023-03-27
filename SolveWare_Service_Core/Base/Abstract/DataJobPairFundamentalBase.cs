using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.General;
using SolveWare_Service_Core.Manager.Base.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Core.Base.Abstract
{
    public class DataJobPairFundamentalBase<TData> : JobFundamentalBase, ICommonJobFundamental
    {
        public TData Data { get; private set; }
        protected string filePath = string.Empty;

        public DataJobPairFundamentalBase()
        {
            
        }
        public DataJobPairFundamentalBase(string name)
        {
            this.Name = name;
            this.filePath = Path.Combine(SystemPath.GetSystemDataPairPath, $"{this.Name}.xml");
            Data = XMLHelper.Load<TData>(filePath);
            if ( Data == null ) 
            {
                this.Data =(TData)Activator.CreateInstance(typeof(TData));    
                Save(); 
            }

        }
        public void Save(bool isWindowShow = false)
        {
            string msg = string.Empty;
            try
            {
                XMLHelper.Save(Data, filePath);
                msg = "储存成功";
            }
            catch 
            {
                msg = "储存失败";
            }

            SolveWare.Core.MMgr.Infohandler.LogMessage(msg, isWindowShow);
        }
    }
}
