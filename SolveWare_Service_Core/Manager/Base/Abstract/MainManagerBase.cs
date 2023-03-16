﻿using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.Definition;
using SolveWare_Service_Core.FSM.Base.Interface;
using SolveWare_Service_Core.General;
using SolveWare_Service_Core.Info.Base.Interface;
using SolveWare_Service_Core.Manager.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SolveWare_Service_Core.Manager.Base.Abstract
{
    public abstract class MainManagerBase : IMainManager
    {

        public MainManagerBase(IInfoHandler infoHandler)
        {
            this.Infohandler = infoHandler;
        }
        public MainManagerBase(IInfoHandler infoHandler, IFSM fsm)
        {
            this.Infohandler = infoHandler;
            this.FSM = fsm;
        }

        public IView MainWint { get; set; }
        public IFSM FSM { get; set; }
        public string LoadingStatus { get; set; }
        public IMachineUI MachineUI { get; set; }
        public IInfoHandler Infohandler { get; set; }
        public IList<IDataResourceProvider> Resource_Data_Center { get; set; }
        public IList<IToolResourceProvider> Resource_Tool_Center { get; set; }

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

        public abstract void CloseAll();

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
            IResourceProvider provider = Resource_Data_Center.ToList().FirstOrDefault(x => x.ResourceKey == classType);
            return provider;
        }

        public IResourceProvider Get_Single_Tool_Resource(Type classType)
        {
            IResourceProvider provider = Resource_Tool_Center.ToList().FirstOrDefault(x => x.ResourceKey == classType);
            return provider;
        }
        public IElement Get_Single_Element_Form_Data_Resource(Type classType, string name)
        {
            IResourceProvider provider = Resource_Data_Center.ToList().FirstOrDefault(x => x.ResourceKey == classType);
            IElement data = provider.Get_Single_Item(name);

            return data;
        }

        public IElement Get_Single_Element_Form_Tool_Resource(Type classType, string name)
        {
            IResourceProvider provider = Resource_Tool_Center.ToList().FirstOrDefault(x => x.ResourceKey == classType);
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


        }

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

        public abstract bool InitDataResource();
        public abstract bool InitToolResource();
        public abstract bool InitVisionResource();
    }
}
