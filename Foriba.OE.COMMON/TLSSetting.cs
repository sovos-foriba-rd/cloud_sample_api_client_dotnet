using System.Collections;
using System.Net;


/// <summary>
/// Copyright © 2018 Foriba Teknoloji
/// Bu proje örnek bir web servis test projesidir. Yalnızca test sisteminde çalışmaktadır.
/// Version Info    : Version.txt
/// Readme          : Readme.txt
/// </summary>

namespace Foriba.OE.COMMON
{
    public static class TlsSetting
    {
        /// <summary>
        /// SSL/TLS ayarlamalarını yapar
        /// </summary>
        /// <returns>SSL/TLS</returns>
        public static SecurityProtocolType TlsSet(ArrayList list)
        {
            SecurityProtocolType protocolType = new SecurityProtocolType();

            int caseSwitch = list.Count;

            switch (caseSwitch)
            {
                case 1:
                    protocolType = list.Contains("TLS v1.2") ? SecurityProtocolType.Tls12 : SecurityProtocolType.Tls11;

                    break;

                case 2:

                    protocolType = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                    break;
            }

            return protocolType;



        }

    }
}
