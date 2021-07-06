using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AesEncEndToEnd
{
    public class ClientEncrptionClass
    {
        public static string EncryptAESEncEndToEnd(string Decryptvalue)
        {
            string IV = Keys.IV;     // 16 chars=128 bytes
            string key = Keys.key; // 32 char =256 bytes
            byte[] textbytes = ASCIIEncoding.ASCII.GetBytes(Decryptvalue);
            AesCryptoServiceProvider acs = new AesCryptoServiceProvider();
            acs.BlockSize = 128;
            acs.KeySize = 256;
            acs.Key = ASCIIEncoding.ASCII.GetBytes(key);
            acs.IV = ASCIIEncoding.ASCII.GetBytes(IV);
            acs.Padding = PaddingMode.PKCS7;
            acs.Mode = CipherMode.CBC;
            ICryptoTransform icrypt = acs.CreateEncryptor(acs.Key, acs.IV);
            byte[] enc = icrypt.TransformFinalBlock(textbytes, 0, textbytes.Length);
            icrypt.Dispose();
            string base64string = Convert.ToBase64String(enc);//.Replace('/', '|');
            return base64string;
        }
        public static string DecryptAESEncEndToEnd(string Encryptvalue)
        {
            try
            {
                string replacevalue = Encryptvalue;//.Replace('|', '/');
                string IV = Keys.IV;     // 16 chars=128 bytes
                string key = Keys.key; // 32 char =256 bytes
                byte[] encbytes = Convert.FromBase64String(replacevalue);
                AesCryptoServiceProvider acs = new AesCryptoServiceProvider();
                acs.BlockSize = 128;
                acs.KeySize = 256;
                acs.Key = ASCIIEncoding.ASCII.GetBytes(key);
                acs.IV = ASCIIEncoding.ASCII.GetBytes(IV);
                acs.Padding = PaddingMode.PKCS7;
                acs.Mode = CipherMode.CBC;
                ICryptoTransform icrypt = acs.CreateDecryptor(acs.Key, acs.IV);
                byte[] dec = icrypt.TransformFinalBlock(encbytes, 0, encbytes.Length);
                icrypt.Dispose();

                return ASCIIEncoding.ASCII.GetString(dec);
            }
            catch (Exception)
            {
                string replacevalue = Encryptvalue.Replace('|', '/').Replace(" ", "+");
                string IV = Keys.IV;     // 16 chars=128 bytes
                string key = Keys.key; // 32 char =256 bytes
                byte[] encbytes = Convert.FromBase64String(replacevalue);
                AesCryptoServiceProvider acs = new AesCryptoServiceProvider();
                acs.BlockSize = 128;
                acs.KeySize = 256;
                acs.Key = ASCIIEncoding.ASCII.GetBytes(key);
                acs.IV = ASCIIEncoding.ASCII.GetBytes(IV);
                acs.Padding = PaddingMode.PKCS7;
                acs.Mode = CipherMode.CBC;
                ICryptoTransform icrypt = acs.CreateDecryptor(acs.Key, acs.IV);
                byte[] dec = icrypt.TransformFinalBlock(encbytes, 0, encbytes.Length);
                icrypt.Dispose();

                return ASCIIEncoding.ASCII.GetString(dec);
            }
        }
    }
}
