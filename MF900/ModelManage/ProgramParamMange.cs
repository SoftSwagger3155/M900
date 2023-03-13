using HVision;
using MotionCard;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF900
{
    public class ProgramParamMange
    {
        public static ProductManage ProductManage { get; set; }
        public static ProductDataModel ProductDataPara { get; set; }
        public static BoardMessageModel BoardMessagePara { get; set; }
        public static RunOptionsModel RunOptionPara { get; set; }
        public static MarkerSetModel MarkerPara { get; set; }
        public static DebugTableTopModel DebugTableTopPara { get; set; }
        public static LoginMarkImageModel LoginMarkImagePara { get; set; }
        public static HandPosModel HandPosPara { get; set; }
        public static JipDataModel UpJipDataPara { get; set; }
        public static JipDataModel DownJipDataPara { get; set; }

        #region 轴、IO
        public static Dictionary<string, int> InIo { get; set; }
        public static Dictionary<string, int> OutIo { get; set; }
        public static Dictionary<string, AxisPara> AxisPara { get; set; }
        public static Dictionary<string, AxisSpeed> AxisSpeed { get; set; }
        public static Dictionary<float, int> AxisPulse { get; set; }
        #endregion
        public static Dictionary<string, BaslerCamera> MyCameras { get; set; }//相机
        public static PreLocationModel PreLocationPara { get; set; }
        public static FuncPara RunFuncPara { get; set; }
        public static DeviceBaseParaModel DeviceBasePara { get; set; }

        public static CommumicModel CommumicPara { get; set; }


        /// <summary>
        /// 读取轴参数
        /// </summary>
        public static void ReadAxisPara()
        {
            string inPutSignalPara = string.Empty;
            inPutSignalPara = File.ReadAllText(ParaFliePath.AxisParaPath + "\\MF900_In.xml");
            InIo = JsonConvert.DeserializeObject<Dictionary<string, int>>(inPutSignalPara);

            inPutSignalPara = File.ReadAllText(ParaFliePath.AxisParaPath + "\\MF900_AxisPara.xml");
            AxisPara = JsonConvert.DeserializeObject<Dictionary<string, AxisPara>>(inPutSignalPara);

            inPutSignalPara = File.ReadAllText(ParaFliePath.AxisParaPath + "\\MF900_Out.xml");
            OutIo = JsonConvert.DeserializeObject<Dictionary<string, int>>(inPutSignalPara);

            inPutSignalPara = File.ReadAllText(ParaFliePath.AxisParaPath + "\\MF900_AxisPluse.xml");
            AxisPulse = JsonConvert.DeserializeObject<Dictionary<float, int>>(inPutSignalPara);

            inPutSignalPara = File.ReadAllText(ParaFliePath.AxisParaPath + "\\AxisSpeedPara.xml");
            AxisSpeed = JsonConvert.DeserializeObject<Dictionary<string, AxisSpeed>>(inPutSignalPara);
        }
        /// <summary>
        /// 读取料号参数
        /// </summary>
        public static void ReadProductPara()
        {
            ProductDataPara = SerializeHelper.DeSerializeXml<ProductDataModel>(ParaFliePath.ProductPath + $"{ProductManage.NowProgramName}\\ProductDataModel.xml");
            RunOptionPara = SerializeHelper.DeSerializeXml<RunOptionsModel>(ParaFliePath.ProductPath + $"{ProductManage.NowProgramName}\\RunOptionsModel.xml");
            LoginMarkImagePara = SerializeHelper.DeSerializeXml<LoginMarkImageModel>(ParaFliePath.ProductPath + $"{ProductManage.NowProgramName}\\LoginMarkImageModel.xml");
            DebugTableTopPara = SerializeHelper.DeSerializeXml<DebugTableTopModel>(ParaFliePath.ProductPath + $"{ProductManage.NowProgramName}\\DebugTableTopModels.xml");
            MarkerPara = SerializeHelper.DeSerializeXml<MarkerSetModel>(ParaFliePath.ProductPath + $"{ProductManage.NowProgramName}\\MarkerSetModel.xml");
            HandPosPara = SerializeHelper.DeSerializeXml<HandPosModel>(ParaFliePath.ProductPath + $"{ProductManage.NowProgramName}\\HandPosModel.xml");
            UpJipDataPara = SerializeHelper.DeSerializeXml<JipDataModel>(ParaFliePath.ProductPath + $"{ProductManage.NowProgramName}\\UpJipDataModel.xml");
            DownJipDataPara = SerializeHelper.DeSerializeXml<JipDataModel>(ParaFliePath.ProductPath + $"{ProductManage.NowProgramName}\\DownJipDataModel.xml");
            BoardMessagePara = SerializeHelper.DeSerializeXml<BoardMessageModel>(ParaFliePath.ProductPath + $"{ProductManage.NowProgramName}\\BoardMessageModel.xml");
            PreLocationPara = SerializeHelper.DeSerializeXml<PreLocationModel>(ParaFliePath.ProductPath + $"{ProductManage.NowProgramName}\\PreLocationModel.xml");
            RunFuncPara = SerializeHelper.DeSerializeXml<FuncPara>(ParaFliePath.ProductPath + $"{ProductManage.NowProgramName}\\FuncPara.xml");
        }

        /// <summary>
        /// 设备基准参数
        /// </summary>
        public static void ReadBasePara()
        {
            DeviceBasePara = SerializeHelper.DeSerializeXml<DeviceBaseParaModel>(ParaFliePath.SystemParaPath + "DeviceBasePara.xml");
        }
    }
}
