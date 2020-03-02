
using System;
using System.Collections;
using System.Net;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Web;
using Foriba.OE.CLIENT.serviceArchive;
using Foriba.OE.COMMON.UBLSerializer;
using Foriba.OE.UBL.UBLCreate;
using Foriba.OE.COMMON.Zip;
using System.Text;
using Foriba.OE.COMMON.Model;

/// <summary>
/// Copyright © 2018 Foriba Teknoloji
/// Bu proje örnek bir web servis test projesidir. Yalnızca test sisteminde çalışmaktadır.
/// Version Info    : Version.txt
/// Readme          : Readme.txt
/// </summary>

namespace Foriba.OE.COMMON.WebServices
{
    public class ArchiveWebService
    {
        protected eArsivInvoicePortTypeClient WsClient = new eArsivInvoicePortTypeClient();

        protected void WebServisAdresDegistir()
        {
            if (UrlModel.SelectedItem != "Foriba Bulut")
            {
                WsClient.Endpoint.Address = new EndpointAddress("https://earsivtest.ingbank.com.tr/ClientEArsivServicesPort.svc");
            }
        }

        /// <summary>
        ///  Zipli faturanın hash bilgisini alır
        /// </summary>
        /// <returns>Zipli Faturanın Hash Bilgisi</returns>
        public string GetHashInfo(byte[] ziplifatura)
        {
            using (var md5 = MD5.Create())
            {
                byte[] aa = md5.ComputeHash(ziplifatura);
                var hash = BitConverter.ToString(aa).Replace("-", "").ToLowerInvariant();
                return hash;
            }
        }


        /// <summary>
        /// Sisteme e-Arşiv fatura gönderir
        /// </summary>
        /// <returns>Sisteme gönderilen e-Arşiv faturanın bilgileri ve binary PDF datası</returns>
        public sendInvoiceResponseType EArsivGonder(TextModel m, BaseInvoiceUBL arsiv, ArrayList sslList)
        {

            var createdUBL = arsiv.BaseUBL;  // e-Arşiv fatura UBL i oluşturulur
            UBLBaseSerializer serializer = new InvoiceSerializer();  // UBL  XML e dönüştürülür
            var strFatura = serializer.GetXmlAsString(createdUBL); // XML byte tipinden string tipine dönüştürülür
            byte[] zipliFile = ZipUtility.CompressFile(Encoding.UTF8.GetBytes(strFatura), createdUBL.UUID.Value); // XML  ziplenir
            string hash = GetHashInfo(zipliFile); // zipli dosyanın hash bilgisi alınır
            ServicePointManager.SecurityProtocol = TlsSetting.TlsSet(sslList);  // TLS/SSL ayarları
            WebServisAdresDegistir();
            using (new OperationContextScope(WsClient.InnerChannel))
            {
                if (WebOperationContext.Current != null)
                    WebOperationContext.Current.OutgoingRequest.Headers.Add(HttpRequestHeader.Authorization,
                        Authorization.GetAuthorization(m.Kullanici, m.Sifre));

                var req = new sendInvoiceRequest
                {
                    sendInvoiceRequestType = new SendInvoiceRequestType
                    {
                        senderID = m.VknTckn, //gönderici VKN-TCKN
                        receiverID = "2222222222", //alıcı VKN-TCKN
                        fileName = createdUBL.UUID.Value, //dosya ismi
                        binaryData = zipliFile, //gönderilecek fatura
                        docType = "XML", //dosya tipi 
                        hash = hash, //dosyanın hash bilgisi
                        customizationParams = new[]
                        {
                            new CustomizationParam
                            {
                                paramName = "BRANCH", //parametre ismi
                                paramValue = m.Sube //şube adı opsiyoneldir. Gönderilmez ise varsayılan olarak "default" şube setlenir.
                            }
                        }
                      

                    }
                };
               
                return WsClient.sendInvoice(req.sendInvoiceRequestType);


            }


        }


        
        /// <summary>
        /// Gönderilen faturayı UBL olarak kayıt eder
        /// </summary>
        /// <returns>Gönderilen faturanın binary UBL datası </returns>
        public getSignedInvoiceResponseType FaturaUBLIndir(TextModel m, ArrayList sslList)
        {
            ServicePointManager.SecurityProtocol = TlsSetting.TlsSet(sslList); // TLS/SSL ayarları
            WebServisAdresDegistir();
            using (new OperationContextScope(WsClient.InnerChannel))
            {
                if (WebOperationContext.Current != null)
                    WebOperationContext.Current.OutgoingRequest.Headers.Add(HttpRequestHeader.Authorization,
                        Authorization.GetAuthorization(m.Kullanici, m.Sifre));
                var req = new getSignedInvoiceRequestType //UBL datası istenen e-arşiv faturasının request parametrelerini set ediyoruz.
                {
                    vkn = m.VknTckn, // vkn veya tckn
                    UUID = m.FaturaUUID, // UBL datası alınacak faturanın UUID si
                    invoiceNumber = m.FaturaID   // UBL datası alınacak faturanın ID si
                };
                return WsClient.getSignedInvoice(req);

               
            }


        }


        /// <summary>
        /// Gönderilen faturayı PDF olarak kayıt eder
        /// </summary>
        /// <returns>Gönderilen faturanın binary PDF datası </returns>
        public getInvoiceDocumentResponseType FaturaPDFIndir(TextModel m, ArrayList sslList)
        {

            ServicePointManager.SecurityProtocol = TlsSetting.TlsSet(sslList); // TLS/SSL ayarları
            WebServisAdresDegistir();
            using (new OperationContextScope(WsClient.InnerChannel))
            {
                if (WebOperationContext.Current != null)
                    WebOperationContext.Current.OutgoingRequest.Headers.Add(HttpRequestHeader.Authorization,
                        Authorization.GetAuthorization(m.Kullanici, m.Sifre));
                var req = new getInvoiceDocumentRequestType //PDF görüntüsü istenen e-arşiv faturasının request parametrelerini set ediyoruz.
                {
                    vkn = m.VknTckn, // vkn veya tckn
                    UUID = m.FaturaUUID, // PDF görüntüsü alınacak faturanın UUID si
                    invoiceNumber = m.FaturaID , // PDF görüntüsü alınacak faturanın ID si
                    outputType="PDF"
                };
                return WsClient.getInvoiceDocument(req);

               
            }
            
        }



        /// <summary>
        /// Gönderilen faturayı HTML olarak kayıt eder
        /// </summary>
        /// <returns>Gönderilen faturanın binary HTML datası </returns>
        public getInvoiceDocumentResponseType FaturaHTMLIndir(TextModel m, ArrayList sslList)
        {

            ServicePointManager.SecurityProtocol = TlsSetting.TlsSet(sslList); // TLS/SSL ayarları
            WebServisAdresDegistir();
            using (new OperationContextScope(WsClient.InnerChannel))
            {
                if (WebOperationContext.Current != null)
                    WebOperationContext.Current.OutgoingRequest.Headers.Add(HttpRequestHeader.Authorization,
                        Authorization.GetAuthorization(m.Kullanici, m.Sifre));
                var req = new getInvoiceDocumentRequestType //HTML datası istenen e-arşiv faturasının request parametrelerini set ediyoruz.
                {
                    vkn = m.VknTckn, // vkn veya tckn
                    UUID = m.FaturaUUID, // HTML datası alınacak faturanın UUID si
                    invoiceNumber = m.FaturaID, // HTML datası alınacak faturanın ID si
                    outputType = "HTML"
                };
                return WsClient.getInvoiceDocument(req);
                
            }

        }
    }
}
