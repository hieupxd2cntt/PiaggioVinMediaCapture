using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Diagnostics;

namespace TwainScan
{
    public partial class MessageBoxForm : Form
    {
        #region Public statics

        /// <summary>
        /// Defines the maximum width for all FlexibleMessageBox instances in percent of the working area.
        /// 
        /// Allowed values are 0.2 - 1.0 where: 
        /// 0.2 means:  The FlexibleMessageBox can be at most half as wide as the working area.
        /// 1.0 means:  The FlexibleMessageBox can be as wide as the working area.
        /// 
        /// Default is: 70% of the working area width.
        /// </summary>
        public static double MAX_WIDTH_FACTOR = 0.7;

        /// <summary>
        /// Defines the maximum height for all FlexibleMessageBox instances in percent of the working area.
        /// 
        /// Allowed values are 0.2 - 1.0 where: 
        /// 0.2 means:  The FlexibleMessageBox can be at most half as high as the working area.
        /// 1.0 means:  The FlexibleMessageBox can be as high as the working area.
        /// 
        /// Default is: 90% of the working area height.
        /// </summary>
        public static double MAX_HEIGHT_FACTOR = 0.9;

        /// <summary>
        /// Defines the font for all FlexibleMessageBox instances.
        /// 
        /// Default is: SystemFonts.MessageBoxFont
        /// </summary>
        public static Font FONT = SystemFonts.MessageBoxFont;

        #endregion

        #region Private constants

        //These separators are used for the "copy to clipboard" standard operation, triggered by Ctrl + C (behavior and clipboard format is like in a standard MessageBox)
        private static readonly String STANDARD_MESSAGEBOX_SEPARATOR_LINES = "---------------------------\n";
        private static readonly String STANDARD_MESSAGEBOX_SEPARATOR_SPACES = "   ";

        //These are the possible buttons (in a standard MessageBox)
        private enum ButtonID { OK = 0, CANCEL, YES, NO, ABORT, RETRY, IGNORE };

        //These are the buttons texts for different languages. 
        //If you want to add a new language, add it here and in the GetButtonText-Function
        private enum TwoLetterISOLanguageID { en, de, es, it, vi };
        private static readonly String[] BUTTON_TEXTS_ENGLISH_EN = { "OK", "Cancel", "&Yes", "&No", "&Abort", "&Retry", "&Ignore" }; //Note: This is also the fallback language
        private static readonly String[] BUTTON_TEXTS_VI = { "Chấp nhận", "Đóng", "&Đồng ý", "&Hủy bỏ", "&Hủy bỏ", "&Thử lại", "&Bỏ qua" }; //Note: This is also the fallback language
        private static readonly String[] BUTTON_TEXTS_GERMAN_DE = { "OK", "Abbrechen", "&Ja", "&Nein", "&Abbrechen", "&Wiederholen", "&Ignorieren" };
        private static readonly String[] BUTTON_TEXTS_SPANISH_ES = { "Aceptar", "Cancelar", "&Sí", "&No", "&Abortar", "&Reintentar", "&Ignorar" };
        private static readonly String[] BUTTON_TEXTS_ITALIAN_IT = { "OK", "Annulla", "&Sì", "&No", "&Interrompi", "&Riprova", "&Ignora" };

        private Exception _exc = null;

        public Exception Exc
        {
            get { return _exc; }
            set { _exc = value; }
        }

        #endregion

        #region Private members

        private MessageBoxDefaultButton defaultButton;
        private int visibleButtonsCount;
        private TwoLetterISOLanguageID languageID = TwoLetterISOLanguageID.vi;

        #endregion

        #region Private constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="messageBoxForm"/> class.
        /// </summary>
        private MessageBoxForm()
        {
            InitializeComponent();

            //Try to evaluate the language. If this fails, the fallback language English will be used
            System.Enum.TryParse<TwoLetterISOLanguageID>(new CultureInfo("vi", false).TwoLetterISOLanguageName, out this.languageID);
            this.KeyPreview = true;
            this.KeyUp += messageBoxForm_KeyUp;
        }

        #endregion

        #region Private helper functions

        /// <summary>
        /// Gets the string rows.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The string rows as 1-dimensional array</returns>
        private static string[] GetStringRows(string message)
        {
            if (string.IsNullOrEmpty(message)) return null;

            var messageRows = message.Split(new char[] { '\n' }, StringSplitOptions.None);
            return messageRows;
        }

        /// <summary>
        /// Gets the button text for the CurrentUICulture language.
        /// Note: The fallback language is English
        /// </summary>
        /// <param name="buttonID">The ID of the button.</param>
        /// <returns>The button text</returns>
        private string GetButtonText(ButtonID buttonID)
        {
            var buttonTextArrayIndex = Convert.ToInt32(buttonID);

            switch (this.languageID)
            {
                case TwoLetterISOLanguageID.de: return BUTTON_TEXTS_GERMAN_DE[buttonTextArrayIndex];
                case TwoLetterISOLanguageID.es: return BUTTON_TEXTS_SPANISH_ES[buttonTextArrayIndex];
                case TwoLetterISOLanguageID.it: return BUTTON_TEXTS_ITALIAN_IT[buttonTextArrayIndex];
                case TwoLetterISOLanguageID.vi: return BUTTON_TEXTS_VI[buttonTextArrayIndex];

                default: return BUTTON_TEXTS_ENGLISH_EN[buttonTextArrayIndex];
            }
        }

        /// <summary>
        /// Ensure the given working area factor in the range of  0.2 - 1.0 where: 
        /// 
        /// 0.2 means:  20 percent of the working area height or width.
        /// 1.0 means:  100 percent of the working area height or width.
        /// </summary>
        /// <param name="workingAreaFactor">The given working area factor.</param>
        /// <returns>The corrected given working area factor.</returns>
        private static double GetCorrectedWorkingAreaFactor(double workingAreaFactor)
        {
            const double MIN_FACTOR = 0.2;
            const double MAX_FACTOR = 1.0;

            if (workingAreaFactor < MIN_FACTOR) return MIN_FACTOR;
            if (workingAreaFactor > MAX_FACTOR) return MAX_FACTOR;

            return workingAreaFactor;
        }

        /// <summary>
        /// Set the dialogs start position when given. 
        /// Otherwise center the dialog on the current screen.
        /// </summary>
        /// <param name="messageBoxForm">The FlexibleMessageBox dialog.</param>
        /// <param name="owner">The owner.</param>
        private static void SetDialogStartPosition(MessageBoxForm messageBoxForm, IWin32Window owner)
        {
            //If no owner given: Center on current screen
            if (owner == null)
            {
                var screen = Screen.FromPoint(Cursor.Position);
                messageBoxForm.StartPosition = FormStartPosition.Manual;
                messageBoxForm.Left = screen.Bounds.Left + screen.Bounds.Width / 2 - messageBoxForm.Width / 2;
                messageBoxForm.Top = screen.Bounds.Top + screen.Bounds.Height / 2 - messageBoxForm.Height / 2;
            }
        }

        /// <summary>
        /// Calculate the dialogs start size (Try to auto-size width to show longest text row).
        /// Also set the maximum dialog size. 
        /// </summary>
        /// <param name="messageBoxForm">The FlexibleMessageBox dialog.</param>
        /// <param name="text">The text (the longest text row is used to calculate the dialog width).</param>
        /// <param name="text">The caption (this can also affect the dialog width).</param>
        private static void SetDialogSizes(MessageBoxForm messageBoxForm, string text, string caption, int extWidth = 0)
        {
            //First set the bounds for the maximum dialog size
            messageBoxForm.MaximumSize = new Size(Convert.ToInt32(SystemInformation.WorkingArea.Width * MessageBoxForm.GetCorrectedWorkingAreaFactor(MAX_WIDTH_FACTOR)),
                                                          Convert.ToInt32(SystemInformation.WorkingArea.Height * MessageBoxForm.GetCorrectedWorkingAreaFactor(MAX_HEIGHT_FACTOR)));

            //Get rows. Exit if there are no rows to render...
            var stringRows = GetStringRows(text);
            if (stringRows == null) return;

            //Calculate whole text height
            var textHeight = TextRenderer.MeasureText(text, FONT).Height;

            //Calculate width for longest text line
            const int SCROLLBAR_WIDTH_OFFSET = 15;
            var longestTextRowWidth = stringRows.Max(textForRow => TextRenderer.MeasureText(textForRow, FONT).Width);
            var captionWidth = TextRenderer.MeasureText(caption, SystemFonts.CaptionFont).Width;
            var textWidth = Math.Max(longestTextRowWidth + SCROLLBAR_WIDTH_OFFSET, captionWidth);

            //Calculate margins
            var marginWidth = messageBoxForm.Width - messageBoxForm.richTextBoxMessage.Width;
            var marginHeight = messageBoxForm.Height - messageBoxForm.richTextBoxMessage.Height;

            //Set calculated dialog size (if the calculated values exceed the maximums, they were cut by windows forms automatically)
            if(extWidth == 0)
                messageBoxForm.Size = new Size(textWidth + marginWidth,
                                                   textHeight + marginHeight);
            else
                messageBoxForm.Size = new Size(textWidth + marginWidth + extWidth,
                                                   textHeight + marginHeight);
            
        }

        /// <summary>
        /// Set the dialogs icon. 
        /// When no icon is used: Correct placement and width of rich text box.
        /// </summary>
        /// <param name="messageBoxForm">The FlexibleMessageBox dialog.</param>
        /// <param name="icon">The MessageBoxIcon.</param>
        private static void SetDialogIcon(MessageBoxForm messageBoxForm, MessageBoxIcon icon)
        {
            switch (icon)
            {
                case MessageBoxIcon.Information:
                    messageBoxForm.pictureBoxForIcon.Image = Resource1.Infomation;
                    break;
                case MessageBoxIcon.Warning:
                    messageBoxForm.pictureBoxForIcon.Image = Resource1.Warning;
                    break;
                case MessageBoxIcon.Error:
                    messageBoxForm.pictureBoxForIcon.Image = Resource1.Error;
                    break;
                case MessageBoxIcon.Question:
                    messageBoxForm.pictureBoxForIcon.Image = Resource1.Question;
                    break;
                
                default:
                    //When no icon is used: Correct placement and width of rich text box.
                    messageBoxForm.pictureBoxForIcon.Visible = false;
                    messageBoxForm.richTextBoxMessage.Left -= messageBoxForm.pictureBoxForIcon.Width;
                    messageBoxForm.richTextBoxMessage.Width += messageBoxForm.pictureBoxForIcon.Width;
                    break;
            }
        }

        /// <summary>
        /// Set dialog buttons visibilities and texts. 
        /// Also set a default button.
        /// </summary>
        /// <param name="messageBoxForm">The FlexibleMessageBox dialog.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="defaultButton">The default button.</param>
        private static void SetDialogButtons(MessageBoxForm messageBoxForm, MessageBoxButtons buttons, MessageBoxDefaultButton defaultButton)
        {
            int iX1 = messageBoxForm.button1.Location.X;
            int iY1 = messageBoxForm.button1.Location.Y;

            int iX2 = messageBoxForm.button2.Location.X;
            int iY2 = messageBoxForm.button2.Location.Y;

            int iX3 = messageBoxForm.button3.Location.X;
            int iY3 = messageBoxForm.button3.Location.Y;

            int iW = messageBoxForm.Width;

            //Set the buttons visibilities and texts
            switch (buttons)
            {
                case MessageBoxButtons.AbortRetryIgnore:
                    messageBoxForm.visibleButtonsCount = 3;

                    messageBoxForm.button1.Visible = true;
                    messageBoxForm.button1.Text = messageBoxForm.GetButtonText(ButtonID.ABORT);
                    messageBoxForm.button1.DialogResult = DialogResult.Abort;


                    messageBoxForm.button2.Visible = true;
                    messageBoxForm.button2.Text = messageBoxForm.GetButtonText(ButtonID.RETRY);
                    messageBoxForm.button2.DialogResult = DialogResult.Retry;

                    messageBoxForm.button3.Visible = true;
                    messageBoxForm.button3.Text = messageBoxForm.GetButtonText(ButtonID.IGNORE);
                    messageBoxForm.button3.DialogResult = DialogResult.Ignore;

                    messageBoxForm.ControlBox = false;
                    break;

                case MessageBoxButtons.OKCancel:
                   

                    messageBoxForm.visibleButtonsCount = 2;

                    messageBoxForm.button2.Visible = true;
                    messageBoxForm.button2.Text = messageBoxForm.GetButtonText(ButtonID.OK);
                    messageBoxForm.button2.DialogResult = DialogResult.OK;
                    messageBoxForm.button2.Width = 85;//75

                    iX2 = messageBoxForm.button2.Location.X;
                    iY2 = messageBoxForm.button2.Location.Y;

                    messageBoxForm.button2.Location = new Point(iX2 - 10, iY2);

                    messageBoxForm.button3.Visible = true;
                    messageBoxForm.button3.Text = messageBoxForm.GetButtonText(ButtonID.CANCEL);
                    messageBoxForm.button3.DialogResult = DialogResult.Cancel;
                    

                    messageBoxForm.CancelButton = messageBoxForm.button3;
                    break;

                case MessageBoxButtons.RetryCancel:
                    messageBoxForm.visibleButtonsCount = 2;

                    messageBoxForm.button2.Visible = true;
                    messageBoxForm.button2.Text = messageBoxForm.GetButtonText(ButtonID.RETRY);
                    messageBoxForm.button2.DialogResult = DialogResult.Retry;

                    messageBoxForm.button3.Visible = true;
                    messageBoxForm.button3.Text = messageBoxForm.GetButtonText(ButtonID.CANCEL);
                    messageBoxForm.button3.DialogResult = DialogResult.Cancel;

                    messageBoxForm.CancelButton = messageBoxForm.button3;
                    break;

                case MessageBoxButtons.YesNo:
                    {
                       

                        messageBoxForm.visibleButtonsCount = 2;

                        messageBoxForm.button2.Visible = true;
                        messageBoxForm.button2.Text = messageBoxForm.GetButtonText(ButtonID.YES);
                        messageBoxForm.button2.DialogResult = DialogResult.Yes;
                        
                        messageBoxForm.button3.Visible = true;
                        messageBoxForm.button3.Text = messageBoxForm.GetButtonText(ButtonID.NO);
                        messageBoxForm.button3.DialogResult = DialogResult.No;
                        messageBoxForm.button3.Width = 100;//75
                        

                        iX2 = messageBoxForm.button2.Location.X;
                        iY2 = messageBoxForm.button2.Location.Y;

                        iX3 = messageBoxForm.button3.Location.X;
                        iY3 = messageBoxForm.button3.Location.Y;

                        messageBoxForm.button2.Location = new Point(iX2 - 25, iY2);
                        messageBoxForm.button3.Location = new Point(iX3 - 15, iY3);

                        messageBoxForm.ControlBox = false;
                    }
                    break;

                case MessageBoxButtons.YesNoCancel:
                    {
                        messageBoxForm.visibleButtonsCount = 3;

                        messageBoxForm.button1.Visible = true;
                        messageBoxForm.button1.Text = messageBoxForm.GetButtonText(ButtonID.YES);
                        messageBoxForm.button1.DialogResult = DialogResult.Yes;
                        

                        messageBoxForm.button2.Visible = true;
                        messageBoxForm.button2.Width = 100;//75
                        messageBoxForm.button2.Text = messageBoxForm.GetButtonText(ButtonID.NO);
                        messageBoxForm.button2.DialogResult = DialogResult.No;

                        messageBoxForm.button3.Visible = true;
                        messageBoxForm.button3.Text = messageBoxForm.GetButtonText(ButtonID.CANCEL);
                        messageBoxForm.button3.DialogResult = DialogResult.Cancel;

                        iX1 = messageBoxForm.button1.Location.X;
                        iY1 = messageBoxForm.button1.Location.Y;

                        iX2 = messageBoxForm.button2.Location.X;
                        iY2 = messageBoxForm.button2.Location.Y;

                        iX3 = messageBoxForm.button3.Location.X;
                        iY3 = messageBoxForm.button3.Location.Y;

                        messageBoxForm.button1.Location = new Point(iX1 - 20, iY1);
                        messageBoxForm.button2.Location = new Point(iX2 - 23, iY2);

                       

                        messageBoxForm.CancelButton = messageBoxForm.button3;
                    }
                    break;

                case MessageBoxButtons.OK:
                default:
                    messageBoxForm.visibleButtonsCount = 1;
                    messageBoxForm.button3.Visible = true;
                    messageBoxForm.button3.Text = messageBoxForm.GetButtonText(ButtonID.CANCEL);
                    messageBoxForm.button3.DialogResult = DialogResult.OK;

                    messageBoxForm.CancelButton = messageBoxForm.button3;
                    break;
            }

            //Set default button (used in messageBoxForm_Shown)
            messageBoxForm.defaultButton = defaultButton;
        }

        #endregion

        #region Private event handlers

        /// <summary>
        /// Handles the Shown event of the messageBoxForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void messageBoxForm_Shown(object sender, EventArgs e)
        {
            int buttonIndexToFocus = 1;
            Button buttonToFocus;

            //Set the default button...
            switch (this.defaultButton)
            {
                case MessageBoxDefaultButton.Button1:
                default:
                    buttonIndexToFocus = 1;
                    break;
                case MessageBoxDefaultButton.Button2:
                    buttonIndexToFocus = 2;
                    break;
                case MessageBoxDefaultButton.Button3:
                    buttonIndexToFocus = 3;
                    break;
            }

            if (buttonIndexToFocus > this.visibleButtonsCount) buttonIndexToFocus = this.visibleButtonsCount;

            if (buttonIndexToFocus == 3)
            {
                buttonToFocus = this.button3;
            }
            else if (buttonIndexToFocus == 2)
            {
                buttonToFocus = this.button2;
            }
            else
            {
                buttonToFocus = this.button1;
            }

            buttonToFocus.Focus();
        }

        /// <summary>
        /// Handles the LinkClicked event of the richTextBoxMessage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.LinkClickedEventArgs"/> instance containing the event data.</param>
        private void richTextBoxMessage_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Process.Start(e.LinkText);
            }
            catch (Exception)
            {
                //Let the caller of messageBoxForm decide what to do with this exception...
                throw;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

        }

        /// <summary>
        /// Handles the KeyUp event of the richTextBoxMessage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        void messageBoxForm_KeyUp(object sender, KeyEventArgs e)
        {
            //Handle standard key strikes for clipboard copy: "Ctrl + C" and "Ctrl + Insert"
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.Insert))
            {
                var buttonsTextLine = (this.button1.Visible ? this.button1.Text + STANDARD_MESSAGEBOX_SEPARATOR_SPACES : string.Empty)
                                    + (this.button2.Visible ? this.button2.Text + STANDARD_MESSAGEBOX_SEPARATOR_SPACES : string.Empty)
                                    + (this.button3.Visible ? this.button3.Text + STANDARD_MESSAGEBOX_SEPARATOR_SPACES : string.Empty);

                //Build same clipboard text like the standard .Net MessageBox
                var textForClipboard = STANDARD_MESSAGEBOX_SEPARATOR_LINES
                                     + this.Text + Environment.NewLine
                                     + STANDARD_MESSAGEBOX_SEPARATOR_LINES
                                     + this.richTextBoxMessage.Text + Environment.NewLine
                                     + STANDARD_MESSAGEBOX_SEPARATOR_LINES
                                     + buttonsTextLine.Replace("&", string.Empty) + Environment.NewLine
                                     + STANDARD_MESSAGEBOX_SEPARATOR_LINES;

                //Set text in clipboard
                Clipboard.SetText(textForClipboard);
            }
        }

        #endregion

        #region Properties (only used for binding)

        /// <summary>
        /// The text that is been used for the heading.
        /// </summary>
        public string CaptionText { get; set; }

        /// <summary>
        /// The text that is been used in the messageBoxForm.
        /// </summary>
        public string MessageText { get; set; }

        #endregion

        #region Public show function

        /// <summary>
        /// Shows the specified message box.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="defaultButton">The default button.</param>
        /// <returns>The dialog result.</returns>
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, Exception ex = null)
        {
            
            //Create a new instance of the FlexibleMessageBox form
            var messageBoxForm = new MessageBoxForm();
            messageBoxForm.ShowInTaskbar = false;
            messageBoxForm.Exc = ex;

            //Bind the caption and the message text
            messageBoxForm.CaptionText = caption;
            messageBoxForm.MessageText = text;
            messageBoxForm.messageBoxFormBindingSource.DataSource = messageBoxForm;

            Image im = Resource1.Error;

            //Set the buttons visibilities and texts. Also set a default button.
            SetDialogButtons(messageBoxForm, buttons, defaultButton);

            //Set the dialogs icon. When no icon is used: Correct placement and width of rich text box.
            if (icon == MessageBoxIcon.None)
                icon = MessageBoxIcon.Information;

            SetDialogIcon(messageBoxForm, icon);

            //Set the font for all controls
            messageBoxForm.Font = FONT;
            messageBoxForm.richTextBoxMessage.Font = FONT;

            //Calculate the dialogs start size (Try to auto-size width to show longest text row). Also set the maximum dialog size. 
            SetDialogSizes(messageBoxForm, text, caption, buttons == MessageBoxButtons.YesNoCancel ? 200 : 0);

            //Set the dialogs start position when given. Otherwise center the dialog on the current screen.
            SetDialogStartPosition(messageBoxForm, owner);

            //Show the dialog
            return messageBoxForm.ShowDialog(owner);
        }

        #endregion
        #region message


        protected void ShowError(string msg)
        {
            MsgBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        protected void ShowError(string title, string msg)
        {
            MsgBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        protected void ShowError(string title, string msg, Exception ex)
        {
            MsgBox.ShowError(msg, title, ex);
        }

        protected void ShowWarning(string msg)
        {
            MsgBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        protected void ShowWarning(string title, string msg)
        {
            MsgBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }


        protected DialogResult ShowQuestion(string title, string msg, MsgType type = MsgType.Ok)
        {
            return MsgBox.Show(msg, title, MessageBoxButtons.YesNo);
        }
        protected DialogResult ShowQuestion(string msg, MsgType type)
        {
            return MsgBox.Show(msg, "Thông báo", MessageBoxButtons.YesNo);
        }

        protected void ShowMessage(string title, string msg)
        {
            MsgBox.Show(msg, title, MessageBoxButtons.OK);
        }

        protected void ShowMessage(string msg)
        {
            MsgBox.Show(msg, "Thông báo", MessageBoxButtons.OK);
        }


        #endregion
    }
}
