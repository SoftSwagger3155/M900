using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.Definition;
using SolveWare_Service_Core.FSM.Base.Interface;
using SolveWare_Service_Core.Info.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Core.Manager.Base.Interface
{
    public interface IMainManager : IMachineStatus
    {
        IView MainWint { get; set; }        
        IFSM FSM_Home { get; set; }
        IFSM FSM_Auto { get; set; }
        IFSM FSM_Reset { get; set; }

        IMasterDriver MasterDriver { get; }
        void AssignMasterDriver(IMasterDriver master);
        bool HasIdenticalWindow();
        string LoadingStatus { get; set; }
        void Initialize();
        void Setup();
        void DoButtonClickTask(Func<int> action);
        IMachineUI MachineUI { get; set; }
        IInfoHandler Infohandler { get; set; }
        IList<IDataResourceProvider> Resource_Data_Center { get; set; }
        IList<IToolResourceProvider> Resource_Tool_Center { get; set; }
        IList<ICommonJobFundamental> Resource_DataPair_Center { get; set; }
        IResourceProvider Get_Single_Data_Resource(string resourceKey);
        IElement Get_Single_Element_Form_Data_Resource(string resourceKey, string name);
        IResourceProvider Get_Single_Tool_Resource(Tool_Resource_Kind kind);
        IElement Get_Single_Element_Form_Tool_Resource(Tool_Resource_Kind kind, string name);
        void CloseAll();
    }
}
