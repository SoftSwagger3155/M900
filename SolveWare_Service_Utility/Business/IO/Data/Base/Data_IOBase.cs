using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Utility.Business.IO.Data.Base
{
    public abstract class Data_IOBase : ElementBase
    {
       public List<IO_DetailData> DetailDatas { get; set; }
    }

    public class IO_DetailData : DetailDataElementBase
    {      
        private string ioName;
        public string IOName
        {
            get { return ioName; }
            set => UpdateProperAction(ref ioName, value, ac: UpdateContent);
        }
        
        private int delayTime;
        public int DelayTime
        {
            get { return delayTime; }
            set=> UpdateProperAction(ref delayTime, value, ac: UpdateContent);
        }
       
        private string triggerMode = ConstantProperty.ON;
        public string TriggerMode
        {
            get { return triggerMode; }
            set => UpdateProperAction(ref triggerMode, value, ac: UpdateContent);
        }

        public override void UpdateContent()
        {
            this.Content =  $"顺序 {Priority} 名称 {IOName} 模式{TriggerMode} 延时 {DelayTime}";
        }
    }
}
