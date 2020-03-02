using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foriba.OE.UBL.UBLObject;

namespace Foriba.OE.UBL.UBLCreate
{
    public class SmmUBL 
    {
        public eArsivVeri UBL { get; protected set; }
        public SmmUBL(string mukellef) 
        {
            UBL = new eArsivVeri();
            Olustur(mukellef);
        }

        /// <summary>
        /// Serbest Makbuz (eArsivVeri) UBL'ini oluşturma.
        /// </summary>
        /// <returns>SMM UBL</returns>
        private void Olustur(string mukellef)
        {
            UBL = new UBLObject.eArsivVeri();
            UBL.baslik = new baslikType();


            UBL.baslik.hazirlayan = new vknTcknType();
            UBL.baslik.mukellef = new vknTcknType();

            UBL.baslik.hazirlayan.vkn = "3880718497"; // Serbest makbuzu imzalayan kurum
            UBL.baslik.mukellef.vkn = mukellef; // Serberst makbuzu gönderen kurum/kişi


            UBL.serbestMeslekMakbuz = SerbestMakbuzOlustur();
            UBL.serbestMeslekMakbuz[0].dosyaAdi = UBL.serbestMeslekMakbuz[0].ETTN + "_.pdf";

            UBL.serbestMeslekMakbuz[0].vergiBilgisi = new vergiBilgiType();


            UBL.serbestMeslekMakbuz[0].vergiBilgisi.vergi = VergiBilgiOlustur();

            UBL.serbestMeslekMakbuz[0].aliciBilgileri = new aliciType();
            UBL.serbestMeslekMakbuz[0].aliciBilgileri.tuzelKisi = new aliciTypeTuzelKisi();
            UBL.serbestMeslekMakbuz[0].aliciBilgileri.adres = new aliciTypeAdres();
            UBL.serbestMeslekMakbuz[0].aliciBilgileri.tuzelKisi.vkn = UBL.baslik.mukellef.vkn;
            UBL.serbestMeslekMakbuz[0].aliciBilgileri.tuzelKisi.unvan = "TEST";
            UBL.serbestMeslekMakbuz[0].aliciBilgileri.adres.caddeSokak = "ÖZ SK.";
            UBL.serbestMeslekMakbuz[0].aliciBilgileri.adres.binaAd = "GOLD PLAZA";
            UBL.serbestMeslekMakbuz[0].aliciBilgileri.adres.binaNo = "19";
            UBL.serbestMeslekMakbuz[0].aliciBilgileri.adres.kapiNo = "43";
            UBL.serbestMeslekMakbuz[0].aliciBilgileri.adres.kasabaKoy = "MALTEPE";
            UBL.serbestMeslekMakbuz[0].aliciBilgileri.adres.semt = "MALTEPE / ALTYAYÇEŞME";
            UBL.serbestMeslekMakbuz[0].aliciBilgileri.adres.sehir = "ISTANBUL";
            UBL.serbestMeslekMakbuz[0].aliciBilgileri.adres.postaKod = "34883";
            UBL.serbestMeslekMakbuz[0].aliciBilgileri.adres.ulke = aliciTypeAdresUlke.TR;
            UBL.serbestMeslekMakbuz[0].aliciBilgileri.adres.vDaire = "MALTEPE VERGİ DAİRESİ";


            UBL.serbestMeslekMakbuz[0].malHizmetBilgisi = new eArsivVeriSerbestMeslekMakbuzMalHizmet[] { new eArsivVeriSerbestMeslekMakbuzMalHizmet() };
            UBL.serbestMeslekMakbuz[0].malHizmetBilgisi[0].vergiBilgisi = new vergiBilgiType();
            UBL.serbestMeslekMakbuz[0].malHizmetBilgisi[0].vergiBilgisi.vergi = VergiBilgiOlustur();
            UBL.serbestMeslekMakbuz[0].malHizmetBilgisi[0].ad = "Hizmet";
            UBL.serbestMeslekMakbuz[0].malHizmetBilgisi[0].burutUcret = 345;

            UBL.serbestMeslekMakbuz[0].parameters = ParametreOlustur();
        }

        /// <summary>
        /// Serbest Makbuz (eArsivVeri) UBL'inin serbest makbuz alanını oluşturma.
        /// Birden fazla parametre alanı oluşturulabilir.
        /// </summary>
        /// <returns>Serbest Makbuz Alanı</returns>
        private eArsivVeriSerbestMeslekMakbuz[] SerbestMakbuzOlustur()
        {
            eArsivVeriSerbestMeslekMakbuz[] eArsivVeriSerbestMeslekMakbuz = new eArsivVeriSerbestMeslekMakbuz[] {

                new eArsivVeriSerbestMeslekMakbuz{

                    makbuzNo = "FIT2020000000001", // Eğer Sovos Foriba tarafından otomatik üretilmeyecekse makbuNo generate edilecek şekilde düzenlenmelidir.
                    ETTN = Guid.NewGuid().ToString(),
                    gonderimSekli = eArsivVeriSerbestMeslekMakbuzGonderimSekli.ELEKTRONIK,
                    belgeTarihi = DateTime.Now,
                    belgeZamani = DateTime.Now,
                    toplamTutar = (decimal)120.0,
                    odenecekTutar = (decimal)120.0,
                    paraBirimi = eArsivVeriSerbestMeslekMakbuzParaBirimi.TRY,
                    kur = (long)1
                }
            };

            return eArsivVeriSerbestMeslekMakbuz;
        }


        /// <summary>
        /// Serbest Makbuz (eArsivVeri) UBL'inin vergi bilgi alanını oluşturma.
        /// Birden fazla vergi bilgi alanı oluşturulabilir.
        /// </summary>
        /// <returns>Vergi Bilgi Alanı</returns>
        private vergiBilgiTypeVergi[] VergiBilgiOlustur()
        {
            var vergiBilgiType= new vergiBilgiTypeVergi[] { new vergiBilgiTypeVergi() { matrah = 345, vergiKodu = vergiBilgiTypeVergiVergiKodu.Item4171, vergiTutari = 27.6m, vergiOrani = 8 }};
            return vergiBilgiType;
        }


        /// <summary>
        /// Serbest Makbuz (eArsivVeri) UBL'inin parametre alanını oluşturma.
        /// Birden fazla parametre alanı oluşturulabilir.
        /// </summary>
        /// <returns>Parameters Alanı</returns>
        private parameter[] ParametreOlustur()
        {
            var parameter= new parameter[]
            {
                new parameter(){ paramName="GONDERICI_VERGI_DAIRESI",paramValue="Gönderici Vergi Dairesi",type="Sender"},
                new parameter(){ paramName="GONDERICI_ADSOYAD_UNVAN",paramValue="Gönderici Ad Soyad/Ünvan",type="Sender"},
                new parameter(){ paramName="GONDERICI_ILCE",paramValue="Gönderici İlçe",type="Sender"},
                new parameter(){ paramName="GONDERICI_IL",paramValue="Gönderici İl",type="Sender"},
                new parameter(){ paramName="GONDERICI_ULKE",paramValue="Gönderici Ülke",type="Sender"},

            };
            return parameter;
        }
    }
}
