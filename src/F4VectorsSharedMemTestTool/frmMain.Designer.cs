namespace BMSVectorsharedMemTestTool
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            this.pbHUD = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblHUDDataSize = new System.Windows.Forms.Label();
            this.pbRWR = new System.Windows.Forms.PictureBox();
            this.pbHMS = new System.Windows.Forms.PictureBox();
            this.lblRWRDataSize = new System.Windows.Forms.Label();
            this.lblHMSDataSize = new System.Windows.Forms.Label();
            this.lblRWR = new System.Windows.Forms.Label();
            this.lblHUD = new System.Windows.Forms.Label();
            this.lblHMS = new System.Windows.Forms.Label();
            this.lblLMFD = new System.Windows.Forms.Label();
            this.lblLMFDDataSize = new System.Windows.Forms.Label();
            this.pbLMFD = new System.Windows.Forms.PictureBox();
            this.lblRMFD = new System.Windows.Forms.Label();
            this.lblRMFDDataSize = new System.Windows.Forms.Label();
            this.pbRMFD = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbHUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRWR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHMS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLMFD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRMFD)).BeginInit();
            this.SuspendLayout();
            // 
            // pbHUD
            // 
            this.pbHUD.BackColor = System.Drawing.Color.Black;
            this.pbHUD.Location = new System.Drawing.Point(3, 25);
            this.pbHUD.Name = "pbHUD";
            this.pbHUD.Size = new System.Drawing.Size(400, 400);
            this.pbHUD.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbHUD.TabIndex = 0;
            this.pbHUD.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 16;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // lblHUDDataSize
            // 
            this.lblHUDDataSize.AutoSize = true;
            this.lblHUDDataSize.Location = new System.Drawing.Point(12, 9);
            this.lblHUDDataSize.Name = "lblHUDDataSize";
            this.lblHUDDataSize.Size = new System.Drawing.Size(56, 13);
            this.lblHUDDataSize.TabIndex = 2;
            this.lblHUDDataSize.Text = "Data Size:";
            // 
            // pbRWR
            // 
            this.pbRWR.BackColor = System.Drawing.Color.Black;
            this.pbRWR.Location = new System.Drawing.Point(409, 25);
            this.pbRWR.Name = "pbRWR";
            this.pbRWR.Size = new System.Drawing.Size(400, 400);
            this.pbRWR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbRWR.TabIndex = 3;
            this.pbRWR.TabStop = false;
            // 
            // pbHMS
            // 
            this.pbHMS.BackColor = System.Drawing.Color.Black;
            this.pbHMS.Location = new System.Drawing.Point(815, 25);
            this.pbHMS.Name = "pbHMS";
            this.pbHMS.Size = new System.Drawing.Size(400, 400);
            this.pbHMS.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbHMS.TabIndex = 4;
            this.pbHMS.TabStop = false;
            // 
            // lblRWRDataSize
            // 
            this.lblRWRDataSize.AutoSize = true;
            this.lblRWRDataSize.Location = new System.Drawing.Point(417, 9);
            this.lblRWRDataSize.Name = "lblRWRDataSize";
            this.lblRWRDataSize.Size = new System.Drawing.Size(56, 13);
            this.lblRWRDataSize.TabIndex = 7;
            this.lblRWRDataSize.Text = "Data Size:";
            // 
            // lblHMSDataSize
            // 
            this.lblHMSDataSize.AutoSize = true;
            this.lblHMSDataSize.Location = new System.Drawing.Point(823, 9);
            this.lblHMSDataSize.Name = "lblHMSDataSize";
            this.lblHMSDataSize.Size = new System.Drawing.Size(56, 13);
            this.lblHMSDataSize.TabIndex = 8;
            this.lblHMSDataSize.Text = "Data Size:";
            // 
            // lblRWR
            // 
            this.lblRWR.AutoSize = true;
            this.lblRWR.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRWR.Location = new System.Drawing.Point(599, 9);
            this.lblRWR.Name = "lblRWR";
            this.lblRWR.Size = new System.Drawing.Size(37, 13);
            this.lblRWR.TabIndex = 10;
            this.lblRWR.Text = "RWR";
            // 
            // lblHUD
            // 
            this.lblHUD.AutoSize = true;
            this.lblHUD.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHUD.Location = new System.Drawing.Point(194, 9);
            this.lblHUD.Name = "lblHUD";
            this.lblHUD.Size = new System.Drawing.Size(34, 13);
            this.lblHUD.TabIndex = 11;
            this.lblHUD.Text = "HUD";
            // 
            // lblHMS
            // 
            this.lblHMS.AutoSize = true;
            this.lblHMS.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHMS.Location = new System.Drawing.Point(1006, 9);
            this.lblHMS.Name = "lblHMS";
            this.lblHMS.Size = new System.Drawing.Size(34, 13);
            this.lblHMS.TabIndex = 12;
            this.lblHMS.Text = "HMS";
            // 
            // lblLMFD
            // 
            this.lblLMFD.AutoSize = true;
            this.lblLMFD.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLMFD.Location = new System.Drawing.Point(186, 834);
            this.lblLMFD.Name = "lblLMFD";
            this.lblLMFD.Size = new System.Drawing.Size(40, 13);
            this.lblLMFD.TabIndex = 15;
            this.lblLMFD.Text = "LMFD";
            // 
            // lblLMFDDataSize
            // 
            this.lblLMFDDataSize.AutoSize = true;
            this.lblLMFDDataSize.Location = new System.Drawing.Point(4, 834);
            this.lblLMFDDataSize.Name = "lblLMFDDataSize";
            this.lblLMFDDataSize.Size = new System.Drawing.Size(56, 13);
            this.lblLMFDDataSize.TabIndex = 14;
            this.lblLMFDDataSize.Text = "Data Size:";
            // 
            // pbLMFD
            // 
            this.pbLMFD.BackColor = System.Drawing.Color.Black;
            this.pbLMFD.Location = new System.Drawing.Point(3, 431);
            this.pbLMFD.Name = "pbLMFD";
            this.pbLMFD.Size = new System.Drawing.Size(400, 400);
            this.pbLMFD.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbLMFD.TabIndex = 13;
            this.pbLMFD.TabStop = false;
            // 
            // lblRMFD
            // 
            this.lblRMFD.AutoSize = true;
            this.lblRMFD.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRMFD.Location = new System.Drawing.Point(592, 834);
            this.lblRMFD.Name = "lblRMFD";
            this.lblRMFD.Size = new System.Drawing.Size(42, 13);
            this.lblRMFD.TabIndex = 18;
            this.lblRMFD.Text = "RMFD";
            // 
            // lblRMFDDataSize
            // 
            this.lblRMFDDataSize.AutoSize = true;
            this.lblRMFDDataSize.Location = new System.Drawing.Point(410, 834);
            this.lblRMFDDataSize.Name = "lblRMFDDataSize";
            this.lblRMFDDataSize.Size = new System.Drawing.Size(56, 13);
            this.lblRMFDDataSize.TabIndex = 17;
            this.lblRMFDDataSize.Text = "Data Size:";
            // 
            // pbRMFD
            // 
            this.pbRMFD.BackColor = System.Drawing.Color.Black;
            this.pbRMFD.Location = new System.Drawing.Point(409, 431);
            this.pbRMFD.Name = "pbRMFD";
            this.pbRMFD.Size = new System.Drawing.Size(400, 400);
            this.pbRMFD.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbRMFD.TabIndex = 16;
            this.pbRMFD.TabStop = false;
            // 
            // frmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1216, 850);
            this.Controls.Add(this.lblRMFD);
            this.Controls.Add(this.lblRMFDDataSize);
            this.Controls.Add(this.pbRMFD);
            this.Controls.Add(this.lblLMFD);
            this.Controls.Add(this.lblLMFDDataSize);
            this.Controls.Add(this.pbLMFD);
            this.Controls.Add(this.lblHMS);
            this.Controls.Add(this.lblHUD);
            this.Controls.Add(this.lblRWR);
            this.Controls.Add(this.lblHMSDataSize);
            this.Controls.Add(this.lblRWRDataSize);
            this.Controls.Add(this.pbHMS);
            this.Controls.Add(this.pbRWR);
            this.Controls.Add(this.lblHUDDataSize);
            this.Controls.Add(this.pbHUD);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "BMS Vectors Shared Memory Test Tool";
            ((System.ComponentModel.ISupportInitialize)(this.pbHUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRWR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHMS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLMFD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRMFD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbHUD;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblHUDDataSize;
        private System.Windows.Forms.PictureBox pbRWR;
        private System.Windows.Forms.PictureBox pbHMS;
        private System.Windows.Forms.Label lblRWRDataSize;
        private System.Windows.Forms.Label lblHMSDataSize;
        private System.Windows.Forms.Label lblRWR;
        private System.Windows.Forms.Label lblHUD;
        private System.Windows.Forms.Label lblHMS;
        private System.Windows.Forms.Label lblLMFD;
        private System.Windows.Forms.Label lblLMFDDataSize;
        private System.Windows.Forms.PictureBox pbLMFD;
        private System.Windows.Forms.Label lblRMFD;
        private System.Windows.Forms.Label lblRMFDDataSize;
        private System.Windows.Forms.PictureBox pbRMFD;
    }
}

