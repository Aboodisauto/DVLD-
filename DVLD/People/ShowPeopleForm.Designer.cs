namespace DVLD.People
{
    partial class ShowPeopleForm
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
            this.peopleInformation1 = new DVLD.People.PeopleInformation();
            this.SuspendLayout();
            // 
            // peopleInformation1
            // 
            this.peopleInformation1.Location = new System.Drawing.Point(-11, 12);
            this.peopleInformation1.Name = "peopleInformation1";
            this.peopleInformation1.PersonId = 0;
            this.peopleInformation1.Size = new System.Drawing.Size(816, 438);
            this.peopleInformation1.TabIndex = 0;
            // 
            // ShowPeopleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.peopleInformation1);
            this.Name = "ShowPeopleForm";
            this.Text = "ShowPeopleForm";
            this.ResumeLayout(false);

        }

        #endregion

        private PeopleInformation peopleInformation1;
    }
}