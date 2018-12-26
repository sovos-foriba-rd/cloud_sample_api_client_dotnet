/// <summary>
/// Copyright © 2018 Foriba Teknoloji
/// Bu proje örnek bir web servis test projesidir. Yalnızca test sisteminde çalışmaktadır.
/// Version Info    : Version.txt
/// Readme          : Readme.txt
/// </summary>

namespace Foriba.OE.COMMON.UBLSerializer
{
    public class ReceiptAdviceSerializer : UBLBaseSerializer
    {
        /// <summary>
        ///  İrsaliye Yaniti XML i için gerekli olan prefix ve namespace ekleme işlemi yapar 
        /// </summary> 

        public ReceiptAdviceSerializer()
        {
            SerializerNamespace.Add("", "urn:oasis:names:specification:ubl:schema:xsd:ReceiptAdvice-2");
        }
    }
}
