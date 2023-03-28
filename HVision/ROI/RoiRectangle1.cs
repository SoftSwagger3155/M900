using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVision
{
    public class RoiRectangle1 : RoiBase
    {
        public RoiRectgancleData RoiRectData { get; set; }
       
        public RoiRectangle1(RoiRectgancleData roiRectData)
        {
            this.RoiRectData = roiRectData;
        }
        public override void CreateDrawingObject(Color color)
        {
            DrawingObject?.Dispose();
            DrawingObject = new HDrawingObject();
            DrawingObject.CreateDrawingObjectRectangle1(RoiRectData.Row1, RoiRectData.Column1,
                RoiRectData.Row2, RoiRectData.Column2);
        }

        public override void GenerateParameter()
        {
            RoiRectData.Row1 = DrawingObject.GetDrawingObjectParams("row1");
            RoiRectData.Column1 = DrawingObject.GetDrawingObjectParams("column1");
            RoiRectData.Row2 = DrawingObject.GetDrawingObjectParams("row2");
            RoiRectData.Column2 = DrawingObject.GetDrawingObjectParams("column2");
        }
    }
}
