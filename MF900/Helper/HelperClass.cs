using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Net;
using System.Runtime.InteropServices;

namespace MF900
{
    /// <summary>
    /// 文件操作类
    /// </summary>
    public class FileOPenation
    {
        /// <summary>
        /// 当前时间
        /// </summary>
        public string CurrentTime
        {
            get { return DateTime.Now.ToString("HH:mm:ss"); }
        }
        /// <summary>
        /// 当前日期
        /// </summary>
        public string CurrentDate
        {
            get { return DateTime.Now.ToString("yyyy-MM-dd"); }
        }

        /// <summary>
        /// 创建并写入CSV文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="file">文件路径下的文件</param>
        /// <param name="strValue1">数据1</param>
        /// <param name="strValue2">数据2</param>
        /// <param name="strValue3">数据3</param>
        /// <param name="strValue4">数据4</param>
        /// <param name="tableName1">表头1名称</param>
        /// <param name="tableName2">表头2名称</param>
        /// <param name="tableName3">表头3名称</param>
        /// <param name="tableName4">表头4名称</param>
        public virtual void SaveCSV(string filePath, string file, string strValue1, string strValue2, string strValue3, string strValue4, string tableName1, string tableName2, string tableName3, string tableName4)
        {
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            if (!File.Exists(file))
            {
                FileStream fs1 = new FileStream(file, FileMode.Create, FileAccess.Write);//创建写入文件 
                StreamWriter sw = new StreamWriter(fs1);
                sw.WriteLine(tableName1 + "," + tableName2 + "," + tableName3 + "," + tableName4);
                sw.WriteLine(strValue1 + "," + strValue2 + "," + strValue3 + "," + strValue4);//开始写入值
                sw.Close();
                fs1.Close();
            }
            else
            {
                StreamWriter sr = new StreamWriter(file, true, Encoding.Default);//true可连续写值
                sr.WriteLine(strValue1 + "," + strValue2 + "," + strValue3 + "," + strValue4);//开始写入值
                sr.Close();

            }
        }
        /// <summary>
        /// 创建并写入CSV文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="file"></param>
        /// <param name="strValue1">数据1</param>
        /// <param name="strValue2">数据2</param>
        /// <param name="strValue3">数据3</param>
        /// <param name="tableName1">表头1名称</param>
        /// <param name="tableName2">表头2名称</param>
        /// <param name="tableName3">表头3名称</param>
        public virtual void SaveCSV(string filePath, string file, string strValue1, string strValue2, string strValue3, string tableName1, string tableName2, string tableName3)
        {
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            if (!File.Exists(filePath + file))
            {
                FileStream fs1 = new FileStream(filePath + file, FileMode.Create, FileAccess.Write);//创建写入文件 
                StreamWriter sw = new StreamWriter(fs1);
                sw.WriteLine(tableName1 + "," + tableName2 + "," + tableName3);
                sw.WriteLine(strValue1 + "," + strValue2 + "," + strValue3);//开始写入值
                sw.Close();
                fs1.Close();
            }
            else
            {
                StreamWriter sr = new StreamWriter(filePath + file, true, Encoding.Default);//true可连续写值
                sr.WriteLine(strValue1 + "," + strValue2 + "," + strValue3);//开始写入值
                sr.Close();

            }
        }
        /// <summary>
        /// 创建并写入CSV文件
        /// </summary>
        /// <param name="filePath">文件夹路径</param>
        /// <param name="file">文件</param>
        /// <param name="strValue1">数据1</param>
        /// <param name="strValue2">数据2</param>
        /// <param name="tableName1">表头1名称</param>
        /// <param name="tableName2">表头2名称</param>
        public virtual void SaveCSV(string filePath, string file, string strValue1, string strValue2, string tableName1, string tableName2)
        {
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            if (!File.Exists(file))
            {
                FileStream fs1 = new FileStream(file, FileMode.Create, FileAccess.Write);//创建写入文件 
                StreamWriter sw = new StreamWriter(fs1);
                sw.WriteLine(tableName1 + "," + tableName2);
                sw.WriteLine(strValue1 + "," + strValue2);//开始写入值
                sw.Close();
                fs1.Close();
            }
            else
            {
                StreamWriter sr = new StreamWriter(file, true, Encoding.Default);//true可连续写值
                sr.WriteLine(strValue1 + "," + strValue2);//开始写入值
                sr.Close();

            }
        }
        /// <summary>
        /// 创建并写入CSV文件
        /// </summary>
        /// <param name="filePath">文件夹路径</param>
        /// <param name="file">文件</param>
        /// <param name="strValue1">数据1</param>
        /// <param name="tableName1">表头1名称</param>
        public virtual void SaveCSV(string filePath, string file, string strValue1, string tableName1)
        {
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            if (!File.Exists(file))
            {
                FileStream fs1 = new FileStream(file, FileMode.Create, FileAccess.Write);//创建写入文件 
                StreamWriter sw = new StreamWriter(fs1);
                sw.WriteLine(tableName1);
                sw.WriteLine(strValue1);//开始写入值
                sw.Close();
                fs1.Close();
            }
            else
            {
                StreamWriter sr = new StreamWriter(file, true, Encoding.Default);//true可连续写值
                sr.WriteLine(strValue1);//开始写入值
                sr.Close();
            }
        }
        /// <summary>
        /// 读取CSV文件（返回table）
        /// </summary>
        /// <param name="filePath">文件</param>
        /// <param name="dt">输出DataTable</param>
        /// <returns></returns>
        public bool ReadCSV(string filePath, out DataTable dt)
        {
            dt = new DataTable();
            try
            {
                System.Text.Encoding encoding = Encoding.Default;
                System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                System.IO.StreamReader sr = new System.IO.StreamReader(fs, encoding);
                //记录每次读取的一行记录
                string strLine = "";
                //记录每行记录中的各字段内容
                string[] aryLine = null;
                string[] tableHead = null;
                //标示列数
                int columnCount = 0;
                //标示是否是读取的第一行
                bool IsFirst = true;
                //逐行读取CSV中的数据
                while ((strLine = sr.ReadLine()) != null)
                {
                    if (IsFirst == true)
                    {
                        tableHead = strLine.Split(',');
                        IsFirst = false;
                        columnCount = tableHead.Length;
                        //创建列
                        for (int i = 0; i < columnCount; i++)
                        {
                            DataColumn dc = new DataColumn(tableHead[i]);
                            dt.Columns.Add(dc);
                        }
                    }
                    else
                    {
                        aryLine = strLine.Split(',');
                        DataRow dr = dt.NewRow();
                        for (int j = 0; j < columnCount; j++)

                        {
                            dr[j] = aryLine[j];
                        }
                        dt.Rows.Add(dr);
                    }
                }

                if (aryLine != null && aryLine.Length > 0)

                {
                    dt.DefaultView.Sort = tableHead[0] + " " + "asc";
                }
                sr.Close();
                fs.Close();
                return true;
            }

            catch (Exception)

            {
                return false;
            }
        }
        /// <summary>
        /// 读取具体行列数据
        /// </summary>
        /// <param name="filePath">文件</param>
        /// <param name="row">具体行</param>
        /// <param name="column">具体列</param>
        /// <returns></returns>
        public string ReadCsvData(string filePath,int row,int column)
        {
            DataTable data = new DataTable();
            ReadCSV(filePath, out data);
            try
            {
                if (data.Rows.Count>row && data.Columns.Count>column)
                {
                    object objValue = data.Rows[row][column];
                    return objValue.ToString();
                }
                else
                {
                    return "不存在该数据";
                }
            }
            catch (Exception ex)
            {
                return "读取失败";
            }
            
        }

        /// <summary>
        /// 定期删除文件夹
        /// </summary>
        /// <param name="fileDirect">文件夹路径</param>
        /// <param name="saveDay">保存天数</param>
        public void DeleteDirectory(string fileDirect, int saveDay)
        {
            DateTime nowTime = DateTime.Now;
            DirectoryInfo root = new DirectoryInfo(fileDirect);
            DirectoryInfo[] dics = root.GetDirectories();//获取文件夹

            FileAttributes attr = File.GetAttributes(fileDirect);
            if (attr == FileAttributes.Directory)//判断是不是文件夹
            {
                foreach (DirectoryInfo file in dics)//遍历文件夹
                {
                    TimeSpan t = nowTime - file.CreationTime;  //当前时间  减去 文件创建时间
                    int day = t.Days;
                    if (day > saveDay)   //保存的时间 ；  单位：天
                    {
                        Directory.Delete(file.FullName, true);  //删除超过时间的文件夹
                    }
                }

            }
        }
        /// <summary>
        /// 定期删除txt文本
        /// </summary>
        /// <param name="fileDirect"></param>
        /// <param name="saveDay"></param>
        public void DeleteDirectoryTxt(string fileDirect,int saveDay)
        {
            DateTime nowTime = DateTime.Now;
            string[] files = Directory.GetFiles(fileDirect, "*.txt", SearchOption.AllDirectories);  //获取该目录下所有 .txt文件
            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                TimeSpan t = nowTime - fileInfo.LastWriteTime;  //当前时间  减去 文件创建时间
                int day = t.Days;
                if (day > saveDay)   //保存的时间 ；  单位：天
                {
                    File.Delete(file);  //删除超过时间的文件
                }
            }
        }

        /// <summary>
        /// 日志消息
        /// </summary>
        /// <param name="listView">listView控件对象</param>
        /// <param name="imageList">imageList对象</param>
        /// <param name="imageIndex">图片</param>
        /// <param name="info">消息内容</param>
        /// <param name="maxDisplayItems">消息数量</param>
        public void Addlog(ListView listView, ImageList imageList, int imageIndex, string info, int maxDisplayItems)
        {
            if (listView.InvokeRequired)
            {
                listView.Invoke(new Action(() =>
                {
                    if (listView.Items.Count > maxDisplayItems)
                    {
                        listView.Items.RemoveAt(maxDisplayItems);
                    }

                    ListViewItem lstItem = new ListViewItem(" " + DateTime.Now.ToString(), imageIndex);
                    lstItem.SubItems.Add("  " + info);
                    listView.Items.Insert(0, lstItem);
                }));
            }
            else
            {
                if (listView.Items.Count > maxDisplayItems)
                {
                    listView.Items.RemoveAt(maxDisplayItems);
                }

                ListViewItem lstItem = new ListViewItem(" " + DateTime.Now.ToString(), imageIndex);
                lstItem.SubItems.Add("  " + info);
                listView.Items.Insert(0, lstItem);
            }
        }

        /// <summary>
        /// 日志消息并保存text文件
        /// </summary>
        /// <param name="listView">listView对象</param>
        /// <param name="imageList">imageList对象</param>
        /// <param name="imageIndex">图片</param>
        /// <param name="info">消息内容</param>
        /// <param name="maxDisplayItems">消息数量</param>
        /// <param name="filePath">日志文件路径</param>
        /// <param name="fileName">文件名</param>
        public void Addlog(ListView listView, ImageList imageList, int imageIndex, string info, int maxDisplayItems, string filePath, string fileName)
        {
            if (listView.InvokeRequired)
            {
                listView.Invoke(new Action(() =>
                {
                    if (listView.Items.Count > maxDisplayItems)
                    {
                        listView.Items.RemoveAt(maxDisplayItems);
                    }

                    ListViewItem lstItem = new ListViewItem(" " + CurrentTime + "  "+ info, imageIndex);
                    //lstItem.SubItems.Add(info);
                    listView.Items.Insert(0, lstItem);
                }));
            }
            else
            {
                if (listView.Items.Count > maxDisplayItems)
                {
                    listView.Items.RemoveAt(maxDisplayItems);
                }
                
                ListViewItem lstItem = new ListViewItem(" " + CurrentTime + "  " + info, imageIndex);
                //lstItem.SubItems.Add(info);
                listView.Items.Insert(0, lstItem);
            }
            CreateText(filePath, fileName, "   " + info);
        }

        /// <summary>
        /// listView设置和imageList绑定
        /// </summary>
        /// <param name="listView"></param>
        /// <param name="imageList"></param>
        public void InitListView(ListView listView, ImageList imageList)
        {
            listView.SmallImageList = imageList;
            ColumnHeader columnHeader1 = new ColumnHeader() { Name = "dateTime", Text = "日志时间", Width = 200 };
            ColumnHeader columnHeader2 = new ColumnHeader() { Name = "infoString", Text = "日志信息", Width = 220 };
            listView.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2 });

            listView.HeaderStyle = ColumnHeaderStyle.None;
            listView.View = View.Details;
            listView.HideSelection = false;
            listView.SmallImageList = imageList;
        }
        /// <summary>
        /// 创建txt文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="file"></param>
        /// <param name="strValue1"></param>
        public virtual void CreateText(string filePath, string file, string strValue1)
        {
            FileAccess fileAccess = FileAccess.Write;
            FileShare fileShare = FileShare.Write;
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            
            FileMode fileMode = FileMode.Create;
            if (File.Exists(filePath + "\\" + file))
            {
                fileMode = FileMode.Append;
            }
            using (FileStream fs = new FileStream(filePath + "\\" + file, fileMode, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (StreamWriter sr = new StreamWriter(fs, Encoding.Default))
                {
                    sr.WriteLine(DateTime.Now.ToString("G") + strValue1);
                    sr.Close();
                    fs.Close();
                }
            }
        }

    }

    /// <summary>
    /// 类型转换类
    /// </summary>
    public class TypeChange
    {
        /// <summary>
        /// ASICC转字符串
        /// </summary>
        /// <param name="asciiCode">十进制数</param>
        /// <returns></returns>
        public string AssiccChString(int asciiCode)
        {
            if (asciiCode >= 0 && asciiCode <= 255)
            {
                System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                byte[] byteArray = new byte[] { (byte)asciiCode };
                string strCharacter = asciiEncoding.GetString(byteArray);
                return (strCharacter);
            }
            else
            {
                throw new Exception("ASCII Code is not valid.");
            }
        }
        /// <summary>
        /// UTF8转字符串
        /// </summary>
        /// <param name="characters"></param>
        /// <returns></returns>
        public static String UTF8ByteArrayToString(byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            String constructedString = encoding.GetString(characters);
            return (constructedString);
        }
        /// <summary>
        /// 字符串转UTF8
        /// </summary>
        /// <param name="pXmlString"></param>
        /// <returns></returns>
        public static byte[] StringToUTF8ByteArray(String pXmlString)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] byteArray = encoding.GetBytes(pXmlString);
            return byteArray;
        }
        /// <summary>
        /// 字符串截取
        /// </summary>
        /// <param name="sourse">截取对象</param>
        /// <param name="startstr">开始截取字符串</param>
        /// <param name="endstr">终止字符串</param>
        /// <returns></returns>
        public static string MidStrEx(string sourse, string startstr, string endstr)
        {
            string result = string.Empty;
            int startindex, endindex;
            try
            {
                startindex = sourse.IndexOf(startstr);
                if (startindex == -1)
                    return result;
                string tmpstr = sourse.Substring(startindex + startstr.Length);
                endindex = tmpstr.IndexOf(endstr);
                if (endindex == -1)
                    return result;
                result = tmpstr.Remove(endindex);
            }
            catch (Exception ex)
            {
                //Log.WriteLog("MidStrEx Err:" + ex.Message);
                MessageBox.Show(ex.Message);
            }
            return result;
        }
    }

    /// <summary>
    /// 序列化操作
    /// </summary>
    public class SerializeHelper
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T">序列化对象</typeparam>
        /// <param name="Data">实例化对象</param>
        /// <param name="FilePath">文件</param>
        public static void SerializeXml<T>(T Data, string FilePath)
        {
            string directoryName = Path.GetDirectoryName(FilePath);
            if (!directoryName.Equals("") && !Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }
            try
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
                    xmlSerializerNamespaces.Add("", "");
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    xmlSerializer.Serialize(memoryStream, Data, xmlSerializerNamespaces);
                    using (FileStream fileStream = new FileStream(FilePath, FileMode.Create))
                    {
                        byte[] buffer = memoryStream.GetBuffer();
                        fileStream.Write(buffer, 0, buffer.Count<byte>());
                        fileStream.Close();
                    }
                    memoryStream.Close();
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Xml序列化异常" + ex.ToString());
            }
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T">反序列化对象</typeparam>
        /// <param name="FilePath">文件</param>
        /// <returns></returns>
        public static T DeSerializeXml<T>(string FilePath) where T : new()
        {
            string directoryName = Path.GetDirectoryName(FilePath);
            if (!directoryName.Equals("") && !Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }
            T result;
            if (File.Exists(FilePath))
            {
                using (FileStream fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    try
                    {
                        result = (T)((object)xmlSerializer.Deserialize(fileStream));
                    }
                    catch (Exception ex)
                    {
                        result = ((default(T) == null) ? Activator.CreateInstance<T>() : default(T));
                        Trace.WriteLine("Xml反序列化异常" + ex.ToString());
                    }
                    fileStream.Close();
                }
            }
            else
            {
                result = ((default(T) == null) ? Activator.CreateInstance<T>() : default(T));
            }
            return result;
        }
        /// <summary>
        /// 字典序列化
        /// </summary>
        /// <typeparam name="T">SerializableDictionary</typeparam>
        /// <param name="pObject">SerializableDictionary实例化对象</param>
        /// <param name="FilePath">文件路径</param>
        /// <returns></returns>
        public static String SerializeObject<T>(object pObject, string FilePath)
        {
            String XmlizedString = "";
            MemoryStream memoryStream = new MemoryStream();
            XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
            xmlSerializerNamespaces.Add("", "");
            XmlSerializer xs = new XmlSerializer(typeof(T));
            XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
            xs.Serialize(memoryStream, pObject, xmlSerializerNamespaces);
            using (FileStream fileStream = new FileStream(FilePath, FileMode.Create))
            {
                byte[] buffer = memoryStream.GetBuffer();
                fileStream.Write(buffer, 0, buffer.Count<byte>());
                fileStream.Close();
            }
            memoryStream.Close();
            return XmlizedString;
        }

        /// <summary>
        /// 字典序列化(Newtonsoft.Json)
        /// </summary>
        /// <param name="dictionary">字典对象</param>
        /// <param name="filePath">文件</param>
        public static void SerializeDictionary(object dictionary,string filePath)
        {
            string str = JsonConvert.SerializeObject(dictionary, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, str, Encoding.UTF8);
        }
        public static T DeSerializeObject<T>(string filePath)
        {
            return (T)JsonConvert.DeserializeObject<T>(filePath);
        }
    }
    /// <summary>
    /// Ini格式保存
    /// </summary>
    public class IniConfigHelper
    {
        #region API函数声明

        [DllImport("kernel32")]//返回0表示失败，非0为成功
        private static extern long WritePrivateProfileString(string section, string key,
            string val, string filePath);

        [DllImport("kernel32")]//返回取得字符串缓冲区的长度
        private static extern long GetPrivateProfileString(string section, string key,
            string def, StringBuilder retVal, int size, string filePath);


        #endregion

        #region 读Ini文件

        /// <summary>
        /// 读取Ini配置信息
        /// </summary>
        /// <param name="Section">区域</param>
        /// <param name="Key">键</param>
        /// <param name="NoText">空字符串</param>
        /// <param name="iniFilePath">文件路径</param>
        /// <returns>值</returns>
        public static string ReadIniData(string Section, string Key, string NoText, string iniFilePath)
        {
            if (File.Exists(iniFilePath))
            {
                StringBuilder temp = new StringBuilder(1024);
                GetPrivateProfileString(Section, Key, NoText, temp, 1024, iniFilePath);
                return temp.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        #endregion

        #region 写Ini文件
        /// <summary>
        /// 写入Ini配置信息
        /// </summary>
        /// <param name="Section">区域</param>
        /// <param name="Key">键</param>
        /// <param name="Value">值</param>
        /// <param name="iniFilePath">文件路径</param>
        /// <returns>是否成功</returns>
        public static bool WriteIniData(string Section, string Key, string Value, string iniFilePath)
        {
            if (!File.Exists(iniFilePath))
            {
                FileStream fs = new FileStream(iniFilePath, FileMode.Create);
                fs.Close();
            }
            if (File.Exists(iniFilePath))
            {
                long OpStation = WritePrivateProfileString(Section, Key, Value, iniFilePath);
                if (OpStation == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        #endregion
    }

    /// <summary>
    /// 字典序列化对象
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    [XmlRoot("dictionary"), Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
    {
        public SerializableDictionary()
        {
        }

        public SerializableDictionary(IDictionary<TKey, TValue> dictionary)
            : base(dictionary)
        {
        }

        public SerializableDictionary(IEqualityComparer<TKey> comparer)
            : base(comparer)
        {
        }


        public SerializableDictionary(int capacity)
            : base(capacity)
        {
        }

        public SerializableDictionary(int capacity, IEqualityComparer<TKey> comparer)
            : base(capacity, comparer)
        {
        }

        protected SerializableDictionary(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #region IXmlSerializable Members
        public XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// 从对象的 XML 表示形式生成该对象
        /// </summary>
        /// <param name="reader"></param>
        public void ReadXml(XmlReader reader)
        {
            var keySerializer = new XmlSerializer(typeof(TKey));
            var valueSerializer = new XmlSerializer(typeof(TValue));
            bool wasEmpty = reader.IsEmptyElement;
            reader.Read();

            if (wasEmpty)
                return;
            while (reader.NodeType != XmlNodeType.EndElement)
            {
                reader.ReadStartElement("item");
                reader.ReadStartElement("key");
                var key = (TKey)keySerializer.Deserialize(reader);
                reader.ReadEndElement();
                reader.ReadStartElement("value");
                var value = (TValue)valueSerializer.Deserialize(reader);
                reader.ReadEndElement();
                Add(key, value);
                reader.ReadEndElement();
                reader.MoveToContent();
            }
            reader.ReadEndElement();
        }

        /// <summary>
        /// 将对象转换为其 XML 表示形式
        /// </summary>
        /// <param name="writer"></param>
        public void WriteXml(XmlWriter writer)
        {
            var keySerializer = new XmlSerializer(typeof(TKey));
            var valueSerializer = new XmlSerializer(typeof(TValue));
            foreach (TKey key in Keys)
            {
                writer.WriteStartElement("item");
                writer.WriteStartElement("key");
                keySerializer.Serialize(writer, key);
                writer.WriteEndElement();
                writer.WriteStartElement("value");
                TValue value = this[key];
                valueSerializer.Serialize(writer, value);
                writer.WriteEndElement();
                writer.WriteEndElement();
            }
        }
        #endregion
    }

    /// <summary>
    /// SQL Server通用数据访问类
    /// </summary>
    public class SQLHelper
    {
        private static string connString = ConfigurationManager.ConnectionStrings["connString"].ToString();

        /// <summary>
        /// 执行增删改操作的方法
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int Update(string sql, SqlParameter[] param = null)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (param != null)
            {
                cmd.Parameters.AddRange(param);
            }
            try
            {
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //可以在这个地方捕获ex对象相关信息，然后保存到日志文件中...

                throw new Exception("执行方法public static int Update(string sql)发生异常：" + ex.Message);
            }
            finally  //不管前面是否发生异常都要执行的代码
            {
                conn.Close();
            }
        }
        /// <summary>
        /// 执行单一结果返回的查询方法
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static object GetSingleResult(string sql)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                //可以在这个地方捕获ex对象相关信息，然后保存到日志文件中...

                throw new Exception("执行方法 public static object GetSingleResult(string sql)发生异常：" + ex.Message);
            }
            finally  //不管前面是否发生异常都要执行的代码
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 执行返回结果集的查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static SqlDataReader GetReader(string sql)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                //我们添加枚举CommandBehavior.CloseConnection之后，将来reader对象的链接会跟随reader对象的关闭自动关闭
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                //可以在这个地方捕获ex对象相关信息，然后保存到日志文件中...

                throw new Exception("执行方法 public static object GetSingleResult(string sql)发生异常：" + ex.Message);
            }
            //finally  //在这个方法里面，绝对不能直接把链接关掉，否则出错
            //{
            //    conn.Close();
            //}
        }
    }
    /// <summary>
    /// 正则表达式
    /// </summary>
    public class RegexText
    {
        Match match;
        /// <summary>
        /// 判断是否为数值
        /// </summary>
        /// <param name="regexType"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool IsNumber(string regexType, string str)
        {
            Regex reg = new Regex(regexType);
            match = reg.Match(str);
            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    /// <summary>
    /// 通信
    /// </summary>
    public class Communic
    {
        public enum MessageType
        {
            ASCII,
            UTF8,
            Hex,
            File,
            JSON
        }
        #region 服务器

        /// <summary>
        /// 启动服务器
        /// </summary>
        /// <param name="socketServer"></param>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public bool StartServer(Socket socketServer,string ipAddress,int port,out Socket socket)
        {
            IPAddress ip = IPAddress.Parse(ipAddress);
            socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipEndPoint = new IPEndPoint(ip, port);
            try
            {
                socketServer.Bind(ipEndPoint);
            }
            catch (Exception)
            {
                socket = socketServer;
                return false;
            }
            socketServer.Listen(10);
            socket = socketServer;
            return true;
        }
        /// <summary>
        /// 服务器接受消息
        /// </summary>
        /// <param name="socketLient"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool ServerReviceMessage(Socket socketLient, ref string message)
        {
            byte[] buffer = new byte[1024 * 1024 * 10];
            int length = -1;
            try
            {
                length = socketLient.Receive(buffer);
            }
            catch (Exception)
            {
                message = "";
                return false;
            }

            if (length > 0)
            {
                message = Encoding.ASCII.GetString(buffer, 0, length);
            }
            else
            {
                message = "";
            }
            return true;
        }
        /// <summary>
        /// 连接客户端
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        /// <param name="socketClient"></param>
        /// <returns></returns>
        public bool SatrtClient(string ipAddress,int port,Socket socketClient)
        {
            IPAddress ip = IPAddress.Parse(ipAddress);
            socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipPoint = new IPEndPoint(ip, port);
            try
            {
                socketClient.Connect(ipPoint);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        /// <summary>
        /// 客户端接受消息
        /// </summary>
        /// <param name="socketClient"></param>
        /// <param name="message"></param>
        public void ClientReviceMessage(Socket socketClient,out string message)
        {
            while (true)
            {
                byte[] bytes = new byte[1024];
                int leng = -1;
                try
                {
                    leng = socketClient.Receive(bytes);
                }
                catch (Exception)
                {
                    message = "";
                    continue;
                }
                if (leng>0)
                {
                    message = Encoding.Default.GetString(bytes);
                }

            }
        }
        #endregion

        /// <summary>
        /// post请求
        /// </summary>
        /// <param name="tempURL">上传地址</param>
        /// <param name="Data"></param>
        /// <param name="recvData">返回数据</param>
        /// <returns></returns>
        public bool MyPost(string tempURL, string Data, out string recvData)
        {
            recvData = "";
            string UrlStr = tempURL;
            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(UrlStr);

                webRequest.Method = "POST";
                webRequest.KeepAlive = true;
                webRequest.Timeout = 1000;

                webRequest.ContentType = "application/json;charset=utf-8";//;charset=utf-8
                //webRequest.ContentType = "application / json";
                //webRequest.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
                byte[] bytes = Encoding.UTF8.GetBytes(Data);
                Stream requestStream = webRequest.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                HttpWebResponse httpWebResponse = (HttpWebResponse)webRequest.GetResponse();
                //Encoding encoding = Encoding.GetEncoding(httpWebResponse.CharacterSet);

                Stream stream = null;
                StreamReader streamReader = null;
                string result;
                try
                {
                    stream = httpWebResponse.GetResponseStream();
                    streamReader = new StreamReader(stream, Encoding.UTF8);
                    //timerRecv.Start();
                    result = streamReader.ReadToEnd();
                }
                finally
                {
                    if (streamReader != null)
                    {
                        streamReader.Close();
                    }
                    if (stream != null)
                    {
                        stream.Close();
                    }
                }
                recvData = result;
                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }
    }
}

