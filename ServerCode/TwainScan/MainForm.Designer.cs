namespace TestApp
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.selectSource = new System.Windows.Forms.Button();
            this.scan = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.useAdfCheckBox = new System.Windows.Forms.CheckBox();
            this.useUICheckBox = new System.Windows.Forms.CheckBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.blackAndWhiteCheckBox = new System.Windows.Forms.CheckBox();
            this.widthLabel = new System.Windows.Forms.Label();
            this.heightLabel = new System.Windows.Forms.Label();
            this.diagnosticsButton = new System.Windows.Forms.Button();
            this.checkBoxArea = new System.Windows.Forms.CheckBox();
            this.showProgressIndicatorUICheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.useDuplexCheckBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.autoRotateCheckBox = new System.Windows.Forms.CheckBox();
            this.autoDetectBorderCheckBox = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnPre = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.imageLst = new System.Windows.Forms.ImageList(this.components);
            this.lblModelInfo = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // selectSource
            // 
            this.selectSource.Location = new System.Drawing.Point(14, 78);
            this.selectSource.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.selectSource.Name = "selectSource";
            this.selectSource.Size = new System.Drawing.Size(136, 55);
            this.selectSource.TabIndex = 0;
            this.selectSource.Text = "Select Source";
            this.selectSource.UseVisualStyleBackColor = true;
            this.selectSource.Click += new System.EventHandler(this.selectSource_Click);
            // 
            // scan
            // 
            this.scan.Location = new System.Drawing.Point(14, 140);
            this.scan.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.scan.Name = "scan";
            this.scan.Size = new System.Drawing.Size(136, 61);
            this.scan.TabIndex = 1;
            this.scan.Text = "Scan";
            this.scan.UseVisualStyleBackColor = true;
            this.scan.Click += new System.EventHandler(this.scan_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(172, 78);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(537, 693);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // useAdfCheckBox
            // 
            this.useAdfCheckBox.AutoSize = true;
            this.useAdfCheckBox.Location = new System.Drawing.Point(14, 221);
            this.useAdfCheckBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.useAdfCheckBox.Name = "useAdfCheckBox";
            this.useAdfCheckBox.Size = new System.Drawing.Size(70, 19);
            this.useAdfCheckBox.TabIndex = 3;
            this.useAdfCheckBox.Text = "Use ADF";
            this.useAdfCheckBox.UseVisualStyleBackColor = true;
            // 
            // useUICheckBox
            // 
            this.useUICheckBox.AutoSize = true;
            this.useUICheckBox.Location = new System.Drawing.Point(14, 276);
            this.useUICheckBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.useUICheckBox.Name = "useUICheckBox";
            this.useUICheckBox.Size = new System.Drawing.Size(59, 19);
            this.useUICheckBox.TabIndex = 4;
            this.useUICheckBox.Text = "Use UI";
            this.useUICheckBox.UseVisualStyleBackColor = true;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(14, 500);
            this.saveButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(136, 61);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // blackAndWhiteCheckBox
            // 
            this.blackAndWhiteCheckBox.AutoSize = true;
            this.blackAndWhiteCheckBox.Location = new System.Drawing.Point(14, 332);
            this.blackAndWhiteCheckBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.blackAndWhiteCheckBox.Name = "blackAndWhiteCheckBox";
            this.blackAndWhiteCheckBox.Size = new System.Drawing.Size(60, 19);
            this.blackAndWhiteCheckBox.TabIndex = 6;
            this.blackAndWhiteCheckBox.Text = "B && W";
            this.blackAndWhiteCheckBox.UseVisualStyleBackColor = true;
            // 
            // widthLabel
            // 
            this.widthLabel.AutoSize = true;
            this.widthLabel.Location = new System.Drawing.Point(14, 387);
            this.widthLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.widthLabel.Name = "widthLabel";
            this.widthLabel.Size = new System.Drawing.Size(39, 15);
            this.widthLabel.TabIndex = 7;
            this.widthLabel.Text = "Width";
            // 
            // heightLabel
            // 
            this.heightLabel.AutoSize = true;
            this.heightLabel.Location = new System.Drawing.Point(14, 402);
            this.heightLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.heightLabel.Name = "heightLabel";
            this.heightLabel.Size = new System.Drawing.Size(43, 15);
            this.heightLabel.TabIndex = 8;
            this.heightLabel.Text = "Height";
            // 
            // diagnosticsButton
            // 
            this.diagnosticsButton.Location = new System.Drawing.Point(14, 568);
            this.diagnosticsButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.diagnosticsButton.Name = "diagnosticsButton";
            this.diagnosticsButton.Size = new System.Drawing.Size(136, 46);
            this.diagnosticsButton.TabIndex = 3;
            this.diagnosticsButton.Text = "Diagnostics";
            this.diagnosticsButton.UseVisualStyleBackColor = true;
            this.diagnosticsButton.Click += new System.EventHandler(this.diagnostics_Click);
            // 
            // checkBoxArea
            // 
            this.checkBoxArea.AutoSize = true;
            this.checkBoxArea.Location = new System.Drawing.Point(14, 358);
            this.checkBoxArea.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxArea.Name = "checkBoxArea";
            this.checkBoxArea.Size = new System.Drawing.Size(76, 19);
            this.checkBoxArea.TabIndex = 10;
            this.checkBoxArea.Text = "Grab area";
            this.checkBoxArea.UseVisualStyleBackColor = true;
            // 
            // showProgressIndicatorUICheckBox
            // 
            this.showProgressIndicatorUICheckBox.AutoSize = true;
            this.showProgressIndicatorUICheckBox.Location = new System.Drawing.Point(14, 303);
            this.showProgressIndicatorUICheckBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.showProgressIndicatorUICheckBox.Name = "showProgressIndicatorUICheckBox";
            this.showProgressIndicatorUICheckBox.Size = new System.Drawing.Size(103, 19);
            this.showProgressIndicatorUICheckBox.TabIndex = 11;
            this.showProgressIndicatorUICheckBox.Text = "Show Progress";
            this.showProgressIndicatorUICheckBox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(14, 215);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 2);
            this.label1.TabIndex = 12;
            // 
            // useDuplexCheckBox
            // 
            this.useDuplexCheckBox.AutoSize = true;
            this.useDuplexCheckBox.Location = new System.Drawing.Point(14, 247);
            this.useDuplexCheckBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.useDuplexCheckBox.Name = "useDuplexCheckBox";
            this.useDuplexCheckBox.Size = new System.Drawing.Size(85, 19);
            this.useDuplexCheckBox.TabIndex = 13;
            this.useDuplexCheckBox.Text = "Use Duplex";
            this.useDuplexCheckBox.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(14, 271);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 2);
            this.label2.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(14, 326);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 2);
            this.label3.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Location = new System.Drawing.Point(14, 429);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 2);
            this.label4.TabIndex = 16;
            // 
            // autoRotateCheckBox
            // 
            this.autoRotateCheckBox.AutoSize = true;
            this.autoRotateCheckBox.Location = new System.Drawing.Point(14, 461);
            this.autoRotateCheckBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.autoRotateCheckBox.Name = "autoRotateCheckBox";
            this.autoRotateCheckBox.Size = new System.Drawing.Size(89, 19);
            this.autoRotateCheckBox.TabIndex = 18;
            this.autoRotateCheckBox.Text = "Auto Rotate";
            this.autoRotateCheckBox.UseVisualStyleBackColor = true;
            // 
            // autoDetectBorderCheckBox
            // 
            this.autoDetectBorderCheckBox.AutoSize = true;
            this.autoDetectBorderCheckBox.Location = new System.Drawing.Point(14, 434);
            this.autoDetectBorderCheckBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.autoDetectBorderCheckBox.Name = "autoDetectBorderCheckBox";
            this.autoDetectBorderCheckBox.Size = new System.Drawing.Size(127, 19);
            this.autoDetectBorderCheckBox.TabIndex = 17;
            this.autoDetectBorderCheckBox.Text = "Auto Detect Border";
            this.autoDetectBorderCheckBox.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Location = new System.Drawing.Point(14, 484);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(140, 2);
            this.label5.TabIndex = 19;
            // 
            // btnPre
            // 
            this.btnPre.Location = new System.Drawing.Point(343, 777);
            this.btnPre.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnPre.Name = "btnPre";
            this.btnPre.Size = new System.Drawing.Size(82, 31);
            this.btnPre.TabIndex = 20;
            this.btnPre.Text = "<<";
            this.btnPre.UseVisualStyleBackColor = true;
            this.btnPre.Click += new System.EventHandler(this.btnPre_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(450, 777);
            this.btnNext.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(82, 31);
            this.btnNext.TabIndex = 20;
            this.btnNext.Text = ">>";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // imageLst
            // 
            this.imageLst.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageLst.ImageSize = new System.Drawing.Size(16, 16);
            this.imageLst.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // lblModelInfo
            // 
            this.lblModelInfo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblModelInfo.ForeColor = System.Drawing.Color.Red;
            this.lblModelInfo.Location = new System.Drawing.Point(16, 19);
            this.lblModelInfo.Name = "lblModelInfo";
            this.lblModelInfo.Size = new System.Drawing.Size(693, 36);
            this.lblModelInfo.TabIndex = 21;
            this.lblModelInfo.Text = "Thông tin xe";
            this.lblModelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 641);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(136, 61);
            this.button1.TabIndex = 2;
            this.button1.Text = "Test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 808);
            this.Controls.Add(this.lblModelInfo);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPre);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.autoRotateCheckBox);
            this.Controls.Add(this.autoDetectBorderCheckBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.useDuplexCheckBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.showProgressIndicatorUICheckBox);
            this.Controls.Add(this.checkBoxArea);
            this.Controls.Add(this.diagnosticsButton);
            this.Controls.Add(this.heightLabel);
            this.Controls.Add(this.widthLabel);
            this.Controls.Add(this.blackAndWhiteCheckBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.useUICheckBox);
            this.Controls.Add(this.useAdfCheckBox);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.scan);
            this.Controls.Add(this.selectSource);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scan tài liệu";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button selectSource;
        private System.Windows.Forms.Button scan;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox useAdfCheckBox;
        private System.Windows.Forms.CheckBox useUICheckBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.CheckBox blackAndWhiteCheckBox;
        private System.Windows.Forms.Label widthLabel;
        private System.Windows.Forms.Label heightLabel;
        private System.Windows.Forms.Button diagnosticsButton;
        private System.Windows.Forms.CheckBox checkBoxArea;
        private System.Windows.Forms.CheckBox showProgressIndicatorUICheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox useDuplexCheckBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox autoRotateCheckBox;
        private System.Windows.Forms.CheckBox autoDetectBorderCheckBox;
        private System.Windows.Forms.Label label5;
        private Button btnPre;
        private Button btnNext;
        private ImageList imageLst;
        private Label lblModelInfo;
        private Button button1;
    }
}

