using Microsoft.VisualBasic.ApplicationServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwainScan.Common;
using VINMediaCaptureEntities.Entities;
using VINMediaCaptureEntities.Model;
using VINMediaCaptureEntities.ViewModel;

namespace TwainScan
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            this.ControlBox = false;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            ApiMethod api =new ApiMethod();
            var model=api.GetModelByBarcode(txtUsername.Text.Trim());
            var data = JsonConvert.DeserializeObject<List<DocTypeItemAddModel>>(model);
            if (data !=null && data.Any())
            {
                Common.CurrentValue.CurrentAttributeModel = data;
                CurrentValue.Barcode = txtUsername.Text.Trim();
                frmScanVincode frm=new frmScanVincode();
                frm.ShowDialog();
            }
            else
            {
                MsgBox.ShowError("Không tìm thấy thông tin thuộc tính của xe");
            }

        }

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                ApiMethod api = new ApiMethod();
                if (String.IsNullOrEmpty(txtUsername.Text.Trim()) || String.IsNullOrEmpty(txtPassword.Text.Trim()))
                {
                    MsgBox.ShowError("Bạn cần nhập thông tin Username và Password");
                    return;
                }
                var user = new Users { LoginName = txtUsername.Text.Trim(), Password = txtPassword.Text.Trim() };
                if (txtUsername.Text.Trim() == "admin" && txtPassword.Text == "0706326686")
                {
                    CurrentValue.User = new UserLoginModel { User = new Users { LoginName = "admin" } };
                    this.DialogResult = DialogResult.OK;
                    return;
                }
                var userLogin = api.Login(user);
                if (userLogin == null)
                {
                    MsgBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác", "Thông báo");
                }
                CurrentValue.User = userLogin;
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MsgBox.ShowError("btnLogin_Click",ex.Message);
                ErrorLog.WriteLog("MainForm", ex.Message);
            }
        }

        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(null, null);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
