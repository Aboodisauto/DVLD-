namespace DVLD.Applications
{
    partial class ShowApplicationfrm
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
            this.applicationBasicInfo1 = new DVLD.Applications.ApplicationBasicInfo();
            this.driving_License_Application_Info1 = new DVLD.Tests.Driving_License_Application_Info();
            this.SuspendLayout();
            // 
            // applicationBasicInfo1
            // 
            this.applicationBasicInfo1.Location = new System.Drawing.Point(4, 223);
            this.applicationBasicInfo1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.applicationBasicInfo1.Name = "applicationBasicInfo1";
            this.applicationBasicInfo1.Size = new System.Drawing.Size(1107, 327);
            this.applicationBasicInfo1.TabIndex = 0;
            // 
            // driving_License_Application_Info1
            // 
            this.driving_License_Application_Info1.Location = new System.Drawing.Point(4, 13);
            this.driving_License_Application_Info1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.driving_License_Application_Info1.Name = "driving_License_Application_Info1";
            this.driving_License_Application_Info1.Size = new System.Drawing.Size(1107, 181);
            this.driving_License_Application_Info1.TabIndex = 1;
            // 
            // ShowApplicationfrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 563);
            this.Controls.Add(this.driving_License_Application_Info1);
            this.Controls.Add(this.applicationBasicInfo1);
            this.Name = "ShowApplicationfrm";
            this.Text = "ShowApplicationfrm";
            this.Load += new System.EventHandler(this.ShowApplicationfrm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ApplicationBasicInfo applicationBasicInfo1;
        private Tests.Driving_License_Application_Info driving_License_Application_Info1;
    }
}