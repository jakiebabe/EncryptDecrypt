using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using log4net;

namespace EncryptDecrypt.Controller
{
    public class AesController
    {
        private static readonly ILog log =
        LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string EncryptString(string text)
        {
            string key = "Xc4u7x!A%D*G-KaPlSr56tp2s5v8y/B?";
            byte[] iv = new byte[16];
            byte[] array;
            try
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(key);
                    aes.IV = iv;
                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                            {
                                streamWriter.Write(text);
                            }
                            array = memoryStream.ToArray();
                        }
                    }
                }
            }
            catch
            {
                return("An error has encountered while encrypting.");
            }
            string encryptedString = Convert.ToBase64String(array);
            return (encryptedString);
        }

        public string DecryptString(string cipherText)
        {
            string decryptedText = "";
            try
            {
                string key = "Xc4u7x!A%D*G-KaPlSr56tp2s5v8y/B?";
                byte[] iv = new byte[16];
                byte[] buffer = Convert.FromBase64String(cipherText);
                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(key);
                    aes.IV = iv;
                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (MemoryStream memoryStream = new MemoryStream(buffer))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader streamReader = new StreamReader(cryptoStream))
                            {
                                decryptedText = streamReader.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                log.Error("Error decrypting text. " + ex.ToString());
                return ("An error has encountered while decrypting.");
            }
            return (decryptedText);
        }
    }
}
