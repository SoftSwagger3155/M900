using HalconDotNet;
using HVision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF900
{
    public class HlCommonsFunction
    {
        static HalconTool halconTool = new HalconTool();
        public static bool FindBaseLocation(CameraHWControls cameraHWControls, HTuple row, HTuple col, HTuple radius,
            ref HTuple hv_RowCenter, ref HTuple hv_ColCenter, ref HTuple hv_Radius)
        {
            HObject ho_Circle = null;
            HTuple resultRow = null, resultCol = null, arcType = null;
            HTuple hv_TempRow = new HTuple();
            HTuple hv_TempCol = new HTuple();
            HTuple hv_StartPhi, hv_EndPhi;
            HTuple hv_PointOrder, hv_ArcAngle;
            HObject ho_Cross;
            hv_TempRow = hv_TempRow.TupleConcat(row);
            hv_TempRow = hv_TempRow.TupleConcat(row + radius);
            hv_TempRow = hv_TempRow.TupleConcat(row);
            hv_TempRow = hv_TempRow.TupleConcat(row - radius);
            hv_TempRow = hv_TempRow.TupleConcat(row);
            hv_TempCol = hv_TempCol.TupleConcat(col - radius);
            hv_TempCol = hv_TempCol.TupleConcat(col);
            hv_TempCol = hv_TempCol.TupleConcat(col + radius);
            hv_TempCol = hv_TempCol.TupleConcat(col);
            hv_TempCol = hv_TempCol.TupleConcat(col - radius);
            halconTool.spoke(cameraHWControls.SourceImage, out ho_Circle, hv_TempRow, hv_TempCol,
                new CircleRoiPara()
                {
                    Elements = 100,
                    DeteHeight = 10,
                    DetectWidth = 20,
                    Threshold = 30,
                    Direct = "inner",
                    Select = "first",
                    Transition = "negative"
                }, out resultRow, out resultCol, out arcType);
            halconTool.pts_to_best_circle2(out ho_Circle, resultRow, resultCol, 30, "circle",
             out hv_RowCenter, out hv_ColCenter, out hv_Radius, out hv_StartPhi, out hv_EndPhi, out hv_PointOrder, out hv_ArcAngle);
            HOperatorSet.GenCrossContourXld(out ho_Cross, hv_RowCenter, hv_ColCenter, 6, 0.785398);
            HOperatorSet.SetColor(cameraHWControls.HWindows, "green");
            if (hv_RowCenter.D == 0 || hv_ColCenter.D == 0)
            {
                ho_Cross.Dispose();
                ho_Circle.Dispose();
                return false;
            }
            cameraHWControls.userHWControls.AddHObject(ho_Cross);
            cameraHWControls.userHWControls.AddHObject(ho_Circle);
            ho_Cross.Dispose();
            ho_Circle.Dispose();
            return true;
        }
    }
}
