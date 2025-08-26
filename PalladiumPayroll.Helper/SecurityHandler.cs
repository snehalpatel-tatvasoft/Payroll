using System.Security.Cryptography;
using System.Text;

namespace PalladiumPayroll.Helper
{
    public static class SecurityHandler
    {
        private readonly static string SECRET_KEY = "$XPalladium$";
        private readonly static string DEFAULT_KEY = "#Payroll@";
        private readonly static int FIX_ITERATE = 120218;

        public static string Encrypt(string input, string? key = "", int iterate = 0)
        {
            if (string.IsNullOrEmpty(key))
            {
                key = SECRET_KEY;
            }
            string EncryptionKey = string.Concat(DEFAULT_KEY, "#", key);
            byte[] clearBytes = Encoding.Unicode.GetBytes(input);
            using (Aes encryptor = Aes.Create())
            {
                using (var pdb = new Rfc2898DeriveBytes(EncryptionKey, [0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76], FIX_ITERATE + iterate, HashAlgorithmName.SHA256))
                {
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                        }
                        input = Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            return input;
        }

        public static string Decrypt(string encryptedValue, string key = "", int iterate = 0)
        {
            if (string.IsNullOrEmpty(key))
            {
                key = string.Concat("#", SECRET_KEY);
            }
            string EncryptionKey = string.Concat(DEFAULT_KEY, key);
            byte[] cipherBytes = Convert.FromBase64String(encryptedValue);
            using (Aes encryptor = Aes.Create())
            {
                using (var pdb = new Rfc2898DeriveBytes(EncryptionKey, [0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76], FIX_ITERATE + iterate, HashAlgorithmName.SHA256))
                {
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        encryptedValue = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
            }
            return encryptedValue;
        }
    }
}
