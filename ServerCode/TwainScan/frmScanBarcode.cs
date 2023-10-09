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
            txtVincode.Focus();
            //txtBarcode.LostFocus += txtBarcode_LostFocus;

        }

        private void txtBarcode_LostFocus(object sender, EventArgs e)
        {
            ApiMethod api = new ApiMethod();
            
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            ApiMethod api =new ApiMethod();
            if (String.IsNullOrWhiteSpace(txtVincode.Text))
            {
                MsgBox.ShowError("Vincode không được để trống");
                return;
            }
            CurrentValue.VinCode = txtVincode.Text.Trim();
            MainForm frm = new MainForm();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                txtVincode.Text = "";
                Common.CurrentValue.ClearCurrentBarcodeValue();
            }
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
