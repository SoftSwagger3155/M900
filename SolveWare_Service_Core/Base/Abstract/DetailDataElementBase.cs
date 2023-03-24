using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Core.Base.Abstract
{
    public class DetailDataElementBase: ElementBase
    {
        int priority = 1;
        public int Priority
        {
            get => priority;
            set => UpdateProperAction(ref priority, value, ac: UpdateContent);
        }

        bool isSelected = false;
        public bool IsSelected
        {
            get => isSelected;
            set=> UpdateProper(ref isSelected, value);
        }
    }
}
