using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.General;
using SolveWare_Service_Tool.IO.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Utility.Common.IO
{
    public class Data_IO: ElementBase
    {
        public IList<DetailData_IO> DetailDatas { get; set; }
        public Data_IO()
        {
            DetailDatas = new List<DetailData_IO>();
        }
    }
    public class DetailData_IO: DetailDataElementBase
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
            set => UpdateProperAction(ref delayTime, value, ac: UpdateContent);
        }

        private string triggerMode = ConstantProperty.ON;
        public string TriggerMode
        {
            get { return triggerMode; }
            set => UpdateProperAction(ref triggerMode, value, ac: UpdateContent);
        }

        private IO_Type iOType = IO_Type.Output;
        public IO_Type IOType
        {
            get => iOType;
            set=> UpdateProperAction(ref iOType, value, ac: UpdateContent);
        }

        public override void UpdateContent()
        {
            this.Content = $"顺序 {Priority} 名称 {IOName} 种类 {IOType} 模式{TriggerMode} 延时 {DelayTime}";
        }
    }
}
