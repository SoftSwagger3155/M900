using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.Definition;
using SolveWare_Service_Core.FSM.Base.Interface;
using SolveWare_Service_Core.General;
using SolveWare_Service_Core.Info.Base.Interface;
using SolveWare_Service_Core.Info.Business;
using SolveWare_Service_Core.Manager.Base.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SolveWare_Service_Core.Manager.Base.Abstract
{
    public delegate void AddResourceDelegation();
    public abstract class MainManagerBase : IMainManager
    {
        public MainManagerBase()
        {
            this.Infohandler = InfoHandler.Instance;
            ErrorCodes.InitErrorMap();
            string fullPath = "";//ConfigurationManager.AppSettings["FilePathRoot"];
            fullPath = Debugger.IsAttached ? Directory.GetCurrentDirectory() : fullPath;
            SystemPath.RootInfoDirection = $@"{fullPath}";
            SystemPath.RootLogDirectory = $@"{fullPath} Logs";
            SystemPath.RootDataDirectory = $@"{fullPath} Data";
            SystemPath.CreateDefaultDirectory(true);

            Resource_Tool_Center = new List<IToolResourceProvider>();
            Resource_Data_Center = new List<IDataResourceProvider>();
            Resource_DataPair_Center = new List<ICommonJobFundamental>();
        }
        public MainManagerBase(IInfoHandler infoHandler)
        {
            this.Infohandler = infoHandler;
        }

        public IFSM FSM_Home { get; set; }
        public IFSM FSM_Auto { get; set; }
        public IFSM FSM_Reset { get; set; }


        public event AddResourceDelegation On_Tool_Resource_Loading_Handler;
        public event AddResourceDelegation On_Data_Resource_Loading_Handler;
        public event AddResourceDelegation On_Machine_Resource_Loading_Handler;
        public IView MainWint { get; set; }
        public string LoadingStatus { get; set; }
        public IMachineUI MachineUI { get; set; }
        public IInfoHandler Infohandler { get; set; }
        public IList<IDataResourceProvider> Resource_Data_Center { get; set; }
        public IList<IToolResourceProvider> Resource_Tool_Center { get; set; }
        public IList<ICommonJobFundamental> Resource_DataPair_Center { get; set; }

        protected Machine_Status status = Machine_Status.UnInitialised;
        public Machine_Status Status { get; private set; }

        public bool IsStop
        {
            get
            {
                bool stopStatus = this.Status == Machine_Status.Stop || this.Status == Machine_Status.Error;
                return stopStatus;
            }
        }

        public virtual void CloseAll()
        {
            if (this.masterDriver != null)
                masterDriver.Close();

        }

        public void DoButtonClickTask(Func<int> action)
        {
            if (Status == Machine_Status.Busy) return;
            if (Status == Machine_Status.Initialising) return;
            if (Status == Machine_Status.SingleCycle) return;
            if (Status == Machine_Status.Auto) return;
            
            int errorCode = ErrorCodes.NoError;
            List<Task> tasks = new List<Task>();
            tasks.Add(Task.Run(() =>
            {
                errorCode = action();
                if (errorCode != ErrorCodes.NoError) Status = Machine_Status.Error;
            }));
            Task.WaitAll(tasks.ToArray());
        }

        public IResourceProvider Get_Single_Data_Resource(Type classType)
        {
            IResourceProvider provider = null; //Resource_Data_Center.ToList().FirstOrDefault(x => x.ResourceKey == classType);
            return provider;
        }

        public IResourceProvider Get_Single_Tool_Resource(Type classType)
        {
            IResourceProvider provider = null;//Resource_Tool_Center.ToList().FirstOrDefault(x => x.ResourceKey == classType);
            return provider;
        }
        public IElement Get_Single_Element_Form_Data_Resource(Type classType, string name)
        {
            IResourceProvider provider = null;//Resource_Data_Center.ToList().FirstOrDefault(x => x.ResourceKey == classType);
            IElement data = provider.Get_Single_Item(name);

            return data;
        }

        public IElement Get_Single_Element_Form_Tool_Resource(Type classType, string name)
        {
            IResourceProvider provider = null;//Resource_Tool_Center.ToList().FirstOrDefault(x => x.ResourceKey == classType);
            IElement data = provider.Get_Single_Item(name);

            return data;
        }

        public abstract bool HasIdenticalWindow();

        public void Initialize()
        {

            LoadingStatus = "Info Handler 加载...";
            if(this.Infohandler == null)
            {
                SetStatus(Machine_Status.Error_System_Loading);
                LoadingStatus = "Error.... Info Handler 加载失败";
                return;
            }

            if (masterDriver.Init() == false)
            {
                SetStatus(Machine_Status.Error_System_Loading);
                LoadingStatus = "Error.... 驱动 加载失败";
                return;
            }

            InitToolResource();
            InitDataResource();
            InitMachineResource();
        }

        public abstract void Setup();

        public void SetStatus(Machine_Status status)
        {
            this.Status = status;
        }

        protected IMasterDriver masterDriver;
        public IMasterDriver MasterDriver { get => masterDriver; }
   
        public void AssignMasterDriver(IMasterDriver master)
        {
            this.masterDriver = master;
        }

        public  void InitDataResource()
        {
            if (On_Data_Resource_Loading_Handler != null) On_Data_Resource_Loading_Handler();
        }
        public  void InitToolResource()
        {
            if (On_Tool_Resource_Loading_Handler != null) On_Tool_Resource_Loading_Handler();
        }
        public void InitMachineResource()
        {
            if (On_Machine_Resource_Loading_Handler != null) On_Machine_Resource_Loading_Handler();

        }

        public IResourceProvider Get_Single_Tool_Resource(Tool_Resource_Kind kind)
        {
            IResourceProvider provider = null;
            switch (kind)
            {
                case Tool_Resource_Kind.Motor:
                    provider = this.Resource_Tool_Center.ToList().FirstOrDefault(x => x.ResourceKey == ConstantProperty.Motor);
                    break;
                case Tool_Resource_Kind.IO:
                    provider = this.Resource_Tool_Center.ToList().FirstOrDefault(x => x.ResourceKey == ConstantProperty.IO);
                    break;
                case Tool_Resource_Kind.Camera:
                    provider = this.Resource_Tool_Center.ToList().FirstOrDefault(x => x.ResourceKey == ConstantProperty.Camera);
                    break;
                case Tool_Resource_Kind.Lighting:
                    provider = this.Resource_Tool_Center.ToList().FirstOrDefault(x => x.ResourceKey == ConstantProperty.Lighting);
                    break;
                case Tool_Resource_Kind.BarCode_Gun:
                    provider = this.Resource_Tool_Center.ToList().FirstOrDefault(x => x.ResourceKey == ConstantProperty.BarCodeGun);
                    break;
                case Tool_Resource_Kind.Printer:
                    provider = this.Resource_Tool_Center.ToList().FirstOrDefault(x => x.ResourceKey == ConstantProperty.Printer);
                    break;
            }

            return provider;
        }

        public IElement Get_Single_Element_Form_Tool_Resource(Tool_Resource_Kind kind, string name)
        {
            IElement element = null;
            try
            {
                IResourceProvider provider = Get_Single_Tool_Resource(kind);
                if (provider == null) { return null; }
                
                element = provider.Get_Single_Item(name);
            }
            catch (Exception)
            {
                return null;
            }

            return element;
        }

        public IResourceProvider Get_Single_Data_Resource(string resourceKey)
        {
            IResourceProvider provider = Resource_Data_Center.ToList().FirstOrDefault(x => x.ResourceKey == resourceKey);
            return provider;
        }

        public IElement Get_Single_Element_Form_Data_Resource(string resourceKey, string name)
        {
            IResourceProvider provider = Get_Single_Data_Resource(resourceKey);
            IElement element = provider.Get_Single_Item(name);

            return element;
        }
    }
}
