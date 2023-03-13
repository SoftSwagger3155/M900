using Newtonsoft.Json;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace MF900
{
    public partial class FormProgramSet : Form
    {
        private FormWorkPieceList formWorkPiece;
        public static ProSetSelect proSetSelect = ProSetSelect.None;
        public event Action<string> ShowOperationformAction;
        public event Action<string> ShowMainformAction;
        public event Action<string> ShowProductForm;  //触发一次Product界面以更新参数
        public event Action<string> SetMainFormProName;
        public FormProgramSet()
        {
            InitializeComponent();
            SetSelectBtnColor();
            ProgramParamMange.ProductManage = SerializeHelper.DeSerializeXml<ProductManage>(ParaFliePath.SystemParaPath + "ProlductManage.xml");
            ProgramParamMange.ReadProductPara();
            //programModeSql.SetConectionConfigs = programModeSql.SetConnectionConfig("Parameter\\ProgramModelSql", SqlSugar.DbType.Sqlite);
            SetNowProText(ProgramParamMange.ProductManage.NowProgramName);
        }
        public void SetNowProText(string name) => 
            txt_NowProduct.Text = name;

        /// <summary>
        /// 程序设定选择
        /// </summary>
        public enum ProSetSelect
        {
            None,
            新建,
            编辑,
            复制编辑,
            选择,
            微调整,
            生产机型变更,
            治具拆卸,
            导入,
            导出
        }
        private void SetSelectBtnColor()
        {
            UIButton btn;
            foreach (Control control in tableLayoutPanel3.Controls)
            {
                if (control is UIButton)
                {
                    btn = (UIButton)control;
                    btn.FillHoverColor = Color.FromArgb(0, 192, 0);
                    //btn.FillColor = btn.Name == btnName ? color2 : color1;
                }
            }
        }
        
        /// <summary>
        /// 切换产品参数更新
        /// </summary>
        /// <param name="nowProduct"></param>
        public void SetTextNowProduct(string nowProduct)
        {
            ProgramParamMange.ProductManage.NowProgramName = nowProduct;
            SetNowProText(ProgramParamMange.ProductManage.NowProgramName);
            ProgramParamMange.ReadProductPara();
            SerializeHelper.SerializeXml<ProductManage>(ProgramParamMange.ProductManage, ParaFliePath.SystemParaPath + "ProlductManage.xml");
        }
        private void btn_CheckWorkpiece_Click(object sender, EventArgs e)
        {
            formWorkPiece = new FormWorkPieceList();
            formWorkPiece.SetTextNowProduct += SetTextNowProduct;
            formWorkPiece.ShowDialog();
            formWorkPiece.Dispose();
            SetMainFormProName(ProgramParamMange.ProductManage.NowProgramName);
        }

        private void btn_CreateProcedure_Click(object sender, EventArgs e)
        {
            proSetSelect = ProSetSelect.新建;
            txt_NewProName.Enabled = true;
            SetEnable(false, false, true);
        }
        private void SetEnable(bool nowProEbe, bool btnCkWork, bool enable)
        {
            txt_NowProduct.Enabled = nowProEbe;
            btn_CheckWorkpiece.Enabled = btnCkWork;
            txt_NewProName.Enabled = enable;
        }
        private bool ProductIsNull()
        {
            if (txt_NewProName.Text.Trim() == null || txt_NewProName.Text.Trim() == "(新机型)")
            {
                proSetSelect = ProSetSelect.None;
                MessageBox.Show("请输入料号名!", "提示");
                return true;
            }
            return false;
        }
        private void btn_StartSetPro_Click(object sender, EventArgs e)
        {
            switch (proSetSelect)
            {
                case ProSetSelect.None:
                    break;
                case ProSetSelect.新建:
                    FormSetProcess.proProcess = ProProcess.制品数据;
                    if (ProductIsNull()) break;
                    if (ProductFind(txt_NewProName.Text.Trim()))
                    {
                        proSetSelect = ProSetSelect.None;
                        MessageBox.Show("当前料号已经存在!", "提示");
                        break;
                    }
                    AddProduct(txt_NewProName.Text.Trim());
                    if (txt_NewProName.Text != "(新机型)") 
                        txt_NewProName.Text = "(新机型)";
                    ShowOperationformAction("FormSetProcess");
                    ShowMainformAction("FormPanel");
                    ShowProductForm("FormProductData");
                    SetEnable(false, false, false);
                    break;
                case ProSetSelect.编辑:
                    FormSetProcess.proProcess = ProProcess.制品数据;
                    ShowMainformAction("FormPanel");
                    ShowProductForm("FormProductData");
                    ShowOperationformAction("FormSetProcess");
                    break;
                case ProSetSelect.复制编辑:
                    if (ProductIsNull()) break;
                    if (!Directory.Exists(ParaFliePath.ProductPath + txt_NewProName.Text))
                        Directory.CreateDirectory(ParaFliePath.ProductPath + txt_NewProName.Text);
                    CopyFiles(ParaFliePath.ProductPath + ProgramParamMange.ProductManage.NowProgramName, ParaFliePath.ProductPath + txt_NewProName.Text);
                    ProgramParamMange.ProductManage.ProgramList.Add(txt_NewProName.Text);
                    SerializeHelper.SerializeXml<ProductManage>(ProgramParamMange.ProductManage, ParaFliePath.SystemParaPath + "ProlductManage.xml");

                    break;
                case ProSetSelect.选择:
                    break;
                case ProSetSelect.微调整:
                    break;
                case ProSetSelect.生产机型变更:
                    break;
                case ProSetSelect.治具拆卸:
                    break;
                case ProSetSelect.导入:
                    break;
                case ProSetSelect.导出:
                    break;
            }
        }
       
        private void btn_SelectProduct_Click(object sender, EventArgs e)
        {
            proSetSelect = ProSetSelect.选择;
            SetEnable(false, true, false);
        }

        private void btn_Redact_Click(object sender, EventArgs e)
        {
            proSetSelect = ProSetSelect.编辑;
            SetEnable(false, false, false);
        }

        private void btn_CopyRedact_Click(object sender, EventArgs e)
        {
            proSetSelect = ProSetSelect.复制编辑;
            SetEnable(false, true, true);
        }

        #region 添加产品
        public bool ProductFind(string name)
        {
            ProgramParamMange.ProductManage.ProgramList = SerializeHelper.DeSerializeXml<ProductManage>(ParaFliePath.SystemParaPath + "ProlductManage.xml").ProgramList;
            for (int i = 0; i < ProgramParamMange.ProductManage.ProgramList.Count; i++)
            {
                if (name == ProgramParamMange.ProductManage.ProgramList[i]) return true;
            }
            return false;
        }
        public void AddProduct(string name)
        {
            Directory.CreateDirectory(ParaFliePath.ProductPath + name + "\\");
            ProgramParamMange.ProductManage.NowProgramName = name;
            ProgramParamMange.ProductManage.ProgramList.Add(name);
            SetNowProText(ProgramParamMange.ProductManage.NowProgramName);
            CopyFiles(ParaFliePath.ProductPath + "InitalProduct", ParaFliePath.ProductPath + name);
            SerializeHelper.SerializeXml<ProductManage>(ProgramParamMange.ProductManage, ParaFliePath.SystemParaPath + "ProlductManage.xml");
            ProgramParamMange.ReadProductPara();
        }

        #region 复制料号（文件夹及子文件）
        public void CopyFiles(string sourceFilePath, string targetFilePath)
        {
            string[] files = Directory.GetFiles(sourceFilePath);
            string fileName;
            string destFile;
            foreach (string item in files)
            {
                fileName = Path.GetFileName(item);
                destFile = Path.Combine(targetFilePath, fileName);
                File.Copy(item, destFile, true);
            }

            //CopyDirectory(sourceFilePath + "\\121222", targetFilePath);
        }

        private void CopyDirectory(string sourcePath, string destPath)
        {
            string floderName = Path.GetFileName(sourcePath);
            DirectoryInfo di = Directory.CreateDirectory(Path.Combine(destPath, floderName));
            string[] files = Directory.GetFileSystemEntries(sourcePath);

            foreach (string file in files)
            {
                if (Directory.Exists(file))
                    CopyDirectory(file, di.FullName);
                else
                    File.Copy(file, Path.Combine(di.FullName, Path.GetFileName(file)), true);
            }
        }
        #endregion

        #endregion
    }
}
