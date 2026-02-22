namespace DVLD.License
{
    partial class ShowLicenseForm
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
            this.driver_License_Info1 = new DVLD.License.Driver_License_Info();
            this.SuspendLayout();
            // 
            // driver_License_Info1
            // 
            this.driver_License_Info1.Location = new System.Drawing.Point(12, 26);
            this.driver_License_Info1.Name = "driver_License_Info1";
            this.driver_License_Info1.Size = new System.Drawing.Size(886, 571);
            this.driver_License_Info1.TabIndex = 0;
            // 
            // ShowLicenseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 630);
            this.Controls.Add(this.driver_License_Info1);
            this.Name = "ShowLicenseForm";
            this.Text = "ShowLicenseForm";
            this.ResumeLayout(false);

        }

        #endregion

        private Driver_License_Info driver_License_Info1;
    }
}