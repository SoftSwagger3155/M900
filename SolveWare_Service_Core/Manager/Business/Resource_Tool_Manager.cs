using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.General;
using SolveWare_Service_Core.Manager.Base.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Core.Manager.Business
{
    public class Resource_Tool_Manager<TConfigData> : RESTFulBase, IToolResourceProvider where TConfigData : IElement
    {
        public IFactory Factory { get; private set; }
        public IList<IElement> WareHouse { get; private set; }
        public Type ResourceKey { get; private set; }
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
        }

        public Resource_Tool_Manager(IFactory factory, Type resourceKey)
        {
            this.Factory = factory;
            this.ResourceKey = resourceKey;
            Name = $"Resource_Tool_{typeof(TConfigData).Name}";
            this.FilePath = Path.Combine(SystemPath.GetSystemPath, $"{this.Name}.xml");
        }

        public void AssignResourceKey(Type resourceKey)
        {
            this.ResourceKey = resourceKey;
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
            this.DataBase.Clear();

            try
            {
                if (!SystemPath.IsFileExisted(FilePath))
                {
                    for (int i = 1; i <= 1; i++)
                    {
                        var configData = Activator.CreateInstance(typeof(TConfigData));
                        this.AddSingleData(configData as IElement);
                    }

                    XMLHelper.Save<List<TConfigData>>((this.DataBase.ToList() as List<TConfigData>), FilePath);
                }
                else
                {
                    var tempList = XMLHelper.Load<List<TConfigData>>(FilePath);
                    this.DataBase.Clear();
                    foreach (var item in tempList)
                    {
                        this.DataBase.Add(item);
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

        public void Save()
        {
            try
            {
                XMLHelper.Save<List<TConfigData>>((this.DataBase.ToList() as List<TConfigData>), FilePath);
                SolveWare.Core.MMgr.Infohandler.PopUpHandyControlMessage($"[{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}] 储存 成功");
            }
            catch (Exception ex)
            {
                SolveWare.Core.MMgr.Infohandler.PopUpHandyControlMessage($"[{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}] 储存 失败{Environment.NewLine}{ex.Message}");
            }
        }

        private bool CreateObject()
        {
            bool isOk = false;
            if (Factory == null) throw new Exception("无生产工厂");

            try
            {
                foreach (var item in this.DataBase)
                {
                    IElement tool = Factory.BuildTool(item);
                    if (tool == null) throw new Exception($"{this.Name} 创造资源物件失败");
                    this.WareHouse.Add(tool);
                }

                isOk = true;
            }
            catch (Exception ex)
            {
                SolveWare.Core.MMgr.Infohandler.PopUpHandyControlMessage($"{this.Name} 生产物件失败 {Environment.NewLine} {ex.Message}");
            }

            return isOk;
        }
    }
}
