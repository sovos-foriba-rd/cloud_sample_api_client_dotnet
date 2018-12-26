using System;
using System.Collections.Generic;
using System.Xml;
using UblReceiptAdvice;

/// <summary>
/// Copyright © 2018 Foriba Teknoloji
/// Bu proje örnek bir web servis test projesidir. Yalnızca test sisteminde çalışmaktadır.
/// Version Info    : Version.txt
/// Readme          : Readme.txt
/// </summary> 

namespace Foriba.OE.UBL.UBLCreate
{
    public class IrsaliyeYanitiUBL
    {

        /// <summary>
        ///  İrsaliye Yanıtı'nın(Receipt Advice) UBL' ini oluşturma
        /// </summary>
        /// <returns>İrsaliye Yanıtı UBL'i</returns>
        public ReceiptAdviceType CreateReceiptAdvice(string vknTckn, DateTime tarih, string UUID)
        {
            //İrsaliye Yanıtı kalem sayısını random olarak üretme
            Random rnd = new Random();
            int lineNumber = rnd.Next(1, 10);

            ReceiptAdviceType irsaliyeYaniti = SetReceiptHeader(lineNumber, tarih);
            irsaliyeYaniti.OrderReference = SetOrderReference();
            irsaliyeYaniti.DespatchDocumentReference = SetDocumentReference(UUID);
            irsaliyeYaniti.AdditionalDocumentReference = SetAdditionalDocumentReference();
            irsaliyeYaniti.Signature = SetSignature();
            switch (vknTckn.Length)
            {
                case 10:
                    irsaliyeYaniti.DeliveryCustomerParty = SetCustomerParty(vknTckn,"VKN");
                    irsaliyeYaniti.DespatchSupplierParty = SetSupplierParty(vknTckn,"VKN");
                    break;
                case 11:
                    irsaliyeYaniti.DeliveryCustomerParty = SetCustomerParty(vknTckn,"TCKN");
                    irsaliyeYaniti.DespatchSupplierParty = SetSupplierParty(vknTckn,"TCKN");
                    break;
            }

            irsaliyeYaniti.Shipment = SetShipment(lineNumber);
            irsaliyeYaniti.ReceiptLine = SetReceiptLine(lineNumber);

            return irsaliyeYaniti;
        }

        private DocumentReferenceType[] SetAdditionalDocumentReference()
        {
            var additionalDocumentReference = new[]
              {
                  new DocumentReferenceType
                   {
                       ID=new IDType {Value= Guid.NewGuid().ToString() },
                       IssueDate=new IssueDateType {Value=DateTime.Now },
                       DocumentTypeCode=new DocumentTypeCodeType {Value="CUST_DES_ID" }
                   },

                };
            return additionalDocumentReference;
        }


        /// <summary>
        /// İrsaliye Yanıtı (Receipt Advice) UBL'inin header alanlarını oluşturma
        /// </summary>
        /// <returns>UBL'in Header Alanı</returns>
        private ReceiptAdviceType SetReceiptHeader(int lineNumber, DateTime tarih1)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<xml />");

            var irsaliyeYaniti = new ReceiptAdviceType
            {
                UBLExtensions = new[]   //UBL Dijital İmza Düğümü
                {
                    new UBLExtensionType
                    {
                        ExtensionContent=doc.DocumentElement
                    }
                },
                UBLVersionID = new UBLVersionIDType { Value = "2.1" },  //uluslararası fatura standardı 2.1
                CustomizationID = new CustomizationIDType { Value = "TR1.2.1" },  // GİB UBLTR olarak isimlendirdiği Türkiye'ye özgü 1.2.1 eİrsaliye formatını kullanıyor.
                ProfileID = new ProfileIDType { Value = "TEMELIRSALIYE" },  //Kullanılan Senaryo
                ID = new IDType { Value = "GIB2018000000001" },  // İrsaliye Yanıtına Ait Numara 
                CopyIndicator = new CopyIndicatorType { Value = false },  //İrsaliye Yanıtının Asıl veya Suret Bilgisi 
                UUID = new UUIDType { Value = Guid.NewGuid().ToString() },  //İrsaliye Yanıtının Evrensel Tekliğini Sağlayan Numara
                IssueDate = new IssueDateType { Value = tarih1 },  //İrsaliye Yanıtının Düzenleme Tarihi 
                IssueTime = new IssueTimeType { Value = default(DateTime).AddHours(11).AddMinutes(20) },  //İrsaliye Yanıtının Düzenleme Zamanı
                ReceiptAdviceTypeCode = new ReceiptAdviceTypeCodeType { Value = "SEVK" },  //İrsaliye Yanıtı Tipi Kodu 
                Note = new[]  //İrsaliye Yanıtı İle İlgili Genel Açıklamalar 
                  {
                    new NoteType {Value="Ürünler geç teslim edildi.." },
                   
                  },
                LineCountNumeric = new LineCountNumericType { Value = lineNumber },  //İrsaliye Yanıtı Kalem Sayısı 

            };
            return irsaliyeYaniti;

        }


        /// <summary>
        ///  İrsaliye Yanıtı (Receipt Advice) UBL'inin OrderReference alanını oluşturma.Birden 
        /// fazla OrderReference alanı oluşturulabilir.
        /// </summary>
        /// <returns>OrderReference Alanı</returns>
        private OrderReferenceType[] SetOrderReference()
        {
            var orderReference = new[]  //Sipariş Bilgileri 
                 {
                    new OrderReferenceType
                    {
                        ID = new IDType { Value = Guid.NewGuid().ToString() },
                        IssueDate = new IssueDateType { Value = DateTime.Now
                     },

                    }

                 };
            return orderReference;
        }


        /// <summary>
        ///  İrsaliye Yanıtı (Receipt Advice) UBL'inin  DespatchDocumentReference alanını oluşturma.
        /// Birden fazla DocumentReference alanı oluşturulabilir.
        /// </summary>
        /// <returns>DespatchDocumentReference Alanı</returns>
        private DocumentReferenceType SetDocumentReference(string UUID)
        {
            var despatchDocumentReference = new DocumentReferenceType  //İrsaliye Bilgileri 
            {
                ID = new IDType { Value = UUID },
                IssueDate = new IssueDateType
                {
                    Value = DateTime.Now
                },
            };
            return despatchDocumentReference;
        }


        /// <summary>
        ///  İrsaliye Yanıtı(Receipt Advice) UBL'inin Signature alanını oluşturma.
        /// </summary>
        /// <returns>Signature Alanı</returns>
        private SignatureType[] SetSignature()
        {
            var signature = new[]   //Elektronik Mali Mühür ve/veya Elektronik İmza ile Bunlara Ait Sertifika Bilgileri
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
        ///  İrsaliye Yanıtı(Receipt Advice) UBL'inin DeliveryCustomerParty alanını oluşturma.
        /// </summary>
        /// <returns>DeliveryCustomerParty Alanı</returns>
        private CustomerPartyType SetCustomerParty(string vknTckn, string parametre)
        {
            var deliveryCustomerParty = new CustomerPartyType //İrsaliye Yanıtındaki Malları Teslim Alan Tarafın Bilgileri 
            {
                Party = new PartyType()
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
                    // İrsaliye Yanıtındaki Malları Teslim Alan Tarafın VKN si yazılırsa bu  alan kullanılmak zorundadır.
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
                    // İrsaliye Yanıtındaki Malları Teslim Alan Tarafın TCKN si yazılırsa bu alan kullanılmak zorundadır.
                    Person = new PersonType
                    {
                        FirstName = new FirstNameType { Value = "İsim" },
                        FamilyName = new FamilyNameType { Value = "Soyisim" },
                    }
                }
            };

            return deliveryCustomerParty;

        }


        /// <summary>
        ///  İrsaliye Yanıtı(Receipt Advice) UBL'inin DespatchSupplierParty alanını oluşturma.
        /// </summary>
        /// <returns>DespatchSupplierParty Alanı</returns>
        private SupplierPartyType SetSupplierParty(string vknTckn, string parametre)
        {
            var despatchSupplierParty = new SupplierPartyType  //İrsaliye Yanıtındaki Malların Sevkiyatını Sağlayan Tarafın Bilgileri 
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
                    // İrsaliye Yanıtındaki Malların Sevkiyatını Sağlayan tarafın VKN si yazılırsa bu alan kullanılmak zorundadır.
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
                    // İrsaliye Yanıtındaki Malların Sevkiyatını Sağlayan tarafın TCKN si yazılırsa bu alan kullanılmak zorundadır.
                    Person = new PersonType
                    {
                        FirstName = new FirstNameType { Value = "İsim" },
                        FamilyName = new FamilyNameType { Value = "Soyisim" },
                    }
                }


            };
            return despatchSupplierParty;

        }


        /// <summary>
        ///  İrsaliye Yanıtı(Receipt Advice) UBL'inin Shipment alanını oluşturma.
        /// </summary>
        /// <returns>Shipment Alanı</returns>
        private ShipmentType SetShipment(int kalemSayisi)
        {
            var shipment = new ShipmentType   // Gönderi Hakkındaki Bilgiler
            {
                ID = new IDType { Value = "" },  // Kargo numarası girilir.
                GoodsItem = new [] { new GoodsItemType { ValueAmount = new ValueAmountType { currencyID = "TRY", Value = 20000 * kalemSayisi } } },
                Delivery = new DeliveryType
                {
                    ActualDeliveryDate = new ActualDeliveryDateType  // Gerçekleşen gönderim tarihi girilir.(Fiili Sevk Tarihi)
                    {
                        Value = DateTime.Now
                    },
                    ActualDeliveryTime = new ActualDeliveryTimeType { Value = DateTime.Now } //Gerçekleşen gönderim zamanı girilir.(Fiili Sevk Zamanı)
                }
            };
            return shipment;
        }


        /// <summary>
        ///  İrsaliye Yanıtı(Receipt Advice) UBL'inin ReceiptLine alanını oluşturma.
        /// Birden fazla ReceiptLine alanı oluşturulabilir.
        /// </summary>
        /// <returns>ReceiptLine Listesi</returns>
        private ReceiptLineType[] SetReceiptLine(int satirSayisi)
        {
            var list = new List<ReceiptLineType>();

            for (int i = 1; i <= satirSayisi; i++)
            {
                ReceiptLineType receiptLine = new ReceiptLineType()  //Teslim Alınan Mal Kalemleri 
                {
                    ID = new IDType { Value = i.ToString() }, //Kalem numarası girilir.
                    ReceivedQuantity = new ReceivedQuantityType { unitCode = "C62", Value = 10 },// Teslim alınan mal adedi girilir. 
                    OrderLineReference = new OrderLineReferenceType  //İlgili irsaliye dokümanı kalemi girilir.
                    {
                        LineID = new LineIDType { Value = i.ToString() } // Kalem numarası girilir
                    },
                    DespatchLineReference = new LineReferenceType //İlgili irsaliye dokümanı kalemi girilir.
                    {
                        LineID = new LineIDType { Value = i.ToString() } // Kalem numarası girilir
                    },
                    Item = new ItemType  // Teslim alınan mal bilgisi girilir.
                    {
                        Name = new NameType1 { Value = "Notebook Bilgisayar" }, //Mal/hizmet adı serbest metin olarak girilir.
                        SellersItemIdentification = new ItemIdentificationType  // Satıcının mal/hizmete verdiği tanımlama bilgisi girilir.
                        {
                            ID = new IDType { Value = "PNC1234" }
                        }
                    },
                    Shipment = new []
                    {
                        new ShipmentType
                        {
                            ID= new IDType {Value="" },
                            GoodsItem=new []
                            {
                                new GoodsItemType
                                {
                                    InvoiceLine=new []
                                    {
                                       new InvoiceLineType
                                       {
                                            ID= new IDType {Value="" },
                                            InvoicedQuantity=new InvoicedQuantityType {Value=0 },
                                            LineExtensionAmount=new LineExtensionAmountType { currencyID="TRY", Value=10*2000 },
                                            Item=new ItemType
                                            {
                                                Name=new NameType1 {Value="Notebook Bilgisayar" }
                                            },
                                            Price=new PriceType
                                            {
                                                PriceAmount=new PriceAmountType {currencyID="TRY",Value=2000 }
                                            }
                                       }
                                    }
                                }
                            }

                        }
                    }
                };

                list.Add(receiptLine);
            }

            return list.ToArray();


        }
    }
}
