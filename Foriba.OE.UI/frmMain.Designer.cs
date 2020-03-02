namespace Foriba.OE.UI
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.btnFatura = new System.Windows.Forms.Button();
            this.btnArsiv = new System.Windows.Forms.Button();
            this.btnIrsaliye = new System.Windows.Forms.Button();
            this.lblAPI = new System.Windows.Forms.LinkLabel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // btnFatura
            // 
            this.btnFatura.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnFatura.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btnFatura.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btnFatura.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFatura.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnFatura.Location = new System.Drawing.Point(67, 143);
            this.btnFatura.Margin = new System.Windows.Forms.Padding(2);
            this.btnFatura.Name = "btnFatura";
            this.btnFatura.Size = new System.Drawing.Size(223, 34);
            this.btnFatura.TabIndex = 1;
            this.btnFatura.Text = "e-Fatura ";
            this.btnFatura.UseVisualStyleBackColor = false;
            this.btnFatura.Click += new System.EventHandler(this.btnFatura_Click);
            // 
            // btnArsiv
            // 
            this.btnArsiv.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnArsiv.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btnArsiv.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btnArsiv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnArsiv.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnArsiv.Location = new System.Drawing.Point(67, 188);
            this.btnArsiv.Margin = new System.Windows.Forms.Padding(2);
            this.btnArsiv.Name = "btnArsiv";
            this.btnArsiv.Size = new System.Drawing.Size(223, 34);
            this.btnArsiv.TabIndex = 3;
            this.btnArsiv.Text = "e-Arşiv Fatura";
            this.btnArsiv.UseVisualStyleBackColor = false;
            this.btnArsiv.Click += new System.EventHandler(this.btnArsiv_Click);
            // 
            // btnIrsaliye
            // 
            this.btnIrsaliye.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnIrsaliye.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btnIrsaliye.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btnIrsaliye.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIrsaliye.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnIrsaliye.Location = new System.Drawing.Point(67, 234);
            this.btnIrsaliye.Margin = new System.Windows.Forms.Padding(2);
            this.btnIrsaliye.Name = "btnIrsaliye";
            this.btnIrsaliye.Size = new System.Drawing.Size(223, 34);
            this.btnIrsaliye.TabIndex = 4;
            this.btnIrsaliye.Text = "e-İrsaliye";
            this.btnIrsaliye.UseVisualStyleBackColor = false;
            this.btnIrsaliye.Click += new System.EventHandler(this.btnIrsaliye_Click);
            // 
            // lblAPI
            // 
            this.lblAPI.AutoSize = true;
            this.lblAPI.LinkColor = System.Drawing.Color.MediumBlue;
            this.lblAPI.Location = new System.Drawing.Point(127, 100);
            this.lblAPI.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAPI.Name = "lblAPI";
            this.lblAPI.Size = new System.Drawing.Size(119, 13);
            this.lblAPI.TabIndex = 56;
            this.lblAPI.TabStop = true;
            this.lblAPI.Text = "https://api.fitbulut.com/";
            this.lblAPI.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblAPI_LinkClicked);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.ErrorImage")));
            this.pictureBox3.Image = global::Foriba.OE.UI.Properties.Resources.Foriba_Logo;
            this.pictureBox3.Location = new System.Drawing.Point(0, 0);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(352, 128);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox3.TabIndex = 53;
            this.pictureBox3.TabStop = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button1.Location = new System.Drawing.Point(67, 283);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(223, 34);
            this.button1.TabIndex = 4;
            this.button1.Text = "e-Smm";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnSmm_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button2.Location = new System.Drawing.Point(67, 330);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(223, 34);
            this.button2.TabIndex = 4;
            this.button2.Text = "e-Mm";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.btnMm_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(352, 388);
            this.Controls.Add(this.lblAPI);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnIrsaliye);
            this.Controls.Add(this.btnArsiv);
            this.Controls.Add(this.btnFatura);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Foriba Bulut API Test Projesi  v1.7";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFatura;
        private System.Windows.Forms.Button btnArsiv;
        private System.Windows.Forms.Button btnIrsaliye;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.LinkLabel lblAPI;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

