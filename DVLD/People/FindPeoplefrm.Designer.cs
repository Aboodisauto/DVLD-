namespace DVLD.People
{
    partial class FindPeoplefrm
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
            this.filterPeople1 = new DVLD.People.FilterPeople();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // filterPeople1
            // 
            this.filterPeople1.Location = new System.Drawing.Point(13, 13);
            this.filterPeople1.Margin = new System.Windows.Forms.Padding(4);
            this.filterPeople1.Name = "filterPeople1";
            this.filterPeople1.Size = new System.Drawing.Size(1129, 706);
            this.filterPeople1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = global::DVLD.Properties.Resources.Close_32;
            this.button1.Location = new System.Drawing.Point(951, 620);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 51);
            this.button1.TabIndex = 1;
            this.button1.Text = "Close";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FindPeoplefrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1132, 671);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.filterPeople1);
            this.Name = "FindPeoplefrm";
            this.Text = "FindPeoplefrm";
            this.ResumeLayout(false);

        }

        #endregion

        private FilterPeople filterPeople1;
        private System.Windows.Forms.Button button1;
    }
}