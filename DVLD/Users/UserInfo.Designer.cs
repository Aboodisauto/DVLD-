namespace DVLD.Users
{
    partial class UserInfo
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.peopleInformation1 = new DVLD.People.PeopleInformation();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.UserIDlb = new System.Windows.Forms.Label();
            this.Usernamelb = new System.Windows.Forms.Label();
            this.ActiveLB = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // peopleInformation1
            // 
            this.peopleInformation1.Location = new System.Drawing.Point(3, 3);
            this.peopleInformation1.Name = "peopleInformation1";
            this.peopleInformation1.PersonId = 0;
            this.peopleInformation1.Size = new System.Drawing.Size(822, 438);
            this.peopleInformation1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 458);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "UserID :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(280, 458);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Username :";
            // 
            // UserIDlb
            // 
            this.UserIDlb.AutoSize = true;
            this.UserIDlb.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserIDlb.Location = new System.Drawing.Point(94, 458);
            this.UserIDlb.Name = "UserIDlb";
            this.UserIDlb.Size = new System.Drawing.Size(35, 18);
            this.UserIDlb.TabIndex = 4;
            this.UserIDlb.Text = "???";
            // 
            // Usernamelb
            // 
            this.Usernamelb.AutoSize = true;
            this.Usernamelb.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Usernamelb.Location = new System.Drawing.Point(381, 458);
            this.Usernamelb.Name = "Usernamelb";
            this.Usernamelb.Size = new System.Drawing.Size(35, 18);
            this.Usernamelb.TabIndex = 5;
            this.Usernamelb.Text = "???";
            // 
            // ActiveLB
            // 
            this.ActiveLB.AutoSize = true;
            this.ActiveLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ActiveLB.Location = new System.Drawing.Point(651, 458);
            this.ActiveLB.Name = "ActiveLB";
            this.ActiveLB.Size = new System.Drawing.Size(35, 18);
            this.ActiveLB.TabIndex = 7;
            this.ActiveLB.Text = "???";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(569, 458);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 18);
            this.label4.TabIndex = 6;
            this.label4.Text = "IsActive :";
            // 
            // UserInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ActiveLB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Usernamelb);
            this.Controls.Add(this.UserIDlb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.peopleInformation1);
            this.Name = "UserInfo";
            this.Size = new System.Drawing.Size(832, 517);
            this.Load += new System.EventHandler(this.UserInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private People.PeopleInformation peopleInformation1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label UserIDlb;
        private System.Windows.Forms.Label Usernamelb;
        private System.Windows.Forms.Label ActiveLB;
        private System.Windows.Forms.Label label4;
    }
}
