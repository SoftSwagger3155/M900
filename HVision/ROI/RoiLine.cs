using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVision
{
    public class RoiLine : RoiBase
    {
        public RoiLineData RoiLineDatas { get; set; }
        public RoiLine(RoiLineData roiLineData)
        {
            this.RoiLineDatas = roiLineData;
        }
        public override void CreateDrawingObject(Color color)
        {
            DrawingObject?.Dispose();
            DrawingObject = new HDrawingObject();
            DrawingObject.CreateDrawingObjectLine(RoiLineDatas.StartRow, RoiLineDatas.StartColumn, 
                RoiLineDatas.EndRow, RoiLineDatas.EndColumn);
        }

        public override void GenerateParameter()
        {
            RoiLineDatas.StartRow = DrawingObject.GetDrawingObjectParams("row1");
            RoiLineDatas.StartColumn = DrawingObject.GetDrawingObjectParams("column1");
            RoiLineDatas.EndRow = DrawingObject.GetDrawingObjectParams("row2");
            RoiLineDatas.EndColumn = DrawingObject.GetDrawingObjectParams("column2");
        }
    }
}
