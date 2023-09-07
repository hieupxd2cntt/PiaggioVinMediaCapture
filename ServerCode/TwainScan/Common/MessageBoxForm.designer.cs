namespace TwainScan
{
    partial class MessageBoxForm
    {
        #region Form-Designer generated code

        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBoxMessage = new System.Windows.Forms.RichTextBox();
            this.messageBoxFormBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBoxForIcon = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.messageBoxFormBindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxForIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.AutoSize = true;
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(67, 123);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.MinimumSize = new System.Drawing.Size(0, 30);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 30);
            this.button1.TabIndex = 2;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // richTextBoxMessage
            // 
            this.richTextBoxMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxMessage.BackColor = System.Drawing.Color.White;
            this.richTextBoxMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxMessage.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.messageBoxFormBindingSource, "MessageText", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.richTextBoxMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxMessage.Location = new System.Drawing.Point(88, 32);
            this.richTextBoxMessage.Margin = new System.Windows.Forms.Padding(0);
            this.richTextBoxMessage.Name = "richTextBoxMessage";
            this.richTextBoxMessage.ReadOnly = true;
            this.richTextBoxMessage.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBoxMessage.Size = new System.Drawing.Size(289, 70);
            this.richTextBoxMessage.TabIndex = 0;
            this.richTextBoxMessage.TabStop = false;
            this.richTextBoxMessage.Text = "<Message>";
            this.richTextBoxMessage.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richTextBoxMessage_LinkClicked);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.pictureBoxForIcon);
            this.panel1.Controls.Add(this.richTextBoxMessage);
            this.panel1.Location = new System.Drawing.Point(-4, -5);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(401, 113);
            this.panel1.TabIndex = 1;
            // 
            // pictureBoxForIcon
            // 
            this.pictureBoxForIcon.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxForIcon.Location = new System.Drawing.Point(20, 23);
            this.pictureBoxForIcon.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBoxForIcon.Name = "pictureBoxForIcon";
            this.pictureBoxForIcon.Size = new System.Drawing.Size(64, 59);
            this.pictureBoxForIcon.TabIndex = 8;
            this.pictureBoxForIcon.TabStop = false;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(175, 123);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.MinimumSize = new System.Drawing.Size(0, 30);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 30);
            this.button2.TabIndex = 3;
            this.button2.Text = "OK";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.AutoSize = true;
            this.button3.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(283, 123);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.MinimumSize = new System.Drawing.Size(0, 30);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(100, 30);
            this.button3.TabIndex = 4;
            this.button3.Text = "OK";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            // 
            // MessageBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 159);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.messageBoxFormBindingSource, "CaptionText", true));
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(366, 166);
            this.Name = "MessageBoxForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "<Caption>";
            this.Shown += new System.EventHandler(this.messageBoxForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.messageBoxFormBindingSource)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxForIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.BindingSource messageBoxFormBindingSource;
        private System.Windows.Forms.RichTextBox richTextBoxMessage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBoxForIcon;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;

        #endregion
    }
}