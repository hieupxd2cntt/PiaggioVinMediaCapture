using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwainScan.Common;

namespace TwainScan
{
    public partial class frmMain : Form
    {
        private BackgroundWorker backgroundWorker1 { get; set; }
        public frmMain()
        {
            InitializeComponent();
            configToolStripMenuItem.Enabled = false;
            scanDocMenuItem.Enabled = false;
            backgroundWorker1 = new BackgroundWorker();
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerAsync();
            try
            {
                lblVersion.Text = String.Format("Version: {0}", Assembly.GetExecutingAssembly().GetName().Version.ToString()) ;
            }
            catch (Exception e)
            {

                lblVersion.Text = "Không lấy được Version hệ thống";
            }
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            SyncSystemDate();
        }
        private void configToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var config = new frmConfig();
            config.ShowDialog();
        }
        private void ShowLogin()
        {
            var frm = new frmLogin();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (CurrentValue.User != null && CurrentValue.User.User.LoginName.ToLower() == "admin")
                {
                    configToolStripMenuItem.Enabled = true;
                }
                else
                {
                    configToolStripMenuItem.Enabled = false;
                }
                lblUser.Text = CurrentValue.User == null ? "" : "Người dùng:"+ CurrentValue.User.User.LoginName;
                scanDocMenuItem.Enabled = true;
                var frmScan = new frmScanBarcode();
                frmScan.ShowDialog();
            }
            else
            {
                configToolStripMenuItem.Enabled = false;
                scanDocMenuItem.Enabled = false;

            }
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            ShowLogin();
            //Thread syncSystemDate = new Thread(SyncSystemDate);
            //syncSystemDate.Start();
            lblVersion.Text = GetRunningVersion();
        }
       
        private void SyncSystemDate()
        {
            try
            {
                if (!this.IsHandleCreated)
                    this.CreateControl();
                while (true)
                {
                    Thread.Sleep(1000);

                    this.Invoke((MethodInvoker)delegate
                    {
                        lblServerDate.Text = SystemDate.ToString("dd/MM/yyyy HH:mm:ss");
                    });
                }
            }
            catch (Exception e)
            {
            }

        }
        private string GetRunningVersion()
        {
            try
            {
                return "";// Environment.GetEnvironmentVariable("ClickOnce_CurrentVersion")
;
            }
            catch (Exception)
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }
        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private static DateTime? _startDate = null;
        private static DateTime _startLocal = DateTime.Now;
        private static DateTime _datelast = DateTime.Now;
        public static DateTime _timeout = DateTime.Now;
        public DateTime SystemDate
        {
            get
            {
                try
                {
                    if (_startDate == null)
                    {
                        try
                        {
                            ApiMethod api = new ApiMethod();


                                _startDate = api.GetSystemDate();
                            _startLocal = DateTime.Now;
                            _timeout = DateTime.Now;

                        }
                        catch
                        {
                            _startDate = DateTime.Now;
                            _startLocal = DateTime.Now;
                            _timeout = DateTime.Now;
                        }
                    }

                    var now = DateTime.Now;
                    var d = _startDate.Value;
                    d = d.AddSeconds((now - _startLocal).TotalSeconds);
                    if (Math.Abs((now - _datelast).TotalSeconds) > 2)
                    {
                        d = d.AddSeconds((now - _datelast).TotalSeconds);
                    }
                    _datelast = now;
                    return d;
                }
                catch (Exception)
                {
                    return DateTime.Now;
                }

            }
            set
            {
                try
                {
                    _startDate = value;
                    _startLocal = DateTime.Now;
                    _timeout = DateTime.Now;
                }
                catch (Exception)
                {

                }
            }
        }
        private void LogOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Common.CurrentValue.User = null;
            Common.CurrentValue.CurrentAttributeModel = null;
            Common.CurrentValue.Barcode = String.Empty;
            Common.CurrentValue.VinCode = String.Empty;
            ShowLogin();
        }

        private void scanDocMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmScanBarcode();
            frm.ShowDialog();
        }
    }
}
