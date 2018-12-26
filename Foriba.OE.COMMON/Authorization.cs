using System;
using System.Text;

/// <summary>
/// Copyright © 2018 Foriba Teknoloji
/// Bu proje örnek bir web servis test projesidir. Yalnızca test sisteminde çalışmaktadır.
/// Version Info    : Version.txt
/// Readme          : Readme.txt
/// </summary>

namespace Foriba.OE.COMMON
{
    public static class Authorization
    {
        /// <summary>
        /// Basic Login
        /// </summary>
        /// <returns>Basic Login</returns>
        public static string GetAuthorization(string username, string pass)
        {
            string authorization = username + ":" + pass;
            byte[] byteArray = Encoding.UTF8.GetBytes(authorization);
            var base64Authorization = Convert.ToBase64String(byteArray);

            return $"Basic {base64Authorization}";
        }
    }
}
