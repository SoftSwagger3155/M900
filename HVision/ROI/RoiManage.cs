using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVision
{
    public class RoiManage
    {
        public RoiBase m_RoiBase { get; set; }
        public RoiLineData m_RoiLineData { get; set; }
        public RoiCircleData m_RoiCircleData { get; set; }
        public RoiRectgancleData m_RoiRecData { get; set; }

        public RoiManage(RoiType roiType,RoiDataBase roiDataBase)
        {
            switch (roiType)
            {
                case RoiType.circle:
                    m_RoiCircleData = (RoiCircleData)roiDataBase;
                    m_RoiBase = new RoiCircle(m_RoiCircleData);
                    break;
                case RoiType.rectangle:
                    m_RoiRecData = (RoiRectgancleData)roiDataBase;
                    m_RoiBase = new RoiRectangle1(m_RoiRecData);
                    break;
                case RoiType.line:
                    m_RoiLineData = (RoiLineData)roiDataBase;
                    m_RoiBase = new RoiLine(m_RoiLineData);
                    break;
                default:
                    break;
            }
        }
    }
}
