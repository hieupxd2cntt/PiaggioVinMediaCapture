namespace TwainScan
{
    partial class frmScanBarcode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmScanBarcode));
            this.btnNext = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtVincode = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(104, 80);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 3;
            this.btnNext.Text = "Tiếp theo";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Vincode";
            // 
            // txtVincode
            // 
            this.txtVincode.Location = new System.Drawing.Point(104, 39);
            this.txtVincode.Name = "txtVincode";
            this.txtVincode.Size = new System.Drawing.Size(392, 23);
            this.txtVincode.TabIndex = 2;
            this.txtVincode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVincode_KeyDown);
            // 
            // frmScanBarcode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 122);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.txtVincode);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmScanBarcode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scan barcode";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button btnNext;
        private Label label2;
        private TextBox txtVincode;
    }
}