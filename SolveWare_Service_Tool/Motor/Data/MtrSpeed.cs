using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Tool.Motor.Data
{
    public class MtrSpeed : ElementBase
    {
        public List<SpeedSeting> SpeedSettings { get; set; }

        public MtrSpeed()
        {
            SpeedSettings = new List<SpeedSeting>();
        }

        ////Home 专区
        //[Category("Home Speed")]
        //[DisplayName("Start Velocity 初速")]
        //[Description("Start velocity")]
        //public double Home_Min_Velocity { get; set; }

        //[Category("Home Speed")]
        //[DisplayName("Max Velocity 最大速度")]
        //[Description("Max velocity")]
        //public double Home_Max_Velocity { get; set; }

        //[Category("Home Speed")]
        //[DisplayName("Acceleration 加速度")]
        //[Description("Acceleration")]
        //public double Home_Acceleration { get; set; }

        //[Category("Home Speed")]
        //[DisplayName("Deceleration 减速度")]
        //[Description("Deceleration")]
        //public double Home_Deceleration { get; set; }

        //[Category("Home Speed")]
        //[DisplayName("Jerk 拉力")]
        //[Description("Jerk")]
        //public double Home_Jerk { get; set; }

        //public double SpeedRate_Home { get; set; }
        #region ctor
        //public MtrSpeed()
        //{
        //    //Jog_Min_Velocity = 0;
        //    //Jog_Max_Velocity = 10;
        //    //Jog_Acceleration = 0.1;
        //    //Jog_Deceleration = 0.1;
        //    //Jog_Jerk = 1;
        //    //SpeedRate_Jog = 100;

        //    //Home_Min_Velocity = 0;
        //    //Home_Max_Velocity = 10;
        //    //Home_Acceleration = 0.1;
        //    //Home_Deceleration = 0.1;
        //    //Home_Jerk = 1;
        //    //SpeedRate_Home = 100;
        //}
        #endregion
    }

    public class SpeedSeting: ElementBase
    {
        //Jog 专区
        [Category("Speed")]
        [DisplayName("Start Velocity 初速")]
        [Description("Start velocity")]
        public double Min_Velocity { get; set; } = 0;

        [Category("Speed")]
        [DisplayName("Max Velocity 最大速度")]
        [Description("Max velocity")]
        public double Max_Velocity { get; set; } = 10;

        [Category("Speed")]
        [DisplayName("Acceleration 加速度")]
        [Description("Acceleration")]
        public double Acceleration { get; set; } = 1000;

        [Category("Speed")]
        [DisplayName("Deceleration 减速度")]
        [Description("Deceleration")]
        public double Deceleration { get; set; } = 1000;

        [Category("Speed")]
        [DisplayName("Jerk 拉力")]
        [Description("Jerk")]
        public double Jerk { get; set; }
        public double SpeedRatio { get; set; }
    }
}
