using log4net;
using log4net.Config;
using log4net.Layout;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MF900
{
    public class Log4NetHepler
    {
        private static ILog log;
        private static ListViewBaseAppender list_logAppender;
        //log4net.Config.XmlConfigurator.Configure(new FileInfo("log4net.config"));
        
        public static void BingListView(ListView listView1)
        {
            list_logAppender = new ListViewBaseAppender()
            {
                listView = listView1,//注释后 就只有文件log
                Layout = new PatternLayout("%d{HH:mm:ss}   %m%n")//%d{yyyy-MM-dd HH:mm:ss}  %m%n
            };
            log4net.Config.BasicConfigurator.Configure(list_logAppender);
            //log4net.Config.XmlConfigurator.Configure(new FileInfo("log4net.config"));
        }
        public static void WriteInfo(string message)
        {
            log = LogManager.GetLogger("InfoLog");
            list_logAppender.ImageIndex = 0;
            log.Info(message);
        }
        public static void WriteDebug(string message)
        {
            log = LogManager.GetLogger("DebugLog");
            list_logAppender.ImageIndex = 1;
            log.Debug(message);
        }
        public static void WriteError(string message)
        {
             
            log = LogManager.GetLogger("ErrorLog");
            list_logAppender.ImageIndex = 2;
            log.Error(message);
        }
    }
}
