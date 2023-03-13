using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVision
{
    public enum RoiType
    {
        circle,
        rectangle,
        line
    }
    public abstract class RoiBase
    {
        [NonSerialized]
        private HDrawingObject m_drawingObject = null;
        public HDrawingObject DrawingObject { get => m_drawingObject; set => m_drawingObject = value; }
        private Color roiColor;

        public Color RoiColor
        {
            get { return roiColor; }
            set 
            { 
                roiColor = value;
                m_drawingObject.SetDrawingObjectParams("color", "green");
            }
        }

        public RoiBase() { }
        
        public HRegion GetRegion()
        {
            return new HRegion(DrawingObject.GetDrawingObjectIconic());
        }

        public abstract void CreateDrawingObject(Color color);
        public abstract void GenerateParameter();
    }
}
