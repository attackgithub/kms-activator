namespace kms_activator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.KMSServer = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.OsppPath = new System.Windows.Forms.TextBox();
            this.windows_option = new System.Windows.Forms.RadioButton();
            this.office_option = new System.Windows.Forms.RadioButton();
            this.debug_option = new System.Windows.Forms.CheckBox();
            this.kms_option = new System.Windows.Forms.CheckBox();
            this.Activate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "KMS Server:";
            // 
            // KMSServer
            // 
            this.KMSServer.Location = new System.Drawing.Point(93, 25);
            this.KMSServer.Name = "KMSServer";
            this.KMSServer.Size = new System.Drawing.Size(184, 20);
            this.KMSServer.TabIndex = 0;
            this.KMSServer.Text = "127.0.0.1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "OSPP.VBS:";
            // 
            // OsppPath
            // 
            this.OsppPath.Location = new System.Drawing.Point(93, 51);
            this.OsppPath.Name = "OsppPath";
            this.OsppPath.Size = new System.Drawing.Size(184, 20);
            this.OsppPath.TabIndex = 1;
            // 
            // windows_option
            // 
            this.windows_option.AutoSize = true;
            this.windows_option.Location = new System.Drawing.Point(22, 94);
            this.windows_option.Name = "windows_option";
            this.windows_option.Size = new System.Drawing.Size(69, 17);
            this.windows_option.TabIndex = 2;
            this.windows_option.TabStop = true;
            this.windows_option.Text = "Windows";
            this.windows_option.UseVisualStyleBackColor = true;
            // 
            // office_option
            // 
            this.office_option.AutoSize = true;
            this.office_option.Location = new System.Drawing.Point(174, 94);
            this.office_option.Name = "office_option";
            this.office_option.Size = new System.Drawing.Size(53, 17);
            this.office_option.TabIndex = 3;
            this.office_option.TabStop = true;
            this.office_option.Text = "Office";
            this.office_option.UseVisualStyleBackColor = true;
            this.office_option.CheckedChanged += new System.EventHandler(this.Office_option_CheckedChanged);
            // 
            // debug_option
            // 
            this.debug_option.AutoSize = true;
            this.debug_option.Location = new System.Drawing.Point(22, 123);
            this.debug_option.Name = "debug_option";
            this.debug_option.Size = new System.Drawing.Size(106, 17);
            this.debug_option.TabIndex = 4;
            this.debug_option.Text = "Show debug info";
            this.debug_option.UseVisualStyleBackColor = true;
            this.debug_option.CheckedChanged += new System.EventHandler(this.Debug_option_CheckedChanged);
            // 
            // kms_option
            // 
            this.kms_option.AutoSize = true;
            this.kms_option.Location = new System.Drawing.Point(174, 123);
            this.kms_option.Name = "kms_option";
            this.kms_option.Size = new System.Drawing.Size(102, 17);
            this.kms_option.TabIndex = 5;
            this.kms_option.Text = "Start kms server";
            this.kms_option.UseVisualStyleBackColor = true;
            this.kms_option.CheckedChanged += new System.EventHandler(this.Kms_option_CheckedChanged);
            // 
            // Activate
            // 
            this.Activate.Location = new System.Drawing.Point(22, 171);
            this.Activate.Name = "Activate";
            this.Activate.Size = new System.Drawing.Size(255, 52);
            this.Activate.TabIndex = 6;
            this.Activate.Text = "Activate";
            this.Activate.UseVisualStyleBackColor = true;
            this.Activate.Click += new System.EventHandler(this.Activate_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 237);
            this.Controls.Add(this.Activate);
            this.Controls.Add(this.kms_option);
            this.Controls.Add(this.debug_option);
            this.Controls.Add(this.office_option);
            this.Controls.Add(this.windows_option);
            this.Controls.Add(this.OsppPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.KMSServer);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "kms-activator 1.0";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox KMSServer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox OsppPath;
        private System.Windows.Forms.RadioButton windows_option;
        private System.Windows.Forms.RadioButton office_option;
        private System.Windows.Forms.CheckBox debug_option;
        private System.Windows.Forms.CheckBox kms_option;
        private System.Windows.Forms.Button Activate;
    }
}

