﻿using SolveWare_Service_Core.Attributes;
using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.General;
using SolveWare_Service_Core.Manager.Base.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SolveWare_Service_Core.Manager.Business
{
    public class Resource_Tool_Manager<TConfigData> : IToolResourceProvider where TConfigData : IElement
    {
        private RESTFulBase<TConfigData> RESTFul = null;
        private string FilePath = string.Empty;

        public IFactory Factory { get; private set; }
        public IList<IElement> WareHouse { get; private set; }
        public string ResourceKey { get; private set; }
        public string Name { get; set; }
        public IList<IElement> Configurations
        {
            get;
            set;
        }
        public Resource_Tool_Manager(IFactory factory)
        {
            this.Factory = factory;
            Name = $"Resource_Tool_{typeof(TConfigData).Name}";
            this.FilePath = Path.Combine(SystemPath.GetSystemPath, $"{this.Name}.xml");
            this.RESTFul = new RESTFulBase<TConfigData>() { FilePath = this.FilePath };

            var customeKeys = typeof(TConfigData).GetCustomAttributes();
            foreach (var item in customeKeys)
            {
                if(item is ResourceBaseAttribute)
                {
                    this.ResourceKey = (item as ResourceBaseAttribute).ResourceKey;
                }
            }
        }

        public IList<IElement> Get_All_Items()
        {
            return this.WareHouse.ToList().ConvertAll(x => x as IElement);
        }

        public IList<string> Get_All_Item_Name()
        {
            List<string> names = new List<string>();
            this.Get_All_Items().ToList().ForEach(x => names.Add(x.Name));

            return names;
        }

        public IElement Get_Single_Item(string name)
        {
            IElement obj = this.Get_All_Items().ToList().ConvertAll(x => x as IElement).Find(x => x.Name == name);
            return obj;
        }

        public bool Initialize()
        {
            bool isOK = false;

            try
            {
                do
                {
                    if (Load() == false) break;
                    if (CreateObject() == false) break;

                } while (false);

                isOK = true;
            }
            catch (Exception ex)
            {
                SolveWare.Core.MMgr.Infohandler.LogMessage($"{this.Name} Initializing Failed\r\n {ex.Message}", true, true);
            }

            return isOK;
        }

        public bool Load()
        {
            bool isOk = false;
            if (string.IsNullOrEmpty(FilePath)) throw new Exception($"无档案路径");
            this.RESTFul.DataBase = new List<TConfigData>();
            List<TConfigData> tempConfigDatas = new List<TConfigData>();
            try
            {
                if (!SystemPath.IsFileExisted(FilePath))
                {
                    for (int i = 1; i <= 1; i++)
                    {
                        var configData = Activator.CreateInstance(typeof(TConfigData));
                        tempConfigDatas.Add((TConfigData)configData);
                        this.AddSingleData(configData as IElement);
                    }

                    XMLHelper.Save(tempConfigDatas, FilePath);
                }
                else
                {
                    var tempList = XMLHelper.Load<List<TConfigData>>(FilePath);
                    this.RESTFul.DataBase.Clear();
                    foreach (var item in tempList)
                    {
                        this.RESTFul.DataBase.Add(item);
                    }
                }

                isOk = true;
            }
            catch (Exception ex)
            {
                SolveWare.Core.MMgr.Infohandler.LogMessage($"{this.GetType().Name} 资料下载失败\r\n{ex.Message}", true, true);
            }

            return isOk;
        }

        public void Save(bool isWindowShowMsg = true)
        {
            try
            {
                //List<TConfigData> tempList = new List<TConfigData>();
                //this.DataBase.ToList().ForEach(x => tempList.Add((TConfigData)x));
                
                XMLHelper.Save(RESTFul.DataBase.ToList(), FilePath);
                SolveWare.Core.MMgr.Infohandler.LogMessage($"储存 成功", isWindowShow: isWindowShowMsg);
                //SolveWare.Core.MMgr.Infohandler.PopUpHandyControlMessage($"[{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}] 储存 成功");
            }
            catch (Exception ex)
            {
                SolveWare.Core.MMgr.Infohandler.LogMessage($"储存 成功", isWindowShow: isWindowShowMsg, true);
                //SolveWare.Core.MMgr.Infohandler.PopUpHandyControlMessage($"[{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}] 储存 失败{Environment.NewLine}{ex.Message}");
            }
        }

        private bool CreateObject()
        {
            bool isOk = false;
            if (Factory == null) throw new Exception("无生产工厂");
            this.WareHouse = new List<IElement>();
            try
            {
                foreach (var item in this.RESTFul.DataBase)
                {
                    IElement tool = Factory.BuildTool(item);
                    if (tool == null ) throw new Exception($"{this.Name} 创造资源物件失败");
                    tool.Name = item.Name;
                    this.WareHouse.Add(tool);
                }

                isOk = true;
            }
            catch (Exception ex)
            {
                SolveWare.Core.MMgr.Infohandler.LogMessage($"{this.Name} 生产物件失败 {Environment.NewLine} {ex.Message}", isWindowShow:true, isError:true);
            }
            if (isOk) Save(false);

            return isOk;
        }

        public void DoubleCheck(params string[] names)
        {
            int totalCount = names.Length;
            List<IElement> correntTools = new List<IElement>();
            List<IElement> corretDatas = new List<IElement>();
            var tools = this.WareHouse.ToList();

            foreach (string name in names)
            {
                int index = tools.FindIndex(x => x.Name == name);
                //没找到
                if (index < 0)
                {
                    TConfigData config = (TConfigData)Activator.CreateInstance(typeof(TConfigData));
                    config.Name = name;
                    IElement tool = Factory.BuildTool(config);
                    tool.Name = name;
                    correntTools.Add(tool);
                    corretDatas.Add(config);
                }
                else
                {
                    correntTools.Add((IElement)WareHouse[index]);
                    IElement data = this.RESTFul.DataBase.ToList().FirstOrDefault(x => x.Name == name);
                    corretDatas.Add(data);
                }
            }

            this.WareHouse.Clear();
            this.RESTFul.DataBase.Clear();
            correntTools.ForEach(x => this.WareHouse.Add(x as IElement));
            corretDatas.ForEach(x => this.RESTFul.DataBase.Add((TConfigData)x));
            Save(false);
        }

        public void Plug_In()
        {
            SolveWare.Core.MMgr.Resource_Tool_Center.Add(this);
        }

        public void StartStatusReading()
        {
            this.WareHouse.ToList().ForEach(x=> (x as IToolElement).StartStatusReading());
        }

        public void StopStatusReading()
        {
            this.WareHouse.ToList().ForEach(x => (x as IToolElement).StopStatusReading());
        }

        public bool SaveSingleData(IElement item)
        {
            return this.RESTFul.SaveSingleData((TConfigData) item);
        }

        public bool AddSingleData(IElement item)
        {
            return this.RESTFul.AddSingleData((TConfigData)item);
        }

        public bool DeleteSingleData(IElement item)
        {
            return this.RESTFul.DeleteSingleData((TConfigData)item);
        }
    }
}
