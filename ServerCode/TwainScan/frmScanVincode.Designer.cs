namespace TwainScan
{
    partial class frmScanVincode
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtVinCode = new System.Windows.Forms.TextBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lblModelInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Barcode";
            // 
            // txtVinCode
            // 
            this.txtVinCode.Location = new System.Drawing.Point(111, 96);
            this.txtVinCode.Name = "txtVinCode";
            this.txtVinCode.Size = new System.Drawing.Size(392, 23);
            this.txtVinCode.TabIndex = 1;
            this.txtVinCode.Text = "Vespa SprintVTDBlue";
            this.txtVinCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBarcode_KeyDown);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(111, 137);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 2;
            this.btnBack.Text = "Quay lại";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(192, 137);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Tiếp theo";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // lblModelInfo
            // 
            this.lblModelInfo.AutoSize = true;
            this.lblModelInfo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblModelInfo.ForeColor = System.Drawing.Color.Red;
            this.lblModelInfo.Location = new System.Drawing.Point(17, 21);
            this.lblModelInfo.Name = "lblModelInfo";
            this.lblModelInfo.Size = new System.Drawing.Size(125, 25);
            this.lblModelInfo.TabIndex = 0;
            this.lblModelInfo.Text = "Thông tin xe";
            // 
            // frmScanVincode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 306);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.txtVinCode);
            this.Controls.Add(this.lblModelInfo);
            this.Controls.Add(this.label1);
            this.Name = "frmScanVincode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scan barcode";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private TextBox txtVinCode;
        private Button btnBack;
        private Button button1;
        private Label lblModelInfo;
    }
}