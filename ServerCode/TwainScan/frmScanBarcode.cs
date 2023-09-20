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
using TestApp;
using TwainScan.Common;
using VINMediaCaptureEntities.ViewModel;

namespace TwainScan
{
    public partial class frmScanBarcode : Form
    {
        public frmScanBarcode()
        {
            InitializeComponent();
            txtBarcode.Focus();
            //txtBarcode.LostFocus += txtBarcode_LostFocus;

        }

        private void txtBarcode_LostFocus(object sender, EventArgs e)
        {
            ApiMethod api = new ApiMethod();
            var model = api.GetModelByBarcode(txtBarcode.Text.Trim());
            var data = JsonConvert.DeserializeObject<List<DocTypeItemAddModel>>(model);
            if (data != null && data.Any())
            {
                lblModelInfo.Text = data.First().Model.ModelName;
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin thuộc tính của xe","Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBarcode.Focus();
            }
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            ApiMethod api =new ApiMethod();
            if (String.IsNullOrWhiteSpace(txtBarcode.Text))
            {
                MsgBox.ShowError("Barcode không được để trống");
                return;
            }
            if (String.IsNullOrWhiteSpace(txtVincode.Text))
            {
                MsgBox.ShowError("Vincode không được để trống");
                return;
            }
            var model=api.GetModelByBarcode(txtBarcode.Text.Trim());
            var data = JsonConvert.DeserializeObject<List<DocTypeItemAddModel>>(model);
            if (data !=null && data.Any())
            {
                Common.CurrentValue.CurrentAttributeModel = data;
                CurrentValue.Barcode = txtBarcode.Text.Trim();
                CurrentValue.VinCode = txtVincode.Text.Trim();
                MainForm frm=new MainForm();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    txtVincode.Text = "";
                    txtBarcode.Text = "";
                    Common.CurrentValue.ClearCurrentBarcodeValue();
                }
            }
            else
            {
                MsgBox.ShowError("Không tìm thấy thông tin thuộc tính của xe");
            }
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ApiMethod api = new ApiMethod();
                var model = api.GetModelByBarcode(txtBarcode.Text.Trim());
                var data = JsonConvert.DeserializeObject<List<DocTypeItemAddModel>>(model);
                if (data != null && data.Any())
                {
                    lblModelInfo.Text = data.First().Model.ModelName;
                    txtVincode.Focus();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin thuộc tính của xe", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtBarcode.Focus();
                }
            }
            else if (e.KeyCode == Keys.Tab)
            {

            }
            
            //if (e.KeyCode == Keys.Enter)
            //{
            //    btnNext_Click(null,null);
            //}
        }

        private void txtVincode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnNext_Click(null, null);
            }
        }
    }
}
