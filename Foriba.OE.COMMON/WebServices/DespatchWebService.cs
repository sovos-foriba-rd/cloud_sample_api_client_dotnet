using System.Collections;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;
using Foriba.OE.CLIENT.serviceDespatch;
using Foriba.OE.COMMON.UBLSerializer;
using Foriba.OE.UBL.UBLObject;
using Foriba.OE.COMMON.Model;
using Foriba.OE.UBL.UBLCreate;
using Foriba.OE.COMMON.Zip;
using System.Text;
using System.Collections.Generic;

/// <summary>
/// Copyright © 2018 Foriba Teknoloji
/// Bu proje örnek bir web servis test projesidir. Yalnızca test sisteminde çalışmaktadır.
/// Version Info    : Version.txt
/// Readme          : Readme.txt
/// </summary>


namespace Foriba.OE.COMMON.WebServices
{
    public class DespatchWebService
    {

        /// <summary>
        /// Textbox a girilen TCKN veya VKN numarası ile sisteme kayıtlı mükellef sorgulaması yapar.
        /// </summary>
        /// <returns>Mükellef Listesi</returns
        public getDesUserListResponse MukellefSorgula(TextModel m, ArrayList sslList)
        {
            ServicePointManager.SecurityProtocol = TlsSetting.TlsSet(sslList); // TLS/SSL ayarları
            ClientEDespatchServicesPortClient wsClient = new ClientEDespatchServicesPortClient();
            using (new OperationContextScope(wsClient.InnerChannel))
            {
                if (WebOperationContext.Current != null)
                    WebOperationContext.Current.OutgoingRequest.Headers.Add(HttpRequestHeader.Authorization,
                        Authorization.GetAuthorization(m.Kullanici, m.Sifre));

                var req = new getDesUserListRequest
                {
                    Identifier = m.GbEtiketi, //kullanıcı etiketi
                    VKN_TCKN = m.VknTckn,   //kullanıcı vkn vaya tckn
                    Role = "PK"           //sorgulanacak GB veya PK etiketi
                };
                return wsClient.getDesUserList(req);


            }

        }


        /// <summary>
        /// Sisteme e-İrsaliye gönderir
        /// </summary>
        /// <returns>Gönderilen irsaliye bilgileri</returns>
        public sendDesUBLResponse IrsaliyeGonder(TextModel m, ArrayList sslList)
        {
            DespatchAdvice despatchAdvice = new DespatchAdvice();
            var createdUBL = despatchAdvice.CreateDespactAdvice(m.VknTckn, m.IssueDate, m.FaturaID); // İrsaliye UBL i oluşturulur
            UBLBaseSerializer serializer = new DespatchAdviceSerializer(); // UBL  XML e dönüştürülür
            var strIrsaliye = serializer.GetXmlAsString(createdUBL); // XML byte tipinden string tipine dönüştürülür
            byte[] zipliFile = ZipUtility.CompressFile(Encoding.UTF8.GetBytes(strIrsaliye), createdUBL.UUID.Value); // XML  ziplenir
            ServicePointManager.SecurityProtocol = TlsSetting.TlsSet(sslList); // TLS/SSL ayarları

            ClientEDespatchServicesPortClient wsClient = new ClientEDespatchServicesPortClient();
            using (new OperationContextScope(wsClient.InnerChannel))
            {
                if (WebOperationContext.Current != null)
                    WebOperationContext.Current.OutgoingRequest.Headers.Add(HttpRequestHeader.Authorization,
                        Authorization.GetAuthorization(m.Kullanici, m.Sifre));

                var req = new sendDesUBLRequest
                {
                    VKN_TCKN = m.VknTckn, // gönderici vkn veya tckn
                    SenderIdentifier = m.GbEtiketi, //gönderici birim etiketi
                    ReceiverIdentifier = m.PkEtiketi, //alıcı birim etiketi
                    DocType = "DESPATCH", // gönderilecek doküman tipi
                    DocData = zipliFile   //gönderilecek irsaliyenin ziplenmiş byte datası
                };
                return wsClient.sendDesUBL(req);

            }

        }


        /// <summary>
        ///  Sisteme e-irsaliye yanıtı gönderir
        /// </summary>
        /// <returns>Gönderilen irsaliye yanıtı bilgileri</returns>
        public sendDesUBLResponse IrsaliyeYanitiGonder(TextModel m, string UUID, ArrayList sslList)
        {
            IrsaliyeYanitiUBL receiptAdvice = new IrsaliyeYanitiUBL();
            var createdUBL = receiptAdvice.CreateReceiptAdvice(m.VknTckn, m.IssueDate, UUID); // İrsaliye yanıtı UBL i oluşturulur
            UBLBaseSerializer serializer = new ReceiptAdviceSerializer();  // UBL  XML e dönüştürülür
            var strIrsaliyeYaniti = serializer.GetXmlAsString(createdUBL); // XML byte tipinden string tipine dönüştürülür
            byte[] zipliFile = ZipUtility.CompressFile(Encoding.UTF8.GetBytes(strIrsaliyeYaniti), createdUBL.UUID.Value);  // XML  ziplenir
            ServicePointManager.SecurityProtocol = TlsSetting.TlsSet(sslList); // TLS/SSL ayarları

            ClientEDespatchServicesPortClient wsClient = new ClientEDespatchServicesPortClient();
            using (new OperationContextScope(wsClient.InnerChannel))
            {
                if (WebOperationContext.Current != null)
                    WebOperationContext.Current.OutgoingRequest.Headers.Add(HttpRequestHeader.Authorization,
                        Authorization.GetAuthorization(m.Kullanici, m.Sifre));
                var req = new sendDesUBLRequest
                {
                    VKN_TCKN = m.VknTckn,  // gönderici vkn veya tckn
                    SenderIdentifier = m.PkEtiketi, //gönderici birim etiketi
                    ReceiverIdentifier = m.GbEtiketi, //alıcı birim etiketi
                    DocType = "RECEIPT", // gönderilecek doküman tipi
                    DocData = zipliFile  //gönderilecek irsaliye yanıtının ziplenmiş byte datası
                };

                return wsClient.sendDesUBL(req);

            }
        }



        /// <summary>
        /// Sisteme gelen irsaliye zarf listesini alır
        /// </summary>
        /// <returns>Sisteme gelen irsaliye zarf listesi</returns>
        public getDesUBLListResponse GelenZarflar(TextModel m, ArrayList sslList)
        {
            ServicePointManager.SecurityProtocol = TlsSetting.TlsSet(sslList); // TLS/SSL ayarları
            ClientEDespatchServicesPortClient wsClient = new ClientEDespatchServicesPortClient();
            using (new OperationContextScope(wsClient.InnerChannel))
            {
                if (WebOperationContext.Current != null)
                    WebOperationContext.Current.OutgoingRequest.Headers.Add(HttpRequestHeader.Authorization,
                        Authorization.GetAuthorization(m.Kullanici, m.Sifre));

                var req = new getDesUBLListRequest
                {
                    Identifier = m.PkEtiketi, // alıcı birim etiketi
                    VKN_TCKN = m.VknTckn, //alıcı VKN veya TCKN
                    DocType = "ENVELOPE", //doküman tipi
                    Type = "INBOUND",  // gelen dosyalar için INBOUND, gönderilen dosyalar için ise OUTBOUND yazılmalı
                    FromDate = m.IssueDate, //sorgulanacak başlangıç tarihi. Max 1 günlük tarih aralığı limiti verilmeli
                    ToDate = m.EndDate,    //sorgulanacak bitiş tarihi
                    FromDateSpecified = true,
                    ToDateSpecified = true //başlangıç ve bitiş tarihi verildikten sonra bu iki alanın true olarak set edilmesi gerekmektedir.
                };
                return wsClient.getDesUBLList(req);

            }
        }


        /// <summary>
        /// Sisteme gelen e-İrsaliye listesini alır
        /// </summary>
        /// <returns>Sisteme gelen e-İrsaliye listesi</returns>
        public getDesUBLListResponse GelenIrsaliyeler(TextModel textModel, ArrayList sslList, RequestModel reqModel)
        {
            ServicePointManager.SecurityProtocol = TlsSetting.TlsSet(sslList); // TLS/SSL ayarları
            ClientEDespatchServicesPortClient wsClient = new ClientEDespatchServicesPortClient();
            using (new OperationContextScope(wsClient.InnerChannel))
            {
                if (WebOperationContext.Current != null)
                    WebOperationContext.Current.OutgoingRequest.Headers.Add(HttpRequestHeader.Authorization,
                        Authorization.GetAuthorization(textModel.Kullanici, textModel.Sifre));

                var req = new getDesUBLListRequest
                {
                    Identifier = reqModel.Identifier,  // alıcı etiketi
                    VKN_TCKN = textModel.VknTckn,  //alıcı VKN veya TCKN
                    DocType = reqModel.DocType,  //doküman tipi
                    Type = reqModel.DespatchType,  // gelen dosyalar için INBOUND, gönderilen dosyalar için ise OUTBOUND yazılmalı
                    FromDate = textModel.IssueDate, //sorgulanacak başlangıç tarihi. Max 1 günlük tarih aralığı limiti verilmeli
                    ToDate = textModel.EndDate,   //sorgulanacak bitiş tarihi
                    FromDateSpecified = true,
                    ToDateSpecified = true   //başlangıç ve bitiş tarihi verildikten sonra bu iki alanın true olarak set edilmesi gerekmektedir.
                };
                return wsClient.getDesUBLList(req);

            }
        }


        /// <summary>
        /// Sisteme gelen irsaliye yanıtlarının listesini alır
        /// </summary>
        /// <returns>Sisteme gelen irsaliye yanıtlarının listesi</returns>
        public getDesUBLListResponse GelenIrsaliyeYanitlari(TextModel textModel, ArrayList sslList, RequestModel reqModel)
        {
            ServicePointManager.SecurityProtocol = TlsSetting.TlsSet(sslList); // TLS/SSL ayarları
            ClientEDespatchServicesPortClient wsClient = new ClientEDespatchServicesPortClient();
            using (new OperationContextScope(wsClient.InnerChannel))
            {
                if (WebOperationContext.Current != null)
                    WebOperationContext.Current.OutgoingRequest.Headers.Add(HttpRequestHeader.Authorization,
                        Authorization.GetAuthorization(textModel.Kullanici, textModel.Sifre));

                var req = new getDesUBLListRequest
                {
                    Identifier = reqModel.Identifier,  // alıcı etiketi
                    VKN_TCKN = textModel.VknTckn,  //alıcı VKN veya TCKN
                    DocType = reqModel.DocType,   //doküman tipi
                    Type = reqModel.DespatchType, // gelen dosyalar için INBOUND, gönderilen dosyalar için ise OUTBOUND yazılmalı
                    FromDate = textModel.IssueDate,  //sorgulanacak başlangıç tarihi. Max 1 günlük tarih aralığı limiti verilmeli
                    ToDate = textModel.EndDate,  //sorgulanacak bitiş tarihi
                    FromDateSpecified = true,
                    ToDateSpecified = true //başlangıç ve bitiş tarihi verildikten sonra bu iki alanın true olarak set edilmesi gerekmektedir.
                };
                return wsClient.getDesUBLList(req);

            }
        }


        /// <summary>
        /// Sisteme gönderilen zarfın statüsünü sorgular
        /// </summary>
        /// <returns>Sisteme gönderilen zarfın statüsü </returns>
        public GetDesEnvelopeStatusResponseType[] ZarfDurumSorgula(TextModel m, string[] envelopeUUID, ArrayList sslList)
        {
            ServicePointManager.SecurityProtocol = TlsSetting.TlsSet(sslList); // TLS/SSL ayarları
            ClientEDespatchServicesPortClient wsClient = new ClientEDespatchServicesPortClient();
            using (new OperationContextScope(wsClient.InnerChannel))
            {
                if (WebOperationContext.Current != null)
                    WebOperationContext.Current.OutgoingRequest.Headers.Add(HttpRequestHeader.Authorization,
                        Authorization.GetAuthorization(m.Kullanici, m.Sifre));

                var req = new getDesEnvelopeStatusRequest
                {
                    Identifier = m.GbEtiketi, // gönderici birim etiketi
                    VKN_TCKN = m.VknTckn,  // gönderici VKN veya TCKN
                    UUID = envelopeUUID // zarf UUIDleri

                };
                return wsClient.getDesEnvelopeStatus(req).Response;

            }
        }


        /// <summary>
        /// Sisteme gönderilen zarf listesini alır
        /// </summary>
        /// <returns>Sisteme gönderilen zarf listesi</returns>
        public GetDesUBLListResponseType[] GonderilenZarflar(TextModel m, ArrayList sslList)
        {
            ServicePointManager.SecurityProtocol = TlsSetting.TlsSet(sslList); // TLS/SSL ayarları
            ClientEDespatchServicesPortClient wsClient = new ClientEDespatchServicesPortClient();
            using (new OperationContextScope(wsClient.InnerChannel))
            {
                if (WebOperationContext.Current != null)
                    WebOperationContext.Current.OutgoingRequest.Headers.Add(HttpRequestHeader.Authorization,
                        Authorization.GetAuthorization(m.Kullanici, m.Sifre));

                var req = new getDesUBLListRequest
                {
                    Identifier = m.GbEtiketi, //gönderici birim etiketi
                    VKN_TCKN = m.VknTckn, //gönderici VKN veya TCKN
                    DocType = "ENVELOPE", //doküman tipi
                    Type = "OUTBOUND", //gelen dosyalar için INBOUND, gönderilen dosyalar için ise OUTBOUND yazılmalı
                    FromDate = m.IssueDate, //sorgulanacak başlangıç tarihi Max 1 günlük tarih aralığı limiti verilmeli
                    ToDate = m.EndDate,   //sorgulanacak bitiş tarihi
                    FromDateSpecified = true,
                    ToDateSpecified = true   //başlangıç ve bitiş tarihi verildikten sonra bu iki alanın true olarak set edilmesi gerekmektedir.
                };

                return wsClient.getDesUBLList(req).Response;


            }
        }


        /// <summary>
        /// Sisteme gönderilen e-irsaliye listesini alır
        /// </summary>
        /// <returns>Sisteme gönderilen e-irsaliye listesi</returns>
        public GetDesUBLListResponseType[] GonderilenIrsaliyeler(TextModel textModel, ArrayList sslList, RequestModel reqModel)
        {
            ServicePointManager.SecurityProtocol = TlsSetting.TlsSet(sslList); // TLS/SSL ayarları
            ClientEDespatchServicesPortClient wsClient = new ClientEDespatchServicesPortClient();
            using (new OperationContextScope(wsClient.InnerChannel))
            {
                if (WebOperationContext.Current != null)
                    WebOperationContext.Current.OutgoingRequest.Headers.Add(HttpRequestHeader.Authorization,
                        Authorization.GetAuthorization(textModel.Kullanici, textModel.Sifre));

                var req = new getDesUBLListRequest
                {
                    Identifier = reqModel.Identifier.Trim(), //gönderici birim etiketi
                    VKN_TCKN = textModel.VknTckn, //gönderici VKN veya TCKN
                    DocType = reqModel.DocType.Trim(), //doküman tipi
                    Type = reqModel.DespatchType.Trim(), //gelen dosyalar için INBOUND, gönderilen dosyalar için ise OUTBOUND yazılmalı
                    FromDate = textModel.IssueDate,//sorgulanacak başlangıç tarihi. Max 1 günlük tarih aralığı limiti verilmeli
                    ToDate = textModel.EndDate, //sorgulanacak bitiş tarihi
                    FromDateSpecified = true,
                    ToDateSpecified = true //başlangıç ve bitiş tarihi verildikten sonra bu iki alanın true olarak set edilmesi gerekmektedir.
                };

                return wsClient.getDesUBLList(req).Response;


            }
        }


        // <summary>
        /// Sisteme gönderilen e-irsaliye yanıtlarının listesini alır
        /// </summary>
        /// <returns>Sisteme gönderilen e-irsaliye yanıtlarının listesi</returns>
        public GetDesUBLListResponseType[] GonderilenIrsaliyeYanitlari(TextModel textModel, ArrayList sslList, RequestModel reqModel)
        {
            ServicePointManager.SecurityProtocol = TlsSetting.TlsSet(sslList); // TLS/SSL ayarları
            ClientEDespatchServicesPortClient wsClient = new ClientEDespatchServicesPortClient();
            using (new OperationContextScope(wsClient.InnerChannel))
            {
                if (WebOperationContext.Current != null)
                    WebOperationContext.Current.OutgoingRequest.Headers.Add(HttpRequestHeader.Authorization,
                        Authorization.GetAuthorization(textModel.Kullanici, textModel.Sifre));

                var req = new getDesUBLListRequest
                {
                    Identifier = reqModel.Identifier, //gönderici birim etiketi
                    VKN_TCKN = textModel.VknTckn, //gönderici VKN veya TCKN
                    DocType = reqModel.DocType, //döküman tipi
                    Type = reqModel.DespatchType, //gelen dosyalar için INBOUND, gönderilen dosyalar için ise OUTBOUND yazılmalı
                    FromDate = textModel.IssueDate,  //sorgulanacak başlangıç tarihi. Max 1 günlük tarih aralığı limiti verilmeli.
                    ToDate = textModel.EndDate,  //sorgulanacak bitiş tarihi
                    FromDateSpecified = true,
                    ToDateSpecified = true  //başlangıç ve bitiş tarihi verildikten sonra bu iki alanın true olarak set edilmesi gerekmektedir.
                };

                return wsClient.getDesUBLList(req).Response;


            }
        }

        /// <summary>
        /// Aşağıdaki metodda Grid üzerindeki gelen veya gönderilen e-irsaliyelerin sadece PDF binary datası alınmaktadır.
        /// Aynı request içerisinde DocDetails alanından max 20 adet olabilir . 
        /// Aynı request içerisinde istenirse (gönderilen etiket ve VKN aynı olmak şartıyla) aynı veya farklı UUID lere ait belgelerin HTML, PDF, XSLT, PDF_DEFAULT veya HTML_DEFAULT belgeleride alınabilir.
        /// </summary>
        /// <returns>Grid üzerindeki gelen veya gönderilen e-irsaliyelerin PDF binary datası</returns>
        public getDesViewResponse IrsaliyePDFIndir(TextModel m, string[] UUID, ArrayList sslList, RequestModel reqModel)
        {
            List<GetDesViewRequestType> docDetails = new List<GetDesViewRequestType>();
            ServicePointManager.SecurityProtocol = TlsSetting.TlsSet(sslList);  // TLS/SSL ayarları
            ClientEDespatchServicesPortClient wsClient = new ClientEDespatchServicesPortClient();
            using (new OperationContextScope(wsClient.InnerChannel))
            {
                if (WebOperationContext.Current != null)
                    WebOperationContext.Current.OutgoingRequest.Headers.Add(HttpRequestHeader.Authorization,
                        Authorization.GetAuthorization(m.Kullanici, m.Sifre));

                var req = new getDesViewRequest
                {
                    Identifier = reqModel.Identifier,  // Alınacak dosyanın bulunduğu  etiket
                    VKN_TCKN = m.VknTckn, // gönderici VKN veya TCKN
                };


                foreach (var item in UUID)
                {

                    var data = new GetDesViewRequestType //Her requestType da bulunan UUID  aynı veya farklı olabilir.
                    {
                        UUID = item,   // datagrid de seçilen irsaliye veya irsaliye yanıtının UUID si
                        Type = reqModel.DespatchType, // Alınacak dosya gelen ise INBOUND , gönderilen ise OUTBOUND olmalı.
                        DocType = reqModel.DocType, // Alınacak dosya irsaliye ise DESPATCH, irsaliye yanıtı ise RECEIPT yazılmalı
                        ViewType = "PDF" // Dosyanın tipi HTML ,PDF, XSLT, PDF_DEFAULT veya HTML_DEFAULT olabilir


                    };
                    docDetails.Add(data);

                }

                req.DocDetails = docDetails.ToArray();

                return wsClient.getDesView(req); // Yazılan her UUID için istenilen dosya tipinde data dönülür.

            }
        }


        /// <summary>
        /// Grid üzerinden seçilen gelen veya gönderilen e-irsaliyeyi UBL olarak kayıt eder
        /// </summary>
        /// <returns>Grid üzerinden seçilen gelen veya gönderilen e-irsaliyenin UBL binary datası</returns>
        public getDesUBLResponse IrsaliyeUBLIndir(TextModel m, string[] UUID, ArrayList sslList, RequestModel reqModel)
        {
            ServicePointManager.SecurityProtocol = TlsSetting.TlsSet(sslList);  // TLS/SSL ayarları
            ClientEDespatchServicesPortClient wsClient = new ClientEDespatchServicesPortClient();
            using (new OperationContextScope(wsClient.InnerChannel))
            {
                if (WebOperationContext.Current != null)
                    WebOperationContext.Current.OutgoingRequest.Headers.Add(HttpRequestHeader.Authorization,
                        Authorization.GetAuthorization(m.Kullanici, m.Sifre));

                var req = new getDesUBLRequest
                {
                    Identifier = reqModel.Identifier,  // Alınacak dosyanın bulunduğu etiket
                    VKN_TCKN = m.VknTckn,           // VKN veya TCKN
                    UUID = UUID,  // datagrid deki irsaliye veya irsaliye yanıtlarının UUID si (en fazla 20 UUID sorgulanabilir)
                    DocType = reqModel.DocType,      // Alınacak dosya irsaliye ise DESPATCH, irsaliye yanıtı ise RECEIPT yazılmalı
                    Type = reqModel.DespatchType   // Alınacak dosya gelen ise INBOUND , gönderilen ise OUTBOUND olmalı.
                };

                return wsClient.getDesUBL(req); // UBL in zipli base64 formatındaki datası döner

            }

        }


    }
}
