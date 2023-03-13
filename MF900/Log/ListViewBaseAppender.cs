using log4net.Layout;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net.Appender;
using log4net.Core;

namespace MF900
{
    public class ListViewBaseAppender : AppenderSkeleton
    {
        public ListView listView { get; set; }
        public int ImageIndex { get; set; }

        public ListViewBaseAppender()
        {

        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            if (this.listView == null)
            {
                return;
            }

            if (!this.listView.IsHandleCreated)
            {
                return;
            }

            if (this.listView.IsDisposed)
            {
                return;
            }

            var patternLayout = this.Layout as PatternLayout;

            var str = string.Empty;
            if (patternLayout != null)
            {
                str = patternLayout.Format(loggingEvent);

                if (loggingEvent.ExceptionObject != null)
                {
                    str += loggingEvent.ExceptionObject.ToString() + Environment.NewLine;
                }
            }
            else
            {
                str = loggingEvent.LoggerName + "-" + loggingEvent.RenderedMessage + Environment.NewLine;
            }

            if (!this.listView.InvokeRequired)
            {
                printf(str, ImageIndex);
            }
            else
            {
                this.listView.BeginInvoke((MethodInvoker)delegate
                {
                    if (!this.listView.IsHandleCreated)
                    {
                        return;
                    }

                    if (this.listView.IsDisposed)
                    {
                        return;
                    }

                    printf(str, ImageIndex);
                });
            }
        }

        private void printf(string str, int index)
        {
            if (listView.Items.Count > 50)
            {
                listView.Items.Clear();
            }

            ListViewItem item = new ListViewItem(" " + str, index);
            //item.Text = str.ToString();

            listView.BeginUpdate();
            listView.Items.Add(item);
            listView.Items[listView.Items.Count - 1].EnsureVisible();//滚动到最后  
            listView.EndUpdate();
        }
    }
}
