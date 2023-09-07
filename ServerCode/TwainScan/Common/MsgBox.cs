using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TwainScan
{
    public class MsgBox
    {
        /// <summary>
        /// Shows the specified message box.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <returns>The dialog result.</returns>
        public static DialogResult Show(string stext, string scaption)
        {
            return MessageBoxForm.Show(null, stext, scaption, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        }
        public static DialogResult ShowError(string text, string caption, Exception ex)
        {
            return MessageBoxForm.Show(null, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, ex);
        }
        public static DialogResult ShowError(string text, string caption="Thông báo")
        {
            return MessageBoxForm.Show(null, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        }
        public static DialogResult ShowError(string text)
        {
            return MessageBoxForm.Show(null, text,"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// Shows the specified message box.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <returns>The dialog result.</returns>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons)
        {
            if ((buttons == MessageBoxButtons.OKCancel) || (buttons == MessageBoxButtons.YesNo))
                return MessageBoxForm.Show(null, text, caption, buttons, MessageBoxIcon.None, MessageBoxDefaultButton.Button2);
            else
            return MessageBoxForm.Show(null, text, caption, buttons, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// Shows the specified message box.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="icon">The icon.</param>
        /// <returns></returns>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            if ((buttons == MessageBoxButtons.OKCancel) || 
                (buttons == MessageBoxButtons.YesNo)
                )
                return MessageBoxForm.Show(null, text, caption, buttons, icon, MessageBoxDefaultButton.Button2);
            else
                return MessageBoxForm.Show(null, text, caption, buttons, icon, MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// Shows the specified message box.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="defaultButton">The default button.</param>
        /// <returns>The dialog result.</returns>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        {
            return MessageBoxForm.Show(null, text, caption, buttons, icon, defaultButton);
        }

        public static DialogResult ShowDelete(string sText, string sCaption)
        {
            //"Xác nhận xóa dữ liệu?", "Xóa dữ liệu?"
            sText = string.IsNullOrEmpty(sText) ? "Xác nhận xóa dữ liệu?" : sText;
            sCaption = string.IsNullOrEmpty(sCaption) ? "Xóa dữ liệu?" : sCaption;
            return MessageBoxForm.Show(null, sText, sCaption,MessageBoxButtons.YesNo, MessageBoxIcon.Warning,MessageBoxDefaultButton.Button1);
        }
        public static DialogResult ShowWarning(string text, string caption)
        {
            return MessageBoxForm.Show(null, text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
        }
        public static DialogResult ShowSendReport(string text, string caption)
        {
            return MessageBoxForm.Show(null, text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
        }
    }
}
