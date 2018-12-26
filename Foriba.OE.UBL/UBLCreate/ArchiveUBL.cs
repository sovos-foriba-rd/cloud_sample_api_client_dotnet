using System;
using UblInvoiceObject;

/// <summary>
/// Copyright © 2018 Foriba Teknoloji
/// Bu proje örnek bir web servis test projesidir. Yalnızca test sisteminde çalışmaktadır.
/// Version Info    : Version.txt
/// Readme          : Readme.txt
/// </summary>

namespace Foriba.OE.UBL.UBLCreate
{
    public class ArchiveUBL : BaseInvoiceUBL
    {


        public ArchiveUBL(string profileId, string invoiceTypeCode, string documentCurrencyCode)
            : base(profileId, invoiceTypeCode, documentCurrencyCode)
        {
            BaseUBL.IssueTime = new IssueTimeType { Value = default(DateTime).AddHours(11).AddMinutes(20) };
            BaseUBL.ID = new IDType { Value = "FIT2018000000001" };

            ArsivUBLOlusturma();

        }

        /// <summary>
        /// e-Arşiv UBL de fatura ye ek olarak eklenecek alanların eklenmesi
        /// </summary>
        private void ArsivUBLOlusturma()
        {

            PutAdditionalDocumentReference(new DocumentReferenceType
            {
                ID = new IDType { Value = "0100" },//Email Gonder
                IssueDate = BaseUBL.IssueDate,
                DocumentTypeCode = new DocumentTypeCodeType { Value = "OUTPUT_TYPE" }
            });

            PutAdditionalDocumentReference(new DocumentReferenceType  //efatura dan farklı olarak sadece bu alan eklenmiştir. 
            {
                ID = new IDType { Value = "KAGIT" },
                IssueDate = BaseUBL.IssueDate,
                DocumentTypeCode = new DocumentTypeCodeType { Value = "EREPSENDT" }
            });

            PutAdditionalDocumentReference(new DocumentReferenceType
            {
                ID = new IDType { Value = "99" },
                IssueDate = BaseUBL.IssueDate,
                DocumentTypeCode = new DocumentTypeCodeType { Value = "TRANSPORT_TYPE" }
            });

        }

    }
}
