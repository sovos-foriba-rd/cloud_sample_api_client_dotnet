using Foriba.OE.CLIENT.serviceDespatch;
using Microsoft.VisualBasic;
using System;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.ServiceModel;
using System.Windows.Forms;
using System.Linq;
using System.Collections;
using Foriba.OE.COMMON;
using Foriba.OE.UI.Exceptions;
using Foriba.OE.COMMON.Zip;
using Foriba.OE.COMMON.WebServices;
using Foriba.OE.COMMON.Model;

/// <summary>
/// Copyright © 2018 Foriba Teknoloji
/// Bu proje örnek bir web servis test projesidir. Yalnızca test sisteminde çalışmaktadır.
/// Version Info    : Version.txt
/// Readme          : Readme.txt
/// </summary>

namespace Foriba.OE.UI
{
    public partial class FrmDespatch : Form
    {
        public FrmDespatch()
        {
            InitializeComponent();
        }
        public string DespatchUUID;
        public byte[] XmlByte;
        public readonly RequestModel RequestModel = new RequestModel();


        /// <summary>
        /// Formun en üstünde bağlantı için kullanılan alanların dolu olmasını kontrol eder.
        /// </summary>
        /// <returns>True/False</returns>
        private bool CheckConnParam()
        {
            var check = true;
            foreach (var item in panelControls.Controls)
            {
                if (item.GetType() != typeof(TextBox)) continue;
                var t = (TextBox)item;
                if (string.IsNullOrEmpty(t.Text.Trim()))
                    check = false;
            }
            return check;
        }





        /// <summary>
        ///  Gelen ve gönderilen irsaliye butonlarına tıklanınca html, pdf ve ubl indir butonlarını aktif eder.
        /// </summary>
        /// <returns></returns>
        private void ButtonAktifPasif(bool htmlAndPdf, bool ubl)
        {
            btnIrsaliyeHtmlPdfIndir.Enabled = htmlAndPdf;
            btnIrsaliyeUblIndir.Enabled = ubl;
        }



        /// <summary>
        ///  CheckBox dan işaretlenen  SSL/TLS versionlarını listeye ekler.
        /// </summary>
        /// <returns>Listeye eklenen SSL/TLS listesi </returns>
        private ArrayList CheckedSSL()
        {
            var checkSSLList = new ArrayList();
            foreach (var item in panelControls.Controls)
            {
                if (item.GetType() == typeof(CheckBox))
                {
                    var t = (CheckBox)item;
                    if (t.Checked)
                    {
                        checkSSLList.Add(t.Text.Trim());
                    }
                }
            }
            return checkSSLList;
        }



        /// <summary>
        /// Gelen veya Gönderilen Irsaliye UUID değişkenini alıyor.
        /// </summary>
        /// <returns>Gelen veya Gönderilen Irsaliye UUID'sini alma</returns>
        private void grdListIrsaliye_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DespatchUUID = grdListIrsaliye.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
            catch (Exception)
            {
                DespatchUUID = "0";
            }
        }


        /// <summary>
        /// Text ve grid alanlarını temizleme
        /// </summary>
        private void ClearText()
        {
            lblBaslik.Text = "";
            DespatchUUID = null;
        }


        /// <summary>
        /// TextBoxlardan gelen bilgileri model nesnesine ekler
        /// </summary>
        /// <returns>Model nesnesi</returns>
        private TextModel SetTextModel()
        {
            var textModel = new TextModel
            {
                VknTckn = txtTcVkn.Text.Trim(),
                Kullanici = txtKullanici.Text.Trim(),
                Sifre = txtSifre.Text.Trim(),
                GbEtiketi = txtGonBirim.Text.Trim(),
                PkEtiketi = txtPostaKutusu.Text.Trim(),
                IssueDate = new DateTime(dtpIrsaliyeTarih1.Value.Year, dtpIrsaliyeTarih1.Value.Month,
                    dtpIrsaliyeTarih1.Value.Day, 00, 00, 00),
                EndDate = new DateTime(dtpIrsaliyeTarih2.Value.Year, dtpIrsaliyeTarih2.Value.Month,
                    dtpIrsaliyeTarih2.Value.Day, 23, 59, 59)
            };
            return textModel;
        }



        /// <summary>
        /// Web servisde getDesUserList metodundan  gelen mükellef listesi tablolarını tek 
        /// DataGridview de göstermeyi sağlar
        /// </summary>
        /// <returns>Array tipinde Linq Sorgusu</returns>
        private Array JoinTable()
        {
            var ds = new DataSet("tUserList");
            ds.ReadXml(new MemoryStream(XmlByte));
            var userTable = ds.Tables["User"];
            var documentsTable = ds.Tables["Documents"];
            var documentTable = ds.Tables["Document"];
            var aliasTable = ds.Tables["Alias"];

            var query = from a in userTable.AsEnumerable()
                        join b in documentsTable.AsEnumerable() on a.Field<int>("User_Id") equals b.Field<int>("User_Id")
                        join c in documentTable.AsEnumerable() on b.Field<int>("Documents_Id") equals c.Field<int>(
                            "Documents_Id")
                        join d in aliasTable.AsEnumerable() on c.Field<int>("Document_Id") equals d.Field<int>("Document_Id")
                        where c.Field<string>("type") == "DespatchAdvice"
                        select new
                        {
                            Identifier = a.Field<string>("Identifier"),
                            Name = d.Field<string>("Name"),
                            Title = a.Field<string>("Title"),
                            Type = a.Field<string>("Type"),
                            DocumentType = c.Field<string>("type"),
                            AccountType = a.Field<string>("AccountType"),
                            FirstCreationTime = a.Field<string>("FirstCreationTime"),
                            CreationTime = d.Field<string>("CreationTime")
                        };
            return query.ToArray();
        }


        /// <summary>
        /// TCKN/VKN parametresi ile sisteme kayıtlı  mükellefi sorgular
        /// </summary>
        /// <returns>TCKN/VKN ile Mükellef Sorgulama</returns>
        private void btnMukSorgu_Click(object sender, EventArgs e)
        {
            ClearText();

            try
            {
                if (!CheckConnParam())
                    throw new CheckConnParamException("TCKN/VKN, Gönderici Birim Etiketi, Posta Kutusu Etiketi, WS Kullanıcı Adı ve WS Şifre alanları boş bırakılamaz!");

                lblBaslik.Text = "Mükellef Sorgulanıyor";
                ButtonAktifPasif(false, false);
                grdListIrsaliye.DataSource = null;

                DespatchWebService despatch = new DespatchWebService();
                var result = despatch.MukellefSorgula(SetTextModel(), CheckedSSL()).DocData;
                var zippedStream = new MemoryStream(result);
                using (var archive = new ZipArchive(zippedStream))
                {
                    foreach (var entry in archive.Entries)
                    {
                        var ms = new MemoryStream();
                        var zipStream = entry.Open();
                        zipStream.CopyTo(ms);
                        XmlByte = ms.ToArray();
                    }
                }

                var fbDialog = new FolderBrowserDialog();
                fbDialog.Description = "Lütfen kaydetmek istediğiniz dizini seçiniz...";
                fbDialog.RootFolder = Environment.SpecialFolder.Desktop;
                if (fbDialog.ShowDialog() == DialogResult.OK)
                {
                    //dialog ile kullanıcıya seçtirilen dizine irsaliye UUID si ile dosya ismini set ederek kayıt işlemi yapıyoruz.
                    File.WriteAllBytes(fbDialog.SelectedPath + "\\" + "mükellefListesi" + ".xml", ZipUtility.UncompressFile(result));
                    MessageBox.Show("Mükellef Listesi İndirme Başarılı", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                lblBaslik.Text = "Mükellef Sorgulama";
                grdListIrsaliye.DataSource = JoinTable();

            }
            catch (CheckConnParamException ex)
            {
                MessageBox.Show(ex.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            catch (FaultException<ProcessingFault> ex)
            {
                MessageBox.Show(ex.Detail.Message, "ProcessingFault", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FaultException ex)
            {
                MessageBox.Show(ex.Message, "FaultException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }



        /// <summary>
        /// Oluşturulan irsaliye UBL'ini gönderir
        /// </summary>
        /// <returns>e-İrsaliye Gönderme</returns>
        private void btnIrsaliyeGon_Click(object sender, EventArgs e)
        {
            ClearText();

            try
            {
                if (!CheckConnParam())
                    throw new CheckConnParamException("TCKN/VKN, Gönderici Birim Etiketi, Posta Kutusu Etiketi, WS Kullanıcı Adı ve WS Şifre alanları boş bırakılamaz!");

                grdListIrsaliye.DataSource = null;
                ButtonAktifPasif(false, false);
                lblBaslik.Text = "İrsaliye Gönderiliyor.";
                var despatch = new DespatchWebService();
                var result = despatch.IrsaliyeGonder(SetTextModel(), CheckedSSL());
                grdListIrsaliye.DataSource = result.Response;
                lblBaslik.Text = "İrsaliye Gönderildi.";
                MessageBox.Show("e-İrsaliye başarıyla gönderildi", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (CheckConnParamException ex)
            {
                MessageBox.Show(ex.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (FaultException<ProcessingFault> ex)
            {
                MessageBox.Show(ex.Detail.Message, "ProcessingFault", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FaultException ex)
            {
                MessageBox.Show(ex.Message, "FaultException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }





        /// <summary>
        /// Sisteme gelen e-İrsaliye'ye irsaliye yanıtı  gönderir
        /// </summary>
        /// <returns>İrsaliye Yanıtı Gönderme</returns>
        private void btnIrsYanitGon_Click(object sender, EventArgs e)
        {
            DespatchUUID = null;
           
            try
            {
                if (!CheckConnParam())
                    throw new CheckConnParamException("TCKN/VKN, Gönderici Birim Etiketi, Posta Kutusu Etiketi, WS Kullanıcı Adı ve WS Şifre alanları boş bırakılamaz!");

                var UUID = Interaction.InputBox("Irsaliye yanıtı göndermek için Irsaliye UUID giriniz", "Irsaliye Yanıtı Gönderme");

                if (string.IsNullOrEmpty(UUID))
                    throw new UUIDNullException("Geçerli bir Irsaliye UUID giriniz!");
                
                var receiptAdvice = new DespatchWebService();
                var result = receiptAdvice.IrsaliyeYanitiGonder(SetTextModel(), UUID, CheckedSSL());
                ButtonAktifPasif(false, false);
                grdListIrsaliye.DataSource = null;
                grdListIrsaliye.DataSource = result.Response;
                lblBaslik.Text = "İrsaliye Yanıtı Gönderildi.";
                MessageBox.Show("İrsaliye yanıtı başarıyla gönderildi", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (CheckConnParamException ex)
            {
                MessageBox.Show(ex.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (UUIDNullException ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (FaultException<ProcessingFault> ex)
            {
                MessageBox.Show(ex.Detail.Message, "ProcessingFault", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FaultException ex)
            {
                MessageBox.Show(ex.Message, "FaultException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }



        /// <summary>
        ///  Zarf durumunu sorgular
        /// </summary>
        /// <returns> Zarf Durum Sorgulama</returns>
        private void btnZarfDurumSorgula_Click(object sender, EventArgs e)
        {
            DespatchUUID = null;
           

            try
            {
                if (!CheckConnParam())
                    throw new CheckConnParamException("TCKN/VKN, Gönderici Birim Etiketi, Posta Kutusu Etiketi, WS Kullanıcı Adı ve WS Şifre alanları boş bırakılamaz!");

                var envelopeUUID = Interaction.InputBox("Lütfen sorgulamak istediğiniz Zarf UUID giriniz.", "Zarf Durum Sorgulama", "", -1, -1);

                if (string.IsNullOrEmpty(envelopeUUID))
                    throw new UUIDNullException("Geçerli bir Zarf UUID giriniz!");
               

                var despatch = new DespatchWebService();
                var result = despatch.ZarfDurumSorgula(SetTextModel(), envelopeUUID, CheckedSSL());
                grdListIrsaliye.DataSource = null;
                ButtonAktifPasif(false, false);

                var dt = new DataTable();
                dt.Columns.Add("ZarfUUID");
                dt.Columns.Add("IssueDate");
                dt.Columns.Add("DocumentTypeCode");
                dt.Columns.Add("DocumentType");
                dt.Columns.Add("ResponseCode");
                dt.Columns.Add("Description");

                DataRow dRow = dt.NewRow();
                dRow["ZarfUUID"] = result[0].UUID;
                dRow["IssueDate"] = result[0].IssueDate;
                dRow["DocumentTypeCode"] = result[0].DocumentTypeCode;
                dRow["DocumentType"] = result[0].DocumentType;
                dRow["ResponseCode"] = result[0].ResponseCode;
                dRow["Description"] = result[0].Description;

                dt.Rows.Add(dRow);
                grdListIrsaliye.DataSource = dt;
                lblBaslik.Text = "Zarf Durumu Sorgulandı.";
            }
            catch (CheckConnParamException ex)
            {
                MessageBox.Show(ex.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (UUIDNullException ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
              
            }
            catch (FaultException<ProcessingFault> ex)
            {
                MessageBox.Show(ex.Detail.Message, "ProcessingFault", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FaultException ex)
            {
                MessageBox.Show(ex.Message, "FaultException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }





        /// <summary>
        /// Sisteme gönderilen zarf listesini getirir
        /// </summary>
        /// <returns>Gönderilen  Zarf Listesi</returns>
        private void btnGonZarf_Click(object sender, EventArgs e)
        {
            ClearText();
            try
            {
                if (!CheckConnParam())
                    throw new CheckConnParamException("TCKN/VKN, Gönderici Birim Etiketi, Posta Kutusu Etiketi, WS Kullanıcı Adı ve WS Şifre alanları boş bırakılamaz!");

                lblBaslik.Text = "Gönderilen Zarflar";
                ButtonAktifPasif(false, false);
                grdListIrsaliye.DataSource = null;
                DespatchWebService despatch = new DespatchWebService();
                grdListIrsaliye.DataSource = despatch.GonderilenZarflar(SetTextModel(), CheckedSSL());
                grdListIrsaliye.ClearSelection();
            }
            catch (CheckConnParamException ex)
            {
                MessageBox.Show(ex.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (FaultException<ProcessingFault> ex)
            {
                MessageBox.Show(ex.Detail.Message, "ProcessingFault", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FaultException ex)
            {
                MessageBox.Show(ex.Message, "FaultException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }



        /// <summary>
        /// Sisteme gönderilen e-İrsaliye listesini getirir
        /// </summary>
        /// <returns>Gönderilen e-İrsaliye Listesi</returns>
        private void btnGonIrsaliye_Click(object sender, EventArgs e)
        {
            ClearText();
            try
            {
                if (!CheckConnParam())
                    throw new CheckConnParamException("TCKN/VKN, Gönderici Birim Etiketi, Posta Kutusu Etiketi, WS Kullanıcı Adı ve WS Şifre alanları boş bırakılamaz!");

                RequestModel.Identifier = txtGonBirim.Text.Trim();
                RequestModel.DespatchType = "OUTBOUND";
                RequestModel.DocType = "DESPATCH";

                grdListIrsaliye.DataSource = null;
                DespatchWebService despatch = new DespatchWebService();
                grdListIrsaliye.DataSource = despatch.GonderilenIrsaliyeler(SetTextModel(), CheckedSSL(), RequestModel);
                lblBaslik.Text = "Gönderilen İrsaliyeler";
                ButtonAktifPasif(true, true);
                grdListIrsaliye.ClearSelection();
            }
            catch (CheckConnParamException ex)
            {
                MessageBox.Show(ex.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (FaultException<ProcessingFault> ex)
            {
                MessageBox.Show(ex.Detail.Message, "ProcessingFault", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FaultException ex)
            {
                MessageBox.Show(ex.Message, "FaultException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }




        /// <summary>
        /// Sisteme gönderilen e-irsaliye yanıtlarının listesi getirir
        /// </summary>
        /// <returns>Gönderilen e-irsaliye Yanıtlarının Listesi</returns>
        private void btnGonIrsYanit_Click(object sender, EventArgs e)
        {
            ClearText();
            try
            {
                if (!CheckConnParam())
                    throw new CheckConnParamException("TCKN/VKN, Gönderici Birim Etiketi, Posta Kutusu Etiketi, WS Kullanıcı Adı ve WS Şifre alanları boş bırakılamaz!");

                // getDesView ve getDesUBL metodunun requestinde gerekli olan bilgileri, requestModelin parametrelerinde dolduruyoruz 
                RequestModel.Identifier = txtPostaKutusu.Text.Trim();
                RequestModel.DocType = "RECEIPT";
                RequestModel.DespatchType = "OUTBOUND";

                grdListIrsaliye.DataSource = null;
                DespatchWebService despatch = new DespatchWebService();
                grdListIrsaliye.DataSource = despatch.GonderilenIrsaliyeYanitlari(SetTextModel(), CheckedSSL(), RequestModel);
                ButtonAktifPasif(true, true);
                lblBaslik.Text = "Gönderilen İrsaliye Yanıtları";
                grdListIrsaliye.ClearSelection();
            }
            catch (CheckConnParamException ex)
            {
                MessageBox.Show(ex.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (FaultException<ProcessingFault> ex)
            {
                MessageBox.Show(ex.Detail.Message, "ProcessingFault", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FaultException ex)
            {
                MessageBox.Show(ex.Message, "FaultException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }




        /// <summary>
        /// Sisteme gelen zarf listesini getirir
        /// </summary>
        /// <returns>Gelen Zarf Listesi</returns>
        private void btnGelZarf_Click(object sender, EventArgs e)
        {
            ClearText();
            try
            {
                if (!CheckConnParam())
                    throw new CheckConnParamException("TCKN/VKN, Gönderici Birim Etiketi, Posta Kutusu Etiketi, WS Kullanıcı Adı ve WS Şifre alanları boş bırakılamaz!");

                var despatch = new DespatchWebService();
                var result = despatch.GelenZarflar(SetTextModel(), CheckedSSL()).Response;
                grdListIrsaliye.DataSource = result;
                lblBaslik.Text = "Gelen Zarflar";
                ButtonAktifPasif(false, false);
            }
            catch (CheckConnParamException ex)
            {
                MessageBox.Show(ex.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (FaultException<ProcessingFault> ex)
            {
                MessageBox.Show(ex.Detail.Message, "ProcessingFault", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FaultException ex)
            {
                MessageBox.Show(ex.Message, "FaultException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }




        /// <summary>
        /// Sisteme gelen e-irsaliye listesini getirir
        /// </summary>
        /// <returns>Gelen e-İrsaliye Listesi</returns>
        private void btnGelIrsaliye_Click(object sender, EventArgs e)
        {
            ClearText();
            try
            {
                if (!CheckConnParam())
                    throw new CheckConnParamException("TCKN/VKN, Gönderici Birim Etiketi, Posta Kutusu Etiketi, WS Kullanıcı Adı ve WS Şifre alanları boş bırakılamaz!");

                // getDesView ve getDesUBL metodunun requestinde gerekli olan bilgileri, requestModelin parametrelerinde dolduruyoruz 
                RequestModel.Identifier = txtPostaKutusu.Text.Trim();
                RequestModel.DocType = "DESPATCH";
                RequestModel.DespatchType = "INBOUND";

                var despatch = new DespatchWebService();
                grdListIrsaliye.DataSource = null;
                var result = despatch.GelenIrsaliyeler(SetTextModel(), CheckedSSL(), RequestModel).Response;
                grdListIrsaliye.DataSource = result;
                lblBaslik.Text = "Gelen İrsaliyeler";
                ButtonAktifPasif(true, true);
            }
            catch (CheckConnParamException ex)
            {
                MessageBox.Show(ex.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (FaultException<ProcessingFault> ex)
            {
                MessageBox.Show(ex.Detail.Message, "ProcessingFault", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FaultException ex)
            {
                MessageBox.Show(ex.Message, "FaultException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }



        /// <summary>
        /// Sisteme gelen e-irsaliye yanıtı listesini getirir
        /// </summary>
        /// <returns>Gelen e-İrsaliye Yanıt Listesi</returns>
        private void btnGelIrsYanit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckConnParam())
                    throw new CheckConnParamException("TCKN/VKN, Gönderici Birim Etiketi, Posta Kutusu Etiketi, WS Kullanıcı Adı ve WS Şifre alanları boş bırakılamaz!");

                // getDesView ve getDesUBL metodunun requestinde gerekli olan bilgileri, requestModelin parametrelerinde dolduruyoruz 
                RequestModel.Identifier = txtGonBirim.Text.Trim();
                RequestModel.DocType = "RECEIPT";
                RequestModel.DespatchType = "INBOUND";

                DespatchWebService despatch = new DespatchWebService();
                grdListIrsaliye.DataSource = null;
                var result = despatch.GelenIrsaliyeYanitlari(SetTextModel(), CheckedSSL(), RequestModel).Response;
                grdListIrsaliye.DataSource = result;
                lblBaslik.Text = "Gelen İrsaliye Yanıtları";
                ButtonAktifPasif(true, true);
            }
            catch (CheckConnParamException ex)
            {
                MessageBox.Show(ex.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (FaultException<ProcessingFault> ex)
            {
                MessageBox.Show(ex.Detail.Message, "ProcessingFault", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FaultException ex)
            {
                MessageBox.Show(ex.Message, "FaultException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }



        /// <summary>
        /// Grid üzerinden seçilen irsaliye id bilgisini aldıktan sonra html ve pdf formatında bilgisayara kaydını yapar
        /// </summary>
        /// <returns>e-İrsaliye HTML ve PDF Kaydetme</returns>
        private void btnIrsaliyeHtmlPdfIndir_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckConnParam())
                    throw new CheckConnParamException("TCKN/VKN, Gönderici Birim Etiketi, Posta Kutusu Etiketi, WS Kullanıcı Adı ve WS Şifre alanları boş bırakılamaz!");

                if (DespatchUUID == "0" || DespatchUUID == null)
                    throw new UUIDNullException("Lütfen bir irsaliye seçiniz!");

                var fatura = new DespatchWebService();

                var result = fatura.IrsaliyeHTMLPDFIndir(SetTextModel(), DespatchUUID, CheckedSSL(), RequestModel).Response;

                FolderBrowserDialog fbDialog = new FolderBrowserDialog();

                fbDialog.Description = "Lütfen kaydetmek istediğiniz dizini seçiniz...";

                fbDialog.RootFolder = Environment.SpecialFolder.Desktop;

                if (fbDialog.ShowDialog() == DialogResult.OK)
                {
                    //dialog ile kullanıcıya seçtirilen dizine irsaliye UUID si ile dosya ismini set ederek kayıt işlemi yapıyoruz.

                    File.WriteAllBytes(fbDialog.SelectedPath + "\\" + DespatchUUID + ".html", ZipUtility.UncompressFile(result[0].DocData));

                    File.WriteAllBytes(fbDialog.SelectedPath + "\\" + DespatchUUID + ".pdf", ZipUtility.UncompressFile(result[1].DocData));

                    MessageBox.Show("HTML ve PDF İndirme Başarılı", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }

            catch (UUIDNullException ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            catch (CheckConnParamException ex)
            {
                MessageBox.Show(ex.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            catch (FaultException<ProcessingFault> ex)
            {
                MessageBox.Show(ex.Detail.Message, "ProcessingFault", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FaultException ex)
            {
                MessageBox.Show(ex.Message, "FaultException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }



        /// <summary>
        /// Grid üzerinden seçilen irsaliyenin id bilgisini aldıktan sonra XML(UBL) formatında bilgisayara kaydını yapar
        /// </summary>
        /// <returns>e-İrsaliye UBL Kaydetme</returns>
        private void btnIrsaliyeUblIndir_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckConnParam())
                    throw new CheckConnParamException("TCKN/VKN, Gönderici Birim Etiketi, Posta Kutusu Etiketi, WS Kullanıcı Adı ve WS Şifre alanları boş bırakılamaz!");

                if (DespatchUUID == "0" || DespatchUUID == null)
                    throw new UUIDNullException("Lütfen bir irsaliye seçiniz!");

                var fatura = new DespatchWebService();

                var result = fatura.IrsaliyeUBLIndir(SetTextModel(), DespatchUUID, CheckedSSL(), RequestModel).Response;

                FolderBrowserDialog fbDialog = new FolderBrowserDialog();

                fbDialog.Description = "Lütfen kaydetmek istediğiniz dizini seçiniz...";

                fbDialog.RootFolder = Environment.SpecialFolder.Desktop;

                if (fbDialog.ShowDialog() == DialogResult.OK)
                {
                    //dialog ile kullanıcıya seçtirilen dizine irsaliye UUID si ile dosya ismini set ederek kayıt işlemi yapıyoruz.

                    File.WriteAllBytes(fbDialog.SelectedPath + "\\" + DespatchUUID + ".xml", ZipUtility.UncompressFile(result[0].DocData));

                    MessageBox.Show("UBL İndirme Başarılı", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }

            catch (UUIDNullException ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            catch (CheckConnParamException ex)
            {
                MessageBox.Show(ex.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            catch (FaultException<ProcessingFault> ex)
            {
                MessageBox.Show(ex.Detail.Message, "ProcessingFault", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FaultException ex)
            {
                MessageBox.Show(ex.Message, "FaultException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        /// <summary>
        ///  e-İrsaliye Test Web Servis Adresi
        /// </summary>
        private void linkTestURL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://efaturawstest.fitbulut.com/ClientEDespatchServicePort.svc");
        }

        /// <summary>
        ///  Foriba Bulut API Adresi
        /// </summary>
        private void linkAPI_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://api.fitbulut.com/");
        }
    }
}
