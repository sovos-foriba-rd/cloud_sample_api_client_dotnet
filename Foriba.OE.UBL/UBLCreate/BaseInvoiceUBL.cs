using System;
using System.Collections.Generic;
using System.Xml;
using UblInvoiceObject;


/// <summary>
/// Copyright © 2018 Foriba Teknoloji
/// Bu proje örnek bir web servis test projesidir. Yalnızca test sisteminde çalışmaktadır.
/// Version Info    : Version.txt
/// Readme          : Readme.txt
/// </summary>

namespace Foriba.OE.UBL.UBLCreate
{
    public abstract class BaseInvoiceUBL
    {
        public InvoiceType BaseUBL { get; protected set; }
        public List<DocumentReferenceType> DocRefList { get; set; }
        protected BaseInvoiceUBL(string profileId, string invoiceTypeCode, string documentCurrencyCode, string faturaId = null)
        {
            DocRefList = new List<DocumentReferenceType>();
            BaseUBL = new InvoiceType { IssueDate = new IssueDateType { Value = DateTime.Now } };
            CreateInvoiceHeader(profileId, invoiceTypeCode, documentCurrencyCode, faturaId);

        }

        /// <summary>
        /// Fatura header alanlarını ekleme
        /// </summary>
        private void CreateInvoiceHeader(string profileId, string invoiceTypeCode, string documentCurrencyCode, string faturaId = null)
        {

            var doc = new XmlDocument();
            doc.LoadXml("<xml />");

            BaseUBL.UBLExtensions = new[]
                 {
                    new UBLExtensionType
                    {
                        ExtensionContent = doc.DocumentElement
                    }
                };

            BaseUBL.UBLVersionID = new UBLVersionIDType { Value = "2.1" }; //uluslararası fatura standardı 2.1
            BaseUBL.CustomizationID = new CustomizationIDType { Value = "TR1.2" }; //fakat GİB UBLTR olarak isimlendirdiği Türkiye'ye özgü 1.2 efatura formatını kullanıyor.
            BaseUBL.InvoiceTypeCode = new InvoiceTypeCodeType { Value = invoiceTypeCode };
            BaseUBL.DocumentCurrencyCode = new DocumentCurrencyCodeType { Value = documentCurrencyCode };
            BaseUBL.ProfileID = new ProfileIDType { Value = profileId };
            BaseUBL.CopyIndicator = new CopyIndicatorType { Value = false };
            BaseUBL.UUID = new UUIDType { Value = Guid.NewGuid().ToString() };
            BaseUBL.ID = faturaId != null || faturaId != "" ? new IDType { Value = faturaId } : null;  // ID = new IDType { Value = "GIB2018000000001" },
        }

        /// <summary>
        /// Fatura AdditionalDocumentReference alanlarını ekleme
        /// </summary>
        public void PutAdditionalDocumentReference(DocumentReferenceType documentRef)
        {
            DocRefList.Clear();
            if (BaseUBL.AdditionalDocumentReference == null)
                BaseUBL.AdditionalDocumentReference = new DocumentReferenceType[0];

            DocRefList.AddRange(BaseUBL.AdditionalDocumentReference);

            documentRef.IssueDate = BaseUBL.IssueDate;

            DocRefList.Add(documentRef);

            BaseUBL.AdditionalDocumentReference = DocRefList.ToArray();
        }

        /// <summary>
        ///  Fatura ID otomatik oluşacak ise bu alanı göndermelisiniz.
        /// </summary>
        public void SetCustInvIdDocumentReference()
        {

            var idRef = new DocumentReferenceType()
            {
                ID = new IDType { Value = Guid.NewGuid().ToString() },

                IssueDate = BaseUBL.IssueDate,

                DocumentTypeCode = new DocumentTypeCodeType { Value = "CUST_INV_ID" }
            };


            DocRefList.Add(idRef);
            BaseUBL.AdditionalDocumentReference = DocRefList.ToArray();
        }

        /// <summary>
        /// Fatura imza düğümü
        /// </summary>
        public void SetSignature()
        {
            var signature = new[]
            {
               new SignatureType
               {
                ID = new IDType { schemeID = "VKN_TCKN", Value = "3880718497" },
                SignatoryParty = new PartyType
                {
                    WebsiteURI = new WebsiteURIType { Value = "www.FITsolutions.com.tr" },
                    PartyIdentification = new[]
                    {
                            new PartyIdentificationType
                           {
                              ID = new IDType { schemeID = "VKN", Value = "3880718497" }
                           }
                    },
                    PartyName = new PartyNameType
                    {
                        Name = new NameType1 { Value = "FIT DANIŞMANLIK VE TEKNOLOJİ BİLİŞİM HİZMETLERİ A.Ş." }
                    },

                    PostalAddress = new AddressType
                    {
                        Room = new RoomType { Value = "34" },
                        StreetName = new StreetNameType { Value = "Öz Sokak" },
                        BuildingName = new BuildingNameType { Value = "Gold Plaza" },
                        BuildingNumber = new BuildingNumberType { Value = "19" },
                        CitySubdivisionName = new CitySubdivisionNameType { Value = "Altayçeşme Mahellesi" },
                        CityName = new CityNameType { Value = "İSTANBUL" },
                        PostalZone = new PostalZoneType { Value = "34843" },
                        Region = new RegionType { Value = "Marmara" },
                        Country = new CountryType { Name = new NameType1 { Value = "TÜRKİYE" } }
                    },
                    Contact = new ContactType
                    {
                        ElectronicMail = new ElectronicMailType { Value = "muhasebe@FITcons.com" },
                        Telefax = new TelefaxType { Value = "0(216) 445 92 87" },
                        Telephone = new TelephoneType { Value = "0(216) 445 93 79" }
                    }
                },

                DigitalSignatureAttachment = new AttachmentType
                {
                    ExternalReference = new ExternalReferenceType
                    {
                        URI = new URIType { Value = "#Signature" }
                    }
                }
                 }
              };
            BaseUBL.Signature = signature;
        }


        /// <summary>
        /// Fatura gönderici bilgileri
        /// </summary>
        public virtual void SetSupplierParty(PartyType supplierParty)
        {
            var accountingSupplierParty = new SupplierPartyType //göndericinin fatura üzerindeki bilgileri
            {
                Party = supplierParty
            };

            BaseUBL.AccountingSupplierParty = accountingSupplierParty;

        }

        /// <summary>
        /// Fatura alıcı bilgileri
        /// </summary>
        public virtual void SetCustomerParty(PartyType customerParty)
        {
            var accountingCustomerParty = new CustomerPartyType //Alıcının fatura üzerindeki bilgileri
            {
                Party = customerParty
            };

            BaseUBL.AccountingCustomerParty = accountingCustomerParty;

        }


        /// <summary>
        /// fatura gönderici ve alıcı bilgilerinin doldurulması
        /// </summary>
        public PartyType GetParty(string vknTckn, string parametre)
        {
            return new PartyType
            {
                WebsiteURI = new WebsiteURIType { Value = "web sitesi" },


                PartyIdentification = new[]
                {
                   new PartyIdentificationType { ID = new IDType { schemeID = parametre, Value = vknTckn } }
                },

                PartyName = new PartyNameType { Name = new NameType1 { Value = "asdasd" } },

                PostalAddress = new AddressType
                {
                    Room = new RoomType { Value = "kapi no" },
                    StreetName = new StreetNameType { Value = "cadde" },
                    BuildingName = new BuildingNameType { Value = "bina" },
                    BuildingNumber = new BuildingNumberType { Value = "bina no" },
                    CitySubdivisionName = new CitySubdivisionNameType { Value = "mahalle" },
                    CityName = new CityNameType { Value = "sehir" },
                    PostalZone = new PostalZoneType { Value = "posta kodu" },
                    Region = new RegionType { Value = "asdasd" },
                    Country = new CountryType { Name = new NameType1 { Value = "ülke" } }
                },

                PartyTaxScheme = new PartyTaxSchemeType
                {
                    TaxScheme = new TaxSchemeType { Name = new NameType1 { Value = "vergi dairesi" } }
                },

                Contact = new ContactType
                {
                    Telephone = new TelephoneType { Value = "telefon" },
                    Telefax = new TelefaxType { Value = "faks" },
                    ElectronicMail = new ElectronicMailType { Value = "mail" }
                },
                Person = new PersonType
                {
                    FirstName = new FirstNameType { Value = "İsim" },
                    FamilyName = new FamilyNameType { Value = "Soyisim" },
                }
            };
        }


        /// <summary>
        /// Ödenecek fiyat
        /// </summary>
        public virtual void SetAllowanceCharge(AllowanceChargeType[] allowenceCharges)
        {
            BaseUBL.AllowanceCharge = allowenceCharges;
        }


        /// <summary>
        /// Ödenecek fiyatın hesaplanması
        /// </summary>
        public virtual AllowanceChargeType[] CalculateAllowanceCharges()
        {
            AllowanceChargeType allowanceCharge = new AllowanceChargeType
            {
                Amount = new AmountType2 { Value = 0 },
                BaseAmount = new BaseAmountType { Value = 0 },
                ChargeIndicator = new ChargeIndicatorType { Value = false },

            };
            foreach (var item in BaseUBL.InvoiceLine)
            {
                foreach (var iskonto in item.AllowanceCharge)
                {
                    allowanceCharge.BaseAmount.currencyID = iskonto.Amount.currencyID;
                    allowanceCharge.Amount.currencyID = iskonto.Amount.currencyID;
                    allowanceCharge.Amount.Value += iskonto.Amount.Value;
                    allowanceCharge.BaseAmount.Value += iskonto.BaseAmount.Value;
                }
            }

            return new[] { allowanceCharge };
        }



        /// <summary>
        /// Toplam verginin hesaplanması
        /// </summary>
        public virtual TaxTotalType[] CalculateTaxTotal()
        {
            List<TaxTotalType> taxTotalList = new List<TaxTotalType>();
            List<TaxSubtotalType> taxSubTotalList = new List<TaxSubtotalType>();

            TaxTotalType taxTotal = new TaxTotalType { TaxAmount = new TaxAmountType { Value = 0 } };

            var taxSubtotal = new TaxSubtotalType
            {
                TaxableAmount = new TaxableAmountType { Value = 0 },
                TaxAmount = new TaxAmountType { Value = 0 },
                Percent = new PercentType1 { Value = 0 },
                TaxCategory = new TaxCategoryType
                {
                    TaxScheme = new TaxSchemeType
                    {
                        Name = new NameType1 { Value = "KDV" },
                        TaxTypeCode = new TaxTypeCodeType
                        {
                            Value = "0015"

                        }
                    }
                }
            };

            foreach (var item in BaseUBL.InvoiceLine)
            {
                taxTotal.TaxAmount.Value += item.TaxTotal.TaxAmount.Value;
                taxTotal.TaxAmount.currencyID = item.TaxTotal.TaxAmount.currencyID;

                foreach (var tax in item.TaxTotal.TaxSubtotal)
                {
                    taxSubtotal.TaxableAmount.Value += tax.TaxableAmount.Value;
                    taxSubtotal.TaxableAmount.currencyID = tax.TaxableAmount.currencyID;

                    taxSubtotal.TaxAmount.Value += item.TaxTotal.TaxAmount.Value;
                    taxSubtotal.TaxAmount.currencyID = tax.TaxAmount.currencyID;

                    taxSubtotal.Percent.Value = tax.Percent.Value;

                }
            }
            taxSubTotalList.Add(taxSubtotal);
            taxTotal.TaxSubtotal = taxSubTotalList.ToArray();
            taxTotalList.Add(taxTotal);
            return taxTotalList.ToArray();
        }



        /// <summary>
        /// Toplam vergi değeri
        /// </summary>
        public virtual void SetTaxTotal(TaxTotalType[] taxTotal)
        {
            BaseUBL.TaxTotal = taxTotal;
        }



        /// <summary>
        /// Toplam parasal değerlerin hesaplanması
        /// </summary>
        public virtual MonetaryTotalType CalculateLegalMonetaryTotal()
        {
            MonetaryTotalType legalMonetaryTotal = new MonetaryTotalType
            {
                LineExtensionAmount = new LineExtensionAmountType { Value = 0 },

                TaxExclusiveAmount = new TaxExclusiveAmountType { Value = 0 },

                TaxInclusiveAmount = new TaxInclusiveAmountType { Value = 0 },

                AllowanceTotalAmount = new AllowanceTotalAmountType { Value = 0 },

                PayableAmount = new PayableAmountType { Value = 0 }
            };

            foreach (var line in BaseUBL.InvoiceLine)
            {


                foreach (var allowance in line.AllowanceCharge)
                {
                    legalMonetaryTotal.AllowanceTotalAmount.currencyID = allowance.Amount.currencyID;
                    legalMonetaryTotal.AllowanceTotalAmount.Value += allowance.Amount.Value;
                    legalMonetaryTotal.TaxInclusiveAmount.currencyID = line.LineExtensionAmount.currencyID;

                    legalMonetaryTotal.TaxInclusiveAmount.Value += line.LineExtensionAmount.Value -
                        allowance.Amount.Value + line.TaxTotal.TaxAmount.Value;
                }

                legalMonetaryTotal.LineExtensionAmount.currencyID = line.LineExtensionAmount.currencyID;
                legalMonetaryTotal.LineExtensionAmount.Value += line.LineExtensionAmount.Value;


                legalMonetaryTotal.PayableAmount.currencyID = line.LineExtensionAmount.currencyID;
                legalMonetaryTotal.PayableAmount.Value = legalMonetaryTotal.TaxInclusiveAmount.Value;

                foreach (var tax in line.TaxTotal.TaxSubtotal)
                {

                    legalMonetaryTotal.TaxExclusiveAmount.currencyID = tax.TaxableAmount.currencyID;
                    legalMonetaryTotal.TaxExclusiveAmount.Value += tax.TaxableAmount.Value;


                }

            }

            /* Opsiyonel : Dip toplam değerlerinin virgülden sonra 2 haneye yuvarlanması */
            legalMonetaryTotal.LineExtensionAmount.Value = Math.Round(legalMonetaryTotal.LineExtensionAmount.Value, 2);
            legalMonetaryTotal.TaxExclusiveAmount.Value = Math.Round(legalMonetaryTotal.TaxExclusiveAmount.Value, 2);
            legalMonetaryTotal.TaxInclusiveAmount.Value = Math.Round(legalMonetaryTotal.TaxInclusiveAmount.Value, 2);
            legalMonetaryTotal.AllowanceTotalAmount.Value = Math.Round(legalMonetaryTotal.AllowanceTotalAmount.Value, 2);
            legalMonetaryTotal.PayableAmount.Value = Math.Round(legalMonetaryTotal.PayableAmount.Value, 2);

            return legalMonetaryTotal;
        }


        /// <summary>
        /// Toplam parasal değerler
        /// </summary>
        public virtual void SetLegalMonetaryTotal(MonetaryTotalType legalMonetoryTotal)
        {
            BaseUBL.LegalMonetaryTotal = legalMonetoryTotal;
        }



        /// <summary>
        /// fatura kalem bilgileri
        /// </summary>
        public virtual void SetInvoiceLines(InvoiceLineType[] invoiceLines)
        {
            BaseUBL.InvoiceLine = invoiceLines;
            BaseUBL.LineCountNumeric = new LineCountNumericType { Value = invoiceLines.Length };
        }


        /// <summary>
        /// fatura kalem bilgilerinin doldurulması
        /// </summary>
        public InvoiceLineType[] GetInvoiceLines()
        {
            Random rnd = new Random();
            int lineCount = rnd.Next(1, 10);

            List<InvoiceLineType> list = new List<InvoiceLineType>();

            for (int i = 1; i <= lineCount; i++)
            {
                #region invoiceLine
                InvoiceLineType invoiceLine = new InvoiceLineType
                {

                    ID = new IDType { Value = i.ToString() },
                    InvoicedQuantity = new InvoicedQuantityType { unitCode = "C62", Value = 10 },
                    LineExtensionAmount = new LineExtensionAmountType { currencyID = "TRY", Value = 20.00M },

                    AllowanceCharge = new[]
                        {
                            new AllowanceChargeType
                            {
                                ChargeIndicator = new ChargeIndicatorType { Value = false },
                                MultiplierFactorNumeric = new MultiplierFactorNumericType { Value = 0.05M },

                                Amount = new AmountType2
                                {
                                    currencyID = "TRY",
                                    Value = 1.00M
                                },

                                BaseAmount = new BaseAmountType
                                {
                                    currencyID = "TRY",
                                    Value = 20
                                }
                            }
                        },

                    TaxTotal = new TaxTotalType
                    {
                        TaxAmount = new TaxAmountType
                        {
                            currencyID = "TRY",
                            Value = 0.19M
                        },

                        TaxSubtotal = new[]
                            {
                                new TaxSubtotalType
                                {
                                    TaxableAmount = new TaxableAmountType
                                    {
                                        currencyID = "TRY",
                                        Value = 19.00M
                                    },

                                    TaxAmount = new TaxAmountType
                                    {
                                        currencyID = "TRY",
                                        Value = 0.19M
                                    },
                                    CalculationSequenceNumeric=new CalculationSequenceNumericType
                                    {
                                        Value=1
                                    },

                                    Percent = new PercentType1 { Value = 1},

                                    TaxCategory = new TaxCategoryType
                                    {
                                        TaxScheme = new TaxSchemeType
                                        {
                                            Name = new NameType1 { Value = "KDV" },
                                            TaxTypeCode = new TaxTypeCodeType { Value = "0015" }
                                        }
                                    }

                                }
                            }
                    },

                    Item = new ItemType
                    {
                        Name = new NameType1 { Value = "Kalem" }
                    },

                    Price = new PriceType
                    {
                        PriceAmount = new PriceAmountType
                        {
                            currencyID = "TRY",
                            Value = 2.00M
                        }
                    }
                };
                #endregion

                list.Add(invoiceLine);
            }

            return list.ToArray();

        }

    }
}
