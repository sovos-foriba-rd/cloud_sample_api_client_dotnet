



using UblInvoiceObject;
/// <summary>
/// Copyright © 2018 Foriba Teknoloji
/// Bu proje örnek bir web servis test projesidir. Yalnızca test sisteminde çalışmaktadır.
/// Version Info    : Version.txt
/// Readme          : Readme.txt
/// </summary>
namespace Foriba.OE.UBL.UBLCreate
{
    public class InvoiceUBL : BaseInvoiceUBL
    {
        public InvoiceUBL(string profileId, string invoiceTypeCode, string documentCurrencyCode)
            : base(profileId, invoiceTypeCode, documentCurrencyCode)
        {
           // BaseUBL.ID = new IDType { Value = "FIT2018000000001" }; //Fatura ID sistem tarafından otomatik üretilecek ise bu alan kullanılmamalı.
            //AdditionalDocumentReference tagı içerisinde CUST_INV_ID alanı gönderilmelidir.

        }
    }

}

