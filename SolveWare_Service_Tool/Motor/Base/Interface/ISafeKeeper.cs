using SolveWare_Service_Tool.Motor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Tool.Motor.Base.Interface
{
    public interface ISafeKeeper
    {
        bool IsPositionDangerous(PositionSafeData pSafeData);
        void RunSafeKeeper();
        void StopSafeKeeper();
        bool CheckZoneSafeToMove(double pos);

        bool MoveToObserverPos(PositionSafeData pSafeData);
    }
}
