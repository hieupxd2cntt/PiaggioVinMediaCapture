namespace TwainScan
{
    partial class frmConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConfig));
            this.label1 = new System.Windows.Forms.Label();
            this.txtScanFolder = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtHostUrl = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtApiUrl = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtScanFailFolder = new System.Windows.Forms.TextBox();
            this.btnBrowseScanFail = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtScanSuccessFolder = new System.Windows.Forms.TextBox();
            this.btnBrowseSuccessFolder = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLogPath = new System.Windows.Forms.TextBox();
            this.btnBrowseLogPath = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtLineName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Scan Folder";
            // 
            // txtScanFolder
            // 
            this.txtScanFolder.Location = new System.Drawing.Point(156, 18);
            this.txtScanFolder.Name = "txtScanFolder";
            this.txtScanFolder.Size = new System.Drawing.Size(331, 23);
            this.txtScanFolder.TabIndex = 1;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(493, 18);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(38, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Host Url";
            // 
            // txtHostUrl
            // 
            this.txtHostUrl.Location = new System.Drawing.Point(156, 141);
            this.txtHostUrl.Name = "txtHostUrl";
            this.txtHostUrl.Size = new System.Drawing.Size(375, 23);
            this.txtHostUrl.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 185);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "Api Url";
            // 
            // txtApiUrl
            // 
            this.txtApiUrl.Location = new System.Drawing.Point(156, 182);
            this.txtApiUrl.Name = "txtApiUrl";
            this.txtApiUrl.Size = new System.Drawing.Size(375, 23);
            this.txtApiUrl.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(156, 322);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(73, 32);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave1_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(254, 322);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(73, 32);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Đóng";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "Folder Scan Lỗi";
            // 
            // txtScanFailFolder
            // 
            this.txtScanFailFolder.Location = new System.Drawing.Point(156, 57);
            this.txtScanFailFolder.Name = "txtScanFailFolder";
            this.txtScanFailFolder.Size = new System.Drawing.Size(331, 23);
            this.txtScanFailFolder.TabIndex = 1;
            // 
            // btnBrowseScanFail
            // 
            this.btnBrowseScanFail.Location = new System.Drawing.Point(493, 57);
            this.btnBrowseScanFail.Name = "btnBrowseScanFail";
            this.btnBrowseScanFail.Size = new System.Drawing.Size(38, 23);
            this.btnBrowseScanFail.TabIndex = 2;
            this.btnBrowseScanFail.Text = "...";
            this.btnBrowseScanFail.UseVisualStyleBackColor = true;
            this.btnBrowseScanFail.Click += new System.EventHandler(this.btnBrowseScanFail_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "Folder Scan Thành Công";
            // 
            // txtScanSuccessFolder
            // 
            this.txtScanSuccessFolder.Location = new System.Drawing.Point(156, 96);
            this.txtScanSuccessFolder.Name = "txtScanSuccessFolder";
            this.txtScanSuccessFolder.Size = new System.Drawing.Size(331, 23);
            this.txtScanSuccessFolder.TabIndex = 1;
            // 
            // btnBrowseSuccessFolder
            // 
            this.btnBrowseSuccessFolder.Location = new System.Drawing.Point(493, 96);
            this.btnBrowseSuccessFolder.Name = "btnBrowseSuccessFolder";
            this.btnBrowseSuccessFolder.Size = new System.Drawing.Size(38, 23);
            this.btnBrowseSuccessFolder.TabIndex = 2;
            this.btnBrowseSuccessFolder.Text = "...";
            this.btnBrowseSuccessFolder.UseVisualStyleBackColor = true;
            this.btnBrowseSuccessFolder.Click += new System.EventHandler(this.btnBrowseSuccessFolder_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 233);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 15);
            this.label6.TabIndex = 0;
            this.label6.Text = "Folder log Lỗi";
            // 
            // txtLogPath
            // 
            this.txtLogPath.Location = new System.Drawing.Point(156, 230);
            this.txtLogPath.Name = "txtLogPath";
            this.txtLogPath.Size = new System.Drawing.Size(331, 23);
            this.txtLogPath.TabIndex = 1;
            // 
            // btnBrowseLogPath
            // 
            this.btnBrowseLogPath.Location = new System.Drawing.Point(493, 230);
            this.btnBrowseLogPath.Name = "btnBrowseLogPath";
            this.btnBrowseLogPath.Size = new System.Drawing.Size(38, 23);
            this.btnBrowseLogPath.TabIndex = 2;
            this.btnBrowseLogPath.Text = "...";
            this.btnBrowseLogPath.UseVisualStyleBackColor = true;
            this.btnBrowseLogPath.Click += new System.EventHandler(this.btnBrowseLogPath_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 281);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 15);
            this.label7.TabIndex = 0;
            this.label7.Text = "Tên line";
            // 
            // txtLineName
            // 
            this.txtLineName.Location = new System.Drawing.Point(156, 278);
            this.txtLineName.Name = "txtLineName";
            this.txtLineName.Size = new System.Drawing.Size(375, 23);
            this.txtLineName.TabIndex = 1;
            // 
            // frmConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 407);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnBrowseSuccessFolder);
            this.Controls.Add(this.btnBrowseLogPath);
            this.Controls.Add(this.btnBrowseScanFail);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtApiUrl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtScanSuccessFolder);
            this.Controls.Add(this.txtLineName);
            this.Controls.Add(this.txtHostUrl);
            this.Controls.Add(this.txtLogPath);
            this.Controls.Add(this.txtScanFailFolder);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtScanFolder);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cấu hình hệ thống";
            this.Load += new System.EventHandler(this.frmConfig_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private TextBox txtScanFolder;
        private Button btnBrowse;
        private Label label2;
        private TextBox txtHostUrl;
        private Label label3;
        private TextBox txtApiUrl;
        private Button btnSave;
        private Button btnClose;
        private Label label4;
        private TextBox txtScanFailFolder;
        private Button btnBrowseScanFail;
        private Label label5;
        private TextBox txtScanSuccessFolder;
        private Button btnBrowseSuccessFolder;
        private Label label6;
        private TextBox txtLogPath;
        private Button btnBrowseLogPath;
        private Label label7;
        private TextBox txtLineName;
    }
}