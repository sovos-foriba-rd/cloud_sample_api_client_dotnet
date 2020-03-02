using Foriba.OE.UBL.UBLObject.MmObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Foriba.OE.UBL.UBLCreate
{
    public class MmUBL
    {
        CreditNoteType eMm;
        public CreditNoteType CreateCreditNote(string vknTckn, DateTime tarih1)
        {
            
            Random rnd = new Random();
            int lineNumber = rnd.Next(1, 10);

            eMm= GetCreditHeader(lineNumber, tarih1);
            eMm.AdditionalDocumentReference = GetDocumentReference();
            eMm.Signature = GetSignature();
            switch (vknTckn.Length)
            {
                case 10:
                    eMm.AccountingSupplierParty = GetSupplierParty(vknTckn, "VKN");
                    eMm.AccountingCustomerParty = GetCustomerParty(vknTckn, "VKN");
                    break;
                case 11:
                    eMm.AccountingSupplierParty = GetSupplierParty(vknTckn, "TCKN");
                    eMm.AccountingCustomerParty = GetCustomerParty(vknTckn, "TCKN");
                    break;
            }

            
            eMm.Delivery = GetDelivery();
            eMm.CreditNoteLine = GetCreditNoteLine(lineNumber);
            eMm.LegalMonetaryTotal = LegalMonetaryTotal();
            return eMm;
        }
        private CreditNoteType GetCreditHeader(int lineNumber, DateTime tarih1)
        {

            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<xml />");
            var eMM = new CreditNoteType
            {
                UBLExtensions = new[]  //UBL Dijital İmza Düğümü
                {
                    new UBLExtensionType
                    {
                        ExtensionContent=doc.DocumentElement
                    }
                },
                UBLVersionID = new UBLVersionIDType { Value = "2.1" },  
                CustomizationID = new CustomizationIDType { Value = "TR1.2.1" }, 
                ProfileID = new ProfileIDType { Value = "EARSIVBELGE" }, 
                ID =new IDType { Value = "FM02020000000001" }, //Eğer Sovos Foriba tarafından otomatik üretilmeyecekse Fatura ID generate edilecek şekilde düzenlenmelidir.            
                CopyIndicator = new CopyIndicatorType { Value = false },  
                UUID = new UUIDType { Value = Guid.NewGuid().ToString() },  
                IssueDate = new IssueDateType { Value = tarih1 },  
                IssueTime = new IssueTimeType { Value = default(DateTime).AddHours(11).AddMinutes(20) },
                CreditNoteTypeCode = new CreditNoteTypeCodeType { Value = "MUSTAHSILMAKBUZ" }, 
                DocumentCurrencyCode=new DocumentCurrencyCodeType { Value="TRY"},
                Note = new[]  
                {
                    new NoteType {Value="MÜSTAHSİL MAKBUZU" }

                },
                LineCountNumeric = new LineCountNumericType { Value = lineNumber }  

            };
            return eMM;
        }




        /// <summary>
        ///  Mühtahsil Makbuz (Credit Note) UBL'inin AdditionalDocumentReference alanını oluşturma.
        /// Birden fazla DocumentReference alanı oluşturulabilir.
        /// </summary>
        /// <returns>AdditionalDocumentReference Alanı</returns>
        private DocumentReferenceType[] GetDocumentReference()
        {
            var additionalDocumentReference = new[]  //Mühtahsil ile ilgili Diğer Dokümanlara Ait Bilgiler 
             {
                 //Fatura ID otomatik oluşacak ise bu alanı göndermelisiniz.
                  new DocumentReferenceType
                   {
                       ID=new IDType {Value= Guid.NewGuid().ToString() },
                       IssueDate=new IssueDateType {Value=DateTime.Now },
                       DocumentTypeCode=new DocumentTypeCodeType {Value="CUST_MM_ID" }
                   }
                             };
            return additionalDocumentReference;
        }


        /// <summary>
        ///  Mühtahsil Makbuz (Credit Note) UBL'inin Signature alanını oluşturma
        /// </summary>
        /// <returns>Signature alanı</returns>
        private SignatureType[] GetSignature()
        {

            var signature = new[]  //Elektronik Mali Mühür ve/veya Elektronik İmza ile Bunlara Ait Sertifika Bilgileri
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
                        PartyName=new PartyNameType
                        {
                            Name=new NameType1 {Value="FIT DANIŞMANLIK VE TEKNOLOJİ BİLİŞİM HİZMETLERİ A.Ş." }
                        },

                       PostalAddress = new AddressType
                        {
                            Room=new RoomType {Value="34" },
                            StreetName = new StreetNameType { Value = "Öz Sokak" },
                            BuildingName = new BuildingNameType { Value = "Gold Plaza" },
                            BuildingNumber=new BuildingNumberType {Value="19" },
                            CitySubdivisionName = new CitySubdivisionNameType { Value = "Altayçeşme Mahellesi" },
                            CityName = new CityNameType { Value = "İSTANBUL" },
                            PostalZone = new PostalZoneType { Value = "34843" },
                            Region=new RegionType {Value="Marmara" },
                            Country = new CountryType { Name = new NameType1 { Value = "TÜRKİYE" } }
                        },
                       Contact=new ContactType
                       {
                           ElectronicMail=new ElectronicMailType {Value="muhasebe@FITcons.com" },
                           Telefax=new TelefaxType {Value="0(216) 445 92 87" },
                           Telephone=new TelephoneType {Value="0(216) 445 93 79" }
                       }
                    },

                    DigitalSignatureAttachment = new AttachmentType
                    {
                        ExternalReference = new ExternalReferenceType
                        {
                            URI = new URIType { Value = "#Signature" }
                        }
                    }
                },
              };
            return signature;
        }


        /// <summary>
        ///  Mühtahsil Makbuz (Credit Note) UBL'inin DespatchSupplierParty alanını oluşturma
        /// </summary>
        /// <returns>DespatchSupplierParty Alanı</returns>
        private SupplierPartyType GetSupplierParty(string vknTckn, string parametre)
        {
            var mmSupplierParty = new SupplierPartyType  
            {
                Party = new PartyType
                {
                    WebsiteURI = new WebsiteURIType { Value = "web sitesi" },
                    PartyIdentification = new[]
                   {
                        new PartyIdentificationType
                        {
                            ID= new IDType {schemeID=parametre,Value=vknTckn }
                        }
                   },
                   
                    PartyName = new PartyNameType
                    {
                        Name = new NameType1 { Value = "Kurum Adı" }
                    },
                    PostalAddress = new AddressType
                    {
                        ID = new IDType { Value = "123" },
                        StreetName = new StreetNameType { Value = "Cadde" },
                        BuildingName = new BuildingNameType { Value = "Bina" },
                        BuildingNumber = new BuildingNumberType { Value = "Bina No" },
                        CitySubdivisionName = new CitySubdivisionNameType { Value = "İlçe/Semt" },
                        CityName = new CityNameType { Value = "Sehir" },
                        PostalZone = new PostalZoneType { Value = "Posta Kodu" },
                        Country = new CountryType { Name = new NameType1 { Value = "Ülke" } }
                    },
                    PhysicalLocation = new LocationType1
                    {
                        ID = new IDType { Value = "Depo Şube" },
                        Address = new AddressType
                        {
                            ID = new IDType { Value = "12345" },
                            StreetName = new StreetNameType { Value = "Cadde" },
                            BuildingName = new BuildingNameType { Value = "Bina" },
                            BuildingNumber = new BuildingNumberType { Value = "Bina no" },
                            CitySubdivisionName = new CitySubdivisionNameType { Value = "İlçe/Semt" },
                            CityName = new CityNameType { Value = "Sehir" },
                            PostalZone = new PostalZoneType { Value = "Posta Kodu" },
                            Country = new CountryType
                            {
                                Name = new NameType1 { Value = "Ülke" }
                            }
                        }
                    },
                    PartyTaxScheme = new PartyTaxSchemeType
                    {
                        TaxScheme = new TaxSchemeType { Name = new NameType1 { Value = "Vergi Dairesi" } }
                    },
                    Contact = new ContactType
                    {
                        Telephone = new TelephoneType { Value = "Telefon" },
                        Telefax = new TelefaxType { Value = "Fax" },
                        ElectronicMail = new ElectronicMailType { Value = "Mail" }
                    },
                   
                    Person = new PersonType
                    {
                        FirstName = new FirstNameType { Value = "İsim" },
                        FamilyName = new FamilyNameType { Value = "Soyisim" },
                    }
                }

            };
            return mmSupplierParty;

        }


        /// <summary>
        ///  Mühtahsil Makbuz (Credit Note) UBL'inin DeliveryCustomerParty alanını oluşturma
        /// </summary>
        /// <returns>DeliveryCustomerParty Alanı</returns>
        private CustomerPartyType GetCustomerParty(string vknTckn, string parametre)
        {
            var mmCustomerParty = new CustomerPartyType  
            {
                Party = new PartyType
                {
                    WebsiteURI = new WebsiteURIType { Value = "web sitesi" },
                    PartyIdentification = new[]
                  {
                        new PartyIdentificationType
                        {
                            ID= new IDType {schemeID=parametre,Value=vknTckn }
                        },
                        new PartyIdentificationType
                        {
                            ID=new IDType {schemeID="MUSTERINO",Value="1123" }
                        }
                  },
                 
                    PartyName = new PartyNameType
                    {
                        Name = new NameType1 { Value = "Kurum Adı" }
                    },
                    PostalAddress = new AddressType
                    {
                        ID = new IDType { Value = "123" },
                        StreetName = new StreetNameType { Value = "Cadde" },
                        BuildingName = new BuildingNameType { Value = "Bina" },
                        BuildingNumber = new BuildingNumberType { Value = "Bina No" },
                        CitySubdivisionName = new CitySubdivisionNameType { Value = "İlçe/Semt" },
                        CityName = new CityNameType { Value = "Şehir" },
                        PostalZone = new PostalZoneType { Value = "Posta Kodu" },
                        Country = new CountryType { Name = new NameType1 { Value = "Ülke" } }
                    },
                    PhysicalLocation = new LocationType1
                    {
                        ID = new IDType { Value = "Depo Şube" },
                        Address = new AddressType
                        {
                            ID = new IDType { Value = "111" },
                            StreetName = new StreetNameType { Value = "Cadde" },
                            BuildingName = new BuildingNameType { Value = "Bina" },
                            BuildingNumber = new BuildingNumberType { Value = "Bina no" },
                            CitySubdivisionName = new CitySubdivisionNameType { Value = "İlçe/Semt" },
                            CityName = new CityNameType { Value = "Sehir" },
                            PostalZone = new PostalZoneType { Value = "Posta Kodu" },
                            Country = new CountryType
                            {
                                Name = new NameType1 { Value = "Ülke" }
                            }
                        }
                    },
                    PartyTaxScheme = new PartyTaxSchemeType
                    {
                        TaxScheme = new TaxSchemeType { Name = new NameType1 { Value = "Vergi Dairesi" } }
                    },
                    Contact = new ContactType
                    {
                        Telephone = new TelephoneType { Value = "Telefon" },
                        Telefax = new TelefaxType { Value = "Fax" },
                        ElectronicMail = new ElectronicMailType { Value = "Mail" }
                    },
                 
                    Person = new PersonType
                    {
                        FirstName = new FirstNameType { Value = "İsim" },
                        FamilyName = new FamilyNameType { Value = "Soyisim" },
                    }
                }
            };
            return mmCustomerParty;

        }


        /// <summary>
        ///  Mühtahsil Makbuz (Credit Note) UBL'inin Delivery alanını oluşturma
        /// </summary>
        /// <returns>Shipment Alanı</returns>
        private DeliveryType[] GetDelivery()
        {
            var mmDelivery = new DeliveryType[] { new DeliveryType { ActualDeliveryDate = new ActualDeliveryDateType { Value = DateTime.Now } } };

            return mmDelivery;

        }

        public virtual MonetaryTotalType LegalMonetaryTotal()
        {
            MonetaryTotalType legalMonetaryTotal = new MonetaryTotalType
            {
                LineExtensionAmount = new LineExtensionAmountType { Value = 0 },

                TaxExclusiveAmount = new TaxExclusiveAmountType { Value = 0 },

                TaxInclusiveAmount = new TaxInclusiveAmountType { Value = 0 },

                AllowanceTotalAmount = new AllowanceTotalAmountType { Value = 0 },

                PayableAmount = new PayableAmountType { Value = 0 }
            };

            foreach (var line in eMm.CreditNoteLine)
            {


                foreach (var allowance in line.AllowanceCharge)
                {
                    legalMonetaryTotal.AllowanceTotalAmount.currencyID = allowance.Amount.currencyID;
                    legalMonetaryTotal.AllowanceTotalAmount.Value += allowance.Amount.Value;
                    legalMonetaryTotal.TaxInclusiveAmount.currencyID = line.LineExtensionAmount.currencyID;

                    legalMonetaryTotal.TaxInclusiveAmount.Value += line.LineExtensionAmount.Value -
                        allowance.Amount.Value + line.TaxTotal[0].TaxAmount.Value;
                }

                legalMonetaryTotal.LineExtensionAmount.currencyID = line.LineExtensionAmount.currencyID;
                legalMonetaryTotal.LineExtensionAmount.Value += line.LineExtensionAmount.Value;


                legalMonetaryTotal.PayableAmount.currencyID = line.LineExtensionAmount.currencyID;
                legalMonetaryTotal.PayableAmount.Value = legalMonetaryTotal.TaxInclusiveAmount.Value;

                foreach (var tax in line.TaxTotal[0].TaxSubtotal)
                {

                    legalMonetaryTotal.TaxExclusiveAmount.currencyID = tax.TaxableAmount.currencyID;
                    legalMonetaryTotal.TaxExclusiveAmount.Value += tax.TaxableAmount.Value;


                }

            }
            return legalMonetaryTotal;
        }
        /// <summary>
        ///  Mühtahsil Makbuz (Credit Note) UBL'inin DespatchLine alanlarını oluşturma.Birden fazla 
        /// CreditNoteLine alanı oluşturulabilir.
        /// </summary>
        /// <returns>DespatchLine Listesi</returns>
        private CreditNoteLineType[] GetCreditNoteLine(int kalemSayisi)
        {


            List<CreditNoteLineType> list = new List<CreditNoteLineType>();

            for (int i = 1; i <= kalemSayisi; i++)
            {

                CreditNoteLineType creditNoteLine = new CreditNoteLineType() 
                {

                    ID = new IDType { Value = i.ToString() },  
                    Note = new[] { new NoteType { Value = "" } },   
                    CreditedQuantity = new CreditedQuantityType { unitCode = "C62", Value = 10 }, 
                    Price=new PriceType { PriceAmount=new PriceAmountType { currencyID="TRY",Value=3M}},
                    Delivery =new DeliveryType[] {},
                    LineExtensionAmount=new LineExtensionAmountType { currencyID="TRY",Value=30m },
                    DeliveryTerms=new DeliveryTermsType[] { },
                    PaymentTerms = new PaymentTermsType[]{  },
                    TaxTotal=new TaxTotalType[] { new TaxTotalType {TaxAmount = new TaxAmountType
                        {
                            currencyID = "TRY",
                            Value = 0.30M
                        },

                        TaxSubtotal = new[]
                            {
                                new TaxSubtotalType
                                {
                                    TaxableAmount = new TaxableAmountType
                                    {
                                        currencyID = "TRY",
                                        Value = 30.00M
                                    },

                                    TaxAmount = new TaxAmountType
                                    {
                                        currencyID = "TRY",
                                        Value = 0.30M
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
                    } },
                    AllowanceCharge=new AllowanceChargeType[] { new AllowanceChargeType
                            {
                                ChargeIndicator = new ChargeIndicatorType { Value = false },
                                MultiplierFactorNumeric = new MultiplierFactorNumericType { Value = 0 },

                                Amount = new AmountType2
                                {
                                    currencyID = "TRY",
                                    Value = 0.00M
                                },

                                BaseAmount = new BaseAmountType
                                {
                                    currencyID = "TRY",
                                    Value = 0
                                }
                            } },
                    
                    Item = new ItemType  
                    {
                        Name = new NameType1 { Value = "ELMA TOHUMU" }, 
                        SellersItemIdentification = new ItemIdentificationType
                        {
                            ID = new IDType { Value = "APP1234" } 

                        }
                    }
                };

                list.Add(creditNoteLine);
            }

            return list.ToArray();

        }
    }
}
