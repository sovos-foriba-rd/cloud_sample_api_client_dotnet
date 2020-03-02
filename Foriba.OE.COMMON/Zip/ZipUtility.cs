using System.IO;
using System.IO.Compression;
using System.Text;


/// <summary>
/// Copyright © 2018 Foriba Teknoloji
/// Bu proje örnek bir web servis test projesidir. Yalnızca test sisteminde çalışmaktadır.
/// Version Info    : Version.txt
/// Readme          : Readme.txt
/// </summary>


namespace Foriba.OE.COMMON.Zip
{
    public static class ZipUtility
    {
        /// <summary>
        ///  Dönüştürülen XML verisini zipleme işlemi yapıyor.
        /// </summary>
        /// <returns>Zipli XML </returns>
        public static byte[] CompressFile(byte[] xml, object fileName)
        {
           

            MemoryStream zipStream = new MemoryStream();

            using (ZipArchive zip = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
            {
                ZipArchiveEntry zipElaman = zip.CreateEntry(fileName + ".xml");
                Stream entryStream = zipElaman.Open();
                entryStream.Write(xml, 0, xml.Length);
                entryStream.Flush();
                entryStream.Close();
            }
            zipStream.Position = 0;
            return zipStream.ToArray();

        }


        /// <summary>
        /// Zipli datayı zipten çıkarır
        /// </summary>
        /// <returns>Zipten çıkarılan data</returns>
        public static byte[] UncompressFile(byte[] docData)
        {
            byte[] zipsizData = { };
            MemoryStream zippedStream = new MemoryStream(docData);
            using (ZipArchive archive = new ZipArchive(zippedStream))
            {

                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    MemoryStream ms = new MemoryStream();
                    Stream zipStream = entry.Open();
                    zipStream.CopyTo(ms);
                    zipsizData = ms.ToArray();
                }

            }
            return zipsizData;
        }
    }

}

    
       

