using Foriba.OE.CLIENT.serviceSmm;
using Foriba.OE.COMMON.Model;
using Foriba.OE.COMMON.UBLSerializer;
using Foriba.OE.COMMON.Zip;
using Foriba.OE.UBL.UBLCreate;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Foriba.OE.COMMON.WebServices
{
    public class MmWebService
    {
        protected ForibaESmmServicesPortClient WsClient = new ForibaESmmServicesPortClient();

        protected void WebServisAdresDegistir()
        {
            if (UrlModel.SelectedItem != "Foriba Bulut")
            {
                WsClient.Endpoint.Address = new EndpointAddress("https://earsivtest.ingbank.com.tr/ClientESmmServicesPort.svc");
            }
            else
            {
                WsClient.Endpoint.Address = new EndpointAddress("https://earsivwstest.fitbulut.com/ClientEMmServicesPort.svc");
            }
        }

        public string GetHashInfo(byte[] ziplifatura)
        {
            using (var md5 = MD5.Create())
            {
                byte[] aa = md5.ComputeHash(ziplifatura);
                var hash = BitConverter.ToString(aa).Replace("-", "").ToLowerInvariant();
                return hash;
            }
        }

        public sendDocumentResponseType[] EMmGonder(TextModel m, ArrayList sslList)
        {
            MmUBL mm = new MmUBL();
            var createdUBL = mm.CreateCreditNote(m.VknTckn,DateTime.Now);  // e-Mm  UBL i oluşturulur
            UBLBaseSerializer serializer = new MmSerializer();  // UBL  XML e dönüştürülür
            var strFatura = serializer.GetXmlAsString(createdUBL); // XML byte tipinden string tipine dönüştürülür.
            byte[] zipliFile = ZipUtility.CompressFile(Encoding.UTF8.GetBytes(strFatura), createdUBL.UUID.Value + ".xml"); // XML  ziplenir
            string hash = GetHashInfo(zipliFile); // zipli dosyanın hash bilgisi alınır
            ServicePointManager.SecurityProtocol = TlsSetting.TlsSet(sslList);  // TLS/SSL ayarları
            WebServisAdresDegistir();
            using (new OperationContextScope(WsClient.InnerChannel))
            {
                if (WebOperationContext.Current != null)
                    WebOperationContext.Current.OutgoingRequest.Headers.Add(HttpRequestHeader.Authorization,
                        Authorization.GetAuthorization(m.Kullanici, m.Sifre));

                var req = new sendDocumentRequest
                {
                    VKN_TCKN = m.VknTckn,
                    Branch = m.Sube,
                    SendDocDetails = new sendDocumentRequestType[]
                        {
                            new sendDocumentRequestType()
                            {
                                UUID=createdUBL.UUID.Value,
                                Type="MM",
                                DocType="XML",
                                DocData=zipliFile,
                            }
                        }
                };
                return WsClient.sendDocument(req);
            }
        }

        public getDocumentResponseType[] MmPDFIndir(TextModel m, ArrayList sslList)
        {
            ServicePointManager.SecurityProtocol = TlsSetting.TlsSet(sslList); // TLS/SSL ayarları
            WebServisAdresDegistir();
            using (new OperationContextScope(WsClient.InnerChannel))
            {
                if (WebOperationContext.Current != null)
                    WebOperationContext.Current.OutgoingRequest.Headers.Add(HttpRequestHeader.Authorization,
                        Authorization.GetAuthorization(m.Kullanici, m.Sifre));
                var req = new getDocumentRequest //UBL datası istenen e-arşiv faturasının request parametrelerini set ediyoruz.
                {
                    VKN_TCKN = m.VknTckn,
                    GetDocDetails = new getDocumentRequestType[] {
                        new getDocumentRequestType{
                            Type = "MM",
                            ViewType="PDF",
                            UUID=m.FaturaUUID,
                            ID=m.FaturaID
                        }

                    }

                };
                return WsClient.getDocument(req);
            }
        }
    }
}
