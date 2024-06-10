using System;
using System.Security.Cryptography;
using System.Text;

namespace PcPoint
{
    public class SecureData
    {
        public static string EncryptData(string simpleString)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            byte[] passwordHash = Encoding.UTF8.GetBytes(simpleString);

            return Encoding.UTF8.GetString(md5.ComputeHash(passwordHash));
        }
    }
}
