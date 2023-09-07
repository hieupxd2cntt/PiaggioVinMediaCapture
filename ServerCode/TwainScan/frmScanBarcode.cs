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
using VINMediaCaptureEntities.ViewModel;

namespace TwainScan
{
    public partial class frmScanBarcode : Form
    {
        public frmScanBarcode()
        {
            InitializeComponent();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            ApiMethod api =new ApiMethod();
            var model=api.GetModelByBarcode(txtBarcode.Text.Trim());
            var data = JsonConvert.DeserializeObject<List<DocTypeItemAddModel>>(model);
            if (data !=null && data.Any())
            {
                Common.CurrentValue.CurrentAttributeModel = data;
                CurrentValue.Barcode = txtBarcode.Text.Trim();
                frmScanVincode frm=new frmScanVincode();
                frm.ShowDialog();
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
                btnNext_Click(null,null);
            }
        }
    }
}
