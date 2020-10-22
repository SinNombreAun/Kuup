using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace Funciones.Kuup.Cifrado
{
    public static class MoCifrado
    {
        private static readonly int tamanyoClave = 32;
        private static readonly int tamanyoVector = 16;
        private static readonly String Clave = "Kuup&1231";
        private static readonly String Vector = "Kuup de Ventor & 12345";
        public static byte[] Key = UTF8Encoding.UTF8.GetBytes(Clave);
        public static byte[] IV = UTF8Encoding.UTF8.GetBytes(Vector);

        [System.Diagnostics.DebuggerHidden()]
        public static String Cifrado(String TextoACifrar)
        {
            Array.Resize(ref Key, tamanyoClave);
            Array.Resize(ref IV, tamanyoVector);
            Rijndael RijndaelAlg = Rijndael.Create();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, RijndaelAlg.CreateEncryptor(Key, IV), CryptoStreamMode.Write);
            byte[] txtPlanoBytes = UTF8Encoding.UTF8.GetBytes(TextoACifrar);
            cryptoStream.Write(txtPlanoBytes, 0, txtPlanoBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherMessageBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();

            return Convert.ToBase64String(cipherMessageBytes);
        }
        [System.Diagnostics.DebuggerHidden()]
        public static String Descifrado(String TextoADescifrar)
        {
            Array.Resize(ref Key, tamanyoClave);
            Array.Resize(ref IV, tamanyoVector);
            byte[] cipherTextBytes = Convert.FromBase64String(TextoADescifrar);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length]; 
            Rijndael RijndaelAlg = Rijndael.Create();
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, RijndaelAlg.CreateDecryptor(Key, IV), CryptoStreamMode.Read);
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();

            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }
    }
}
