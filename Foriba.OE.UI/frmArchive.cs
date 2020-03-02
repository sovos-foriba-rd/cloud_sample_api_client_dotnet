
using Foriba.OE.CLIENT.serviceArchive;
using Foriba.OE.COMMON;
using Foriba.OE.COMMON.WebServices;
using Foriba.OE.UBL.UBLCreate;
using Foriba.OE.UI.Exceptions;
using System;
using System.Collections;
using System.IO;
using System.ServiceModel;
using System.Windows.Forms;
using Foriba.OE.COMMON.Model;

/// <summary>
/// Copyright © 2018 Foriba Teknoloji
/// Bu proje örnek bir web servis test projesidir. Yalnızca test sisteminde çalışmaktadır.
/// Version Info    : Version.txt
/// Readme          : Readme.txt
/// </summary>


namespace Foriba.OE.UI
{
    public partial class FrmArchive : Form
    {
      
        public FrmArchive( )
        {
            InitializeComponent();
            
        }


        /// <summary>
        /// Bağlantı için kullanılan alanların dolu olmasını kontrol eder.
        /// </summary>
        /// <returns>True/False</returns>
        private bool CheckConnParam()
        {
            var check = !(string.IsNullOrEmpty(txtKullanici.Text.Trim()) || string.IsNullOrEmpty(txtSifre.Text.Trim()) || string.IsNullOrEmpty(txtTcknVkn.Text.Trim()) || string.IsNullOrEmpty(txtSube.Text.Trim()));
            return check;
        }


        /// <summary>
        /// Faturaların sorgulanması ve indirilebilmesi için Fatura UUID ve ID alanlarının 
        /// dolu olmasını kontrol eder
        /// </summary>
        /// <returns>True/False</returns>
        private bool TextBoxFatura()
        {
            var check = !(string.IsNullOrEmpty(txtFaturaUUID.Text.Trim()) && string.IsNullOrEmpty(txtFaturaID.Text.Trim()));
            return check;

        }


        /// <summary>
        ///  CheckBox dan işaretlenen  SSL/TLS versionlarını listeye ekler.
        /// </summary>
        /// <returns>Listeye eklenen SSL/TLS listesi </returns>
        private ArrayList CheckedSSL()
        {
            var checkSSLList = new ArrayList();
            if (checkboxTLS11.Checked && !checkboxTLS12.Checked)
            {
                checkSSLList.Add(checkboxTLS11.Text.Trim());
            }
            else if (!checkboxTLS11.Checked && checkboxTLS12.Checked)
            {
                checkSSLList.Add(checkboxTLS12.Text.Trim());
            }
            else
            {
                checkSSLList.Add(checkboxTLS11.Text.Trim());
                checkSSLList.Add(checkboxTLS12.Text.Trim());
            }
            return checkSSLList;
        }


        /// <summary>
        /// TextBoxlardan gelen bilgileri model nesnesine ekler
        /// </summary>
        /// <returns>Model nesnesi</returns>
        private TextModel SetValue()
        {
            var model = new TextModel
            {
                VknTckn = txtTcknVkn.Text.Trim(),
                Kullanici = txtKullanici.Text.Trim(),
                Sifre = txtSifre.Text.Trim(),
                Sube = txtSube.Text.Trim(),
                FaturaID = txtFaturaID.Text.Trim(),
                FaturaUUID = txtFaturaUUID.Text.Trim()
            };
            return model;
        }


        /// <summary>
        ///  Oluşturulan e-Arşiv faturanın gönderimini sağlar
        /// </summary>
        /// <returns>E-Arşiv Fatura Gönderme</returns>
        private void btnArsivGonder_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckConnParam())
                    throw new CheckConnParamException(
                        "TCKN/VKN, Gönderici Birim Etiketi, Posta Kutusu Etiketi, WS Kullanıcı Adı ve WS Şifre alanları boş bırakılamaz!");

                var eArsiv = new ArchiveWebService();
                var result = eArsiv.EArsivGonder(SetValue(), GetUBLArchiveData(), CheckedSSL());
                if (result.Result.Result1 == ResultType.SUCCESS)
                {
                    MessageBox.Show(result.Detail, "Response", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtFaturaUUID.Text = result.preCheckSuccessResults[0].UUID;
                    txtFaturaID.Text = result.preCheckSuccessResults[0].InvoiceNumber;

                }
                else
                {
                    MessageBox.Show(result.preCheckErrorResults[0].ErrorDesc, "Response", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (CheckConnParamException ex)
            {
                MessageBox.Show(ex.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (FaultException<processingFaultType> ex)
            {
                MessageBox.Show(ex.Message, "ProcessingFault", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// Gönderilen faturanın (vknTCKN ile Fatura UUID veya ID bilgisi mevcut olan ) UBL(XML) formatında 
        /// kayıt eder
        /// </summary>
        /// <returns>E-Arşiv Fatura UBL Kaydetme</returns>
        private void btnArsivFaturaIndir_Click(object sender, EventArgs e)
        {

            try
            {
                if (!CheckConnParam()|| !TextBoxFatura())
                    throw new CheckConnParamException(
                        "TCKN / VKN, Şube , WS Kullanıcı Adı, WS Şifre ve İndirilecek Faturanın UUID veya ID alanları boş olamaz!");

                ArchiveWebService eArsiv = new ArchiveWebService();
                var result = eArsiv.FaturaUBLIndir(SetValue(), CheckedSSL());
                FolderBrowserDialog fbDialog = new FolderBrowserDialog();
                fbDialog.Description = "Lütfen kaydetmek istediğiniz dizini seçiniz...";
                fbDialog.RootFolder = Environment.SpecialFolder.Desktop;
                if (fbDialog.ShowDialog() == DialogResult.OK)
                {
                    //dialog ile kullanıcıya seçtirilen dizine fatura UUID si ile dosya ismini set ederek kayıt işlemi yapıyoruz.
                    File.WriteAllBytes(fbDialog.SelectedPath + "\\" + txtFaturaUUID.Text + ".xml", result.binaryData);

                    MessageBox.Show("Fatura UBL İndirme Başarılı", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (CheckConnParamException ex)
            {
                MessageBox.Show(ex.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (FaultException<processingFaultType> ex)
            {
                MessageBox.Show(ex.Detail.Text, "ProcessingFault", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// Gönderilen faturanın (vknTCKN ile Fatura UUID veya ID bilgisi mevcut olan ) PDF formatında 
        /// kayıt eder
        /// </summary>
        /// <returns>E-Arşiv Fatura PDF Kaydetme</returns>
        private void btnArsivPDFIndir_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckConnParam() || !TextBoxFatura())
                    throw new CheckConnParamException(
                        "TCKN / VKN, Şube , WS Kullanıcı Adı, WS Şifre ve İndirilecek Faturanın UUID veya ID alanları boş olamaz!");

                ArchiveWebService eArsiv = new ArchiveWebService();
                var result = eArsiv.FaturaPDFIndir(SetValue(), CheckedSSL());
                FolderBrowserDialog fbDialog = new FolderBrowserDialog();
                fbDialog.Description = "Lütfen kaydetmek istediğiniz dizini seçiniz...";
                fbDialog.RootFolder = Environment.SpecialFolder.Desktop;
                if (fbDialog.ShowDialog() == DialogResult.OK)
                {
                    //dialog ile kullanıcıya seçtirilen dizine fatura UUID si ile dosya ismini set ederek kayıt işlemi yapıyoruz.
                    File.WriteAllBytes(fbDialog.SelectedPath + "\\" + txtFaturaUUID.Text + ".pdf", result.binaryData);

                    MessageBox.Show("Fatura PDF İndirme Başarılı", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (CheckConnParamException ex)
            {
                MessageBox.Show(ex.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (FaultException<processingFaultType> ex)
            {
                MessageBox.Show(ex.Detail.Text, "ProcessingFault", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// Gönderilen faturanın (vknTCKN ile Fatura UUID veya ID bilgisi mevcut olan ) HTML formatında 
        /// kayıt eder
        /// </summary>
        /// <returns>E-Arşiv Fatura HTML Kaydetme</returns>
        private void btnArsivHTMLIndir_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckConnParam() || !TextBoxFatura() )
                    throw new CheckConnParamException(
                        "TCKN / VKN, Şube , WS Kullanıcı Adı, WS Şifre ve İndirilecek Faturanın UUID veya ID alanları boş olamaz!");
                ArchiveWebService eArsiv = new ArchiveWebService();
                var result = eArsiv.FaturaHTMLIndir(SetValue(), CheckedSSL());
                FolderBrowserDialog fbDialog = new FolderBrowserDialog();
                fbDialog.Description = "Lütfen kaydetmek istediğiniz dizini seçiniz...";
                fbDialog.RootFolder = Environment.SpecialFolder.Desktop;
                if (fbDialog.ShowDialog() == DialogResult.OK)
                {
                    //dialog ile kullanıcıya seçtirilen dizine fatura UUID si ile dosya ismini set ederek kayıt işlemi yapıyoruz.
                    File.WriteAllBytes(fbDialog.SelectedPath + "\\" + txtFaturaUUID.Text + ".html", result.binaryData);

                    MessageBox.Show("Fatura HTML İndirme Başarılı", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (CheckConnParamException ex)
            {
                MessageBox.Show(ex.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (FaultException<processingFaultType> ex)
            {
                MessageBox.Show(ex.Detail.Text, "ProcessingFault", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        ///  e-Arşiv Fatura UBL'inin alanlarını kalıtım alınan BaseInvoiceUBL sınıfında oluşturulan  
        ///  metodları kullanarak doldurur
        /// </summary>
        /// <returns>OLuşturulan e-Arşiv Fatura UBL'i </returns>
        private BaseInvoiceUBL GetUBLArchiveData()
        {
            //UBL Oluşturma metodları
            BaseInvoiceUBL ublArcive = new ArchiveUBL("EARSIVFATURA", "SATIS", "TRY");
            ublArcive.SetSignature();
            ublArcive.SetCustInvIdDocumentReference();
            ublArcive.SetInvoiceLines(ublArcive.GetInvoiceLines());
            switch (txtTcknVkn.Text.Length)
            {
                case 10:
                    ublArcive.SetSupplierParty(ublArcive.GetParty(txtTcknVkn.Text, "VKN"));
                    ublArcive.SetCustomerParty(ublArcive.GetParty("2222222222", "VKN"));
                    break;
                case 11:
                    ublArcive.SetSupplierParty(ublArcive.GetParty(txtTcknVkn.Text, "TCKN"));
                    ublArcive.SetCustomerParty(ublArcive.GetParty("2222222222", "VKN"));
                    break;
            }
            ublArcive.SetLegalMonetaryTotal(ublArcive.CalculateLegalMonetaryTotal());
            ublArcive.SetTaxTotal(ublArcive.CalculateTaxTotal());
            ublArcive.SetAllowanceCharge(ublArcive.CalculateAllowanceCharges());
            return ublArcive;
        }



        /// <summary>
        ///  Foriba Bulut API Adresi
        /// </summary>
        private void linkAPI_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://api.fitbulut.com/");
        }


        /// <summary>
        ///  e-Arşiv Test Web Servis Adresi
        /// </summary>
        private void linkTestURL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            System.Diagnostics.Process.Start(linkLabel1.Text);
        }

        private void FrmArchive_Load(object sender, EventArgs e)
        {
            comboBoxUrlAdres.Items.Add("Foriba Bulut");
            comboBoxUrlAdres.Items.Add("INGeF");
            comboBoxUrlAdres.SelectedIndex = 0;
            UrlModel.SelectedItem = comboBoxUrlAdres.SelectedItem.ToString();
            linkLabel1.Text = "https://earsivwstest.fitbulut.com/ClientEArsivServicesPort.svc";

        }

        private void comboBoxUrlAdres_SelectedIndexChanged(object sender, EventArgs e)
        {
            UrlModel.SelectedItem = comboBoxUrlAdres.SelectedItem.ToString();
            linkLabel1.Text = UrlModel.SelectedItem != "INGeF" ? "https://earsivwstest.fitbulut.com/ClientEArsivServicesPort.svc"
                : "https://earsivtest.ingbank.com.tr/ClientEArsivServicesPort.svc";

        }
    }
}
