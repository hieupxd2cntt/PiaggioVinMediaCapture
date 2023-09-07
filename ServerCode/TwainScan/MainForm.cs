using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TwainDotNet;
using System.IO;
using TwainDotNet.WinFroms;

namespace TestApp
{
    using Newtonsoft.Json;
    using System.Drawing.Imaging;
    using System.Net.Http.Headers;
    using System.Net.Http;
    using System.Net.WebSockets;
    using System.Reflection.Metadata;
    using TwainDotNet.TwainNative;
    using TwainScan.Common;
    using VINMediaCaptureApi.Controllers;
    using VINMediaCaptureEntities.Model;
    using VINMediaCaptureEntities.TaiwainAppModel;
    using System.Net.Http.Json;
    using static System.Net.WebRequestMethods;
    using VINMediaCaptureEntities.ViewModel;
    using Microsoft.VisualBasic.ApplicationServices;
    using System.Configuration;
    using TwainScan;

    public partial class MainForm : Form
    {
        private static AreaSettings AreaSettings = new AreaSettings(Units.Centimeters, 0.1f, 5.7f, 0.1F + 2.6f, 5.7f + 2.6f);

        Twain _twain;
        ScanSettings _settings;
        private ConfigModel Config = Common.GetConfig();
        private int currentImage = 0;
        private string ScanFolder = "";
        private string ScanSuccessFolder = "";
        private string ScanFailFolder = "";
        public MainForm()
        {
            InitializeComponent();
            ScanFolder = Config.ScanFolder + "/" + CurrentValue.Barcode + "/" + CurrentValue.VinCode;
            ScanSuccessFolder = Config.ScanSuccessFolder + "/" + CurrentValue.Barcode + "/" + CurrentValue.VinCode;
            ScanFailFolder = Config.ScanFailFolder + "/" + CurrentValue.Barcode + "/" + CurrentValue.VinCode;
            if (CurrentValue.CurrentAttributeModel != null && CurrentValue.CurrentAttributeModel.Any())
            {
                lblModelInfo.Text = CurrentValue.CurrentAttributeModel.FirstOrDefault().Model.ModelName;
            }

            if (!Directory.Exists(ScanFolder))
            {
                Directory.CreateDirectory(ScanFolder);
            }
            _twain = new Twain(new WinFormsWindowMessageHook(this));
            imageLst.ImageSize = new Size(255, 255);
            _twain.TransferImage += delegate (Object sender, TransferImageEventArgs args)
            {
                if (args.Image != null)
                {
                    imageLst.Images.Add(args.Image);
                    pictureBox1.Image = args.Image;
                    var fileName = String.Format("{0}/{1}.png", ScanFolder, Guid.NewGuid());
                    args.Image.Save(fileName, ImageFormat.Png);
                    widthLabel.Text = "Width: " + pictureBox1.Image.Width;
                    heightLabel.Text = "Height: " + pictureBox1.Image.Height;
                    if (imageLst.Images.Count > 0)
                    {
                        currentImage = imageLst.Images.Count - 1;
                    }
                }
            };
            _twain.ScanningComplete += delegate
            {
                Enabled = true;
            };
        }

        private void selectSource_Click(object sender, EventArgs e)
        {
            _twain.SelectSource();
        }

        private void scan_Click(object sender, EventArgs e)
        {
            imageLst.Images.Clear();
            Enabled = false;

            _settings = new ScanSettings();
            _settings.UseDocumentFeeder = useAdfCheckBox.Checked;
            _settings.ShowTwainUI = useUICheckBox.Checked;
            _settings.ShowProgressIndicatorUI = showProgressIndicatorUICheckBox.Checked;
            _settings.UseDuplex = useDuplexCheckBox.Checked;
            _settings.Resolution =
                blackAndWhiteCheckBox.Checked
                ? ResolutionSettings.Fax : ResolutionSettings.ColourPhotocopier;
            _settings.Area = !checkBoxArea.Checked ? null : AreaSettings;
            _settings.ShouldTransferAllPages = true;
            _settings.Rotation = new RotationSettings()
            {
                AutomaticRotate = autoRotateCheckBox.Checked,
                AutomaticBorderDetection = autoDetectBorderCheckBox.Checked
            };

            try
            {
                _twain.StartScanning(_settings);
            }
            catch (TwainException ex)
            {
                MessageBox.Show(ex.Message);
                Enabled = true;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            var config = Common.GetConfig();
            var scanFilesPath = config.ScanFolder + "/" + CurrentValue.Barcode + "/" + CurrentValue.VinCode;
            var files = Directory.GetFiles(scanFilesPath);
            var content = new MultipartFormDataContent();
            var currAttrs = CurrentValue.CurrentAttributeModel;
            var dataSendApi = new List<DocTypeModelListData>();
            foreach (var item in currAttrs)
            {
                var docTypeModel = new DocTypeModelListData
                {
                    attrDocType = item.DocTypeItems.DocTypeID ?? 0,
                    attrId = item.DocTypeItemAttr.ItemID,
                    itemId=item.DocTypeItems.ItemID
                };
                docTypeModel.currentSession = String.Format("{0}-{1}", CurrentValue.Barcode, CurrentValue.VinCode);
                dataSendApi.Add(docTypeModel);
            }
            var urlApi = config.WebApi.TrimEnd('/') + "/Taiwain/UploadFileAsync";
            var rs=Send2Api(urlApi, dataSendApi);
            if (rs.ResultCode>0)
            {
                MoveToSuccessFolder();
                if(MsgBox.Show("Gửi tài liệu thành công","Thông báo")== DialogResult.OK)
                {
                    TwainScan.Common.CurrentValue.User = null;
                    TwainScan.Common.CurrentValue.CurrentAttributeModel = null;
                    TwainScan.Common.CurrentValue.Barcode = String.Empty;
                    TwainScan.Common.CurrentValue.VinCode = String.Empty;
                    var frm=new frmScanBarcode();
                    frm.ShowDialog();
                }
            }
            else
            {
                MoveToFailFolder();
                MsgBox.ShowError("Gửi tài liệu thất bại", "Thông báo");
            }

        }
        private void MoveToSuccessFolder()
        {
            var config = Common.GetConfig();
            var scanFilesPath = config.ScanFolder + "/" + CurrentValue.Barcode;
            var successFolder=config.ScanSuccessFolder;
            Directory.Move(scanFilesPath, successFolder);
        }
        private void MoveToFailFolder()
        {
            var config = Common.GetConfig();
            var scanFilesPath = config.ScanFolder + "/" + CurrentValue.Barcode;
            var failFolder = config.ScanFailFolder;
            Directory.Move(scanFilesPath, failFolder);
        }
        private MobileResult Send2Api(string url, List<DocTypeModelListData> obj)
        {
            var outPut = new MobileResult { ResultCode=-1};
            var config = Common.GetConfig();
            var scanFilesPath = config.ScanFolder + "/" + CurrentValue.Barcode + "/" + CurrentValue.VinCode;
            var files = Directory.GetFiles(scanFilesPath);
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            MultipartFormDataContent mpform = new MultipartFormDataContent();
            using (var httpClient = new HttpClient(clientHandler))
            {
                var authUname = ConfigurationManager.AppSettings["UserNameApi"];
                var authPass = ConfigurationManager.AppSettings["PasswordApi"];
                var textBytes = Encoding.UTF8.GetBytes(authUname + ":" + authPass + ":" + CurrentValue.User.User.LoginName);
                httpClient.DefaultRequestHeaders.Add("UserName", CurrentValue.User.User.LoginName);
                var _authToken = Convert.ToBase64String(textBytes);
                foreach (var item in files)
                {
                    FileInfo arquivoInfo = new FileInfo(item);
                    httpClient.DefaultRequestHeaders.Add("X-Requested-By", "AM-Request");
                    
                    string Name = arquivoInfo.FullName;
                    using (FileStream fs = System.IO.File.OpenRead(Name))
                    {
                        using (var streamContent = new StreamContent(fs))
                        {
                            var fileContent = new ByteArrayContent(streamContent.ReadAsByteArrayAsync().Result);
                            mpform.Add(fileContent, Path.GetFileNameWithoutExtension(Name), Path.GetFileName(Name));
                        }
                    }                    
                }
                JsonSerializerSettings jsonSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
                var json = JsonConvert.SerializeObject(obj, jsonSettings);
                mpform.Add(new StringContent(json), "docTypeModelListData");
                try
                {
                    var response = httpClient.PostAsync(url, mpform).Result;
                    outPut = JsonConvert.DeserializeObject<MobileResult>(response.Content.ReadAsStringAsync().Result);
                }
                catch (Exception e)
                {

                }
            }
            return outPut;
        }
        private void diagnostics_Click(object sender, EventArgs e)
        {
            var diagnostics = new Diagnostics(new WinFormsWindowMessageHook(this));
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            if (currentImage <= 0)
            {
                return;
            }
            currentImage--;
            pictureBox1.Image = imageLst.Images[currentImage];
            widthLabel.Text = "Width: " + imageLst.Images[currentImage].Width;
            heightLabel.Text = "Height: " + imageLst.Images[currentImage].Height;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentImage >= imageLst.Images.Count - 1)
            {
                return;
            }
            currentImage++;
            pictureBox1.Image = imageLst.Images[currentImage];
            widthLabel.Text = "Width: " + imageLst.Images[currentImage].Width;
            heightLabel.Text = "Height: " + imageLst.Images[currentImage].Height;
        }
    }
}
