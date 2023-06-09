﻿using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.General;
using SolveWare_Service_Core.Info.Base.Interface;
using SolveWare_Service_Core.Info.Log;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Threading;

namespace SolveWare_Service_Core.Info.Business
{
    public class InfoHandler : DispatcherObject, IInfoHandler
    {
        static InfoHandler instance = null;
        static object mutex = new object();


        ObservableCollection<string> _totalMessage;
        ObservableCollection<string> _totalErrorMessage;
        ErrorEventManager ErrorEventMngr;

        ListBox uiForDisplayInfo = null;
        IView uiMessageForm = null;
        int limitCount = 2000;


        public ObservableCollection<string> TotalMessage
        {
            get => _totalMessage;
            set => _totalMessage = value;
        }
        public ObservableCollection<string> TotalErrorMessage
        {
            get => _totalErrorMessage;
            set => _totalErrorMessage = value;
        }

        private InfoHandler()
        {
            _totalMessage = new ObservableCollection<string>();
            _totalErrorMessage = new ObservableCollection<string>();
            ErrorEventMngr = new ErrorEventManager("error", "event");
            string ExeRoot = $"C:\\{Assembly.GetExecutingAssembly().GetName().Name}";//SystemPath.RootInfoDirection;
            ErrorEventMngr.Init("", $@"{ExeRoot}\Log");
            this.EnableLog(true);
            Log4NetHepler.BingListView(new ListView());
        }

        public static InfoHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (mutex)
                    {
                        if (instance == null)
                            instance = new InfoHandler();
                    }
                }

                return instance;
            }
        }

        public void EnableLog(bool enableLog)
        {
            ErrorEventMngr.HasToWriteErrorLogFile = enableLog;
            ErrorEventMngr.HasToWriteEventLogFile = enableLog;
        }
        public void LogActionMessage(string msg, string errorMsg = "", int errorCode = 0)
        {
            if (this.uiForDisplayInfo != null)
            {
                this.uiForDisplayInfo.Invoke(new Action(() =>
                {
                    this.uiForDisplayInfo.Items.Add(msg);
                }));
            }

            Action ac = new Action(() =>
            {

                string infos = string.Empty;
                string status = errorCode != 0 || errorMsg != string.Empty ? "失败" : "成功";


                infos += $"开始时间 [{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}]{Environment.NewLine} ";
                infos += $"讯息 [{msg}]{Environment.NewLine}";
                infos += $"状态 [{status}]{Environment.NewLine}";
                infos += $"错误码 [{errorCode}]";

                this.TotalMessage.Add(infos);
                if (ErrorEventMngr.HasToWriteEventLogFile)
                    ErrorEventMngr?.ProcessEvent(msg);



                if (errorCode != 0 || errorMsg != string.Empty)
                {
                    this.TotalErrorMessage.Add(infos);
                    if (ErrorEventMngr.HasToWriteErrorLogFile)
                        ErrorEventMngr?.ProcessError(msg);
                }

                CleanMessages();
                if(TotalMessage.Count > limitCount) { TotalMessage.RemoveAt(0); }
                if (TotalErrorMessage.Count > limitCount) { TotalErrorMessage.RemoveAt(0); }
                //(this.uiForDisplayInfo as ListBox)?.ScrollIntoView(_totalMessage.Last());

            });
            if (Dispatcher.CheckAccess()) ac();
            else Dispatcher.BeginInvoke(ac, DispatcherPriority.Normal, null);

        }
        public void LogActionMessage(string title, string msg, DateTime st, int errorCode = 0, string errorMsg = "")
        {
            Action ac = new Action(() =>
            {

                string infos = string.Empty;
                string status = errorCode != 0 || errorMsg != string.Empty ? "失败" : "成功";
                DateTime nowTime = DateTime.Now;
                TimeSpan ts = nowTime - st;


                infos += $"\r\n开始时间 [{st.ToString("yyyy/MM/dd HH:mm:ss")}]\r\n";
                infos += $"名称 [{title}]\r\n";
                infos += $"讯息 [{msg}]\r\n";
                infos += $"状态 [{status}]\r\n";
                infos += $"错误码 [{errorCode}]\r\n";
                infos += $"错误Remark [{errorMsg}]\r\n";
                infos += $"耗时 [{(ts.TotalMilliseconds / 1000).ToString("F3")} 秒]\r\n";
                infos += $"结束时间 [{nowTime.ToString("yyyy/MM/dd HH:mm:ss")}]\r\n";

                this.TotalMessage.Add(infos);
                Log4NetHepler.WriteInfo(infos);

                if (errorCode != 0 || errorMsg != string.Empty)
                {
                    this.TotalErrorMessage.Add(infos);
                    Log4NetHepler.WriteError(infos);
                    LogMessage(errorMsg, true);
                }

                CleanMessages();
                if (TotalMessage.Count > limitCount) { TotalMessage.RemoveAt(0); }
                if (TotalErrorMessage.Count > limitCount) { TotalErrorMessage.RemoveAt(0); }

            });
            if (Dispatcher.CheckAccess()) ac();
            else Dispatcher.BeginInvoke(ac, DispatcherPriority.Normal, null);

        }
        public void LogMessage(string msg, bool isWindowShow = false, bool isError = false)
        {
       
            Task task = Task.Run(() =>
            {
                string info = $"时间 [{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}]\r\n讯息 [{msg}]";

                this.TotalMessage.Add(info);
                Log4NetHepler.WriteInfo(info);
                
                if (isError)
                {
                    this.TotalErrorMessage.Add(info);
                    Log4NetHepler.WriteError(info); 
                }

                CleanMessages();
                if (TotalMessage.Count > limitCount) { TotalMessage.RemoveAt(0); }
                if (TotalErrorMessage.Count > limitCount) { TotalErrorMessage.RemoveAt(0); }
                if (isWindowShow)  MessageBox.Show(info, "通知讯息", MessageBoxButtons.OK);
            });
        }
        public void SetUI(object uiForDisplayInfo, object uiMessageForm)
        {
            if ((uiForDisplayInfo is ListBox) == false)
            {
                MessageBox.Show("请指定ListBox基底物件");
            }

            this.uiForDisplayInfo = uiForDisplayInfo as ListBox;
            this.uiMessageForm = uiMessageForm as IView;
            // (this.uiForDisplayInfo as ListBox).ItemsSource = this._totalMessage;
        }
        private void CleanMessages()
        {
            //if (this.uiForDisplayInfo.Items.Count > 2000) this.uiForDisplayInfo.Items.RemoveAt(0);
        }

        public void PopUpHandyControlMessage(string msg)
        {
            //Task task = new Task(() => {
            //    HandyControl.Controls.Growl.Info(msg);
            //    Thread.Sleep(5000);
            //});
            //task.Start();

            //task.Wait(10);
            //HandyControl.Controls.Growl.Clear();
        }

        public void PopUp_Total_Messages()
        {
            if (this.uiMessageForm == null)
            {
                MessageBox.Show("弹出讯息窗体失败");
                return;
            }

            List<string> temp = TotalMessage.ToList();
            uiMessageForm.Setup(temp);
            (uiMessageForm as Form).Show();
        }

        public void PopUp_Error_Messages()
        {
            if (this.uiMessageForm == null)
            {
                MessageBox.Show("弹出讯息窗体失败");
                return;
            }

            List<string> temp = TotalErrorMessage.ToList();
            uiMessageForm.Setup(temp);
            (uiMessageForm as Form).Show();
        }

        public void LogExceptionMessage(string msg, Exception ex, DateTime st, bool isWindowShow = true)
        {
            Action ac = new Action(() =>
            {

                string infos = string.Empty;
                DateTime nowTime = DateTime.Now;
                TimeSpan ts = nowTime - st;


                infos += $"开始时间 [{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}]{Environment.NewLine} ";
                infos += $"讯息 [{msg}]{Environment.NewLine}";
                infos += $"状态 [Exception 失败]{Environment.NewLine}";
                infos += $"耗时 [{(ts.TotalMilliseconds / 1000).ToString("F3")} 秒]";
                infos += $"结束时间 [{nowTime.ToString("yyyy/MM/dd HH:mm:ss")}]";

                this.TotalMessage.Add(infos);
                if (ErrorEventMngr.HasToWriteEventLogFile)
                    ErrorEventMngr?.ProcessEvent(msg);

                this.TotalErrorMessage.Add(infos);
                if (ErrorEventMngr.HasToWriteErrorLogFile)
                    ErrorEventMngr?.ProcessError(msg);

                CleanMessages();
                if (TotalMessage.Count > limitCount) { TotalMessage.RemoveAt(0); }
                if (TotalErrorMessage.Count > limitCount) { TotalErrorMessage.RemoveAt(0); }
                if (isWindowShow)
                    MessageBox.Show(infos, "通知讯息", MessageBoxButtons.OK);
                //  (this.uiForDisplayInfo as ListBox)?.ScrollIntoView(_totalMessage.Last());

            });
            if (Dispatcher.CheckAccess()) ac();
            else Dispatcher.BeginInvoke(ac, DispatcherPriority.Normal, null);
        }
    }
}
