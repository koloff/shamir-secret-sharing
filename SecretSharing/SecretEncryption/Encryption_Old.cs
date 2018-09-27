namespace SecretSharing.SecretEncryption
{
    using System.Security.Cryptography;
    using System;

    public class Encryption_Old
    {
        private static byte[] IV = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };

        public static string Encrypt(string text,  byte[] key)
        {
            byte[] plaintextbytes = System.Text.Encoding.ASCII.GetBytes(text);
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
           // aes.BlockSize = 128;
            ////aes.KeySize = 256;
            aes.Key = key;
            //aes.IV = IV;
            aes.Padding = PaddingMode.None;
            aes.Mode = CipherMode.CBC;
            ICryptoTransform crypto = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] encrypted = crypto.TransformFinalBlock(plaintextbytes, 0, plaintextbytes.Length);
            crypto.Dispose();

            return Convert.ToBase64String(encrypted);
        }

        public static string Decrypt(string encrypted, byte[] key)
        {
            byte[] encryptedbytes = Convert.FromBase64String(encrypted);
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
           // aes.BlockSize = 128;
            aes.KeySize = 256;
            aes.Key = key;
            //aes.IV = IV;
            aes.Padding = PaddingMode.None;
            aes.Mode = CipherMode.CBC;
            ICryptoTransform crypto = aes.CreateDecryptor(aes.Key, aes.IV);
            byte[] secret = crypto.TransformFinalBlock(encryptedbytes, 0, encryptedbytes.Length);
            crypto.Dispose();

            return System.Text.Encoding.ASCII.GetString(secret);
        }
    }
}
