using MotionCard;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF900
{
    class MotionCommons
    {
        public static MotionBase motion;
        public static void SetMotion(MotionBase motion)
        {
            MotionCommons.motion = motion;
        }

        /// <summary>
        /// 动作前检查信号
        /// </summary>
        /// <returns></returns>
        public static bool RunCkeckSin()
        {
            bool sin1 = false;
            motion.GetInSignal(ProgramParamMange.InIo["上模治具到位感应"], ref sin1);
            if (!sin1)
            {
                UIMessageBox.Show("上模治具到位未感应信号!","Error", UIStyle.Red);
                return false;
            }
            motion.GetInSignal(ProgramParamMange.InIo["下模治具到位感应"], ref sin1);
            if (!sin1)
            {
                UIMessageBox.Show("下模治具到位未感应信号!", "Error", UIStyle.Red);
                return false;
            }
            motion.GetInSignal(ProgramParamMange.InIo["正前方安全光栅"], ref sin1);
            if (!sin1)
            {
                UIMessageBox.Show("正前方安全光栅触发!", "Error", UIStyle.Red);
                return false;
            }
            motion.GetInSignal(ProgramParamMange.InIo["上方安全光栅"], ref sin1);
            if (!sin1)
            {
                UIMessageBox.Show("上方安全光栅触发!", "Error", UIStyle.Red);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 夹紧气缸拉伸气缸动作
        /// </summary>
        /// <param name="isClamp">是否夹紧拉伸</param>
        public static void AirClamp(bool isClamp)
        {
            uint zero = 0;
            uint one = 1;
            motion.SetOutSignal(ProgramParamMange.OutIo["软板夹紧气缸松"], isClamp ? zero : one);
            motion.SetOutSignal(ProgramParamMange.OutIo["软板夹紧气缸紧"], isClamp ? one : zero);
            motion.SetOutSignal(ProgramParamMange.OutIo["X向拉伸气缸缩"], isClamp ? zero : one);
            motion.SetOutSignal(ProgramParamMange.OutIo["X向拉伸气缸伸"], isClamp ? one : zero);
            motion.SetOutSignal(ProgramParamMange.OutIo["Y向拉伸气缸缩"], isClamp ? zero : one);
            motion.SetOutSignal(ProgramParamMange.OutIo["Y向拉伸气缸伸"], isClamp ? one : zero);
        }
    }
}
