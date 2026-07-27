namespace WindowsFormsApp2
{
    partial class Form1
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
            this.poolControl4 = new WindowsFormsApp2.PoolControl();
            this.poolControl1 = new WindowsFormsApp2.PoolControl();
            this.poolControl2 = new WindowsFormsApp2.PoolControl();
            this.poolControl3 = new WindowsFormsApp2.PoolControl();
            this.poolControl5 = new WindowsFormsApp2.PoolControl();
            this.poolControl6 = new WindowsFormsApp2.PoolControl();
            this.SuspendLayout();
            // 
            // poolControl4
            // 
            this.poolControl4.Location = new System.Drawing.Point(0, 0);
            this.poolControl4.Name = "poolControl4";
            this.poolControl4.RatePerHour = 15.7F;
            this.poolControl4.Size = new System.Drawing.Size(366, 203);
            this.poolControl4.TabIndex = 0;
            this.poolControl4.TableName = "Table 3";
            this.poolControl4.TimerStopped += new System.EventHandler<WindowsFormsApp2.PoolControl.TimerStoppedEventArgs>(this.poolControl4_TimerStopped);
            // 
            // poolControl1
            // 
            this.poolControl1.Location = new System.Drawing.Point(0, 0);
            this.poolControl1.Name = "poolControl1";
            this.poolControl1.RatePerHour = 3.2F;
            this.poolControl1.Size = new System.Drawing.Size(366, 203);
            this.poolControl1.TabIndex = 0;
            this.poolControl1.TableName = "Table";
            this.poolControl1.TimerStopped += new System.EventHandler<WindowsFormsApp2.PoolControl.TimerStoppedEventArgs>(this.poolControl1_TimerStopped);
            // 
            // poolControl2
            // 
            this.poolControl2.Location = new System.Drawing.Point(372, 0);
            this.poolControl2.Name = "poolControl2";
            this.poolControl2.RatePerHour = 3.2F;
            this.poolControl2.Size = new System.Drawing.Size(366, 203);
            this.poolControl2.TabIndex = 1;
            this.poolControl2.TableName = "Table";
            // 
            // poolControl3
            // 
            this.poolControl3.Location = new System.Drawing.Point(744, 0);
            this.poolControl3.Name = "poolControl3";
            this.poolControl3.RatePerHour = 3.2F;
            this.poolControl3.Size = new System.Drawing.Size(366, 203);
            this.poolControl3.TabIndex = 2;
            this.poolControl3.TableName = "Table";
            // 
            // poolControl5
            // 
            this.poolControl5.Location = new System.Drawing.Point(372, 0);
            this.poolControl5.Name = "poolControl5";
            this.poolControl5.RatePerHour = 25F;
            this.poolControl5.Size = new System.Drawing.Size(366, 203);
            this.poolControl5.TabIndex = 1;
            this.poolControl5.TableName = "Table 4";
            // 
            // poolControl6
            // 
            this.poolControl6.Location = new System.Drawing.Point(744, 0);
            this.poolControl6.Name = "poolControl6";
            this.poolControl6.RatePerHour = 20F;
            this.poolControl6.Size = new System.Drawing.Size(366, 203);
            this.poolControl6.TabIndex = 2;
            this.poolControl6.TableName = "Table 5";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1121, 546);
            this.Controls.Add(this.poolControl6);
            this.Controls.Add(this.poolControl5);
            this.Controls.Add(this.poolControl4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private PoolControl poolControl1;
        private PoolControl poolControl2;
        private PoolControl poolControl3;
        private PoolControl poolControl4;
        private PoolControl poolControl5;
        private PoolControl poolControl6;
    }
}

