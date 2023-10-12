using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using VINMediaCaptureEntities.Entities;
using VINMediaCaptureEntities.Model;
using VINMediaCaptureEntities.TaiwainAppModel;
using VINMediaCaptureEntities.ViewModel;

namespace TwainScan.Common
{
    public class Common
    {
        public static void ConvertJPG2PDF1(string jpgfile, string pdf, string text)
        {
            var document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 25, 25, 25, 25);
            PdfWriter pdfWriter;
            using (var stream = new FileStream(pdf, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                //QR Code

                pdfWriter = PdfWriter.GetInstance(document, stream);
                document.Open();
                iTextSharp.text.Image image;
                using (var imageStream = new FileStream(jpgfile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    image = iTextSharp.text.Image.GetInstance(imageStream);
                    if (image.Height > iTextSharp.text.PageSize.A4.Height - 25)
                    {
                        image.ScaleToFit(iTextSharp.text.PageSize.A4.Width - 25, iTextSharp.text.PageSize.A4.Height - 25);
                    }
                    else if (image.Width > iTextSharp.text.PageSize.A4.Width - 25)
                    {
                        image.ScaleToFit(iTextSharp.text.PageSize.A4.Width - 25, iTextSharp.text.PageSize.A4.Height - 25);
                    }
                    image.Alignment = iTextSharp.text.Image.ALIGN_MIDDLE;
                    document.Add(image);

                }

                //Adding text
                var textY = ((PageSize.A4.Height - image.ScaledHeight) / 2) - 5;
                var textX = PageSize.A4.Width / 2;
                var chunk = new Chunk(text, FontFactory.GetFont("Helvetica", 22.0f, BaseColor.BLACK));
                PdfContentByte cb = pdfWriter.DirectContent;
                cb.BeginText();
                ColumnText.ShowTextAligned(cb, Element.ALIGN_CENTER, new Phrase(chunk), textX, textY, 0);
                cb.EndText();

                document.Close();
            }
        }
        public static bool ConvertJPG2PDF(string imgPath, string pdf)
        {
            try
            {
                if (string.IsNullOrEmpty(imgPath) || !Directory.Exists(imgPath))
                {
                    MsgBox.ShowError("Thư mục không tồn tại");
                    return false;
                }
                var files = Directory.GetFiles(imgPath);
                if (files == null || !files.Any())
                {
                    MsgBox.ShowError("Không tìm thấy tài liệu scan");
                    return false;
                }
                var document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 0, 0,0,0);

                iTextSharp.text.Document doc = new iTextSharp.text.Document();
                if (File.Exists(pdf))
                {
                    File.Delete(pdf);
                }
                try
                {
                    var writer=PdfWriter.GetInstance(doc, new FileStream(pdf, FileMode.Create));
                    writer.SetPdfVersion(PdfWriter.PDF_VERSION_1_7);
                    writer.CompressionLevel = PdfStream.NO_COMPRESSION;
                    writer.SetFullCompression();
                    doc.Open();
                    foreach (var jpgfile in files)
                    {
                        //doc.Add(new Paragraph("GIF"));
                        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(jpgfile);
                        
                        if (image.Height > iTextSharp.text.PageSize.A4.Height)
                        {
                            image.ScaleToFit(iTextSharp.text.PageSize.A4.Width, iTextSharp.text.PageSize.A4.Height);
                        }
                        else if (image.Width > iTextSharp.text.PageSize.A4.Width )
                        {
                            image.ScaleToFit(iTextSharp.text.PageSize.A4.Width, iTextSharp.text.PageSize.A4.Height);
                        }
                        image.Alignment = iTextSharp.text.Image.ALIGN_MIDDLE;
                        doc.NewPage();
                        doc.Add(image);
                    }

                }
                catch (Exception ex)
                {
                    ErrorLog.WriteLog("ConvertJPG2PDF", ex.Message);
                    //Log error;
                }
                finally
                {
                    doc.Close();
                }
               
                document.Close();
                return true;
            }
            catch (Exception e)
            {
                ErrorLog.WriteLog("ConvertJPG2PDF", e.Message);
                return false;
            }
        }
        
        public static ConfigModel GetConfig()
        {
            try
            {
                if (CurrentValue.CurrConfig != null)
                {
                    return CurrentValue.CurrConfig;
                }
                var xml = new XMLProcess();
                var configModel = new ConfigModel();
                var configFile = System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonApplicationData) + ConstantConfig.DefaultScanConfig;
                if (!Directory.Exists(Path.GetDirectoryName(configFile)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(configFile));
                }
                if (File.Exists(configFile))
                {
                    var xmlString = xml.LoadXmlFromFile(configFile);
                    configModel = (ConfigModel)xml.LoadObjectFromXMLString(xmlString, typeof(ConfigModel));

                }
                if (String.IsNullOrEmpty(configModel.ScanFolder))
                {
                    configModel.ScanFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + ConstantConfig.DefaultScanFolder;
                }
                if (String.IsNullOrEmpty(configModel.ScanFailFolder))
                {
                    configModel.ScanFailFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + ConstantConfig.DefaultScanFailFolder;
                }
                if (String.IsNullOrEmpty(configModel.ScanSuccessFolder))
                {
                    configModel.ScanSuccessFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + ConstantConfig.DefaultScanSuccessFolder;
                }
                if (String.IsNullOrEmpty(configModel.WebApi))
                {
                    configModel.WebApi = ConstantConfig.ApiUrl;
                }
                if (String.IsNullOrEmpty(configModel.WebApp))
                {
                    configModel.WebApp = ConstantConfig.HostUrl;
                }
                if (String.IsNullOrEmpty(configModel.LogPath))
                {
                    configModel.LogPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + ConstantConfig.LogPath;
                }
                CurrentValue.CurrConfig = configModel;
                return configModel;
            }
            catch (Exception ex)
            {
                return new ConfigModel();

            }
        }
    }
    public class CurrentValue
    {
        public static ConfigModel CurrConfig { get; set; }
        public static String VinCode { get; set; }
        public static UserLoginModel User { get; set; }
        public static List<DocTypeItemAddModel> CurrentAttributeModel { get; set; }
        public static void ClearCurrentBarcodeValue()
        {
            VinCode = "";
        }
    }
}
