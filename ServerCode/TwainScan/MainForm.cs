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
    using iTextSharp.text.pdf;
    using iTextSharp.text;
    using VINMediaCaptureEntities.Enum;

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
            lblVinCode.Text = CurrentValue.VinCode;
            LoadScanSetting();
            try
            {
                ScanFolder = Config.ScanFolder + "/" + CurrentValue.VinCode;
                ScanSuccessFolder = Config.ScanSuccessFolder + "/" + CurrentValue.VinCode;
                ScanFailFolder = Config.ScanFailFolder + "/" + CurrentValue.VinCode;
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
                    scan.Enabled = true;
                };
                //scan.Enabled = true;
            }
            catch (Exception e)
            {
                ErrorLog.WriteLog("MainForm", e.Message);
            }
        }
        private void LoadScanSetting()
        {
            try
            {
                ApiMethod method = new ApiMethod();
                var scanSetting = method.LoadScanSetting();
                if (scanSetting == null && !scanSetting.Any())
                {
                    return;
                }
                chkSendPdf.Checked = scanSetting.Where(x => x.TypeName == EScanSetting.SendPdf.GetMapping() && x.CodeVal == "on").Any() ? true : false;
                useUICheckBox.Checked = scanSetting.Where(x => x.TypeName == EScanSetting.UseUI.GetMapping() && x.CodeVal == "on").Any() ? true : false;
                useDuplexCheckBox.Checked = scanSetting.Where(x => x.TypeName == EScanSetting.UseDuplex.GetMapping() && x.CodeVal == "on").Any() ? true : false;
                useAdfCheckBox.Checked = scanSetting.Where(x => x.TypeName == EScanSetting.UseAdf.GetMapping() && x.CodeVal == "on").Any() ? true : false;
                showProgressIndicatorUICheckBox.Checked = scanSetting.Where(x => x.TypeName == EScanSetting.ShowProgressIndicator.GetMapping() && x.CodeVal == "on").Any() ? true : false;
                checkBoxArea.Checked = scanSetting.Where(x => x.TypeName == EScanSetting.Area.GetMapping() && x.CodeVal == "on").Any() ? true : false;
                autoDetectBorderCheckBox.Checked = scanSetting.Where(x => x.TypeName == EScanSetting.AutoDetectBorder.GetMapping() && x.CodeVal == "on").Any() ? true : false;
                autoRotateCheckBox.Checked = scanSetting.Where(x => x.TypeName == EScanSetting.AutoRotate.GetMapping() && x.CodeVal == "on").Any() ? true : false;
                blackAndWhiteCheckBox.Checked = scanSetting.Where(x => x.TypeName == EScanSetting.BlackAndWhite.GetMapping() && x.CodeVal == "on").Any() ? true : false;
                var dpiTemp = scanSetting.Where(x => x.TypeName == EScanSetting.DPI.GetMapping());
                if (dpiTemp!=null && dpiTemp.Any()) {
                    txtDPI.Text= dpiTemp.First().CodeVal;
                }
                
            }
            catch (Exception e)
            {
                ErrorLog.WriteLog("LoadScanSetting", e.Message);
            }

        }
        private void selectSource_Click(object sender, EventArgs e)
        {
            try
            {
                _twain.SelectSource();
            }
            catch (Exception ex)
            {
                MsgBox.ShowError("Không thể chọn máy in:" + ex.Message);
                ErrorLog.WriteLog("selectSource_Click", ex.Message);
            }

        }

        private void scan_Click(object sender, EventArgs e)
        {
            if (_twain.SourceNames == null || !_twain.SourceNames.Any())
            {
                MsgBox.ShowError("Không tìm thấy thông tin máy Scan");
                return;
            }
            try
            {
                imageLst.Images.Clear();
                //Enabled = false;
                scan.Enabled = false;
                _settings = new ScanSettings(); ;
                _settings.UseDocumentFeeder = useAdfCheckBox.Checked;
                _settings.ShowTwainUI = useUICheckBox.Checked;
                _settings.ShowProgressIndicatorUI = showProgressIndicatorUICheckBox.Checked;
                _settings.UseDuplex = useDuplexCheckBox.Checked;
                
                _settings.Resolution =
                    blackAndWhiteCheckBox.Checked
                    ? ResolutionSettings.Fax : ResolutionSettings.ColourPhotocopier;
                
                _settings.Area = !checkBoxArea.Checked ? null : AreaSettings;
                _settings.ShouldTransferAllPages = true;
                _settings.Resolution.Dpi = Convert.ToInt32(txtDPI.Text.Trim());
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
                    ErrorLog.WriteLog("scan_Click", ex.Message);
                    MsgBox.ShowError("Không thể scan:" + ex.Message);
                    scan.Enabled = true;

                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteLog("scan_Click", ex.Message);
                MsgBox.ShowError("Không thể scan." + ex.Message);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                var config = Common.GetConfig();
                var scanFilesPath = config.ScanFolder + "/" + CurrentValue.VinCode;
                var files = Directory.GetFiles(scanFilesPath);
                if (files == null || !files.Any())
                {
                    MsgBox.ShowError("Không tìm thấy tài liệu scan");
                    return;
                }

                var dataSendApi = new List<DocTypeModelListData>();

                var docTypeModel = new DocTypeModelListData
                {
                    attrDocType =(int) EDocType.ThuThapTaiLieu,
                    attrId = -1,
                    itemId = -1
                };
                docTypeModel.currentSession = String.Format("{0}", CurrentValue.VinCode);
                docTypeModel.sendPdf = chkSendPdf.Checked;
                dataSendApi.Add(docTypeModel);

                if (chkSendPdf.Checked)
                {
                    var savePdf = Common.ConvertJPG2PDF(scanFilesPath, scanFilesPath + "\\" + CurrentValue.VinCode + ".pdf");
                    if (!savePdf)
                    {
                        MsgBox.ShowError("Không thể tạo file pdf", "Thông báo");
                        return;
                    }
                }
                var urlApi = config.WebApi.TrimEnd('/') + "/Taiwain/UploadFileAsync";
                var rs = Send2Api(urlApi, dataSendApi);
                if (rs.ResultCode > 0)
                {
                    MoveToSuccessFolder();
                    if (MsgBox.Show("Gửi tài liệu thành công", "Thông báo") == DialogResult.OK)
                    {
                        this.DialogResult = DialogResult.OK;
                    }
                }
                else
                {
                    MoveToFailFolder();
                    MsgBox.ShowError("Gửi tài liệu thất bại", "Thông báo");
                }
            }
            catch (Exception ex)
            {

                ErrorLog.WriteLog("saveButton_Click", ex.Message);
                MsgBox.ShowError(ex.Message);
            }

        }
        private void MoveToSuccessFolder()
        {
            try
            {
                var config = Common.GetConfig();
                var scanFilesPath = String.Format(@"{0}\{1}", config.ScanFolder, CurrentValue.VinCode);
                var successFolder = config.ScanSuccessFolder + @"\" + CurrentValue.VinCode;
                if (!Directory.Exists(successFolder))
                {
                    Directory.CreateDirectory(successFolder);
                }
                if (Directory.Exists(successFolder))
                {
                    Directory.Move(scanFilesPath, String.Format(@"{0}\{1}_{2}", successFolder, CurrentValue.VinCode, DateTime.Now.ToString("yyyyMMddHHmmss")));
                }
                else
                {
                    Directory.Move(scanFilesPath, successFolder);
                    if (chkSendPdf.Checked) {
                        var files= Directory.GetFiles(successFolder);
                        foreach (var item in files)
                        {
                            if (Path.GetExtension(item).ToLower() !=".pdf")
                            {
                                System.IO.File.Delete(item);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ErrorLog.WriteLog("MoveToSuccessFolder", e.Message);
                MsgBox.ShowError("Không thể di chuyển sang Thư mục Scan thành công:" + e.Message);
            }

        }
        private void MoveToFailFolder()
        {
            try
            {
                var config = Common.GetConfig();
                var scanFilesPath = String.Format(@"{0}\{1}", config.ScanFolder, CurrentValue.VinCode);
                var failFolder = config.ScanFailFolder + @"\" + CurrentValue.VinCode;
                if (!Directory.Exists(config.ScanFailFolder))
                {
                    Directory.CreateDirectory(config.ScanFailFolder);
                }
                if (Directory.Exists(failFolder))
                {
                    Directory.Move(scanFilesPath, String.Format(@"{0}\{1}_{2}", failFolder, CurrentValue.VinCode, DateTime.Now.ToString("yyyyMMddHHmmss")));
                }
                else
                {
                    Directory.Move(scanFilesPath, failFolder );
                    if (chkSendPdf.Checked)
                    {
                        var files = Directory.GetFiles(failFolder );
                        foreach (var item in files)
                        {
                            if (Path.GetExtension(item).ToLower() != ".pdf")
                            {
                                System.IO.File.Delete(item);
                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                ErrorLog.WriteLog("MoveToSuccessFolder", e.Message);
                MsgBox.ShowError("Không thể di chuyển sang Thư mục Scan thành công:" + e.Message);
            }

        }
        private MobileResult Send2Api(string url, List<DocTypeModelListData> obj)
        {
            var outPut = new MobileResult { ResultCode = -1 };
            try
            {
                var config = Common.GetConfig();
                var scanFilesPath = config.ScanFolder + "/" + CurrentValue.VinCode;
                var files = Directory.GetFiles(scanFilesPath);
                if (files == null || !files.Any())
                {
                    MsgBox.ShowError("Không tìm thấy tài liệu scan");
                    outPut.ResultCode = -1;
                    return outPut;
                }
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                if (CurrentValue.User == null)
                {
                    MsgBox.ShowError("Mất thông tin đăng nhập. Vui lòng đăng xuất hệ thống và thử lại");
                    outPut.ResultCode = -1;
                    return outPut;
                }
                MultipartFormDataContent mpform = new MultipartFormDataContent();
                using (var httpClient = new HttpClient(clientHandler))
                {
                    var authUname = ConfigurationManager.AppSettings["UserNameApi"];
                    var authPass = ConfigurationManager.AppSettings["PasswordApi"];
                    var textBytes = Encoding.UTF8.GetBytes(authUname + ":" + authPass + ":" + CurrentValue.User.User.LoginName);
                    httpClient.DefaultRequestHeaders.Add("UserName", CurrentValue.User.User.LoginName);
                    httpClient.DefaultRequestHeaders.Add("MachineName", System.Environment.MachineName);
                    httpClient.DefaultRequestHeaders.Add("LineName", config.LineName);
                    var _authToken = Convert.ToBase64String(textBytes);
                    foreach (var item in files)
                    {
                        if (chkSendPdf.Checked)
                        {
                            if (Path.GetExtension(item).ToLower() == ".pdf")
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
                        }
                        else
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
            }
            catch (Exception ex)
            {
                ErrorLog.WriteLog("Send2Api", ex.Message);
                MsgBox.ShowError("Không thể gửi dữ liệu:" + ex.Message);
            }
            return outPut;
        }
        private void diagnostics_Click(object sender, EventArgs e)
        {
            try
            {
                MoveToFailFolder();
                //var diagnostics = new Diagnostics(new WinFormsWindowMessageHook(this));
            }
            catch (Exception ex)
            {
                ErrorLog.WriteLog("diagnostics_Click", ex.Message);
                MsgBox.ShowError("Không thể gửi dữ liệu:" + ex.Message);
            }

        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            if (currentImage <= 0)
            {
                return;
            }
            currentImage--;
            pictureBox1.Image = imageLst.Images[currentImage];
            widthLabel.Text = "Chiều rộng: " + imageLst.Images[currentImage].Width;
            heightLabel.Text = "Chiều cao: " + imageLst.Images[currentImage].Height;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentImage >= imageLst.Images.Count - 1)
            {
                return;
            }
            currentImage++;
            pictureBox1.Image = imageLst.Images[currentImage];
            widthLabel.Text = "Chiều rộng: " + imageLst.Images[currentImage].Width;
            heightLabel.Text = "Chiều cao: " + imageLst.Images[currentImage].Height;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.Q)
            {
                scan_Click(null, null);
            }
            else if (e.Alt && e.KeyCode == Keys.L)
            {
                saveButton_Click(null, null);
            }
        }
    }
}
