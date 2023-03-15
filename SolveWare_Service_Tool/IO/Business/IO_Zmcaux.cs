using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Tool.IO.Base.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Tool.IO.Business
{
    public class IO_Zmcaux : IOBase
    {
        public IO_Zmcaux(IElement data) : base(data)
        {
        }

        public override void Off()
        {
            throw new NotImplementedException();
        }

        public override void On()
        {
            throw new NotImplementedException();
        }

        public override void UpdateStatus()
        {
            throw new NotImplementedException();
        }
    }
}
