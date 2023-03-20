using SolveWare_Service_Core.Index.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Core.Index.Base.Abstract
{
    public abstract class IndexBase : IIndex, ILoadCargo
    {
        IIndexData data;
        public void Setup(IIndexData data)
        {
            this.data = data;
        }

        public abstract int Go(int number);

        public abstract int GoNext();

        public abstract int GoPrevisou();

        public abstract int Go_Load_Pos();

        public abstract int Go_UnLoad_Pos();

        public abstract int Move_Vision_To_PickTool_Offset();

        public abstract int Pick_Cargo_From_Tray();

        public abstract int Put_Back_Cargo_To_Tray();

        public abstract int Vision_Align_Load_Cargo();

        public abstract int Vision_Align_UnLoad_Cargo();
    }
}
