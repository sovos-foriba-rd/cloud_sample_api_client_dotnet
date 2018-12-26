namespace Foriba.OE.UI
{
    partial class FrmArchive
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmArchive));
            this.panel = new System.Windows.Forms.Panel();
            this.comboBoxUrlAdres = new System.Windows.Forms.ComboBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label13 = new System.Windows.Forms.Label();
            this.btnArsivHTMLIndir = new System.Windows.Forms.Button();
            this.btnArsivPDFIndir = new System.Windows.Forms.Button();
            this.lblAPI = new System.Windows.Forms.LinkLabel();
            this.btnArsivFaturaIndir = new System.Windows.Forms.Button();
            this.label22 = new System.Windows.Forms.Label();
            this.txtFaturaID = new System.Windows.Forms.TextBox();
            this.checkboxTLS11 = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label23 = new System.Windows.Forms.Label();
            this.checkboxTLS12 = new System.Windows.Forms.CheckBox();
            this.txtFaturaUUID = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.btnArsivGonder = new System.Windows.Forms.Button();
            this.txtSube = new System.Windows.Forms.TextBox();
            this.txtSifre = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.txtKullanici = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.txtTcknVkn = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.SystemColors.Control;
            this.panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel.Controls.Add(this.comboBoxUrlAdres);
            this.panel.Controls.Add(this.linkLabel1);
            this.panel.Controls.Add(this.label13);
            this.panel.Controls.Add(this.btnArsivHTMLIndir);
            this.panel.Controls.Add(this.btnArsivPDFIndir);
            this.panel.Controls.Add(this.lblAPI);
            this.panel.Controls.Add(this.btnArsivFaturaIndir);
            this.panel.Controls.Add(this.label22);
            this.panel.Controls.Add(this.txtFaturaID);
            this.panel.Controls.Add(this.checkboxTLS11);
            this.panel.Controls.Add(this.pictureBox1);
            this.panel.Controls.Add(this.label23);
            this.panel.Controls.Add(this.checkboxTLS12);
            this.panel.Controls.Add(this.txtFaturaUUID);
            this.panel.Controls.Add(this.label24);
            this.panel.Controls.Add(this.btnArsivGonder);
            this.panel.Controls.Add(this.txtSube);
            this.panel.Controls.Add(this.txtSifre);
            this.panel.Controls.Add(this.label27);
            this.panel.Controls.Add(this.txtKullanici);
            this.panel.Controls.Add(this.label28);
            this.panel.Controls.Add(this.txtTcknVkn);
            this.panel.Controls.Add(this.label29);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(767, 610);
            this.panel.TabIndex = 2;
            // 
            // comboBoxUrlAdres
            // 
            this.comboBoxUrlAdres.FormattingEnabled = true;
            this.comboBoxUrlAdres.Location = new System.Drawing.Point(280, 179);
            this.comboBoxUrlAdres.Name = "comboBoxUrlAdres";
            this.comboBoxUrlAdres.Size = new System.Drawing.Size(224, 24);
            this.comboBoxUrlAdres.TabIndex = 105;
            this.comboBoxUrlAdres.SelectedIndexChanged += new System.EventHandler(this.comboBoxUrlAdres_SelectedIndexChanged);
            // 
            // linkLabel1
            // 
            this.linkLabel1.ActiveLinkColor = System.Drawing.Color.Red;
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.LinkColor = System.Drawing.Color.MediumBlue;
            this.linkLabel1.Location = new System.Drawing.Point(219, 219);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(409, 18);
            this.linkLabel1.TabIndex = 104;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "https://earsivwstest.fitbulut.com/ClientEArsivServicesPort.svc\r\n";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkTestURL_LinkClicked);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(122, 219);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(79, 18);
            this.label13.TabIndex = 103;
            this.label13.Text = "WS Adres:";
            // 
            // btnArsivHTMLIndir
            // 
            this.btnArsivHTMLIndir.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnArsivHTMLIndir.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btnArsivHTMLIndir.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btnArsivHTMLIndir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnArsivHTMLIndir.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnArsivHTMLIndir.Location = new System.Drawing.Point(409, 525);
            this.btnArsivHTMLIndir.Name = "btnArsivHTMLIndir";
            this.btnArsivHTMLIndir.Size = new System.Drawing.Size(294, 37);
            this.btnArsivHTMLIndir.TabIndex = 102;
            this.btnArsivHTMLIndir.Text = "Fatura HTML İndir";
            this.btnArsivHTMLIndir.UseVisualStyleBackColor = false;
            this.btnArsivHTMLIndir.Click += new System.EventHandler(this.btnArsivHTMLIndir_Click);
            // 
            // btnArsivPDFIndir
            // 
            this.btnArsivPDFIndir.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnArsivPDFIndir.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btnArsivPDFIndir.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btnArsivPDFIndir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnArsivPDFIndir.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnArsivPDFIndir.Location = new System.Drawing.Point(409, 465);
            this.btnArsivPDFIndir.Name = "btnArsivPDFIndir";
            this.btnArsivPDFIndir.Size = new System.Drawing.Size(294, 37);
            this.btnArsivPDFIndir.TabIndex = 101;
            this.btnArsivPDFIndir.Text = "Fatura PDF İndir";
            this.btnArsivPDFIndir.UseVisualStyleBackColor = false;
            this.btnArsivPDFIndir.Click += new System.EventHandler(this.btnArsivPDFIndir_Click);
            // 
            // lblAPI
            // 
            this.lblAPI.AutoSize = true;
            this.lblAPI.LinkColor = System.Drawing.Color.MediumBlue;
            this.lblAPI.Location = new System.Drawing.Point(316, 141);
            this.lblAPI.Name = "lblAPI";
            this.lblAPI.Size = new System.Drawing.Size(150, 17);
            this.lblAPI.TabIndex = 100;
            this.lblAPI.TabStop = true;
            this.lblAPI.Text = "https://api.fitbulut.com/";
            this.lblAPI.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkAPI_LinkClicked);
            // 
            // btnArsivFaturaIndir
            // 
            this.btnArsivFaturaIndir.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnArsivFaturaIndir.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btnArsivFaturaIndir.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btnArsivFaturaIndir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnArsivFaturaIndir.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnArsivFaturaIndir.Location = new System.Drawing.Point(409, 403);
            this.btnArsivFaturaIndir.Margin = new System.Windows.Forms.Padding(4);
            this.btnArsivFaturaIndir.Name = "btnArsivFaturaIndir";
            this.btnArsivFaturaIndir.Size = new System.Drawing.Size(294, 37);
            this.btnArsivFaturaIndir.TabIndex = 17;
            this.btnArsivFaturaIndir.Text = "Fatura UBL İndir";
            this.btnArsivFaturaIndir.UseVisualStyleBackColor = false;
            this.btnArsivFaturaIndir.Click += new System.EventHandler(this.btnArsivFaturaIndir_Click);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label22.Location = new System.Drawing.Point(405, 319);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(88, 20);
            this.label22.TabIndex = 31;
            this.label22.Text = "Fatura ID";
            // 
            // txtFaturaID
            // 
            this.txtFaturaID.BackColor = System.Drawing.Color.White;
            this.txtFaturaID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtFaturaID.Location = new System.Drawing.Point(409, 343);
            this.txtFaturaID.Margin = new System.Windows.Forms.Padding(4);
            this.txtFaturaID.Name = "txtFaturaID";
            this.txtFaturaID.Size = new System.Drawing.Size(294, 26);
            this.txtFaturaID.TabIndex = 1;
            // 
            // checkboxTLS11
            // 
            this.checkboxTLS11.AutoSize = true;
            this.checkboxTLS11.Location = new System.Drawing.Point(52, 491);
            this.checkboxTLS11.Name = "checkboxTLS11";
            this.checkboxTLS11.Size = new System.Drawing.Size(87, 21);
            this.checkboxTLS11.TabIndex = 98;
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
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(765, 158);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 62;
            this.pictureBox1.TabStop = false;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label23.Location = new System.Drawing.Point(405, 263);
            this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(114, 20);
            this.label23.TabIndex = 29;
            this.label23.Text = "Fatura UUID";
            // 
            // checkboxTLS12
            // 
            this.checkboxTLS12.AutoSize = true;
            this.checkboxTLS12.Checked = true;
            this.checkboxTLS12.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkboxTLS12.Location = new System.Drawing.Point(178, 491);
            this.checkboxTLS12.Name = "checkboxTLS12";
            this.checkboxTLS12.Size = new System.Drawing.Size(87, 21);
            this.checkboxTLS12.TabIndex = 99;
            this.checkboxTLS12.Text = "TLS v1.2";
            this.checkboxTLS12.UseVisualStyleBackColor = true;
            // 
            // txtFaturaUUID
            // 
            this.txtFaturaUUID.BackColor = System.Drawing.Color.White;
            this.txtFaturaUUID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtFaturaUUID.Location = new System.Drawing.Point(409, 287);
            this.txtFaturaUUID.Margin = new System.Windows.Forms.Padding(4);
            this.txtFaturaUUID.Name = "txtFaturaUUID";
            this.txtFaturaUUID.Size = new System.Drawing.Size(294, 26);
            this.txtFaturaUUID.TabIndex = 0;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label24.Location = new System.Drawing.Point(48, 434);
            this.label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(51, 20);
            this.label24.TabIndex = 72;
            this.label24.Text = "Şube";
            // 
            // btnArsivGonder
            // 
            this.btnArsivGonder.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnArsivGonder.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btnArsivGonder.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btnArsivGonder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnArsivGonder.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnArsivGonder.Location = new System.Drawing.Point(52, 526);
            this.btnArsivGonder.Margin = new System.Windows.Forms.Padding(4);
            this.btnArsivGonder.Name = "btnArsivGonder";
            this.btnArsivGonder.Size = new System.Drawing.Size(294, 37);
            this.btnArsivGonder.TabIndex = 15;
            this.btnArsivGonder.Text = "Fatura Gönder";
            this.btnArsivGonder.UseVisualStyleBackColor = false;
            this.btnArsivGonder.Click += new System.EventHandler(this.btnArsivGonder_Click);
            // 
            // txtSube
            // 
            this.txtSube.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtSube.Location = new System.Drawing.Point(52, 458);
            this.txtSube.Margin = new System.Windows.Forms.Padding(4);
            this.txtSube.Name = "txtSube";
            this.txtSube.Size = new System.Drawing.Size(294, 26);
            this.txtSube.TabIndex = 71;
            // 
            // txtSifre
            // 
            this.txtSifre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtSifre.Location = new System.Drawing.Point(55, 403);
            this.txtSifre.Margin = new System.Windows.Forms.Padding(4);
            this.txtSifre.Name = "txtSifre";
            this.txtSifre.Size = new System.Drawing.Size(294, 26);
            this.txtSifre.TabIndex = 69;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label27.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label27.Location = new System.Drawing.Point(53, 379);
            this.label27.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(84, 20);
            this.label27.TabIndex = 66;
            this.label27.Text = "WS Şifre";
            // 
            // txtKullanici
            // 
            this.txtKullanici.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtKullanici.Location = new System.Drawing.Point(55, 343);
            this.txtKullanici.Margin = new System.Windows.Forms.Padding(4);
            this.txtKullanici.Name = "txtKullanici";
            this.txtKullanici.Size = new System.Drawing.Size(294, 26);
            this.txtKullanici.TabIndex = 62;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label28.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label28.Location = new System.Drawing.Point(53, 319);
            this.label28.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(149, 20);
            this.label28.TabIndex = 65;
            this.label28.Text = "WS Kullanıcı Adı";
            // 
            // txtTcknVkn
            // 
            this.txtTcknVkn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtTcknVkn.Location = new System.Drawing.Point(55, 287);
            this.txtTcknVkn.Margin = new System.Windows.Forms.Padding(4);
            this.txtTcknVkn.Name = "txtTcknVkn";
            this.txtTcknVkn.Size = new System.Drawing.Size(294, 26);
            this.txtTcknVkn.TabIndex = 61;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label29.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label29.Location = new System.Drawing.Point(55, 263);
            this.label29.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(113, 20);
            this.label29.TabIndex = 64;
            this.label29.Text = "VKN / TCKN";
            // 
            // FrmArchive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 610);
            this.Controls.Add(this.panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmArchive";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Foriba Bulut API  -   e-Arşiv Test Projesi  v1.2                                 " +
    "           ";
            this.Load += new System.EventHandler(this.FrmArchive_Load);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Button btnArsivHTMLIndir;
        private System.Windows.Forms.Button btnArsivPDFIndir;
        private System.Windows.Forms.LinkLabel lblAPI;
        private System.Windows.Forms.Button btnArsivFaturaIndir;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtFaturaID;
        private System.Windows.Forms.CheckBox checkboxTLS11;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.CheckBox checkboxTLS12;
        private System.Windows.Forms.TextBox txtFaturaUUID;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Button btnArsivGonder;
        private System.Windows.Forms.TextBox txtSube;
        private System.Windows.Forms.TextBox txtSifre;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtKullanici;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox txtTcknVkn;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox comboBoxUrlAdres;
    }
}