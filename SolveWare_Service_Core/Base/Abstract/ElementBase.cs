using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SolveWare_Service_Core.Base.Abstract
{
    public class ElementBase : IElement, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public void UpdateProper<T>(ref T properValue, T newValue, [CallerMemberName] string properName = "")
        {
            if (object.Equals(properValue, newValue))
                return;

            properValue = newValue;
            OnPropertyChanged(properName);
        }
        public void UpdateProperAction<T>(ref T properValue, T newValue, [CallerMemberName] string properName = "", Action ac = null)
        {
            if (object.Equals(properValue, newValue))
                return;

            properValue = newValue;
            OnPropertyChanged(properName);
            if (ac != null)
                ac();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string info = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(info));
        }
        #endregion INotifyPropertyChanged

        protected bool Get_Result(string jobName, string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                SolveWare.Core.MMgr.Infohandler.LogMessage($"[{Name} 功能 {jobName}] : 成功");
                return true;
            }
            else
            {
                SolveWare.Core.MMgr.Infohandler.LogMessage($"[{Name} 功能 {jobName}] : 失败\r\n{message}", true, true);
                return false;
            }
        }

        public string Name { get; set; }
        public string Content { get; set; }

        protected long id =0;
        [XmlIgnore]
        public long Id { get => id; private set => id =value; }

        public ElementBase()
        {
            if(Id == 0)
                Id = IdentityGenerator.IG.GetIdentity();
        }

        public virtual void UpdateContent()
        {

        }
    }
}
