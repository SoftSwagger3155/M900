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
using System.Xml.Serialization;

namespace SolveWare_Service_Core.Manager.Business
{
    public class Resource_Data_Manager<TData> : IDataResourceProvider where TData:IElement
    {
        private RESTFulBase<TData> RESTFul;
        private string FilePath = string.Empty;
        public string ResourceKey { get; private set; }
        public string Name { get; set; }
       
        public Resource_Data_Manager()
        {
            Name = $"Resource_Data_{typeof(TData).Name}";
            this.FilePath = Path.Combine(SystemPath.GetSystemPath, $"{this.Name}.xml");
            RESTFul = new RESTFulBase<TData>() { FilePath = this.FilePath};

            var customeKeys = typeof(TData).GetCustomAttributes();
            foreach (var item in customeKeys)
            {
                if (item is ResourceBaseAttribute)
                {
                    this.ResourceKey = (item as ResourceBaseAttribute).ResourceKey;
                }
            }
        }
       
        public IList<IElement> Get_All_Items()
        {
            return RESTFul.DataBase.ToList().ConvertAll(x=> x as IElement);
        }

        public IList<string> Get_All_Item_Name()
        {
            List<string> list = new List<string>();
            RESTFul.DataBase.ToList().ForEach(x => list.Add((x as IElement).Name));

            return list;
        }

        public IElement Get_Single_Item(string name)
        {
            return Get_All_Items().FirstOrDefault(x => (x as IElement).Name == name);
        }

        public bool Initialize()
        {
            bool isOk = false;

            try
            {
                do
                {
                    if (Load() == false) break;

                } while (false);
            }
            catch (Exception ex)
            {
                SolveWare.Core.MMgr.Infohandler.LogMessage($"初始化 {Name} 失败\r\n{ex.Message}");
            }


            return isOk;
        }

        public bool Load()
        {
            bool isOk = false;
            if (string.IsNullOrEmpty(FilePath)) throw new Exception($"无档案路径");
            this.RESTFul.DataBase = new List<TData>();

            try
            {
                if (SystemPath.IsFileExisted(FilePath))
                {
                    var tempList = XMLHelper.Load<List<TData>>(FilePath);
                    foreach (var item in tempList)
                    {
                        this.RESTFul.DataBase.Add(item);
                    }
                    isOk = true;
                }
            }
            catch (Exception ex)
            {
                SolveWare.Core.MMgr.Infohandler.LogMessage($"{this.GetType().Name} 资料下载失败\r\n{ex.Message}");
            }

            return isOk;
        }

        public void Save(bool isWindowShowMsg = true)
        {
            try
            {
                //List<TData> tempList = new List<TData>();
                //this.DataBase.ToList().ForEach(x => tempList.Add((TData)x));
                XMLHelper.Save(RESTFul.DataBase.ToList(), FilePath);

                //XMLHelper.Save<List<TData>>((this.DataBase.ToList() as List<TData>), FilePath);
                SolveWare.Core.MMgr.Infohandler.LogMessage($"储存 成功", isWindowShow: isWindowShowMsg);
            }
            catch (Exception ex)
            {
                SolveWare.Core.MMgr.Infohandler.LogMessage($"储存 失败{Environment.NewLine}{ex.Message}", true);
            }
        }

        public void DoubleCheck(params string[] names)
        {
            int totalCount = names.Length;
            List<TData> correctDatas = new List<TData>();
            var databases = this.RESTFul.DataBase.ToList();

            foreach (string name in names)
            {
                int index = databases.FindIndex(x => x.Name == name);
               //没找到
                if(index < 0)
                {
                    TData data = (TData)Activator.CreateInstance(typeof(TData));
                    (data as IElement).Name = name;
                    correctDatas.Add(data);
                }
                else
                {
                    correctDatas.Add((TData)databases[index]);
                }
            }


            this.RESTFul.DataBase.Clear();
            correctDatas.ForEach(x => this.RESTFul.DataBase.Add(x));
            Save(false);
        }
        public void Plug_In()
        {
            SolveWare.Core.MMgr.Resource_Data_Center.Add(this);
        }

        public bool SaveSingleData(IElement item)
        {
            return this.RESTFul.SaveSingleData((TData)item);
        }

        public bool AddSingleData(IElement item)
        {
            return this.RESTFul.AddSingleData((TData)item);
        }

        public bool DeleteSingleData(IElement item)
        {
            return this.RESTFul.DeleteSingleData((TData)item);
        }
    }
}
