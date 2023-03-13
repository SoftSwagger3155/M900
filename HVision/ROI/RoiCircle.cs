using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVision
{
    public class RoiCircle : RoiBase
    {
        public RoiCircleData RoiCircleDatas { get; set; }
        public RoiCircle(RoiCircleData roiCircleData)
        {
            this.RoiCircleDatas = roiCircleData;
        }
        public override void CreateDrawingObject(Color color)
        {
            DrawingObject?.Dispose();
            DrawingObject = new HDrawingObject();
            DrawingObject.CreateDrawingObjectCircle(RoiCircleDatas.Row, RoiCircleDatas.Column, RoiCircleDatas.Radius);
        }

        public override void GenerateParameter()
        {
            RoiCircleDatas.Row = DrawingObject.GetDrawingObjectParams("row");
            RoiCircleDatas.Column = DrawingObject.GetDrawingObjectParams("column");
            RoiCircleDatas.Radius = DrawingObject.GetDrawingObjectParams("radius");
        }
    }
}
