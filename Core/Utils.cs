using System;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;

namespace Core
{
    class Utils
    {
        internal static Bitmap BytesArrayToBitmap(byte[] arr)
        {
            using (var ms = new MemoryStream((byte[])arr))
            {
                return new Bitmap(ms);
            }
        }

        internal static string CulculateSHA1Hash(byte[] byteArray)
        {
            using (SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
            {
                return Convert.ToBase64String(sha1.ComputeHash(byteArray));
            }
        }
    }
}
