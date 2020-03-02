using System;
using System.Collections;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Windows.Forms;
using Foriba.OE.CLIENT.serviceInvoicee;
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
/// 
namespace Foriba.OE.COMMON.WebServices
{
    public class InvoiceWebService
    {
        
        protected ClientEInvoiceServicesPortClient WsClient = new ClientEInvoiceServicesPortClient();

        protected void WebServisAdresDegistir()
        {
            if (UrlModel.SelectedItem != "Foriba Bulut")
            {
                WsClient.Endpoint.Address = new EndpointAddress("https://ingefservicestest.ingbank.com.tr/ClientEInvoiceServices/ClientEInvoiceServicesPort.svc");
            }
        }


        /// <summary>
        /// Textbox a girilen TCKN veya VKN numarası ile sisteme kayıtlı mükellef sorgulaması yapar.
        /// </summary>
        /// <returns>Mükellef Listesi</returns>
        public getRAWUserListResponse MukellefSorgula(TextModel m,ArrayList sslList)
        {
            try
            {
                
                ServicePointManager.SecurityProtocol = TlsSetting.TlsSet(sslList); // TLS/SSL ayarları
                WebServisAdresDegistir();
                using (new OperationContextScope(WsClient.InnerChannel))
                {
                    if (WebOperationContext.Current != null)
                        WebOperationContext.Current.OutgoingRequest.Headers.Add(HttpRequestHeader.Authorization,
                            Authorization.GetAuthorization(m.Kullanici, m.Sifre));

                    var request = new getRAWUserListRequest
                    {
                        Identifier = m.GbEtiketi, //kullanıcı etiketi
                        VKN_TCKN = m.VknTckn,   //kullanıcı vkn vaya tckn
                        Role = "PK"            //sorgulanacak GB veya PK etiketi
                    };
                   return WsClient.getRAWUserList(request);
                   
                }

            }

            catch (FaultException<ProcessingFault> ex)
            {

                MessageBox.Show(ex.Detail.Message, "ProcessingFault", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            catch (FaultException ex)
            {
                MessageBox.Show(ex.Message, "FaultException", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }


        /// <summary>
        /// Sisteme gelen zarf listesini alır
        /// </summary>
        /// <returns>Sisteme gelen zarf listesi</returns>
        public GetUBLListResponseType[] GelenZarflar(TextModel m, ArrayList sslList)
        {
            ServicePointManager.SecurityProtocol = TlsSetting.TlsSet(sslList);  // TLS/SSL ayarları
            WebServisAdresDegistir();
            using (new OperationContextScope(WsClient.InnerChannel))
            {
                if (WebOperationContext.Current != null)
                    WebOperationContext.Current.OutgoingRequest.Headers.Add(HttpRequestHeader.Authorization,
                        Authorization.GetAuthorization(m.Kullanici, m.Sifre));

                var req = new getUBLListRequest //sisteme gelen zarf listesi için request parametreleri
                {
                    Identifier = m.PkEtiketi, // //alıcı posta kutusu
                    VKN_TCKN = m.VknTckn,    // alıcı VKN/TCKN
                    DocType = "ENVELOPE", //döküman tipi
                    Type = "INBOUND", //gelen zarflar INBOUND, giden zarflar için OUTBOUND
                    FromDate = m.IssueDate, // max 1 günlük tarih limiti verilmelidir.
                    ToDate = m.EndDate,
                    FromDateSpecified = true, //başlangıç ve bitiş tarihi verildikten sonra bu iki alanın true olarak set edilmesi gerekmektedir.
                    ToDateSpecified = true
                };

                return WsClient.getUBLList(req);

              
            }

        }


        /// <summary>
        /// Sisteme gelen e-fatura listesini alır
        /// </summary>
        /// <returns>Sisteme gelen e-fatura listesi</returns>
        public GetUBLListResponseType[] GelenFaturalar(TextModel m, ArrayList sslList)
        {
            ServicePointManager.SecurityProtocol = TlsSetting.TlsSet(sslList);  // TLS/SSL ayarları
            WebServisAdresDegistir();
            using (new OperationContextScope(WsClient.InnerChannel))
            {
                if (WebOperationContext.Current != null)
                    WebOperationContext.Current.OutgoingRequest.Headers.Add(HttpRequestHeader.Authorization,
                        Authorization.GetAuthorization(m.Kullanici, m.Sifre));

                var req = new getUBLListRequest //sistemdeki gelen efatura listesi için request parametreleri
                {
                    Identifier = m.PkEtiketi,  //alıcı posta kutusu
                    VKN_TCKN = m.VknTckn, //tckn veya vkn
                    DocType = "INVOICE", //döküman tipi
                    Type = "INBOUND", //gelen faturalar için INBOUND, giden faturalar için OUTBOUND
                    FromDate = m.IssueDate, //fatura listesi çekerken max 1 günlük tarih limiti verilmelidir.
                    ToDate = m.EndDate,
                    FromDateSpecified = true, //başlangıç ve bitiş tarihi verildikten sonra bu iki alanın true olarak set edilmesi gerekmektedir.
                    ToDateSpecified = true
                };

                return WsClient.getUBLList(req);

               
            }
        }


        /// <summary>
        /// Sisteme gelen uygulama yanıtlarının listesini alır
        /// </summary>
        /// <returns>Sisteme gelen uygulama yanıtlarının listesi</returns>
        public GetUBLListResponseType[] GelenUygulamaYanitlari(TextModel m, ArrayList sslList)
        {
            ServicePointManager.SecurityProtocol = TlsSetting.TlsSet(sslList);  // TLS/SSL ayarları
            WebServisAdresDegistir();
            using (new OperationContextScope(WsClient.InnerChannel))
            {
                if (WebOperationContext.Current != null)
                    WebOperationContext.Current.OutgoingRequest.Headers.Add(HttpRequestHeader.Authorization,
                        Authorization.GetAuthorization(m.Kullanici, m.Sifre));

                var req = new getUBLListRequest //fatura ve uygulama yanıtı listesi çekerken aynı metod kullanılıyor. Parametreler değişiklik gösteriyor.
                {
                    Identifier = m.GbEtiketi, // uygulama yanıtında alıcı birim etiketi
                    VKN_TCKN = m.VknTckn, //tckn veya vkn
                    DocType = "APP_RESP", //döküman tipi
                    Type = "INBOUND", //gelen uygulama yanıtları için INBOUND, giden uygulama yanıtları için OUTBOUND
                    FromDate = m.IssueDate, //uygulama yanıtları listesini çekerken max 1 günlük tarih limiti verilmelidir.
                    ToDate = m.EndDate,
                    FromDateSpecified = true, //başlangıç ve bitiş tarihi verildikten sonra bu iki alanın true olarak set edilmesi gerekmektedir.
                    ToDateSpecified = true
                };

                return WsClient.getUBLList(req); //gönderilen parametreler sonucu gelen uygulama yanıtlarının listesi dönüyor.

                
            }
        }


        /// <summary>
        /// Sisteme gönderilen zarf listesini alır
        /// </summary>
        /// <returns>Sisteme gönderilen zarf listesi</returns>
        public GetUBLListResponseType[] GonderilenZarflar(TextModel m, ArrayList sslList)
        {
            ServicePointManager.SecurityProtocol = TlsSetting.TlsSet(sslList);  // TLS/SSL ayarları
            WebServisAdresDegistir();
            using (new OperationContextScope(WsClient.InnerChannel))
            {
                if (WebOperationContext.Current != null)
                    WebOperationContext.Current.OutgoingRequest.Headers.Add(HttpRequestHeader.Authorization,
                        Authorization.GetAuthorization(m.Kullanici, m.Sifre));

                var req = new getUBLListRequest //sistemdeki giden zarflar listesi için request parametreleri
                {
                    Identifier = m.GbEtiketi, //gönderici birim etiketi
                    VKN_TCKN = m.VknTckn, //tckn veya vkn
                    DocType = "ENVELOPE", //döküman tipi
                    Type = "OUTBOUND", //gelen zarflar için INBOUND, giden zarflar için OUTBOUND
                    FromDate = m.IssueDate, //zarf listesi çekerken max 1 günlük tarih limiti verilmelidir.
                    ToDate = m.EndDate,
                    FromDateSpecified = true, //başlangıç ve bitiş tarihi verildikten sonra bu iki alanın true olarak set edilmesi gerekmektedir.
                    ToDateSpecified = true
                };

                return WsClient.getUBLList(req);

            }
        }



        /// <summary>
        /// Sisteme gönderilen e-faturaların listesini alır
        /// </summary>
        /// <returns>Sisteme gönderilen e-faturaların listesi</returns>
        public GetUBLListResponseType[] GonderilenFaturalar(TextModel m, ArrayList sslList)
        {
            ServicePointManager.SecurityProtocol = TlsSetting.TlsSet(sslList);  // TLS/SSL ayarları
            WebServisAdresDegistir();
            using (new OperationContextScope(WsClient.InnerChannel))
            {
                if (WebOperationContext.Current != null)
                    WebOperationContext.Current.OutgoingRequest.Headers.Add(HttpRequestHeader.Authorization,
                        Authorization.GetAuthorization(m.Kullanici, m.Sifre));

                var req = new getUBLListRequest //sistemdeki giden efatura listesi için request parametreleri
                {
                    Identifier = m.GbEtiketi, //gönderici birim etiketi
                    VKN_TCKN = m.VknTckn, ///tckn veya vkn
                    DocType = "INVOICE", //döküman tipi
                    Type = "OUTBOUND", //gelen faturalar için INBOUND, giden faturalar için OUTBOUND
                    FromDate = m.IssueDate, //fatura listesi çekerken max 1 günlük tarih limiti verilmelidir.
                    ToDate = m.EndDate,
                    FromDateSpecified = true, //başlangıç ve bitiş tarihi verildikten sonra bu iki alanın true olarak set edilmesi gerekmektedir.
                    ToDateSpecified = true
                };

                return WsClient.getUBLList(req);

               
            }
        }


        /// <summary>
        /// Sisteme gönderilen uygulama yanıtlarının listesini alır
        /// </summary>
        /// <returns>Sisteme gönderilen uygulama yanıtlarının listesi</returns>
        public GetUBLListResponseType[] GonderilenUygulamaYanitlari(TextModel m, ArrayList sslList)
        {
            ServicePointManager.SecurityProtocol = TlsSetting.TlsSet(sslList);  // TLS/SSL ayarları
            WebServisAdresDegistir();
            using (new OperationContextScope(WsClient.InnerChannel))
            {
                if (WebOperationContext.Current != null)
                    WebOperationContext.Current.OutgoingRequest.Headers.Add(HttpRequestHeader.Authorization,
                        Authorization.GetAuthorization(m.Kullanici, m.Sifre));

                var req = new getUBLListRequest //fatura ve uygulama yanıtı listesi çekerken aynı metod kullanılıyor. parametreler değişiklik gösteriyor.
                {
                    Identifier = m.PkEtiketi, // uygulama yanıtının gönderici birim etiketi
                    VKN_TCKN = m.VknTckn, //tckn veya vkn
                    DocType = "APP_RESP", //döküman tipi
                    Type = "OUTBOUND", //gelen uygulama yanıtları için INBOUND, giden uygulama yanıtları için OUTBOUND
                    FromDate = m.IssueDate, //uygulama yanıtlarının listesini çekerken max 1 günlük tarih limiti verilmelidir.
                    ToDate = m.EndDate,
                    FromDateSpecified = true, //başlangıç ve bitiş tarihi verildikten sonra bu iki alanın true olarak set edilmesi gerekmektedir.
                    ToDateSpecified = true
                };

                return WsClient.getUBLList(req); //gönderilen parametreler sonucu gelen faturalara yolladığımız uygulama yanıtlarının listesi dönüyor.

                
            }
        }



        /// <summary>
        /// Sisteme e-fatura gönderir
        /// </summary>
        /// <returns>Sisteme gönderilen faturanın bilgileri </returns>
        public SendUBLResponseType[] FaturaGonder(TextModel m, ArrayList sslList, BaseInvoiceUBL fatura)
        {
            var createdUBL = fatura.BaseUBL;  // Fatura UBL i oluşturulur
            UBLBaseSerializer serializer = new InvoiceSerializer(); // UBL  XML e dönüştürülür
            var strFatura = serializer.GetXmlAsString(createdUBL); // XML byte tipinden string tipine dönüştürülür
            byte[] zipliFile = ZipUtility.CompressFile(Encoding.UTF8.GetBytes(strFatura), createdUBL.UUID.Value); // XML  ziplenir
            ServicePointManager.SecurityProtocol = TlsSetting.TlsSet(sslList);  // TLS/SSL ayarları
            WebServisAdresDegistir();
            using (new OperationContextScope(WsClient.InnerChannel))
            {
                if (WebOperationContext.Current != null)
                    WebOperationContext.Current.OutgoingRequest.Headers.Add(HttpRequestHeader.Authorization,
                        Authorization.GetAuthorization(m.Kullanici, m.Sifre));

                var req = new sendUBLRequest
                {
                    VKN_TCKN = m.VknTckn, //tckn veya vkn
                    SenderIdentifier = m.GbEtiketi, //gönderici birim etiketi
                    ReceiverIdentifier = m.PkEtiketi, //alıcı posta kutusu
                    DocType = "INVOICE", //gönderilen döküman tipi. zarf, fatura vs.
                    DocData = zipliFile  //içinde xml dosyası olan zip lenen dosya.
                };

                return WsClient.sendUBL(req);

                

            }
        }


        /// <summary>
        /// Sisteme uygulama yanıtı gönderir
        /// </summary>
        /// <returns>Sisteme gönderilen uygulama yanıtının bilgileri</returns>
        public SendUBLResponseType[] UygulamaYanitiGonder(TextModel m, string gelenFaturaID, ArrayList sslList)
        {
            ApplicationResponseUBL applicationResponse = new ApplicationResponseUBL();  
            var createdUBL = applicationResponse.CreateApplicationResponse(m.VknTckn, gelenFaturaID, m.IssueDate); // Uygulama yanıtı UBL i oluşturulur
            UBLBaseSerializer serializer = new InvoiceSerializer();  // UBL  XML e dönüştürülür
            var strUygulamaYaniti = serializer.GetXmlAsString(createdUBL); // XML  byte tipinden string tipine dönüştürülür
            byte[] zipliFile = ZipUtility.CompressFile(Encoding.UTF8.GetBytes(strUygulamaYaniti), createdUBL.UUID.Value); // XML  ziplenir

            ServicePointManager.SecurityProtocol = TlsSetting.TlsSet(sslList);  // TLS/SSL ayarları
            WebServisAdresDegistir();

            using (new OperationContextScope(WsClient.InnerChannel))
            {
                if (WebOperationContext.Current != null)
                    WebOperationContext.Current.OutgoingRequest.Headers.Add(HttpRequestHeader.Authorization,
                        Authorization.GetAuthorization(m.Kullanici, m.Sifre));

                var req = new sendUBLRequest
                {
                    SenderIdentifier = m.PkEtiketi,  //uygulama yanıtı gönderici birim etiketi
                    ReceiverIdentifier = m.GbEtiketi,  //uygulama yanıtı alıcı posta kutusu
                    VKN_TCKN = m.VknTckn, // tckn veya vkn
                    DocType = "APP_RESP",  //gönderilen döküman tipi
                    DocData = zipliFile //gönderilen uygulama yanıtının ziplenmiş byte datası
                };

                return WsClient.sendUBL(req);

               

            }
        }



        /// <summary>
        /// Sisteme gönderilen zarfın statüsünü sorgular
        /// </summary>
        /// <returns>Sisteme gönderilen zarfın statüsü </returns>
        public getEnvelopeStatusResponseType[] ZarfDurumSorgula(TextModel m, string[] zarfUUID, ArrayList sslList)
        {
            ServicePointManager.SecurityProtocol = TlsSetting.TlsSet(sslList);  // TLS/SSL ayarları
            WebServisAdresDegistir();

            using (new OperationContextScope(WsClient.InnerChannel))
            {
                if (WebOperationContext.Current != null)
                    WebOperationContext.Current.OutgoingRequest.Headers.Add(HttpRequestHeader.Authorization,
                        Authorization.GetAuthorization(m.Kullanici, m.Sifre));

                var req = new getEnvelopeStatusRequest //request parametreleri gönderiliyor.
                {
                    Identifier = m.GbEtiketi, //gönderici birim etiketi
                    VKN_TCKN = m.VknTckn, //tckn veya vkn
                    UUID = zarfUUID//sisteme gönderilen faturaların zarfUUID leri
                };

                return WsClient.getEnvelopeStatus(req); //gönderilen request parametrelerinden geri dönen sonuc

               
            }
        }


        /// <summary>
        /// Grid üzerinden seçilen gelen veya gönderilen faturayı HTML olarak kayıt eder
        /// </summary>
        /// <returns>Grid üzerinden seçilen gelen veya gönderilen faturanın HTML binary datası</returns>
        public getInvoiceViewResponse FaturaHTMLIndir(TextModel m, string invUUID, ArrayList sslList)
        {
            ServicePointManager.SecurityProtocol = TlsSetting.TlsSet(sslList);  // TLS/SSL ayarları
            WebServisAdresDegistir();
            using (new OperationContextScope(WsClient.InnerChannel))
            {
                if (WebOperationContext.Current != null)
                    WebOperationContext.Current.OutgoingRequest.Headers.Add(HttpRequestHeader.Authorization,
                        Authorization.GetAuthorization(m.Kullanici, m.Sifre));

                var req = new getInvoiceViewRequest //Faturanın görüntüsünü almak için request parametreleri gönderiyorz.
                {
                    Identifier = m.GbEtiketi, //gönderici birim etiketi
                    VKN_TCKN = m.VknTckn, //tckn veya vkn
                    DocType = "HTML", //dosya formatı
                    Type = "OUTBOUND", //gelen veya giden fatura türü
                    UUID = invUUID //grid üzerinden seçilen faturanın UUID si
                };

                return WsClient.getInvoiceView(req); //gönderilen request parametreleri sonucu dönen değer
                
            }
        }


        /// <summary>
        /// Grid üzerinden seçilen gelen veya gönderilen faturayı PDF olarak kayıt eder
        /// </summary>
        /// <returns>Grid üzerinden seçilen gelen veya gönderilen faturanın PDF binary datası</returns>
        public getInvoiceViewResponse FaturaPDFIndir(TextModel m, string invUUID, ArrayList sslList)
        {
            ServicePointManager.SecurityProtocol = TlsSetting.TlsSet(sslList);  // TLS/SSL ayarları
            WebServisAdresDegistir();
            using (new OperationContextScope(WsClient.InnerChannel))
            {
                if (WebOperationContext.Current != null)
                    WebOperationContext.Current.OutgoingRequest.Headers.Add(HttpRequestHeader.Authorization,
                        Authorization.GetAuthorization(m.Kullanici, m.Sifre));

                var req = new getInvoiceViewRequest //Faturanın görüntüsünü almak için request parametreleri gönderiyorz.
                {
                    Identifier = m.GbEtiketi, //gönderici birim etiketi
                    VKN_TCKN = m.VknTckn, //tckn veya vkn
                    DocType = "PDF", //dosya formatı
                    Type = "OUTBOUND", //gelen veya giden fatura türü
                    UUID = invUUID //grid üzerinden seçilen faturanın UUID si
                };

                return WsClient.getInvoiceView(req); //gönderilen request parametreleri sonucu dönen değer

                
            }
        }


        /// <summary>
        /// Grid üzerinden seçilen gelen veya gönderilen faturayı UBL olarak kayıt eder
        /// </summary>
        /// <returns>Grid üzerinden seçilen gelen veya gönderilen faturanın UBL binary datası</returns>
        public byte[][] FaturaUBLIndir(TextModel m, string[] invUUID, ArrayList sslList)
        {
            ServicePointManager.SecurityProtocol = TlsSetting.TlsSet(sslList);  // TLS/SSL ayarları
            WebServisAdresDegistir();
            using (new OperationContextScope(WsClient.InnerChannel))
            {
                if (WebOperationContext.Current != null)
                    WebOperationContext.Current.OutgoingRequest.Headers.Add(HttpRequestHeader.Authorization,
                        Authorization.GetAuthorization(m.Kullanici, m.Sifre));

                string[] arrayUUID = invUUID;

                var req = new getUBLRequest //Faturanın UBL datasını almak için request parametreleri gönderiyorz.
                {
                    Identifier = m.GbEtiketi, //gönderici birim etiketi
                    VKN_TCKN = m.VknTckn, //tckn veya vkn
                    UUID = arrayUUID, //grid üzerinden seçimi yapılan faturanın UUID si
                    DocType = "INVOICE", //xml olarak kaydedilecek dosya tipi
                    Type = "OUTBOUND", //gelen veya giden fatura seçeneği
                    Parameters = new[] { "XML" } //parametre olarak XML gönderiyoruz.
                };

                return WsClient.getUBL(req);

                
            }
        }


        
    }

}
