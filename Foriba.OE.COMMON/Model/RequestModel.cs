
/// <summary>
/// Copyright © 2018 Foriba Teknoloji
/// Bu proje örnek bir web servis test projesidir. Yalnızca test sisteminde çalışmaktadır.
/// Version Info    : Version.txt
/// Readme          : Readme.txt
/// </summary>

namespace Foriba.OE.COMMON.Model
{
    /// <summary>
    ///  Gelen veya gönderilen irsaliye ve irsaliye yanıtlarının datalarını tek bir buton ile alabilmek
    //   için web servis requestinde gerekli olan parametreleri oluşturarak butonların içerisinde dolduruyoruz
    /// </summary>

    public class RequestModel
    {
        public string Identifier { get; set; }
        public string DocType { get; set; }
        public string DespatchType { get; set; }
        
    }
}
