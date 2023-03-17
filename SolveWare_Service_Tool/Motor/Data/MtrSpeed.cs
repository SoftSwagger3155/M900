using SolveWare_Service_Core.Base.Abstract;
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
        //Jog 专区
        [Category("Jog Speed")]
        [DisplayName("Start Velocity 初速")]
        [Description("Start velocity")]
        public double Jog_Min_Velocity { get; set; }

        [Category("Jog Speed")]
        [DisplayName("Max Velocity 最大速度")]
        [Description("Max velocity")]
        public double Jog_Max_Velocity { get; set; }

        [Category("Jog Speed")]
        [DisplayName("Acceleration 加速度")]
        [Description("Acceleration")]
        public double Jog_Acceleration { get; set; }

        [Category("Jog Speed")]
        [DisplayName("Deceleration 减速度")]
        [Description("Deceleration")]
        public double Jog_Deceleration { get; set; }

        [Category("Jog Speed")]
        [DisplayName("Jerk 拉力")]
        [Description("Jerk")]
        public double Jog_Jerk { get; set; }
        public double SpeedRate_Jog { get; set; }


        //Home 专区
        [Category("Home Speed")]
        [DisplayName("Start Velocity 初速")]
        [Description("Start velocity")]
        public double Home_Min_Velocity { get; set; }

        [Category("Home Speed")]
        [DisplayName("Max Velocity 最大速度")]
        [Description("Max velocity")]
        public double Home_Max_Velocity { get; set; }

        [Category("Home Speed")]
        [DisplayName("Acceleration 加速度")]
        [Description("Acceleration")]
        public double Home_Acceleration { get; set; }

        [Category("Home Speed")]
        [DisplayName("Deceleration 减速度")]
        [Description("Deceleration")]
        public double Home_Deceleration { get; set; }

        [Category("Home Speed")]
        [DisplayName("Jerk 拉力")]
        [Description("Jerk")]
        public double Home_Jerk { get; set; }

        public double SpeedRate_Home { get; set; }
        #region ctor
        public MtrSpeed()
        {
            Jog_Min_Velocity = 0;
            Jog_Max_Velocity = 10;
            Jog_Acceleration = 0.1;
            Jog_Deceleration = 0.1;
            Jog_Jerk = 1;
            SpeedRate_Jog = 100;

            Home_Min_Velocity = 0;
            Home_Max_Velocity = 10;
            Home_Acceleration = 0.1;
            Home_Deceleration = 0.1;
            Home_Jerk = 1;
            SpeedRate_Home = 100;
        }
        #endregion
    }
}
