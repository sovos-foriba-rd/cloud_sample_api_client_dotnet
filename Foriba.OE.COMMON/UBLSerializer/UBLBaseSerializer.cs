using System.IO;
using System.Text;
using System.Xml.Serialization;


/// <summary>
/// Copyright © 2018 Foriba Teknoloji
/// Bu proje örnek bir web servis test projesidir. Yalnızca test sisteminde çalışmaktadır.
/// Version Info    : Version.txt
/// Readme          : Readme.txt
/// </summary>

namespace Foriba.OE.COMMON.UBLSerializer
{
    public abstract class UBLBaseSerializer
    {
        protected XmlSerializerNamespaces SerializerNamespace { get; set; }


        protected UBLBaseSerializer()
        {
            SerializerNamespace = GetXmlSerializerNamespace();
        }


        /// <summary>
        ///  XmlSerializer nesnesini oluşturur ve prefix ile namespace ekler.
        /// </summary>
        /// <returns>Oluşturulan XmlSerializer Nesnesi</returns>
        private XmlSerializerNamespaces GetXmlSerializerNamespace()
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();

            ns.Add("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            ns.Add("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            ns.Add("ccts", "urn:un:unece:uncefact:documentation:2");
            ns.Add("ds", "http://www.w3.org/2000/09/xmldsig#");
            ns.Add("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");
            ns.Add("qdt", "urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2");
            ns.Add("ubltr", "urn:oasis:names:specification:ubl:schema:xsd:TurkishCustomizationExtensionComponents");
            ns.Add("udt", "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2");
            ns.Add("udt", "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2");
            ns.Add("xades", "http://uri.etsi.org/01903/v1.3.2#");
            ns.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");

            return ns;
        }


        /// <summary>
        ///  Byte tipinde Xml'i string tipinde XML'e çevirir
        /// </summary>
        /// <returns></returns>
        public virtual string GetXmlAsString<T>(T obj)
        {
            return Encoding.UTF8.GetString(GetXmlAsByteArray(obj)).Replace("<xml xmlns=\"\" />", "");
        }


        /// <summary>
        ///  Oluşturulan UBL faturayı byte tipinde XML'e çevirir
        /// </summary>
        /// <returns></returns>
        public byte[] GetXmlAsByteArray<T>(T obj)
        {
            XmlSerializer serializerObj = new XmlSerializer(typeof(T));

            MemoryStream ms = new MemoryStream();
            TextWriter writeFileStream = new StreamWriter(ms, Encoding.UTF8);

            serializerObj.Serialize(writeFileStream, obj, SerializerNamespace);

            writeFileStream.Close();

            return ms.ToArray();
        }
    }
}
