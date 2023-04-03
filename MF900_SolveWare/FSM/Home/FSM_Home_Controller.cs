using MF900_SolveWare.FSM.Home.Stations;
using SolveWare_Service_Core.FSM.Base.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF900_SolveWare.FSM.Home
{
    public class FSM_Home_Controller : FSMBase
    {
        private bool isSimulation;
        public FSM_Home_MachineEvent McEvent { get; private set; }
        public FSM_Home_Station TopStation { get; private set; }
        public FSM_Home_Station BtmStation { get; private set; }


        public FSM_Home_Controller(bool isSimulation)
        {
            this.isSimulation = isSimulation;
            McEvent = new FSM_Home_MachineEvent();
            CreateStations();
        }

        private void CreateStations()
        {
            Add_Station(
               TopStation = new FSM_Home_Station(HomeStation_Selector.Up, McEvent, isSimulation),
               BtmStation = new FSM_Home_Station(HomeStation_Selector.Bottom, McEvent, isSimulation)
                );
        }
    }
}
