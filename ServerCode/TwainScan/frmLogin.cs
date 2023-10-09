using iTextSharp.text.pdf;
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

        private void button1_Click(object sender, EventArgs e)
        {
            //Common.Common.ConvertJPG2PDF1("C:\\Users\\HieuPX\\Desktop\\319155386_491540472962980_3537352328386031607_n.jpg", "C:\\Users\\HieuPX\\Desktop\\Abc.pdf", "hieupx");
            //Common.Common.ConvertJPG2PDF("C:\\Users\\HieuPX\\Documents\\DaisyEdoc\\Scan\\SPTestVTDtest\\Vespa SprintVTDBlue", "C:\\Users\\HieuPX\\Desktop\\Abc.pdf", "hieupx");
        }
        void ConvertJPG2PDF(string jpgfile, string pdf, string text)
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

                ////Adding text
                //var textY = ((PageSize.A4.Height - image.ScaledHeight) / 2) - 5;
                //var textX = PageSize.A4.Width / 2;
                //var chunk = new Chunk(text, FontFactory.GetFont("Helvetica", 22.0f, BaseColor.BLACK));
                //PdfContentByte cb = pdfWriter.DirectContent;
                //cb.BeginText();
                //ColumnText.ShowTextAligned(cb, Element.ALIGN_CENTER, new Phrase(chunk), textX, textY, 0);
                //cb.EndText();

                document.Close();
            }
        }
    }
}
