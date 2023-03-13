using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MF900
{
    public class GetFomControls
    {
        /// <summary>
        /// 获取指定窗体或面板下的所有控件
        /// </summary>
        /// <typeparam name="T">控件类型</typeparam>
        /// <param name="containChildControlWindow">包含子控件的窗体（比如窗体是[Controls]、panel是[panel.controls]）</param>
        /// <returns></returns>
        public static Stack<Control> GetAllControlOfWindows(Control.ControlCollection containChildControlWindow)
        {
            Stack<Control> tmpStack = new Stack<Control>();
            foreach (Control control in containChildControlWindow)
            {
                tmpStack.Push(control);
            }

            return tmpStack;
        }

        /// <summary>
        /// 获取指定窗体或面板下的指定类型的控件
        /// </summary>
        /// <typeparam name="T">控件类型</typeparam>
        /// <param name="containChildControlWindow">包含子控件的窗体（比如窗体是[Controls]、panel是[panel.controls]）</param>
        /// <returns></returns>
        public static Stack<T> GetAppointControlOfWindows<T>(Control.ControlCollection containChildControlWindow)
        {
            Stack<T> tmpStack = new Stack<T>();
            foreach (var control in containChildControlWindow)
            {
                if (control is T)
                {
                    tmpStack.Push((T)control);
                }

            }

            return tmpStack;
        }


        /// <summary>
        /// 通过控件名称获取到控件
        /// </summary>
        /// <param name="controlName">控件名称</param>
        /// <param name="containChildControlWindow">包含子控件的窗体（比如窗体是[Controls]、panel是[panel.controls]）</param>
        /// <returns></returns>
        public static Control GetControlOfName(string controlName, Control.ControlCollection containChildControlWindow)
        {
            if (string.IsNullOrEmpty(controlName)) return null;

            foreach (Control item in containChildControlWindow)
            {
                if (item.Name.Equals(controlName))
                {
                    return item;
                }
            }

            return null;
        }

    }
}
