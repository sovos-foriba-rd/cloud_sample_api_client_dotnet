using System;
using System.Windows.Forms;
using Foriba.OE.COMMON;
using Foriba.OE.COMMON.Model;

/// <summary>
/// Copyright © 2018 Foriba Teknoloji
/// Bu proje örnek bir web servis test projesidir. Yalnızca test sisteminde çalışmaktadır.
/// Version Info    : Version.txt
/// Readme          : Readme.txt
/// </summary>

namespace Foriba.OE.UI
{
    public partial class FrmMain : System.Windows.Forms.Form
    {

        protected FrmInvoice FaturaForm = new FrmInvoice();
        protected FrmArchive ArsivForm = new FrmArchive();
        protected FrmDespatch IrsaliyeForm = new FrmDespatch();

        /// <summary>
        /// Temel Form 
        /// </summary>
        public FrmMain()
        {
            InitializeComponent();
        }



        /// <summary>
        /// e-Fatura ekranını getirir
        /// </summary>
        private void btnFatura_Click(object sender, EventArgs e)
        {
            if (FaturaForm == null || FaturaForm.IsDisposed)
            {
                FaturaForm = new FrmInvoice();
                FaturaForm.Show();
            }
            else
            {
                FaturaForm.Show();
            }


        }


        /// <summary>
        /// e-Arşiv ekranını getirir
        /// </summary>
        private void btnArsiv_Click(object sender, EventArgs e)
        {
            if (ArsivForm == null || ArsivForm.IsDisposed)
            {
                ArsivForm = new FrmArchive();
                ArsivForm.Show();
            }
            else
            {
                ArsivForm.Show();
            }

        }


        /// <summary>
        /// e-İrsaliye ekranını getirir
        /// </summary>
        private void btnIrsaliye_Click(object sender, EventArgs e)
        {
            if (IrsaliyeForm == null || IrsaliyeForm.IsDisposed)
            {
                IrsaliyeForm = new FrmDespatch();
                IrsaliyeForm.Show();
            }
            else
            {
                IrsaliyeForm.Show();
            }
        }

        private void lblAPI_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://api.fitbulut.com/");
        }

     
    }
}


