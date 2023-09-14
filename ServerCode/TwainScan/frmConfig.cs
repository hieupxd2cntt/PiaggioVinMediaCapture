using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VINMediaCaptureEntities.TaiwainAppModel;
using static System.Environment;

namespace TwainScan
{
    public partial class frmConfig : Form
    {
        public frmConfig()
        {
            InitializeComponent();
        }

        private void frmConfig_Load(object sender, EventArgs e)
        {
            var configFile = System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonApplicationData) + "/TaiwanScan/TaiwanScanConfig.Xml";
            XMLProcess xMLProcess = new XMLProcess();
            var xml=xMLProcess.LoadXmlFromFile(configFile);
            if (!String.IsNullOrEmpty(xml))
            {
                var obj =(ConfigModel) xMLProcess.LoadObjectFromXMLString(xml,typeof(ConfigModel));
                txtApiUrl.Text = obj.WebApi;
                txtHostUrl.Text = obj.WebApp;
                txtScanFolder.Text = obj.ScanFolder;
                txtScanSuccessFolder.Text= String.IsNullOrEmpty(obj.ScanSuccessFolder) ? System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/TaiwanScan/Success":obj.ScanSuccessFolder;
                txtScanFailFolder.Text= String.IsNullOrEmpty(obj.ScanFailFolder) ? System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/TaiwanScan/Fail":obj.ScanFailFolder;
                txtScanFailFolder.Text = obj.ScanFailFolder;
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog()==DialogResult.OK)
            {
                txtScanFolder.Text = folderDialog.SelectedPath;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var configModel = new ConfigModel();
            configModel.WebApi=txtApiUrl.Text.Trim();
            configModel.WebApp = txtHostUrl.Text.Trim();
            configModel.ScanFolder = txtScanFolder.Text.Trim();
            configModel.ScanSuccessFolder = txtScanSuccessFolder.Text.Trim();
            configModel.ScanFailFolder = txtScanFailFolder.Text.Trim();
            XMLProcess xMLProcess = new XMLProcess();
            var xml = xMLProcess.GetXMLFromObject(configModel);
            var configFile= System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonApplicationData)+ "/TaiwanScan/TaiwanScanConfig.Xml";
            xMLProcess.SaveXmlToFile(xml, configFile);
            MsgBox.Show("Lưu file thành công","Thông báo");

        }

        private void btnBrowseScanFail_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                txtScanFailFolder.Text = folderDialog.SelectedPath;
            }
        }

        private void btnBrowseSuccessFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                txtScanSuccessFolder.Text = folderDialog.SelectedPath;
            }
        }

        private void btnBrowseLogPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                txtLogPath.Text = folderDialog.SelectedPath;
            }
        }
    }
}
