using Foriba.OE.CLIENT.serviceSmm;
using Foriba.OE.COMMON;
using Foriba.OE.COMMON.Model;
using Foriba.OE.COMMON.WebServices;
using Foriba.OE.COMMON.Zip;
using Foriba.OE.UBL.UBLCreate;
using Foriba.OE.UI.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Foriba.OE.UI
{
    public partial class frmSmm : Form
    {
        public frmSmm()
        {
            InitializeComponent();
        }

        private bool CheckConnParam()
        {
            var check = !(string.IsNullOrEmpty(txtKullanici.Text.Trim()) || string.IsNullOrEmpty(txtSifre.Text.Trim()) || string.IsNullOrEmpty(txtTcknVkn.Text.Trim()) || string.IsNullOrEmpty(txtSube.Text.Trim()));
            return check;
        }

        private bool TextBoxFatura()
        {
            var check = !(string.IsNullOrEmpty(txtFaturaUUID.Text.Trim()) && string.IsNullOrEmpty(txtFaturaID.Text.Trim()));
            return check;

        }

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

        private void comboBoxUrlAdres_SelectedIndexChanged(object sender, EventArgs e)
        {
            UrlModel.SelectedItem = comboBoxUrlAdres.SelectedItem.ToString();
            linkLabel1.Text = UrlModel.SelectedItem != "INGeF" ? "https://earsivwstest.fitbulut.com/ClientESmmServicesPort.svc"
                : "https://earsivtest.ingbank.com.tr/ClientESmmServicesPort.svc";
        }

        private void frmSmm_Load(object sender, EventArgs e)
        {
            comboBoxUrlAdres.Items.Add("Foriba Bulut");
            comboBoxUrlAdres.Items.Add("INGeF");
            comboBoxUrlAdres.SelectedIndex = 0;
            UrlModel.SelectedItem = comboBoxUrlAdres.SelectedItem.ToString();
            linkLabel1.Text = "https://earsivwstest.fitbulut.com/ClientESmmServicesPort.svc";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel1.Text);
        }

        private void btnSmmGonder_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckConnParam())
                    throw new CheckConnParamException(
                        "TCKN/VKN, Gönderici Birim Etiketi, Posta Kutusu Etiketi, WS Kullanıcı Adı ve WS Şifre alanları boş bırakılamaz!");

                var eSmm = new SmmWebService();
                var result = eSmm.ESmmGonder(SetValue(), CheckedSSL());

                MessageBox.Show(result[0].UUID, "Response", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtFaturaUUID.Text = result[0].UUID;
                txtFaturaID.Text = result[0].ID;
            }
            catch (CheckConnParamException ex)
            {
                MessageBox.Show(ex.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (FaultException<ProcessingFault> ex)
            {
                MessageBox.Show(ex.Detail.Code + ": " + ex.Message, "ProcessingFault", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnSmmMakbuzIndir_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckConnParam() || !TextBoxFatura())
                    throw new CheckConnParamException(
                        "TCKN / VKN, Şube , WS Kullanıcı Adı, WS Şifre ve İndirilecek Faturanın UUID veya ID alanları boş olamaz!");

                SmmWebService eSmm = new SmmWebService();
                var result = eSmm.SmmPDFIndir(SetValue(), CheckedSSL());
                var pdf = ZipUtility.UncompressFile(result[0].DocData);
                FolderBrowserDialog fbDialog = new FolderBrowserDialog();
                fbDialog.Description = "Lütfen kaydetmek istediğiniz dizini seçiniz...";
                fbDialog.RootFolder = Environment.SpecialFolder.Desktop;
                if (fbDialog.ShowDialog() == DialogResult.OK)
                {
                    //dialog ile kullanıcıya seçtirilen dizine fatura UUID si ile dosya ismini set ederek kayıt işlemi yapıyoruz.
                    File.WriteAllBytes(fbDialog.SelectedPath + "\\" + txtFaturaUUID.Text + ".pdf", pdf);

                    MessageBox.Show("Serbest Makbuz PDF İndirme Başarılı", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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
    }
}
