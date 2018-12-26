using AppResObject;
using System;


/// <summary>
/// Copyright © 2018 Foriba Teknoloji
/// Bu proje örnek bir web servis test projesidir. Yalnızca test sisteminde çalışmaktadır.
/// Version Info    : Version.txt
/// Readme          : Readme.txt
/// </summary> 

namespace Foriba.OE.UBL.UBLCreate
{
    public class ApplicationResponseUBL
    {
        /// <summary>
        ///  e-Fatura Uygulama Yanıtı UBL' ini oluşturma
        /// </summary>
        /// <returns>e-Fatura UBL'i</returns>
        public ApplicationResponseType CreateApplicationResponse(string vknTckn, string gelenfaturaId, DateTime issueDate)
        {

            ApplicationResponseType uygulamaYaniti = GetInvoiceHeader(issueDate);
            switch (vknTckn.Length)
            {
                case 10:
                    uygulamaYaniti.SenderParty = GetSender(vknTckn,"VKN");
                    uygulamaYaniti.ReceiverParty = GetReceiver(vknTckn,"VKN");
                    break;
                case 11:
                    uygulamaYaniti.SenderParty = GetSender(vknTckn,"TCKN");
                    uygulamaYaniti.ReceiverParty = GetReceiver(vknTckn,"TCKN");
                    break;
            }

            uygulamaYaniti.DocumentResponse = GetDocumentResponse( gelenfaturaId, issueDate);

            return uygulamaYaniti;
        }
        

        /// <summary>
        /// Uygulama yanıtı Header alanları
        /// </summary>
        /// <returns>Uygulama yanıtı Header alanları</returns>
        private ApplicationResponseType GetInvoiceHeader(DateTime issueDate)
        {
            var uygulamaYaniti = new ApplicationResponseType
            {
                UBLVersionID = new UBLVersionIDType { Value = "2.1" },
                CustomizationID = new CustomizationIDType { Value = "TR1.2" },
                ProfileID = new ProfileIDType { Value = "TICARIFATURA" },
                ID = new IDType { Value = Guid.NewGuid().ToString() },
                UUID = new UUIDType { Value = Guid.NewGuid().ToString() },

                IssueDate = new IssueDateType
                {
                    Value = issueDate
                }
            };
            return uygulamaYaniti;

        }


        /// <summary>
        ///  Uygulama Yanıtı gönderici bilgileri 
        /// </summary>
        /// <returns>Gönderici bilgileri </returns>
        private PartyType GetSender(string vknTckn,string parametre)
        {
            var senderParty = new PartyType
            {
                PartyIdentification = new[]
                    {
                        new PartyIdentificationType
                        {
                            ID = new IDType { schemeID=parametre, Value = vknTckn }
                        }
                    },

                PostalAddress = new AddressType
                {
                    CitySubdivisionName = new CitySubdivisionNameType { Value = "Maltepe" },
                    CityName = new CityNameType { Value = "İstanbul" },
                    Country = new CountryType { Name = new NameType1 { Value = "Türkiye" } }
                },

                PartyName = new PartyNameType
                {
                    Name = new NameType1 { Value = "FIT Solutions" }
                },
                Person = new PersonType
                {
                    FirstName = new FirstNameType { Value = "isim" },
                    FamilyName = new FamilyNameType { Value = "soyisim" }

                }
            };

            return senderParty;

        }


        /// <summary>
        ///  Uygulama Yanıtı alıcı bilgileri 
        /// </summary>
        /// <returns>Alıcı bilgileri </returns>
        private PartyType GetReceiver(string vknTckn, string parametre)
        {
            var receiverParty = new PartyType
            {
                PartyIdentification = new[]
                     {
                        new PartyIdentificationType
                        {
                            ID = new IDType { schemeID=parametre, Value = vknTckn },
                        }
                     },

                PostalAddress = new AddressType
                {
                    CitySubdivisionName = new CitySubdivisionNameType { Value = "Maltepe" },
                    CityName = new CityNameType { Value = "İstanbul" },
                    Country = new CountryType { Name = new NameType1 { Value = "Türkiye" } }
                },

                PartyName = new PartyNameType
                {
                    Name = new NameType1 { Value = "FIT Solutions" }
                },
                Person = new PersonType
                {
                    FirstName = new FirstNameType { Value = "isim" },
                    FamilyName = new FamilyNameType { Value = "soyisim" }

                }
            };
            return receiverParty;
        }


        /// <summary>
        /// Uygulama Yanıtının bilgileri ve hangi faturaya gönderidiği bilgisi
        /// </summary>
        /// <returns>Uygulama Yanıtının bilgileri</returns>
        private DocumentResponseType[] GetDocumentResponse( string gelenfaturaId, DateTime issueDate)
        {
            var documentResponse = new[]
                {
                    new DocumentResponseType
                    {
                        Response = new ResponseType
                        {
                            ReferenceID = new ReferenceIDType { Value = "12345" },
                            ResponseCode = new ResponseCodeType { Value = "KABUL" } //gönderilen uygulama yanıtının türü. KABUL veya RED
                        },

                        DocumentReference = new DocumentReferenceType
                        {
                            //uygulama yanıtı gönderilecek faturanın  bilgisi
                            ID = new IDType { Value = gelenfaturaId },
                            DocumentType = new DocumentTypeType { Value = "FATURA" },
                            DocumentTypeCode = new DocumentTypeCodeType { Value = "FATURA" },

                            IssueDate = new IssueDateType
                            {
                                Value = issueDate
                            },
                        }
                    }
                };
            return documentResponse;
        }





    }
}
