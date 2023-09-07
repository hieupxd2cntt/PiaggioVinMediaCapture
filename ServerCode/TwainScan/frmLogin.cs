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
using VINMediaCaptureEntities.ViewModel;

namespace TwainScan
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
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
            //this.DialogResult = DialogResult.Cancel;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            ApiMethod api = new ApiMethod();
            var user = new Users {LoginName=txtUsername.Text.Trim(),Password= txtPassword.Text.Trim() };
            var userLogin=api.Login(user);
            CurrentValue.User = userLogin;
            this.DialogResult=DialogResult.OK;
        }
    }
}
