using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace TwainScan
{
    public class FunctionCommon
    {
        /// <summary>
        /// Lấy tất cả các control được add trong parent
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public IEnumerable<System.Windows.Forms.Control> GetAllControlByType(System.Windows.Forms.Control parent, Type type)
        {
            var controls = parent.Controls.Cast<System.Windows.Forms.Control>();

            return controls.SelectMany(ctrl => GetAllControlByType(ctrl, type)).OrderBy(x=>x.TabIndex)
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type);
        }

        public void SentMail()
        {
            try
            {
                SmtpClient mailclient = new SmtpClient("smtp.gmail.com", 587);
                mailclient.EnableSsl = true;
                mailclient.Credentials = new NetworkCredential("hieupham05@gmail.com", "phamhieu5789");

                MailMessage message = new MailMessage("hieupham05@gmail.com", "hieupx3@fpt.com.vn");
                message.Subject = "Mail Test";
                message.Body = "Test";

                mailclient.Send(message);
                System.Windows.Forms.MessageBox.Show("Mail đã được gửi đi", "Thành công", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
