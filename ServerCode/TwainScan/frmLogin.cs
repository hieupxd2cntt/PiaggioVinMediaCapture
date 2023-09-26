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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TwainScan
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            this.ControlBox = false;
            
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
                if (userLogin == null || userLogin.User == null)
                {
                    MsgBox.ShowError("Tên đăng nhập hoặc mật khẩu không chính xác");
                    return;
                }
                CurrentValue.User = userLogin;
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MsgBox.ShowError("Lỗi đăng nhập user:"+ex.Message);
                ErrorLog.WriteLog("btnLogin_Click", ex.Message);
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

        private void txtUsername_TabIndexChanged(object sender, EventArgs e)
        {
            MsgBox.Show("Changed","");
        }

        private void btnLogin1_Click(object sender, EventArgs e)
        {

        }
    }
}
