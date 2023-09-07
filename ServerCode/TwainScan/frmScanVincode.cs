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
using System.Xml.Linq;
using TestApp;
using TwainScan.Common;
using VINMediaCaptureApi.Controllers;
using VINMediaCaptureEntities.ViewModel;

namespace TwainScan
{
    public partial class frmScanVincode : Form
    {
        public frmScanVincode()
        {
            InitializeComponent();
            if (Common.CurrentValue.CurrentAttributeModel != null && Common.CurrentValue.CurrentAttributeModel.Any())
            {
                lblModelInfo.Text = Common.CurrentValue.CurrentAttributeModel.FirstOrDefault().Model.ModelName;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            ApiMethod api = new ApiMethod();
            var model = api.GetModelByBarcode(CurrentValue.Barcode);
            var data = JsonConvert.DeserializeObject<List<DocTypeItemAddModel>>(model);
            if (data != null && data.Any())
            {
                CurrentValue.VinCode = txtVinCode.Text.Trim();
                var dataSendApi = new List<DocTypeModelListData>();
                Common.CurrentValue.CurrentAttributeModel = data;

                MainForm frm = new MainForm();
                frm.ShowDialog();
            }
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnNext_Click(null, null);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
