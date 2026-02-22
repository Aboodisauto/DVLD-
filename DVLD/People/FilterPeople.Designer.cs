namespace DVLD.People
{
    partial class FilterPeople
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.FilterTB = new System.Windows.Forms.TextBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.Addbtn = new System.Windows.Forms.Button();
            this.Findbtn = new System.Windows.Forms.Button();
            this.peopleInformation1 = new DVLD.People.PeopleInformation();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "PersonID",
            "NationalNo"});
            this.comboBox1.Location = new System.Drawing.Point(61, 63);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(129, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Filter :";
            // 
            // FilterTB
            // 
            this.FilterTB.Location = new System.Drawing.Point(196, 62);
            this.FilterTB.Name = "FilterTB";
            this.FilterTB.Size = new System.Drawing.Size(288, 20);
            this.FilterTB.TabIndex = 3;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Enabled = false;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(599, 177);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(139, 20);
            this.linkLabel1.TabIndex = 6;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Edit Person Info";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // Addbtn
            // 
            this.Addbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Addbtn.Image = global::DVLD.Properties.Resources.Add_Person_40;
            this.Addbtn.Location = new System.Drawing.Point(538, 47);
            this.Addbtn.Name = "Addbtn";
            this.Addbtn.Size = new System.Drawing.Size(42, 46);
            this.Addbtn.TabIndex = 5;
            this.Addbtn.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.Addbtn.UseVisualStyleBackColor = true;
            this.Addbtn.Click += new System.EventHandler(this.Addbtn_Click);
            // 
            // Findbtn
            // 
            this.Findbtn.AutoSize = true;
            this.Findbtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Findbtn.Image = global::DVLD.Properties.Resources.SearchPerson;
            this.Findbtn.Location = new System.Drawing.Point(490, 47);
            this.Findbtn.Name = "Findbtn";
            this.Findbtn.Size = new System.Drawing.Size(42, 46);
            this.Findbtn.TabIndex = 4;
            this.Findbtn.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.Findbtn.UseVisualStyleBackColor = true;
            this.Findbtn.Click += new System.EventHandler(this.Findbtn_Click);
            // 
            // peopleInformation1
            // 
            this.peopleInformation1.Location = new System.Drawing.Point(3, 90);
            this.peopleInformation1.Name = "peopleInformation1";
            this.peopleInformation1.PersonId = 0;
            this.peopleInformation1.Size = new System.Drawing.Size(822, 438);
            this.peopleInformation1.TabIndex = 0;
            // 
            // FilterPeople
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.Addbtn);
            this.Controls.Add(this.Findbtn);
            this.Controls.Add(this.FilterTB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.peopleInformation1);
            this.Name = "FilterPeople";
            this.Size = new System.Drawing.Size(847, 531);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PeopleInformation peopleInformation1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox FilterTB;
        private System.Windows.Forms.Button Findbtn;
        private System.Windows.Forms.Button Addbtn;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}
