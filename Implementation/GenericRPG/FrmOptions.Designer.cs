namespace GenericRPG
{
    partial class FrmOptions
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
            this.lblSound = new System.Windows.Forms.Label();
            this.grpSound = new System.Windows.Forms.GroupBox();
            this.rdoSoundOff = new System.Windows.Forms.RadioButton();
            this.rdoSoundOn = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoFullscreenOff = new System.Windows.Forms.RadioButton();
            this.rdoFullscreenOn = new System.Windows.Forms.RadioButton();
            this.lblFullscreen = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.grpSound.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSound
            // 
            this.lblSound.AutoSize = true;
            this.lblSound.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSound.ForeColor = System.Drawing.Color.White;
            this.lblSound.Location = new System.Drawing.Point(12, 33);
            this.lblSound.Name = "lblSound";
            this.lblSound.Size = new System.Drawing.Size(100, 31);
            this.lblSound.TabIndex = 0;
            this.lblSound.Text = "Sound:";
            // 
            // grpSound
            // 
            this.grpSound.Controls.Add(this.rdoSoundOff);
            this.grpSound.Controls.Add(this.rdoSoundOn);
            this.grpSound.Location = new System.Drawing.Point(166, 12);
            this.grpSound.Name = "grpSound";
            this.grpSound.Size = new System.Drawing.Size(200, 83);
            this.grpSound.TabIndex = 1;
            this.grpSound.TabStop = false;
            // 
            // rdoSoundOff
            // 
            this.rdoSoundOff.AutoSize = true;
            this.rdoSoundOff.Checked = true;
            this.rdoSoundOff.ForeColor = System.Drawing.Color.White;
            this.rdoSoundOff.Location = new System.Drawing.Point(6, 48);
            this.rdoSoundOff.Name = "rdoSoundOff";
            this.rdoSoundOff.Size = new System.Drawing.Size(48, 21);
            this.rdoSoundOff.TabIndex = 1;
            this.rdoSoundOff.TabStop = true;
            this.rdoSoundOff.Text = "Off";
            this.rdoSoundOff.UseVisualStyleBackColor = true;
            this.rdoSoundOff.Click += new System.EventHandler(this.rdoSoundOff_Click);
            // 
            // rdoSoundOn
            // 
            this.rdoSoundOn.AutoSize = true;
            this.rdoSoundOn.ForeColor = System.Drawing.Color.White;
            this.rdoSoundOn.Location = new System.Drawing.Point(6, 21);
            this.rdoSoundOn.Name = "rdoSoundOn";
            this.rdoSoundOn.Size = new System.Drawing.Size(48, 21);
            this.rdoSoundOn.TabIndex = 0;
            this.rdoSoundOn.Text = "On";
            this.rdoSoundOn.UseVisualStyleBackColor = true;
            this.rdoSoundOn.Click += new System.EventHandler(this.rdoSoundOn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoFullscreenOff);
            this.groupBox1.Controls.Add(this.rdoFullscreenOn);
            this.groupBox1.Location = new System.Drawing.Point(166, 101);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 83);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // rdoFullscreenOff
            // 
            this.rdoFullscreenOff.AutoSize = true;
            this.rdoFullscreenOff.Checked = true;
            this.rdoFullscreenOff.ForeColor = System.Drawing.Color.White;
            this.rdoFullscreenOff.Location = new System.Drawing.Point(6, 48);
            this.rdoFullscreenOff.Name = "rdoFullscreenOff";
            this.rdoFullscreenOff.Size = new System.Drawing.Size(48, 21);
            this.rdoFullscreenOff.TabIndex = 1;
            this.rdoFullscreenOff.TabStop = true;
            this.rdoFullscreenOff.Text = "Off";
            this.rdoFullscreenOff.UseVisualStyleBackColor = true;
            this.rdoFullscreenOff.Click += new System.EventHandler(this.rdoFullscreenOff_Click);
            // 
            // rdoFullscreenOn
            // 
            this.rdoFullscreenOn.AutoSize = true;
            this.rdoFullscreenOn.ForeColor = System.Drawing.Color.White;
            this.rdoFullscreenOn.Location = new System.Drawing.Point(6, 21);
            this.rdoFullscreenOn.Name = "rdoFullscreenOn";
            this.rdoFullscreenOn.Size = new System.Drawing.Size(48, 21);
            this.rdoFullscreenOn.TabIndex = 0;
            this.rdoFullscreenOn.Text = "On";
            this.rdoFullscreenOn.UseVisualStyleBackColor = true;
            this.rdoFullscreenOn.Click += new System.EventHandler(this.rdoFullscreenOn_Click);
            // 
            // lblFullscreen
            // 
            this.lblFullscreen.AutoSize = true;
            this.lblFullscreen.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFullscreen.ForeColor = System.Drawing.Color.White;
            this.lblFullscreen.Location = new System.Drawing.Point(12, 122);
            this.lblFullscreen.Name = "lblFullscreen";
            this.lblFullscreen.Size = new System.Drawing.Size(148, 31);
            this.lblFullscreen.TabIndex = 3;
            this.lblFullscreen.Text = "Fullscreen:";
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(18, 204);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(142, 28);
            this.btnBack.TabIndex = 4;
            this.btnBack.Text = "Close";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // FrmOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(397, 257);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.lblFullscreen);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpSound);
            this.Controls.Add(this.lblSound);
            this.Name = "FrmOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmOptions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmOptions_FormClosing);
            this.grpSound.ResumeLayout(false);
            this.grpSound.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSound;
        private System.Windows.Forms.GroupBox grpSound;
        private System.Windows.Forms.RadioButton rdoSoundOff;
        private System.Windows.Forms.RadioButton rdoSoundOn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoFullscreenOff;
        private System.Windows.Forms.RadioButton rdoFullscreenOn;
        private System.Windows.Forms.Label lblFullscreen;
        private System.Windows.Forms.Button btnBack;
    }
}