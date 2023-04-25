using SolveWare_Service_Core.Attributes;
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
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SolveWare_Service_Core.Manager.Base.Abstract
{
    public delegate void AddResourceDelegation();
    public abstract class MainManagerBase : IMainManager
    {
        public bool Is_Ready_Home
        {
            get;
            protected set;
        }

        public MainManagerBase()
        {
            this.Is_Ready_Home = false;
            string fullPath = "";//ConfigurationManager.AppSettings["FilePathRoot"];
            fullPath = Debugger.IsAttached ? Directory.GetCurrentDirectory() : fullPath;
            SystemPath.RootInfoDirection = $@"{fullPath}";
            SystemPath.RootLogDirectory = $@"{fullPath} Logs";
            SystemPath.RootDataDirectory = $@"{fullPath} Data";
            SystemPath.CreateDefaultDirectory(true);
            this.Infohandler = InfoHandler.Instance;
            ErrorCodes.InitErrorMap();

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
            this.Resource_Tool_Center.ToList().ForEach(tool => tool.StopStatusReading());   

            if (this.masterDriver != null)
                masterDriver.Close();
        }

        public void DoButtonClickActionTask(Func<Mission_Report> action)
        {
            if (this.Is_Machine_In_Action)
            {
                var result = MessageBox.Show("机器状态运行中\r\n继续请按 是\r\n结束请按 否", "提问", MessageBoxButtons.YesNo);
                if (result == DialogResult.No) return;
            }

            Task task = Task.Factory.StartNew((object obj) =>
            {
                try
                {                 
                    this.Status = Machine_Status.Busy;
                    Data_Mission_Report mReport = obj as Data_Mission_Report;
                    mReport.Context = action();

                    Machine_Status mStatus = Machine_Status.Idle;
                    if (mReport.Context.ErrorCode == ErrorCodes.MachineStopCall)
                    {
                        mStatus = Machine_Status.Stop;
                    }
                    else
                    {
                        mStatus = mReport.Context.ErrorCode == ErrorCodes.NoError ? Machine_Status.Idle : Machine_Status.Error;
                    }

                    this.SetStatus(mStatus);

                }
                catch (Exception ex)
                {
                    this.SetStatus(Machine_Status.Error);
                    Mission_Report context = new Mission_Report();
                    context.Window_Show_Not_Pass_Message(ErrorCodes.ActionFailed, ex.Message);
                }
                
            }, new Data_Mission_Report());
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

            //run status reading
            SolveWare.Core.MMgr.Resource_Tool_Center.ToList().ForEach(x => x.StartStatusReading());
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
                    provider = this.Resource_Tool_Center.ToList().FirstOrDefault(x => x.ResourceKey == ConstantProperty.ReosurceKey_Motor);
                    break;
                case Tool_Resource_Kind.IO:
                    provider = this.Resource_Tool_Center.ToList().FirstOrDefault(x => x.ResourceKey == ConstantProperty.ResourceKey_IO);
                    break;
                case Tool_Resource_Kind.Camera:
                    provider = this.Resource_Tool_Center.ToList().FirstOrDefault(x => x.ResourceKey == ConstantProperty.ResourceKey_Camera);
                    break;
                case Tool_Resource_Kind.Lighting:
                    provider = this.Resource_Tool_Center.ToList().FirstOrDefault(x => x.ResourceKey == ConstantProperty.ResourceKey_Lighting);
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

        public ICommonJobFundamental Get_PairJob(string jobName)
        {
            ICommonJobFundamental job = this.Resource_DataPair_Center.FirstOrDefault(x=> (x as IElement).Name ==  jobName);
            return job;
        }

        public void Do_Homing()
        {
            if(FSM_Home == null)
            {
                this.Infohandler.LogMessage("无FSM Home 物件", true, true);
                return;
            }

            Mission_Report context = FSM_Home.Run_One_Cycle();
            if(context.ErrorCode == ErrorCodes.NoError)
            {
               if(!this.Is_Ready_Home) this.Is_Ready_Home = true;
            }
            
        }
        public void Do_AutoCycle()
        {

        }
        public void Do_SingleCycle()
        {

        }
        public void Do_Resset()
        {

        }

        public abstract void AssignFSM();

        public IList<ICommonJobFundamental> Get_Identical_ReosurcBase_Job(string resourceBaseName)
        {
            if (Resource_DataPair_Center.Count == 0) return null;
            var allItems = Resource_DataPair_Center.ToList().FindAll(x =>
            {
                Type type = x.GetType();
                ResourceBaseAttribute attr = (ResourceBaseAttribute)type.GetCustomAttribute(typeof(ResourceBaseAttribute));
                return attr.ResourceKey == resourceBaseName;
            });

            return allItems;
        }

        public abstract void Stop(bool stopMotor= true);
        public bool Is_Machine_In_Action
        {
              get=>   Status == Machine_Status.Busy ||
                           Status == Machine_Status.Initialising ||
                           Status == Machine_Status.SingleCycle ||
                           Status == Machine_Status.Auto;
        }
    }
}
