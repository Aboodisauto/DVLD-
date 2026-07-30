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
            this.comboBox1.Location = new System.Drawing.Point(81, 78);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(171, 24);
            this.comboBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 81);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Filter :";
            // 
            // FilterTB
            // 
            this.FilterTB.Location = new System.Drawing.Point(261, 76);
            this.FilterTB.Margin = new System.Windows.Forms.Padding(4);
            this.FilterTB.Name = "FilterTB";
            this.FilterTB.Size = new System.Drawing.Size(383, 22);
            this.FilterTB.TabIndex = 3;
            this.FilterTB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FilterTB_KeyPress);
            // 
            // Addbtn
            // 
            this.Addbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Addbtn.Image = global::DVLD.Properties.Resources.Add_Person_40;
            this.Addbtn.Location = new System.Drawing.Point(717, 58);
            this.Addbtn.Margin = new System.Windows.Forms.Padding(4);
            this.Addbtn.Name = "Addbtn";
            this.Addbtn.Size = new System.Drawing.Size(56, 57);
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
            this.Findbtn.Location = new System.Drawing.Point(653, 58);
            this.Findbtn.Margin = new System.Windows.Forms.Padding(4);
            this.Findbtn.Name = "Findbtn";
            this.Findbtn.Size = new System.Drawing.Size(56, 57);
            this.Findbtn.TabIndex = 4;
            this.Findbtn.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.Findbtn.UseVisualStyleBackColor = true;
            this.Findbtn.Click += new System.EventHandler(this.Findbtn_Click);
            // 
            // peopleInformation1
            // 
            this.peopleInformation1.Location = new System.Drawing.Point(4, 111);
            this.peopleInformation1.Margin = new System.Windows.Forms.Padding(5);
            this.peopleInformation1.Name = "peopleInformation1";
            this.peopleInformation1.PersonId = 0;
            this.peopleInformation1.Size = new System.Drawing.Size(1096, 539);
            this.peopleInformation1.TabIndex = 0;
            // 
            // FilterPeople
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Addbtn);
            this.Controls.Add(this.Findbtn);
            this.Controls.Add(this.FilterTB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.peopleInformation1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FilterPeople";
            this.Size = new System.Drawing.Size(1129, 654);
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
    }
}
