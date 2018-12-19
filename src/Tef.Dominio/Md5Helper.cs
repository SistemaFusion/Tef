using System;
using System.Security.Cryptography;
using System.Text;

namespace Tef.Dominio
{
    internal static class Md5Helper
    {
        public static string ComputaUnique()
        {
            var guid = Guid.NewGuid();

            var guidRandom = Convert.ToBase64String(guid.ToByteArray());

            return Computar(DateTime.Now.ToString("O") + guidRandom);
        }

        public static string Computar(string input)
        {
            using (var md5Hash = MD5.Create())
            {
                var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sBuilder = new StringBuilder();
                foreach (var t in data)
                {
                    sBuilder.Append(t.ToString("x2"));
                }

                return sBuilder.ToString();
            }
        }

        public static byte[] ComputarByte(string input)
        {
            using (var md5Hash = MD5.Create())
            {
                var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                return data;
            }
        }
    }
}