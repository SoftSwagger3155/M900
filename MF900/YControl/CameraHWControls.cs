using HalconDotNet;
using HVision;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MF900
{
    public partial class CameraHWControls : UserControl
    {
        public CameraHWControls()
        {
            InitializeComponent();
        }
        public UserHWControls userHWControls { get { return userHWControls1; } }
        public HObject SourceImage { get { return userHWControls1.SourceImage; } }
        public HWindow HWindows { get { return userHWControls1.HWindows; } }
        private void radb_UpCamera_CheckedChanged(object sender, EventArgs e)
        {
            //ProgramParamMange.MyCameras["下相机"].StopGrabbing();
            //ProgramParamMange.MyCameras["上相机"].eventProcessImage += userHWControls1.ShowSourceImage;
            //ProgramParamMange.MyCameras["上相机"].HWindows = userHWControls1.HWindows;
            //ProgramParamMange.MyCameras["上相机"].GrabOne();
        }

        private void radb_DownCamera_CheckedChanged(object sender, EventArgs e)
        {
            //ProgramParamMange.MyCameras["上相机"].StopGrabbing();
            //ProgramParamMange.MyCameras["下相机"].HWindows = userHWControls1.HWindows;
            //ProgramParamMange.MyCameras["下相机"].StartGrabbing();
            //ProgramParamMange.MyCameras["下相机"].GrabOne();
        }
    }
}
