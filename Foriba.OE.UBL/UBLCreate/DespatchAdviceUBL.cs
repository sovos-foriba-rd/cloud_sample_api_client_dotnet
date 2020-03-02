using System;
using System.Collections.Generic;
using System.Xml;
using UblDespatchAdvice;


/// <summary>
/// Copyright © 2018 Foriba Teknoloji
/// Bu proje örnek bir web servis test projesidir. Yalnızca test sisteminde çalışmaktadır..
/// Version Info    : Version.txt
/// Readme          : Readme.txt
/// </summary> 


namespace Foriba.OE.UBL.UBLObject
{
    public class DespatchAdvice
    {

        /// <summary>
        ///  Sevk irsaliyesi'nin(Despatch Advice) UBL' ini oluşturma
        /// </summary>
        /// <returns>Sevk İrsaliyesi UBL'i</returns>
        public DespatchAdviceType CreateDespactAdvice(string vknTckn, DateTime tarih1)
        {
            //Sevk irsaliyesi kalem sayısını random olarak üretme
            Random rnd = new Random();
            int lineNumber = rnd.Next(1, 10);

            DespatchAdviceType eIrsaliye = GetDespatchHeader(lineNumber, tarih1);
            eIrsaliye.OrderReference = GetOrderReference();
            eIrsaliye.AdditionalDocumentReference = GetDocumentReference();
            eIrsaliye.Signature = GetSignature();
            switch (vknTckn.Length)
            {
                case 10:
                    eIrsaliye.DespatchSupplierParty = GetSupplierParty(vknTckn, "VKN");
                    eIrsaliye.DeliveryCustomerParty = GetCustomerParty(vknTckn, "VKN");
                    break;
                case 11:
                    eIrsaliye.DespatchSupplierParty = GetSupplierParty(vknTckn, "TCKN");
                    eIrsaliye.DeliveryCustomerParty = GetCustomerParty(vknTckn, "TCKN");
                    break;
            }

            eIrsaliye.Shipment = GetShipment(lineNumber);
            eIrsaliye.DespatchLine = GetDespatchLine(lineNumber);
            return eIrsaliye;
        }


        /// <summary>
        ///  Sevk irsaliyesi (Despatch Advice) UBL'inin ilk alanlarını oluşturma
        /// </summary>
        /// <returns>UBL'in Alanları</returns>
        private DespatchAdviceType GetDespatchHeader(int lineNumber, DateTime tarih1)
        {

            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<xml />");
            var eIrsaliye = new DespatchAdviceType
            {
                UBLExtensions = new[]  //UBL Dijital İmza Düğümü
                {
                    new UBLExtensionType
                    {
                        ExtensionContent=doc.DocumentElement
                    }
                },
                UBLVersionID = new UBLVersionIDType { Value = "2.1" },  // uluslararası fatura standardı 2.1
                CustomizationID = new CustomizationIDType { Value = "TR1.2.1" }, // GİB UBLTR olarak isimlendirdiği Türkiye'ye özgü 1.2.1 eİrsaliye formatını kullanıyor.
                ProfileID = new ProfileIDType { Value = "TEMELIRSALIYE" }, //Kullanılan Senaryo
                                                                           // ID = new IDType { Value = "GIB2018000000001" },   //Sevk irsaliyesine Ait Numara  
                CopyIndicator = new CopyIndicatorType { Value = false }, // Sevk İrsaliyesinin Asıl veya Suret Bilgisi 
                UUID = new UUIDType { Value = Guid.NewGuid().ToString() },  //Sevk İrsaliyesinin Evrensel Tekliğini Sağlayan Numara 
                IssueDate = new IssueDateType { Value = tarih1 },  //Sevk İrsaliyesinin Düzenleme Tarihi
                IssueTime = new IssueTimeType { Value = default(DateTime).AddHours(11).AddMinutes(20) }, //Sevk İrsaliyesinin Düzenleme Zamanı 
                DespatchAdviceTypeCode = new DespatchAdviceTypeCodeType { Value = "SEVK" }, //Sevk İrsaliyesinin Tip Kodu 

                Note = new[]  //Sevk İrsaliyesi İle İlgili Genel Açıklamalar 
                {
                    new NoteType {Value="Mallar teslim edildi.." }

                },
                LineCountNumeric = new LineCountNumericType { Value = lineNumber }  //Sevk İrsaliyesi Kalem Sayısı  

            };
            return eIrsaliye;
        }

        /// <summary>
        ///  Sevk irsaliyesi (Despatch Advice) UBL'inin OrderReference alanını oluşturma.Birden 
        /// fazla OrderReference alanı oluşturulabilir.
        /// </summary>
        /// <returns>OrderReference Alanı</returns>
        private OrderReferenceType[] GetOrderReference()
        {
            var orderReference = new[]  //Sipariş Bilgileri 
                 {
                    new OrderReferenceType
                    {
                        ID=new IDType {Value="GIB2018000000033" },
                        IssueDate=new IssueDateType {Value=DateTime.Now },
                    }

                 };
            return orderReference;

        }


        /// <summary>
        ///  Sevk irsaliyesi (Despatch Advice) UBL'inin AdditionalDocumentReference alanını oluşturma.
        /// Birden fazla DocumentReference alanı oluşturulabilir.
        /// </summary>
        /// <returns>AdditionalDocumentReference Alanı</returns>
        private DocumentReferenceType[] GetDocumentReference()
        {
            var additionalDocumentReference = new[]  //İrsaliye ile ilgili Diğer Dokümanlara Ait Bilgiler 
             {
                 //Fatura ID otomatik oluşacak ise bu alanı göndermelisiniz.
                  new DocumentReferenceType
                   {
                       ID=new IDType {Value= Guid.NewGuid().ToString() },
                       IssueDate=new IssueDateType {Value=DateTime.Now },
                       DocumentTypeCode=new DocumentTypeCodeType {Value="CUST_DES_ID" }
                   },

                new DocumentReferenceType
               {
                    ID = new IDType { Value = Guid.NewGuid().ToString() },
                    IssueDate = new IssueDateType { Value = DateTime.Now },
                    DocumentType = new DocumentTypeType { Value = "Katalog" },
                    DocumentDescription = new[]
                    {
                       new DocumentDescriptionType
                       {
                           Value= "Katalog Belgesi"
                       }
                    }
                },

                 new DocumentReferenceType
                 {
                    ID = new IDType { Value = Guid.NewGuid().ToString() },
                    IssueDate = new IssueDateType { Value = DateTime.Now },
                    DocumentType = new DocumentTypeType { Value = "Kontrat" },
                    DocumentDescription = new[]
                    {
                       new DocumentDescriptionType
                       {
                           Value= "Kontrat Belgesi"
                       }
                    }
                }


             };
            return additionalDocumentReference;
        }


        /// <summary>
        ///  Sevk irsaliyesi (Despatch Advice) UBL'inin Signature alanını oluşturma
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
        ///  Sevk irsaliyesi (Despatch Advice) UBL'inin DespatchSupplierParty alanını oluşturma
        /// </summary>
        /// <returns>DespatchSupplierParty Alanı</returns>
        private SupplierPartyType GetSupplierParty(string vknTckn, string parametre)
        {
            var despatchSupplierParty = new SupplierPartyType  //Sevk İrsaliyesindeki Malların Sevkiyatını Sağlayan Tarafın Bilgileri
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
                    // Sevkiyatı sağlayan tarafın VKN si yazılırsa bu alan kullanılmak zorundadır.
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
                    // Sevkiyatı sağlayan tarafın TCKN si yazılırsa bu alan kullanılmak zorundadır.
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
        ///  Sevk irsaliyesi (Despatch Advice) UBL'inin DeliveryCustomerParty alanını oluşturma
        /// </summary>
        /// <returns>DeliveryCustomerParty Alanı</returns>
        private CustomerPartyType GetCustomerParty(string vknTckn, string parametre)
        {
            var deliveryCustomerParty = new CustomerPartyType  //Sevk İrsaliyesindeki Malların Sevkiyatını Teslim Alan Tarafın Bilgileri
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
                    // Sevkiyatı alan tarafın VKN si yazılırsa bu alan kullanılmak zorundadır.
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
                    // Sevkiyatı alan tarafın TCKN si yazılırsa bu alan kullanılmak zorundadır.
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
        ///  Sevk irsaliyesi (Despatch Advice) UBL'inin Shipment alanını oluşturma
        /// </summary>
        /// <returns>Shipment Alanı</returns>
        private ShipmentType GetShipment(int kalemSayisi)
        {
            var shipment = new ShipmentType //Gönderi Hakkındaki Bilgiler
            {
                ID = new IDType { Value = "" },  // Kargo numarası girilir.
                GoodsItem = new[] { new GoodsItemType { ValueAmount = new ValueAmountType { currencyID = "TRY", Value = 20000 * kalemSayisi } } },
                ShipmentStage = new []  // Gönderinin hangi aşamada olduğu bilgisi girilir. Ayrıca taşıyıcı (plaka, şoför) gibi detay bilgiler girilir.
                   {
                    new ShipmentStageType
                    {
                        TransportMeans=new TransportMeansType { RoadTransport=new RoadTransportType //Taşımada kullanılan vasıta hakkında bilgi girilir
                         {
                            LicensePlateID =new LicensePlateIDType {schemeID="PLAKA", Value="06DR4077" } // Plaka numarası girilir. 
                         }
                        },

                     DriverPerson =new[]  // Şoför bilgileri girilir
                        {
                            new PersonType
                            {
                                FirstName=new FirstNameType {Value="Mehmet" },
                                FamilyName=new FamilyNameType {Value="Öztürk" },
                                Title=new TitleType {Value="Şoför" },
                                NationalityID=new NationalityIDType {Value="14922266699" }
                            },
                            new PersonType
                            {
                                FirstName=new FirstNameType {Value="Mustafa" },
                                FamilyName=new FamilyNameType {Value="Öztürk" },
                                Title=new TitleType {Value="Şoför" },
                                NationalityID=new NationalityIDType {Value="14922266600" }
                            }
                        }
                }
               },
                Delivery = new DeliveryType
                {
                    CarrierParty = new PartyType
                    {
                        PartyIdentification = new[]
                           {
                            new PartyIdentificationType
                            {
                                ID=new IDType { schemeID="VKN", Value="1234567801" }
                            }
                           },
                        PartyName = new PartyNameType
                        {
                            Name = new NameType1 { Value = "Kurum Adı" }
                        },
                        PostalAddress = new AddressType
                        {
                            CitySubdivisionName = new CitySubdivisionNameType { Value = "İlçe/Semt" },
                            CityName = new CityNameType { Value = "Şehir" },
                            Country = new CountryType { Name = new NameType1 { Value = "Ülke" } }
                        }

                    },
                    Despatch = new DespatchType
                    {
                        ActualDespatchDate = new ActualDespatchDateType { Value = DateTime.Now }, //Gerçekleşen gönderim tarihi girilir. (Fiili Sevk Tarihi) 
                        ActualDespatchTime = new ActualDespatchTimeType { Value = DateTime.Now } //Gerçekleşen gönderim zamanı girilir. (Fiili Sevk Zamanı) 
                    }
                },
                TransportHandlingUnit = new[]  //Taşıma üniteleri bilgisi girilir
                   {
                    new TransportHandlingUnitType
                    {
                        TransportEquipment=new[]
                        {

                            new TransportEquipmentType
                            {
                                ID=new IDType {schemeID="DORSEPLAKA",Value="06DR4088" }
                            },
                             new TransportEquipmentType
                            {
                                ID=new IDType {schemeID="DORSEPLAKA",Value="06DR4099" }
                            }
                         }
                     }
                      }
            };
            return shipment;

        }


        /// <summary>
        ///  Sevk irsaliyesi (Despatch Advice) UBL'inin DespatchLine alanlarını oluşturma.Birden fazla 
        /// DespatchLine alanı oluşturulabilir.
        /// </summary>
        /// <returns>DespatchLine Listesi</returns>
        private DespatchLineType[] GetDespatchLine(int kalemSayisi)
        {


            List<DespatchLineType> list = new List<DespatchLineType>();

            for (int i = 1; i <= kalemSayisi; i++)
            {

                DespatchLineType despatchLine = new DespatchLineType()  //Sevk İrsaliyesindeki Kalemlerin Bilgileri
                {

                    ID = new IDType { Value = i.ToString() },  //İrsaliye kalemi numarası girilir. 
                    Note = new[] { new NoteType { Value = "" } },  // Kalem ile ilgili açıklama girilir. 
                    DeliveredQuantity = new DeliveredQuantityType { unitCode = "C62", Value = 10 },  // Gönderimi gerçekleştirilen mal adedi girilir.
                    OrderLineReference = new OrderLineReferenceType { LineID = new LineIDType { Value = i.ToString() } }, //Siparişin kalemlerine referans atmak için kullanılır. 
                    Item = new ItemType  //Mal / hizmet bilgisi girilir.
                    {
                        Name = new NameType1 { Value = "Notebook Bilgisayar" }, //Mal/hizmet adı serbest metin olarak girilir
                        SellersItemIdentification = new ItemIdentificationType
                        {
                            ID = new IDType { Value = "PNC1234" } //Satıcının mal/hizmete verdiği tanımlama bilgisi girilir.

                        }
                    },
                    Shipment = new[]
                    {
                        new ShipmentType
                        {
                            ID= new IDType {Value="" },
                            GoodsItem=new[]
                            {
                                new GoodsItemType
                                {
                                    InvoiceLine=new[]
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


                list.Add(despatchLine);
            }

            return list.ToArray();

        }



    }
}
