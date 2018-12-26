using System;


/// <summary>
/// Copyright © 2018 Foriba Teknoloji
/// Bu proje örnek bir web servis test projesidir. Yalnızca test sisteminde çalışmaktadır.
/// Version Info    : Version.txt
/// Readme          : Readme.txt
/// </summary>

namespace Foriba.OE.COMMON
{
    public class TextModel
    {
        public string VknTckn { get; set; }
        public string Kullanici { get; set; }
        public string Sifre { get; set; }
        public string GbEtiketi { get; set; }
        public string PkEtiketi { get; set; }
        public string FaturaUUID { get; set; }
        public string FaturaID { get; set; }
        public string Sube { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime EndDate { get; set; }
       
    }
}
