using HalconDotNet;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Tool.Camera.Base.Abstract;
using SolveWare_Service_Utility.Extension;
using SolveWare_Service_Vision.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MF900_SolveWare.Views.Child
{
    public partial class Form_InspectKit_PatternMatch : Form, IView
    {
        public Form_InspectKit_PatternMatch()
        {
            InitializeComponent(); 
        }

        Data_Inspection dataKit;
        CameraMediaBase camera;
        public void Setup<TObj>(TObj obj)
        {
            dataKit = obj as Data_Inspection;
            pGrid_PatternMatch.SelectedObject = dataKit.JobSheet_PatternMatch_Data;
            //pGrid_Blob.SelectedObject = dataKit.JobSheet_Blob_Data;
            //camera = dataKit.CameraName.GetCamera();
            //ctrl_Camera.Setup(camera);  
        }


    }
}
