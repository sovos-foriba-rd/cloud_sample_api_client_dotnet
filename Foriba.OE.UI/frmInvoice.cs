using Foriba.OE.CLIENT.serviceInvoicee;
using Foriba.OE.COMMON;
using Foriba.OE.COMMON.WebServices;
using Foriba.OE.UBL.UBLCreate;
using Foriba.OE.UI.Exceptions;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Data;
using System.IO;
using System.ServiceModel;
using System.Windows.Forms;
using Foriba.OE.COMMON.Model;
using System.Collections.Generic;
using System.Linq;
using System.IO.Compression;
using Foriba.OE.COMMON.Zip;

/// <summary>
/// Copyright © 2018 Foriba Teknoloji
/// Bu proje örnek bir web servis test projesidir. Yalnızca test sisteminde çalışmaktadır.
/// Version Info    : Version.txt
/// Readme          : Readme.txt
/// </summary>

namespace Foriba.OE.UI
{
    public partial class FrmInvoice : Form
    {
        public string InvUUID;
        public byte[] XmlByte;

        public FrmInvoice()
        {
            InitializeComponent();
            btnZarfDurumSorgula.Enabled = false;

        }

        /// <summary>
        /// Formun en üstünde bağlantı için kullanılan alanların dolu olmasını kontrol eder
        /// </summary>
        private bool CheckConnParam()
        {
            var check = true;

            foreach (var item in panelControls.Controls)
            {
                if (item.GetType() != typeof(TextBox)) continue;
                var t = (TextBox)item;
                if (string.IsNullOrEmpty(t.Text.Trim()) && t.Name != "txtFaturaId")
                    check = false;
            }

            return check;
        }


        /// <summary>
        /// Web servisde getRAWUserList metodundan  gelen mükellef listesi tablolarını tek 
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
                        where c.Field<string>("type") == "Invoice"
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
        /// Gelen ve gönderilen faturalar butonlarına tıklanınca html, pdf ve ubl indir butonlarını aktif eder
        /// </summary>
        /// <returns></returns>
        private void ButtonAktifPasif(bool html, bool pdf, bool ubl)
        {
            btnFaturaHtmlIndir.Enabled = html;
            btnFaturaPdfIndir.Enabled = pdf;
            btnFaturaUblIndir.Enabled = ubl;
        }



        /// <summary>
        /// Text ve grid alanlarını temizleme
        /// </summary>
        private void ClearText()
        {
            lblBaslik.Text = "";
            InvUUID = null;
            btnZarfDurumSorgula.Enabled = false;
        }


        /// <summary>
        ///  CheckBoxlarda işaretlenen  SSL/TLS versionlarını listeye ekler
        /// </summary>
        /// <returns>CheckBoxlarda işaretlenen SSL/TLS listesi </returns>
        private ArrayList CheckedSSL()
        {
            var checkSSLList = new ArrayList();
            foreach (var item in panelControls.Controls)
            {
                if (item.GetType() == typeof(CheckBox))
                {
                    CheckBox t = (CheckBox)item;
                    if (t.Checked)
                    {
                        checkSSLList.Add(t.Text.Trim());
                    }

                }
            }
            return checkSSLList;
        }



        /// <summary>
        /// Gelen veya Gönderilen Faturaların UUID değişkenini alır
        /// </summary>
        /// <returns>Gelen veya Gönderilen Faturaların UUID'si</returns>
        private void grdListFatura_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                InvUUID = grdListFatura.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
            catch (Exception)
            {

                InvUUID = "0";
            }
        }



        /// <summary>
        /// TextBoxlardan gelen bilgileri model nesnesine ekler
        /// </summary>
        /// <returns>Model nesnesi</returns>
        private TextModel SetValue()
        {

            var model = new TextModel
            {
                VknTckn = txtTcVkn.Text.Trim(),
                Kullanici = txtKullanici.Text.Trim(),
                Sifre = txtSifre.Text.Trim(),
                GbEtiketi = txtGonBirim.Text.Trim(),
                PkEtiketi = txtPostaKutusu.Text.Trim(),
                IssueDate = new DateTime(dtpFaturaTarih1.Value.Year, dtpFaturaTarih1.Value.Month,
                    dtpFaturaTarih1.Value.Day, 00, 00, 00),
                EndDate = new DateTime(dtpFaturaTarih2.Value.Year, dtpFaturaTarih2.Value.Month,
                    dtpFaturaTarih2.Value.Day, 23, 59, 59),
                FaturaID = txtFaturaId.Text != null || txtFaturaId.Text != "" ? txtFaturaId.Text : null
            };

            return model;
        }



        /// <summary>
        /// TCKN/VKN parametresi ile sisteme kayıtlı e-fatura mükellefi sorgular
        /// </summary>
        /// <returns> e-Fatura Mükellef Listesi</returns>
        private void btnMukSorgu_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckConnParam())
                    throw new CheckConnParamException("TCKN/VKN, Gönderici Birim Etiketi, Posta Kutusu Etiketi, WS Kullanıcı Adı ve WS Şifre alanları boş bırakılamaz!");

                lblBaslik.Text = "Mükellef Sorgulanıyor";
                ButtonAktifPasif(false, false, false);
                grdListFatura.DataSource = null;

                InvoiceWebService invoice = new InvoiceWebService();
                var result = invoice.MukellefSorgula(SetValue(), CheckedSSL()).DocData;
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
                grdListFatura.DataSource = JoinTable();

            }
            catch (UUIDNullException ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            catch (NumericVknTcknException ex)
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
                lblBaslik.Text = "";
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
                    throw new CheckConnParamException(
                        "TCKN/VKN, Gönderici Birim Etiketi, Posta Kutusu Etiketi, WS Kullanıcı Adı ve WS Şifre alanları boş bırakılamaz!");


                lblBaslik.Text = "Gelen Zarflar";
                ButtonAktifPasif(false, false, false);

                grdListFatura.DataSource = null;
                InvoiceWebService fatura = new InvoiceWebService();
                grdListFatura.DataSource = fatura.GelenZarflar(SetValue(), CheckedSSL());
                grdListFatura.ClearSelection();
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
                lblBaslik.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }


        }



        /// <summary>
        /// Sisteme gelen e-fatura listesini getirir
        /// </summary>
        /// <returns> Gelen e-Fatura Listesi</returns>
        private void btnGelFatura_Click(object sender, EventArgs e)
        {

            ClearText();

            try
            {
                if (!CheckConnParam())
                    throw new CheckConnParamException("TCKN/VKN, Gönderici Birim Etiketi, Posta Kutusu Etiketi, WS Kullanıcı Adı ve WS Şifre alanları boş bırakılamaz!");

                lblBaslik.Text = "Gelen Faturalar";
                grdListFatura.DataSource = null;
                ButtonAktifPasif(true, true, true);
                InvoiceWebService fatura = new InvoiceWebService();
                grdListFatura.DataSource = fatura.GelenFaturalar(SetValue(), CheckedSSL());
                grdListFatura.ClearSelection();
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
                lblBaslik.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }



        /// <summary>
        /// Gelen e-faturalara gönderdiğimiz uygulama yanıtlarının (kabul,red) listesini getirir
        /// </summary>
        /// <returns>Gelen e-faturalara gönderdiğimiz uygulama yanıtlarının (kabul,red) listesi</returns>
        private void btnGelenUygYanıt_Click(object sender, EventArgs e)
        {
            ClearText();
            try
            {
                if (!CheckConnParam())
                    throw new CheckConnParamException("TCKN/VKN, Gönderici Birim Etiketi, Posta Kutusu Etiketi, WS Kullanıcı Adı ve WS Şifre alanları boş bırakılamaz!");

                lblBaslik.Text = "Gelen Uygulama Yanıtları";
                ButtonAktifPasif(false, false, false);
                InvoiceWebService fatura = new InvoiceWebService();
                grdListFatura.DataSource = null;
                grdListFatura.DataSource = fatura.GelenUygulamaYanitlari(SetValue(), CheckedSSL());
            }
            catch (CheckConnParamException ex)
            {
                MessageBox.Show(ex.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (FaultException<ProcessingFault> ex)
            {
                MessageBox.Show(ex.Detail.Message, "FaultException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FaultException ex)
            {
                MessageBox.Show(ex.Message, "FaultException", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblBaslik.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }




        /// <summary>
        /// Sisteme gönderilen zarf listesini getirir
        /// </summary>
        /// <returns> Gönderilen Zarf Listesi</returns>
        private void btnGondrlZarf_Click(object sender, EventArgs e)
        {
            ClearText();
            btnZarfDurumSorgula.Enabled = true;
            try
            {
                if (!CheckConnParam())
                    throw new CheckConnParamException("TCKN/VKN, Gönderici Birim Etiketi, Posta Kutusu Etiketi, WS Kullanıcı Adı ve WS Şifre alanları boş bırakılamaz!");

                grdListFatura.DataSource = null;
                lblBaslik.Text = "Gönderilen Zarflar";
                ButtonAktifPasif(false, false, false);
                InvoiceWebService fatura = new InvoiceWebService();
                grdListFatura.DataSource = fatura.GonderilenZarflar(SetValue(), CheckedSSL());
                grdListFatura.ClearSelection();
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
                lblBaslik.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }



        /// <summary>
        /// Sisteme gönderilen e-fatura listesini getirir
        /// </summary>
        /// <returns> Gönderilen e-Fatura Listesi</returns>
        private void btnGonFatura_Click(object sender, EventArgs e)
        {
            ClearText();
            try
            {
                if (!CheckConnParam())
                    throw new CheckConnParamException("TCKN/VKN, Gönderici Birim Etiketi, Posta Kutusu Etiketi, WS Kullanıcı Adı ve WS Şifre alanları boş bırakılamaz!");

                grdListFatura.DataSource = null;
                lblBaslik.Text = "Gönderilen Faturalar";
                ButtonAktifPasif(true, true, true);
                InvoiceWebService fatura = new InvoiceWebService();
                grdListFatura.DataSource = fatura.GonderilenFaturalar(SetValue(), CheckedSSL());
                grdListFatura.ClearSelection();
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
                lblBaslik.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }




        /// <summary>
        /// Gönderilen e-faturalara gelen uygulama yanıtlarının (kabul,red) listesini getirir
        /// </summary>
        /// <returns>Gönderilen e-faturalara gelen uygulama yanıtlarının (kabul,red) listesi</returns>
        private void btnGondrlnUyg_Click(object sender, EventArgs e)
        {
            ClearText();
            try
            {
                if (!CheckConnParam())
                    throw new CheckConnParamException("TCKN/VKN, Gönderici Birim Etiketi, Posta Kutusu Etiketi, WS Kullanıcı Adı ve WS Şifre alanları boş bırakılamaz!");

                lblBaslik.Text = "Gönderilen Uygulama Yanıtları";
                ButtonAktifPasif(false, false, false);
                var fatura = new InvoiceWebService();
                grdListFatura.DataSource = null;
                grdListFatura.DataSource = fatura.GonderilenUygulamaYanitlari(SetValue(), CheckedSSL());
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
                lblBaslik.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }



        /// <summary>
        /// Oluşturulan faturanın gönderimini gerçekleştirir
        /// </summary>
        /// <returns>e-Fatura Gönderme</returns>
        private void btnFaturaGonder_Click(object sender, EventArgs e)
        {
            ClearText();
            try
            {
                if (!CheckConnParam())
                    throw new CheckConnParamException("TCKN/VKN, Gönderici Birim Etiketi, Posta Kutusu Etiketi, WS Kullanıcı Adı ve WS Şifre alanları boş bırakılamaz!");

                grdListFatura.DataSource = null;
                lblBaslik.Text = "Fatura Gönderiliyor..";
                ButtonAktifPasif(false, false, false);
                var fatura = new InvoiceWebService();
                var result = fatura.FaturaGonder(SetValue(), CheckedSSL(), GetUBLInvoiceData());
                grdListFatura.DataSource = result;
                lblBaslik.Text = "Fatura Gönderildi.";
                MessageBox.Show("e-Fatura başarıyla gönderildi", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFaturaId.Text = null;
            }
            catch (CheckConnParamException ex)
            {
                MessageBox.Show(ex.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (FaultException<ProcessingFault> ex)
            {

                MessageBox.Show(ex.Detail.Message + "    " + ex.Detail.Code, "ProcessingFault", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FaultException ex)
            {

                MessageBox.Show(ex.Message, "FaultException", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblBaslik.Text = ex.Code.Name;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }



        /// <summary>
        /// Gelen ticari e-Fatura'ya uygulama yanıtı (kabul,red) gönderir
        /// </summary>
        /// <returns>Ticari faturaya uygulama yanıtı gönderme</returns>
        private void btnUygYanitGon_Click(object sender, EventArgs e)
        {

            try
            {
                if (!CheckConnParam())
                    throw new CheckConnParamException("TCKN/VKN, Gönderici Birim Etiketi, Posta Kutusu Etiketi, WS Kullanıcı Adı ve WS Şifre alanları boş bırakılamaz!");

                var UUID = Interaction.InputBox("Uygulama yanıtı göndermek için Fatura UUID giriniz", "Uygulama Yanıtı Gönderme", "", -1, -1);

                if (string.IsNullOrEmpty(UUID))
                    throw new UUIDNullException("Uygulama yanıtı göndermek için lütfen bir Fatura UUID giriniz!");


                var uygulamaYaniti = new InvoiceWebService();
                var result = uygulamaYaniti.UygulamaYanitiGonder(SetValue(), UUID, CheckedSSL());
                grdListFatura.DataSource = null;
                ButtonAktifPasif(false, false, false);
                ClearText();
                grdListFatura.DataSource = result;
                lblBaslik.Text = "Uygulama Yanıtı Gönderildi.";
                MessageBox.Show("Uygulama yanıtı başarıyla gönderildi", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                lblBaslik.Text = "";
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }



        /// <summary>
        /// Gönderilen e-Fatura'nın durumunu sorgular
        /// </summary>
        /// <returns>e-Fatura Durum Sorgulama</returns>
        private void btnZarfDurumSorgula_Click(object sender, EventArgs e)
        {
            ButtonAktifPasif(false, false, false);

            try
            {
                if (!CheckConnParam())
                    throw new CheckConnParamException(
                        "TCKN/VKN, Gönderici Birim Etiketi, Posta Kutusu Etiketi, WS Kullanıcı Adı ve WS Şifre alanları boş bırakılamaz!");


                List<string> allUUID = new List<string>();
                List<getEnvelopeStatusResponseType> resultList = new List<getEnvelopeStatusResponseType>();
                var fatura = new InvoiceWebService();


                foreach (DataGridViewRow item in grdListFatura.Rows)
                {
                    allUUID.Add(item.Cells[0].Value.ToString());
                }

                if (allUUID.Count != 0)
                {

                    var splitUUIDArray = allUUID.Split(20);

                    foreach (var UUIDList in splitUUIDArray)
                    {
                        var result = fatura.ZarfDurumSorgula(SetValue(), UUIDList.ToArray(), CheckedSSL());

                        foreach (var item in result)
                        {
                            resultList.Add(item);
                        }
                    }


                    grdListFatura.DataSource = null;
                    ClearText();
                    lblBaslik.Text = "Zarf Durumu Sorgulandı.";

                    var dt = new DataTable();
                    dt.Columns.Add("ZarfUUID");
                    dt.Columns.Add("IssueDate");
                    dt.Columns.Add("DocumentTypeCode");
                    dt.Columns.Add("DocumentType");
                    dt.Columns.Add("ResponseCode");
                    dt.Columns.Add("Description");

                    foreach (var item in resultList)
                    {
                        var dRow = dt.NewRow();

                        dRow["ZarfUUID"] = item.UUID;
                        dRow["IssueDate"] = item.IssueDate;
                        dRow["DocumentTypeCode"] = item.DocumentTypeCode;
                        dRow["DocumentType"] = item.DocumentType;
                        dRow["ResponseCode"] = item.ResponseCode;
                        dRow["Description"] = item.Description;

                        dt.Rows.Add(dRow);
                    }
                    grdListFatura.DataSource = dt;

                }
                else
                {
                    MessageBox.Show("Sorgulanacak Zarf Bulunamadı.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }



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
                lblBaslik.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        /// <summary>
        /// Grid üzerinden seçilen faturanın id bilgisini aldıktan sonra html formatında bilgisayara kayıt eder
        /// </summary>
        /// <returns>e-Fatura HTML Kaydetme</returns>
        private void btnFaturaHtmlIndir_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckConnParam())
                    throw new CheckConnParamException("TCKN/VKN, Gönderici Birim Etiketi, Posta Kutusu Etiketi, WS Kullanıcı Adı ve WS Şifre alanları boş bırakılamaz!");

                if (InvUUID == "0" || InvUUID == null)
                    throw new UUIDNullException("Lütfen bir fatura seçiniz!");

                var fatura = new InvoiceWebService();
                var result = fatura.FaturaHTMLIndir(SetValue(), InvUUID, CheckedSSL());
                var fbDialog = new FolderBrowserDialog();
                fbDialog.Description = "Lütfen kaydetmek istediğiniz dizini seçiniz...";
                fbDialog.RootFolder = Environment.SpecialFolder.Desktop;
                if (fbDialog.ShowDialog() == DialogResult.OK)
                {
                    //dialog ile kullanıcıya seçtirilen dizine fatura UUID si ile dosya ismini set ederek kayıt işlemi yapıyoruz.
                    File.WriteAllBytes(fbDialog.SelectedPath + "\\" + InvUUID + ".html", result.DocData);
                    MessageBox.Show("Fatura HTML İndirme Başarılı", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                lblBaslik.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }



        /// <summary>
        /// Grid üzerinden seçilen faturanın id bilgisini aldıktan sonra pdf formatında bilgisayara kayıt eder
        /// </summary>
        /// <returns>e-Fatura PDF Kaydetme</returns>
        private void btnFaturaPdfIndir_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckConnParam())
                    throw new CheckConnParamException("TCKN/VKN, Gönderici Birim Etiketi, Posta Kutusu Etiketi, WS Kullanıcı Adı ve WS Şifre alanları boş bırakılamaz!");

                if (InvUUID == "0" || InvUUID == null)
                    throw new UUIDNullException("Lütfen bir fatura seçiniz!");

                var fatura = new InvoiceWebService();
                var result = fatura.FaturaPDFIndir(SetValue(), InvUUID, CheckedSSL());
                var fbDialog = new FolderBrowserDialog();
                fbDialog.Description = "Lütfen kaydetmek istediğiniz dizini seçiniz...";
                fbDialog.RootFolder = Environment.SpecialFolder.Desktop;
                if (fbDialog.ShowDialog() == DialogResult.OK)
                {
                    //dialog ile kullanıcıya seçtirilen dizine fatura UUID si ile dosya ismini set ederek kayıt işlemi yapıyoruz.
                    File.WriteAllBytes(fbDialog.SelectedPath + "\\" + InvUUID + ".pdf", result.DocData);
                    MessageBox.Show("Fatura PDF İndirme Başarılı", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                lblBaslik.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }



        }


        /// <summary>
        /// Grid üzerindeki tüm faturaların id bilgisini aldıktan sonra xml formatında masaüstünde oluşacak dosyaya kayıt eder.
        /// </summary>
        /// <returns>e-Fatura XML Kaydetme</returns>
        private void btnFaturaUblIndir_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    if (!CheckConnParam())
                        throw new CheckConnParamException("TCKN/VKN, Gönderici Birim Etiketi, Posta Kutusu Etiketi, WS Kullanıcı Adı ve WS Şifre alanları boş bırakılamaz!");

                    List<string> allUUID = new List<string>();
                    var fatura = new InvoiceWebService();

                    foreach (DataGridViewRow item in grdListFatura.Rows)
                    {
                        allUUID.Add(item.Cells[0].Value.ToString());
                    }

                    if (allUUID.Count != 0)
                    {

                        string recordPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\INVOICE-UBL";

                        if (File.Exists(recordPath) == false)
                            Directory.CreateDirectory(recordPath);

                        var splitUUIDArray = allUUID.Split(20);

                        foreach (var UUIDList in splitUUIDArray)
                        {
                            var result = fatura.FaturaUBLIndir(SetValue(), UUIDList.ToArray(), CheckedSSL());

                            for (int i = 0; i < result.Count(); i++)
                            {
                                File.WriteAllBytes(Path.Combine(recordPath, UUIDList[i] + ".xml"), result[i]);
                            }
                        }

                        MessageBox.Show("Fatura UBL İndirme Başarılı", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("İndirilecek Fatura UBL'i Bulunamadı.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    lblBaslik.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }




        /// <summary>
        ///  Fatura UBL'inin alanlarını kalıtım alınan BaseInvoiceUBL sınıfında oluşturulan  metodları kullanarak doldurur
        /// </summary>
        /// <returns>OLuşturulan e-Fatura UBL'i </returns>
        private BaseInvoiceUBL GetUBLInvoiceData()
        {
            string faturaId = txtFaturaId.Text != null || txtFaturaId.Text != "" ? txtFaturaId.Text : null;
            BaseInvoiceUBL ublInvoice = new InvoiceUBL("TICARIFATURA", "SATIS", "TRY", faturaId);
            ublInvoice.SetCustInvIdDocumentReference();
            ublInvoice.SetSignature();
            ublInvoice.SetInvoiceLines(ublInvoice.GetInvoiceLines());
            switch (txtTcVkn.Text.Length)
            {
                case 10:
                    ublInvoice.SetSupplierParty(ublInvoice.GetParty(txtTcVkn.Text, "VKN"));
                    ublInvoice.SetCustomerParty(ublInvoice.GetParty(txtTcVkn.Text, "VKN"));
                    break;
                case 11:
                    ublInvoice.SetSupplierParty(ublInvoice.GetParty(txtTcVkn.Text, "TCKN"));
                    ublInvoice.SetCustomerParty(ublInvoice.GetParty(txtTcVkn.Text, "TCKN"));
                    break;
            }

            ublInvoice.SetLegalMonetaryTotal(ublInvoice.CalculateLegalMonetaryTotal());
            ublInvoice.SetTaxTotal(ublInvoice.CalculateTaxTotal());
            ublInvoice.SetAllowanceCharge(ublInvoice.CalculateAllowanceCharges());
            return ublInvoice;
        }

        /// <summary>
        ///  e-Fatura Test Web Servis Adresi
        /// </summary>
        private void linkTestURL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel1.Text);
        }


        /// <summary>
        ///  Foriba Bulut API Adresi
        /// </summary>
        private void linkAPI_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://api.fitbulut.com/");
        }

        private void FrmInvoice_Load(object sender, EventArgs e)
        {
            comboBoxUrlAdres.Items.Add("Foriba Bulut");
            comboBoxUrlAdres.Items.Add("INGeF");
            comboBoxUrlAdres.SelectedIndex = 0;
            UrlModel.SelectedItem = comboBoxUrlAdres.SelectedItem.ToString();
            linkLabel1.Text =
                "https://efaturawstest.fitbulut.com/ClientEInvoiceServices/ClientEInvoiceServicesPort.svc";
        }

        private void comboBoxUrlAdres_SelectedIndexChanged(object sender, EventArgs e)
        {
            UrlModel.SelectedItem = comboBoxUrlAdres.SelectedItem.ToString();
            linkLabel1.Text = UrlModel.SelectedItem != "INGeF" ? "https://efaturawstest.fitbulut.com/ClientEInvoiceServices/ClientEInvoiceServicesPort.svc"
                : "https://ingefservicestest.ingbank.com.tr/ClientEInvoiceServices/ClientEInvoiceServicesPort.svc";

        }

        private void panelControls_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtGonBirim_TextChanged(object sender, EventArgs e)
        {

        }

        private void grdListFatura_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView gridView = sender as DataGridView;
            if (null != gridView)
            {
                foreach (DataGridViewRow r in gridView.Rows)
                {
                    gridView.Rows[r.Index].HeaderCell.Value = (r.Index + 1).ToString();
                }
            }
        }
    }
}
