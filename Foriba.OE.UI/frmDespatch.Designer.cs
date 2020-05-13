namespace Foriba.OE.UI
{
    partial class FrmDespatch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDespatch));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.grdListIrsaliye = new System.Windows.Forms.DataGridView();
            this.lblBaslik = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btnGonZarf = new System.Windows.Forms.Button();
            this.btnGelZarf = new System.Windows.Forms.Button();
            this.btnIrsaliyePdfIndir = new System.Windows.Forms.Button();
            this.btnGonIrsYanit = new System.Windows.Forms.Button();
            this.btnIrsaliyeGon = new System.Windows.Forms.Button();
            this.btnIrsaliyeUblIndir = new System.Windows.Forms.Button();
            this.btnZarfDurumSorgula = new System.Windows.Forms.Button();
            this.btnGonIrsaliye = new System.Windows.Forms.Button();
            this.btnGelIrsYanit = new System.Windows.Forms.Button();
            this.btnIrsYanitGon = new System.Windows.Forms.Button();
            this.btnMukSorgu = new System.Windows.Forms.Button();
            this.btnGelIrsaliye = new System.Windows.Forms.Button();
            this.panelControls = new System.Windows.Forms.Panel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label13 = new System.Windows.Forms.Label();
            this.checkboxTLS11 = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblAPI = new System.Windows.Forms.LinkLabel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.checkboxTLS12 = new System.Windows.Forms.CheckBox();
            this.dtpIrsaliyeTarih2 = new System.Windows.Forms.DateTimePicker();
            this.txtSifre = new System.Windows.Forms.TextBox();
            this.txtPostaKutusu = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpIrsaliyeTarih1 = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.txtKullanici = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtGonBirim = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTcVkn = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblIrsaliyeId = new System.Windows.Forms.Label();
            this.txtIrsaliyeId = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdListIrsaliye)).BeginInit();
            this.panel4.SuspendLayout();
            this.panelControls.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panelControls);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(963, 682);
            this.panel1.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.grdListIrsaliye);
            this.panel5.Controls.Add(this.lblBaslik);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 204);
            this.panel5.Margin = new System.Windows.Forms.Padding(2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(752, 476);
            this.panel5.TabIndex = 2;
            // 
            // grdListIrsaliye
            // 
            this.grdListIrsaliye.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdListIrsaliye.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdListIrsaliye.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdListIrsaliye.Location = new System.Drawing.Point(0, 39);
            this.grdListIrsaliye.Margin = new System.Windows.Forms.Padding(2);
            this.grdListIrsaliye.MultiSelect = false;
            this.grdListIrsaliye.Name = "grdListIrsaliye";
            this.grdListIrsaliye.RowTemplate.Height = 24;
            this.grdListIrsaliye.Size = new System.Drawing.Size(752, 437);
            this.grdListIrsaliye.TabIndex = 1;
            this.grdListIrsaliye.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdListIrsaliye_CellClick);
            this.grdListIrsaliye.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.grdListIrsaliye_DataBindingComplete);
            // 
            // lblBaslik
            // 
            this.lblBaslik.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBaslik.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblBaslik.Location = new System.Drawing.Point(0, 0);
            this.lblBaslik.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBaslik.Name = "lblBaslik";
            this.lblBaslik.Size = new System.Drawing.Size(752, 39);
            this.lblBaslik.TabIndex = 0;
            this.lblBaslik.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.AutoScroll = true;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.label11);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Controls.Add(this.label12);
            this.panel4.Controls.Add(this.btnGonZarf);
            this.panel4.Controls.Add(this.btnGelZarf);
            this.panel4.Controls.Add(this.btnIrsaliyePdfIndir);
            this.panel4.Controls.Add(this.btnGonIrsYanit);
            this.panel4.Controls.Add(this.btnIrsaliyeGon);
            this.panel4.Controls.Add(this.btnIrsaliyeUblIndir);
            this.panel4.Controls.Add(this.btnZarfDurumSorgula);
            this.panel4.Controls.Add(this.btnGonIrsaliye);
            this.panel4.Controls.Add(this.btnGelIrsYanit);
            this.panel4.Controls.Add(this.btnIrsYanitGon);
            this.panel4.Controls.Add(this.btnMukSorgu);
            this.panel4.Controls.Add(this.btnGelIrsaliye);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(752, 204);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(209, 476);
            this.panel4.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label11.Location = new System.Drawing.Point(-1, 451);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(206, 1);
            this.label11.TabIndex = 122;
            // 
            // label10
            // 
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label10.Location = new System.Drawing.Point(-1, 321);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(206, 1);
            this.label10.TabIndex = 121;
            // 
            // label9
            // 
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label9.Location = new System.Drawing.Point(-1, 187);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(206, 1);
            this.label9.TabIndex = 120;
            // 
            // label12
            // 
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label12.Location = new System.Drawing.Point(-1, 57);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(206, 1);
            this.label12.TabIndex = 119;
            // 
            // btnGonZarf
            // 
            this.btnGonZarf.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnGonZarf.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnGonZarf.Location = new System.Drawing.Point(18, 193);
            this.btnGonZarf.Name = "btnGonZarf";
            this.btnGonZarf.Size = new System.Drawing.Size(168, 32);
            this.btnGonZarf.TabIndex = 110;
            this.btnGonZarf.Text = "Gönderilen Zarflar";
            this.btnGonZarf.UseVisualStyleBackColor = true;
            this.btnGonZarf.Click += new System.EventHandler(this.btnGonZarf_Click);
            // 
            // btnGelZarf
            // 
            this.btnGelZarf.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnGelZarf.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnGelZarf.Location = new System.Drawing.Point(18, 329);
            this.btnGelZarf.Name = "btnGelZarf";
            this.btnGelZarf.Size = new System.Drawing.Size(168, 32);
            this.btnGelZarf.TabIndex = 107;
            this.btnGelZarf.Text = "Gelen Zarflar";
            this.btnGelZarf.UseVisualStyleBackColor = true;
            this.btnGelZarf.Click += new System.EventHandler(this.btnGelZarf_Click);
            // 
            // btnIrsaliyePdfIndir
            // 
            this.btnIrsaliyePdfIndir.Enabled = false;
            this.btnIrsaliyePdfIndir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnIrsaliyePdfIndir.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnIrsaliyePdfIndir.Location = new System.Drawing.Point(18, 462);
            this.btnIrsaliyePdfIndir.Name = "btnIrsaliyePdfIndir";
            this.btnIrsaliyePdfIndir.Size = new System.Drawing.Size(168, 32);
            this.btnIrsaliyePdfIndir.TabIndex = 116;
            this.btnIrsaliyePdfIndir.Text = "İrsaliye PDF  İndir";
            this.btnIrsaliyePdfIndir.UseVisualStyleBackColor = true;
            this.btnIrsaliyePdfIndir.Click += new System.EventHandler(this.btnIrsaliyePdfIndir_Click);
            // 
            // btnGonIrsYanit
            // 
            this.btnGonIrsYanit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnGonIrsYanit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnGonIrsYanit.Location = new System.Drawing.Point(18, 273);
            this.btnGonIrsYanit.Name = "btnGonIrsYanit";
            this.btnGonIrsYanit.Size = new System.Drawing.Size(168, 37);
            this.btnGonIrsYanit.TabIndex = 112;
            this.btnGonIrsYanit.Text = "Gönderilen İrsaliye Yanıtları";
            this.btnGonIrsYanit.UseVisualStyleBackColor = true;
            this.btnGonIrsYanit.Click += new System.EventHandler(this.btnGonIrsYanit_Click);
            // 
            // btnIrsaliyeGon
            // 
            this.btnIrsaliyeGon.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnIrsaliyeGon.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnIrsaliyeGon.Location = new System.Drawing.Point(18, 65);
            this.btnIrsaliyeGon.Name = "btnIrsaliyeGon";
            this.btnIrsaliyeGon.Size = new System.Drawing.Size(168, 32);
            this.btnIrsaliyeGon.TabIndex = 113;
            this.btnIrsaliyeGon.Text = "İrsaliye Gönder";
            this.btnIrsaliyeGon.UseVisualStyleBackColor = true;
            this.btnIrsaliyeGon.Click += new System.EventHandler(this.btnIrsaliyeGon_Click);
            // 
            // btnIrsaliyeUblIndir
            // 
            this.btnIrsaliyeUblIndir.Enabled = false;
            this.btnIrsaliyeUblIndir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnIrsaliyeUblIndir.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnIrsaliyeUblIndir.Location = new System.Drawing.Point(18, 501);
            this.btnIrsaliyeUblIndir.Name = "btnIrsaliyeUblIndir";
            this.btnIrsaliyeUblIndir.Size = new System.Drawing.Size(168, 32);
            this.btnIrsaliyeUblIndir.TabIndex = 118;
            this.btnIrsaliyeUblIndir.Text = "İrsaliye UBL İndir";
            this.btnIrsaliyeUblIndir.UseVisualStyleBackColor = true;
            this.btnIrsaliyeUblIndir.Click += new System.EventHandler(this.btnIrsaliyeUblIndir_Click);
            // 
            // btnZarfDurumSorgula
            // 
            this.btnZarfDurumSorgula.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnZarfDurumSorgula.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnZarfDurumSorgula.Location = new System.Drawing.Point(18, 143);
            this.btnZarfDurumSorgula.Name = "btnZarfDurumSorgula";
            this.btnZarfDurumSorgula.Size = new System.Drawing.Size(168, 32);
            this.btnZarfDurumSorgula.TabIndex = 115;
            this.btnZarfDurumSorgula.Text = "Zarf Durumu Sorgula";
            this.btnZarfDurumSorgula.UseVisualStyleBackColor = true;
            this.btnZarfDurumSorgula.Click += new System.EventHandler(this.btnZarfDurumSorgula_Click);
            // 
            // btnGonIrsaliye
            // 
            this.btnGonIrsaliye.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnGonIrsaliye.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnGonIrsaliye.Location = new System.Drawing.Point(18, 234);
            this.btnGonIrsaliye.Name = "btnGonIrsaliye";
            this.btnGonIrsaliye.Size = new System.Drawing.Size(168, 32);
            this.btnGonIrsaliye.TabIndex = 111;
            this.btnGonIrsaliye.Text = "Gönderilen İrsaliyeler";
            this.btnGonIrsaliye.UseVisualStyleBackColor = true;
            this.btnGonIrsaliye.Click += new System.EventHandler(this.btnGonIrsaliye_Click);
            // 
            // btnGelIrsYanit
            // 
            this.btnGelIrsYanit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnGelIrsYanit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnGelIrsYanit.Location = new System.Drawing.Point(18, 407);
            this.btnGelIrsYanit.Name = "btnGelIrsYanit";
            this.btnGelIrsYanit.Size = new System.Drawing.Size(168, 32);
            this.btnGelIrsYanit.TabIndex = 109;
            this.btnGelIrsYanit.Text = "Gelen İrsaliye Yanıtları";
            this.btnGelIrsYanit.UseVisualStyleBackColor = true;
            this.btnGelIrsYanit.Click += new System.EventHandler(this.btnGelIrsYanit_Click);
            // 
            // btnIrsYanitGon
            // 
            this.btnIrsYanitGon.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnIrsYanitGon.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnIrsYanitGon.Location = new System.Drawing.Point(18, 104);
            this.btnIrsYanitGon.Name = "btnIrsYanitGon";
            this.btnIrsYanitGon.Size = new System.Drawing.Size(168, 32);
            this.btnIrsYanitGon.TabIndex = 114;
            this.btnIrsYanitGon.Text = "İrsaliye Yanıtı Gönder";
            this.btnIrsYanitGon.UseVisualStyleBackColor = true;
            this.btnIrsYanitGon.Click += new System.EventHandler(this.btnIrsYanitGon_Click);
            // 
            // btnMukSorgu
            // 
            this.btnMukSorgu.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnMukSorgu.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnMukSorgu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnMukSorgu.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnMukSorgu.Location = new System.Drawing.Point(18, 12);
            this.btnMukSorgu.Name = "btnMukSorgu";
            this.btnMukSorgu.Size = new System.Drawing.Size(168, 32);
            this.btnMukSorgu.TabIndex = 106;
            this.btnMukSorgu.Text = "Mükellef Listesi İndir";
            this.btnMukSorgu.UseVisualStyleBackColor = true;
            this.btnMukSorgu.Click += new System.EventHandler(this.btnMukSorgu_Click);
            // 
            // btnGelIrsaliye
            // 
            this.btnGelIrsaliye.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnGelIrsaliye.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnGelIrsaliye.Location = new System.Drawing.Point(18, 368);
            this.btnGelIrsaliye.Name = "btnGelIrsaliye";
            this.btnGelIrsaliye.Size = new System.Drawing.Size(168, 32);
            this.btnGelIrsaliye.TabIndex = 108;
            this.btnGelIrsaliye.Text = "Gelen İrsaliyeler\r\n";
            this.btnGelIrsaliye.UseVisualStyleBackColor = true;
            this.btnGelIrsaliye.Click += new System.EventHandler(this.btnGelIrsaliye_Click);
            // 
            // panelControls
            // 
            this.panelControls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelControls.Controls.Add(this.txtIrsaliyeId);
            this.panelControls.Controls.Add(this.lblIrsaliyeId);
            this.panelControls.Controls.Add(this.linkLabel1);
            this.panelControls.Controls.Add(this.label13);
            this.panelControls.Controls.Add(this.checkboxTLS11);
            this.panelControls.Controls.Add(this.panel3);
            this.panelControls.Controls.Add(this.checkboxTLS12);
            this.panelControls.Controls.Add(this.dtpIrsaliyeTarih2);
            this.panelControls.Controls.Add(this.txtSifre);
            this.panelControls.Controls.Add(this.txtPostaKutusu);
            this.panelControls.Controls.Add(this.label8);
            this.panelControls.Controls.Add(this.label7);
            this.panelControls.Controls.Add(this.dtpIrsaliyeTarih1);
            this.panelControls.Controls.Add(this.label6);
            this.panelControls.Controls.Add(this.txtKullanici);
            this.panelControls.Controls.Add(this.label5);
            this.panelControls.Controls.Add(this.label4);
            this.panelControls.Controls.Add(this.txtGonBirim);
            this.panelControls.Controls.Add(this.label3);
            this.panelControls.Controls.Add(this.txtTcVkn);
            this.panelControls.Controls.Add(this.label2);
            this.panelControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControls.Location = new System.Drawing.Point(0, 0);
            this.panelControls.Margin = new System.Windows.Forms.Padding(2);
            this.panelControls.Name = "panelControls";
            this.panelControls.Size = new System.Drawing.Size(961, 204);
            this.panelControls.TabIndex = 0;
            // 
            // linkLabel1
            // 
            this.linkLabel1.ActiveLinkColor = System.Drawing.Color.Red;
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.LinkColor = System.Drawing.Color.MediumBlue;
            this.linkLabel1.Location = new System.Drawing.Point(268, 173);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(356, 15);
            this.linkLabel1.TabIndex = 98;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "https://efaturawstest.fitbulut.com/ClientEDespatchServicePort.svc";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkTestURL_LinkClicked);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(201, 173);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(63, 15);
            this.label13.TabIndex = 97;
            this.label13.Text = "WS Adres:";
            // 
            // checkboxTLS11
            // 
            this.checkboxTLS11.AutoSize = true;
            this.checkboxTLS11.Location = new System.Drawing.Point(16, 74);
            this.checkboxTLS11.Margin = new System.Windows.Forms.Padding(2);
            this.checkboxTLS11.Name = "checkboxTLS11";
            this.checkboxTLS11.Size = new System.Drawing.Size(70, 17);
            this.checkboxTLS11.TabIndex = 94;
            this.checkboxTLS11.Text = "TLS v1.1";
            this.checkboxTLS11.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.lblAPI);
            this.panel3.Controls.Add(this.pictureBox3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(750, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(209, 202);
            this.panel3.TabIndex = 1;
            // 
            // lblAPI
            // 
            this.lblAPI.AutoSize = true;
            this.lblAPI.LinkColor = System.Drawing.Color.MediumBlue;
            this.lblAPI.Location = new System.Drawing.Point(52, 105);
            this.lblAPI.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAPI.Name = "lblAPI";
            this.lblAPI.Size = new System.Drawing.Size(119, 13);
            this.lblAPI.TabIndex = 54;
            this.lblAPI.TabStop = true;
            this.lblAPI.Text = "https://api.fitbulut.com/";
            this.lblAPI.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkAPI_LinkClicked);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(-2, -2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(212, 104);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox3.TabIndex = 53;
            this.pictureBox3.TabStop = false;
            // 
            // checkboxTLS12
            // 
            this.checkboxTLS12.AutoSize = true;
            this.checkboxTLS12.Checked = true;
            this.checkboxTLS12.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkboxTLS12.Location = new System.Drawing.Point(86, 74);
            this.checkboxTLS12.Margin = new System.Windows.Forms.Padding(2);
            this.checkboxTLS12.Name = "checkboxTLS12";
            this.checkboxTLS12.Size = new System.Drawing.Size(70, 17);
            this.checkboxTLS12.TabIndex = 95;
            this.checkboxTLS12.Text = "TLS v1.2";
            this.checkboxTLS12.UseVisualStyleBackColor = true;
            // 
            // dtpIrsaliyeTarih2
            // 
            this.dtpIrsaliyeTarih2.CalendarMonthBackground = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dtpIrsaliyeTarih2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dtpIrsaliyeTarih2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpIrsaliyeTarih2.Location = new System.Drawing.Point(614, 74);
            this.dtpIrsaliyeTarih2.Name = "dtpIrsaliyeTarih2";
            this.dtpIrsaliyeTarih2.Size = new System.Drawing.Size(113, 22);
            this.dtpIrsaliyeTarih2.TabIndex = 92;
            // 
            // txtSifre
            // 
            this.txtSifre.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtSifre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtSifre.Location = new System.Drawing.Point(458, 74);
            this.txtSifre.Name = "txtSifre";
            this.txtSifre.Size = new System.Drawing.Size(128, 22);
            this.txtSifre.TabIndex = 91;
            // 
            // txtPostaKutusu
            // 
            this.txtPostaKutusu.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtPostaKutusu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtPostaKutusu.Location = new System.Drawing.Point(203, 74);
            this.txtPostaKutusu.Name = "txtPostaKutusu";
            this.txtPostaKutusu.Size = new System.Drawing.Size(235, 22);
            this.txtPostaKutusu.TabIndex = 90;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label8.Location = new System.Drawing.Point(612, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 15);
            this.label8.TabIndex = 89;
            this.label8.Text = "Bitiş Tarihi ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(612, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 15);
            this.label7.TabIndex = 88;
            this.label7.Text = "Başlangıç Tarihi ";
            // 
            // dtpIrsaliyeTarih1
            // 
            this.dtpIrsaliyeTarih1.CalendarMonthBackground = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dtpIrsaliyeTarih1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dtpIrsaliyeTarih1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpIrsaliyeTarih1.Location = new System.Drawing.Point(614, 24);
            this.dtpIrsaliyeTarih1.Name = "dtpIrsaliyeTarih1";
            this.dtpIrsaliyeTarih1.Size = new System.Drawing.Size(113, 22);
            this.dtpIrsaliyeTarih1.TabIndex = 82;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(457, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 15);
            this.label6.TabIndex = 87;
            this.label6.Text = "WS Şifre ";
            // 
            // txtKullanici
            // 
            this.txtKullanici.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtKullanici.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtKullanici.Location = new System.Drawing.Point(458, 24);
            this.txtKullanici.Name = "txtKullanici";
            this.txtKullanici.Size = new System.Drawing.Size(128, 22);
            this.txtKullanici.TabIndex = 81;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(457, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 15);
            this.label5.TabIndex = 86;
            this.label5.Text = "WS Kullanıcı Adı ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(201, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(139, 15);
            this.label4.TabIndex = 85;
            this.label4.Text = "Posta Kutusu Etiketi ";
            // 
            // txtGonBirim
            // 
            this.txtGonBirim.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtGonBirim.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtGonBirim.Location = new System.Drawing.Point(203, 24);
            this.txtGonBirim.Name = "txtGonBirim";
            this.txtGonBirim.Size = new System.Drawing.Size(235, 22);
            this.txtGonBirim.TabIndex = 80;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(201, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 15);
            this.label3.TabIndex = 84;
            this.label3.Text = "Gönderici Birim Etiketi ";
            // 
            // txtTcVkn
            // 
            this.txtTcVkn.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtTcVkn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtTcVkn.Location = new System.Drawing.Point(16, 24);
            this.txtTcVkn.Name = "txtTcVkn";
            this.txtTcVkn.Size = new System.Drawing.Size(134, 22);
            this.txtTcVkn.TabIndex = 79;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(15, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 15);
            this.label2.TabIndex = 83;
            this.label2.Text = "VKN / TCKN ";
            // 
            // lblIrsaliyeId
            // 
            this.lblIrsaliyeId.AutoSize = true;
            this.lblIrsaliyeId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblIrsaliyeId.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblIrsaliyeId.Location = new System.Drawing.Point(201, 110);
            this.lblIrsaliyeId.Name = "lblIrsaliyeId";
            this.lblIrsaliyeId.Size = new System.Drawing.Size(69, 15);
            this.lblIrsaliyeId.TabIndex = 102;
            this.lblIrsaliyeId.Text = "İrsaliye Id";
            // 
            // txtIrsaliyeId
            // 
            this.txtIrsaliyeId.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtIrsaliyeId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtIrsaliyeId.Location = new System.Drawing.Point(204, 128);
            this.txtIrsaliyeId.Name = "txtIrsaliyeId";
            this.txtIrsaliyeId.Size = new System.Drawing.Size(235, 22);
            this.txtIrsaliyeId.TabIndex = 103;
            // 
            // FrmDespatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(963, 682);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(979, 686);
            this.Name = "FrmDespatch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Foriba Bulut API   -   e-İrsaliye Test Projesi  v1.7";
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdListIrsaliye)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panelControls.ResumeLayout(false);
            this.panelControls.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panelControls;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lblBaslik;
        private System.Windows.Forms.DataGridView grdListIrsaliye;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnGonZarf;
        private System.Windows.Forms.Button btnGelZarf;
        private System.Windows.Forms.Button btnIrsaliyePdfIndir;
        private System.Windows.Forms.Button btnGonIrsYanit;
        private System.Windows.Forms.Button btnIrsaliyeGon;
        private System.Windows.Forms.Button btnIrsaliyeUblIndir;
        private System.Windows.Forms.Button btnZarfDurumSorgula;
        private System.Windows.Forms.Button btnIrsYanitGon;
        private System.Windows.Forms.Button btnGonIrsaliye;
        private System.Windows.Forms.Button btnGelIrsYanit;
        private System.Windows.Forms.Button btnMukSorgu;
        private System.Windows.Forms.Button btnGelIrsaliye;
        private System.Windows.Forms.DateTimePicker dtpIrsaliyeTarih2;
        private System.Windows.Forms.TextBox txtSifre;
        private System.Windows.Forms.TextBox txtPostaKutusu;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpIrsaliyeTarih1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtKullanici;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtGonBirim;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTcVkn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkboxTLS11;
        private System.Windows.Forms.CheckBox checkboxTLS12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel lblAPI;
        private System.Windows.Forms.Label lblIrsaliyeId;
        private System.Windows.Forms.TextBox txtIrsaliyeId;
    }
}