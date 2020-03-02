namespace Foriba.OE.UI
{
    partial class frmSmm
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
            this.comboBoxUrlAdres = new System.Windows.Forms.ComboBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label13 = new System.Windows.Forms.Label();
            this.lblAPI = new System.Windows.Forms.LinkLabel();
            this.btnSmmMakbuzIndir = new System.Windows.Forms.Button();
            this.label22 = new System.Windows.Forms.Label();
            this.txtFaturaID = new System.Windows.Forms.TextBox();
            this.checkboxTLS11 = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label23 = new System.Windows.Forms.Label();
            this.checkboxTLS12 = new System.Windows.Forms.CheckBox();
            this.txtFaturaUUID = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.btnSmmGonder = new System.Windows.Forms.Button();
            this.txtSube = new System.Windows.Forms.TextBox();
            this.txtSifre = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.txtKullanici = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.txtTcknVkn = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxUrlAdres
            // 
            this.comboBoxUrlAdres.FormattingEnabled = true;
            this.comboBoxUrlAdres.Location = new System.Drawing.Point(210, 142);
            this.comboBoxUrlAdres.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxUrlAdres.Name = "comboBoxUrlAdres";
            this.comboBoxUrlAdres.Size = new System.Drawing.Size(169, 21);
            this.comboBoxUrlAdres.TabIndex = 128;
            this.comboBoxUrlAdres.SelectedIndexChanged += new System.EventHandler(this.comboBoxUrlAdres_SelectedIndexChanged);
            // 
            // linkLabel1
            // 
            this.linkLabel1.ActiveLinkColor = System.Drawing.Color.Red;
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.LinkColor = System.Drawing.Color.MediumBlue;
            this.linkLabel1.Location = new System.Drawing.Point(164, 175);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(334, 15);
            this.linkLabel1.TabIndex = 127;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "https://earsivwstest.fitbulut.com/ClientESmmServicesPort.svc";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(92, 175);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(63, 15);
            this.label13.TabIndex = 126;
            this.label13.Text = "WS Adres:";
            // 
            // lblAPI
            // 
            this.lblAPI.AutoSize = true;
            this.lblAPI.LinkColor = System.Drawing.Color.MediumBlue;
            this.lblAPI.Location = new System.Drawing.Point(237, 112);
            this.lblAPI.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAPI.Name = "lblAPI";
            this.lblAPI.Size = new System.Drawing.Size(119, 13);
            this.lblAPI.TabIndex = 123;
            this.lblAPI.TabStop = true;
            this.lblAPI.Text = "https://api.fitbulut.com/";
            // 
            // btnSmmMakbuzIndir
            // 
            this.btnSmmMakbuzIndir.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnSmmMakbuzIndir.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btnSmmMakbuzIndir.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btnSmmMakbuzIndir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSmmMakbuzIndir.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnSmmMakbuzIndir.Location = new System.Drawing.Point(39, 470);
            this.btnSmmMakbuzIndir.Name = "btnSmmMakbuzIndir";
            this.btnSmmMakbuzIndir.Size = new System.Drawing.Size(220, 30);
            this.btnSmmMakbuzIndir.TabIndex = 109;
            this.btnSmmMakbuzIndir.Text = "Makbuz İndir";
            this.btnSmmMakbuzIndir.UseVisualStyleBackColor = false;
            this.btnSmmMakbuzIndir.Click += new System.EventHandler(this.btnSmmMakbuzIndir_Click);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label22.Location = new System.Drawing.Point(304, 256);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(83, 17);
            this.label22.TabIndex = 111;
            this.label22.Text = "Makbuz ID";
            // 
            // txtFaturaID
            // 
            this.txtFaturaID.BackColor = System.Drawing.Color.White;
            this.txtFaturaID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtFaturaID.Location = new System.Drawing.Point(307, 276);
            this.txtFaturaID.Name = "txtFaturaID";
            this.txtFaturaID.Size = new System.Drawing.Size(222, 22);
            this.txtFaturaID.TabIndex = 107;
            // 
            // checkboxTLS11
            // 
            this.checkboxTLS11.AutoSize = true;
            this.checkboxTLS11.Location = new System.Drawing.Point(39, 396);
            this.checkboxTLS11.Margin = new System.Windows.Forms.Padding(2);
            this.checkboxTLS11.Name = "checkboxTLS11";
            this.checkboxTLS11.Size = new System.Drawing.Size(70, 17);
            this.checkboxTLS11.TabIndex = 121;
            this.checkboxTLS11.Text = "TLS v1.1";
            this.checkboxTLS11.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.Image = global::Foriba.OE.UI.Properties.Resources.Foriba_Logo;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(571, 128);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 114;
            this.pictureBox1.TabStop = false;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label23.Location = new System.Drawing.Point(304, 211);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(105, 17);
            this.label23.TabIndex = 110;
            this.label23.Text = "Makbuz UUID";
            // 
            // checkboxTLS12
            // 
            this.checkboxTLS12.AutoSize = true;
            this.checkboxTLS12.Checked = true;
            this.checkboxTLS12.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkboxTLS12.Location = new System.Drawing.Point(134, 396);
            this.checkboxTLS12.Margin = new System.Windows.Forms.Padding(2);
            this.checkboxTLS12.Name = "checkboxTLS12";
            this.checkboxTLS12.Size = new System.Drawing.Size(70, 17);
            this.checkboxTLS12.TabIndex = 122;
            this.checkboxTLS12.Text = "TLS v1.2";
            this.checkboxTLS12.UseVisualStyleBackColor = true;
            // 
            // txtFaturaUUID
            // 
            this.txtFaturaUUID.BackColor = System.Drawing.Color.White;
            this.txtFaturaUUID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtFaturaUUID.Location = new System.Drawing.Point(307, 230);
            this.txtFaturaUUID.Name = "txtFaturaUUID";
            this.txtFaturaUUID.Size = new System.Drawing.Size(222, 22);
            this.txtFaturaUUID.TabIndex = 106;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label24.Location = new System.Drawing.Point(36, 350);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(45, 17);
            this.label24.TabIndex = 120;
            this.label24.Text = "Şube";
            // 
            // btnSmmGonder
            // 
            this.btnSmmGonder.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnSmmGonder.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btnSmmGonder.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btnSmmGonder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSmmGonder.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnSmmGonder.Location = new System.Drawing.Point(39, 424);
            this.btnSmmGonder.Name = "btnSmmGonder";
            this.btnSmmGonder.Size = new System.Drawing.Size(220, 30);
            this.btnSmmGonder.TabIndex = 108;
            this.btnSmmGonder.Text = "Makbuz Gönder";
            this.btnSmmGonder.UseVisualStyleBackColor = false;
            this.btnSmmGonder.Click += new System.EventHandler(this.btnSmmGonder_Click);
            // 
            // txtSube
            // 
            this.txtSube.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtSube.Location = new System.Drawing.Point(39, 369);
            this.txtSube.Name = "txtSube";
            this.txtSube.Size = new System.Drawing.Size(222, 22);
            this.txtSube.TabIndex = 119;
            // 
            // txtSifre
            // 
            this.txtSifre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtSifre.Location = new System.Drawing.Point(41, 324);
            this.txtSifre.Name = "txtSifre";
            this.txtSifre.Size = new System.Drawing.Size(222, 22);
            this.txtSifre.TabIndex = 118;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label27.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label27.Location = new System.Drawing.Point(40, 305);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(71, 17);
            this.label27.TabIndex = 117;
            this.label27.Text = "WS Şifre";
            // 
            // txtKullanici
            // 
            this.txtKullanici.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtKullanici.Location = new System.Drawing.Point(41, 276);
            this.txtKullanici.Name = "txtKullanici";
            this.txtKullanici.Size = new System.Drawing.Size(222, 22);
            this.txtKullanici.TabIndex = 113;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label28.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label28.Location = new System.Drawing.Point(40, 256);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(126, 17);
            this.label28.TabIndex = 116;
            this.label28.Text = "WS Kullanıcı Adı";
            // 
            // txtTcknVkn
            // 
            this.txtTcknVkn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtTcknVkn.Location = new System.Drawing.Point(41, 230);
            this.txtTcknVkn.Name = "txtTcknVkn";
            this.txtTcknVkn.Size = new System.Drawing.Size(222, 22);
            this.txtTcknVkn.TabIndex = 112;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label29.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label29.Location = new System.Drawing.Point(41, 211);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(95, 17);
            this.label29.TabIndex = 115;
            this.label29.Text = "VKN / TCKN";
            // 
            // frmSmm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 556);
            this.Controls.Add(this.comboBoxUrlAdres);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.lblAPI);
            this.Controls.Add(this.btnSmmMakbuzIndir);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.txtFaturaID);
            this.Controls.Add(this.checkboxTLS11);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.checkboxTLS12);
            this.Controls.Add(this.txtFaturaUUID);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.btnSmmGonder);
            this.Controls.Add(this.txtSube);
            this.Controls.Add(this.txtSifre);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.txtKullanici);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.txtTcknVkn);
            this.Controls.Add(this.label29);
            this.Name = "frmSmm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Foriba Bulut API  -   e-Smm Test Projesi  v1.7                                   " +
    "         ";
            this.Load += new System.EventHandler(this.frmSmm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxUrlAdres;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.LinkLabel lblAPI;
        private System.Windows.Forms.Button btnSmmMakbuzIndir;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtFaturaID;
        private System.Windows.Forms.CheckBox checkboxTLS11;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.CheckBox checkboxTLS12;
        private System.Windows.Forms.TextBox txtFaturaUUID;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Button btnSmmGonder;
        private System.Windows.Forms.TextBox txtSube;
        private System.Windows.Forms.TextBox txtSifre;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtKullanici;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox txtTcknVkn;
        private System.Windows.Forms.Label label29;
    }
}