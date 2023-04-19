using MF900_SolveWare.Safe;
using MF900_SolveWare.Views.AxisMesForm;
using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.General;
using SolveWare_Service_Utility.Extension;
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
    public partial class Form_Safe_Protection : Form, IView
    {
        public Form_Safe_Protection()
        {
            InitializeComponent();
        }

        public Data_Safe Data { get; protected set; } = null;
        List<IView> list = new List<IView>();
        private void Form_Safe_Protection_Load(object sender, EventArgs e)
        {
         
            Fillup_Combobox_Item();
            Refresh(this.Data);
        }

        private void Fillup_Combobox_Item()
        {
            cmb_Selector_Item.Items.Clear();
            cmb_Selector_Item.Items.Add(ConstantProperty.ReosurceKey_Motor);
            cmb_Selector_Item.Items.Add(ConstantProperty.ResourceKey_IO);
        }

        private void Simulate_DataBinding()
        {
            Data = new Data_Safe()
            {
                SafeDetailDatas =
                {
                    new DetailData_Safe_IO(),
                    new DetailData_Safe_Pos()
                }
            };
            IView view = null;
            view = new Form_Safe_Protection_IO();
            view.Setup(Data.SafeDetailDatas[0]);
            StyleForm(ref view);
            list.Add(view);
            // gpb_Content.Controls.Add(view as Form);

            view = new Form_Safe_Protection_Motion();
            view.Setup(Data.SafeDetailDatas[1]);
            StyleForm(ref view);
            list.Add(view);

            list.Reverse();
            list.ForEach(x => gpb_Content.Controls.Add(x as Form));
        }

        private void StyleForm(ref IView form)
        {
            (form as Form).TopLevel = false;
            (form as Form).Visible = true;
            (form as Form).Width = 600;
            (form as Form).Height = 50;
            (form as Form).Dock = DockStyle.Top;
            (form as Form).FormBorderStyle = FormBorderStyle.None;
        }

        private void btn_Execute_Click(object sender, EventArgs e)
        {
            int errorCode = ErrorCodes.NoError;
            string msg = string.Empty;
            if (Job_Safe.CheckPriorityOrder(Data.SafeDetailDatas, ref msg) == false) return;
            Refresh(Data);
            
            Task.Run(() =>
            {
                try
                {
                    do
                    {
                       
                        errorCode = Job_Safe.Do_Safe_Proection(this.Data, ref msg);

                    } while (false);
                }
                catch (Exception ex)
                {
                    msg += ex.Message;
                }
                bool showMsg = !string.IsNullOrEmpty(msg);
                SolveWare.Core.MMgr.Infohandler.LogMessage(msg, showMsg, showMsg);
            });
        }
      

        private void btn_Add_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            try
            {
                do
                {
                    if (Job_Safe.CheckPriorityOrder(Data.SafeDetailDatas, ref msg) == false) break;
                   
                    string selectedTag = cmb_Selector_Item.SelectedItem as string;
                    int maxPriority = Data.SafeDetailDatas.Count == 0 ? 1 : Data.SafeDetailDatas.Max(x => x.Priority) + 1;
                    SafeDetailDataBase detail = null;

                    switch (selectedTag)
                    {
                        case ConstantProperty.ReosurceKey_Motor:
                            detail = new DetailData_Safe_Pos()
                            {
                                IsSelected = false,
                                Priority = maxPriority,
                            };
                            break;

                        case ConstantProperty.ResourceKey_IO:
                            detail = new DetailData_Safe_IO()
                            {
                                IsSelected = false,
                                Priority = maxPriority,
                            };
                            break;
                    }

                    this.Data.SafeDetailDatas.Add(detail);
                    Job_Safe.SortDetailDatas(this.Data);
                    Refresh(this.Data);

                } while (false);
            }
            catch (Exception ex)
            {
                msg += ex.Message;
            }

            bool showMsg = !string.IsNullOrEmpty(msg);
            SolveWare.Core.MMgr.Infohandler.LogMessage(msg, showMsg, showMsg);
        }

        private void Refresh(Data_Safe data)
        {
            string msg = string.Empty;
            int index = 1;
            IView view = null;
            List<IView> tempViews = new List<IView>();

            foreach (var detail in data.SafeDetailDatas)
            {

                if (detail is DetailData_Safe_Pos)
                {
                    view = new Form_Safe_Protection_Motion();
                    view.Setup(detail);
                    StyleForm(ref view);
                    tempViews.Add(view);
                }
                else
                {
                    view = new Form_Safe_Protection_IO();
                    view.Setup(detail);
                    StyleForm(ref view);
                    tempViews.Add(view);
                }
            }

            tempViews.Reverse();
            gpb_Content.Controls.Clear();
            tempViews.ForEach(x => gpb_Content.Controls.Add(x as Form));
        }

        public void Setup<TObj>(TObj obj)
        {
            Data = obj as Data_Safe;
        }

        private void btn_Motor_Controller_Click(object sender, EventArgs e)
        {
            IView view = new Form_Axis_General_Controller();
            (view as Form).Show(this);
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            try
            {
                do
                {
                    if (this.Data.SafeDetailDatas.Count == 0) break;

                    this.Data.SafeDetailDatas.RemoveAll(x => x.IsSelected == true);
                    if (Job_Safe.CheckPriorityOrder(this.Data.SafeDetailDatas, ref msg) == false)
                    {
                        Refresh(this.Data);
                        break;
                    }
                    else 
                    { 
                        Job_Safe.SortDetailDatas(Data);  
                        Refresh(this.Data); 
                    }

                } while (false);
            }
            catch (Exception ex)
            {
                msg += ex.Message;
            }
            SolveWare.Core.ShowMsg(msg);    
        }

        private void btn_ReArrange_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            try
            {
                do
                {
                    if (Job_Safe.CheckPriorityOrder(Data.SafeDetailDatas, ref msg) == false) break;
                    Job_Safe.SortDetailDatas(this.Data);
                    Refresh(Data);

                } while (false);
            }
            catch (Exception ex)
            {
                msg += ex.Message;
            }

            bool showMsg = !string.IsNullOrEmpty(msg);
            SolveWare.Core.MMgr.Infohandler.LogMessage(msg, showMsg, showMsg);
        }
    }
}
