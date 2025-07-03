using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DTOs.DTOs;
using System.Security.Cryptography;
using System.Text;

namespace PalladiumPayroll.Helper.Middleware.Encryption
{
    public class EncryptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly EncryptionSetting? _encryptionSetting;
        public EncryptionMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _encryptionSetting = AppSettingsConfig.GetSection<EncryptionSetting>(configuration, "AESEncryption");
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (_encryptionSetting?.Enable == true)
            {
                List<string> excludeURL = GetExcludeURLList();
                if (httpContext.Request.Path.HasValue && !excludeURL.Contains(httpContext.Request.Path.Value))
                {
                    httpContext.Request.Body = DecryptStream(httpContext.Request.Body);
                    if (httpContext.Request.QueryString.HasValue && !string.IsNullOrEmpty(httpContext.Request.QueryString.Value))
                    {
                        string decryptedString = DecryptString(httpContext.Request.QueryString.Value.Substring(1));
                        httpContext.Request.QueryString = new QueryString($"?{decryptedString}");
                    }
                }
            }
            await _next(httpContext);
        }

        // Main functions that decrypt the payload and parameter which pass from the frontend.
        #region Encryption algo.
        private Aes GetEncryptionAlgorithm()
        {
            Aes aes = Aes.Create();
            var secret_key = Encoding.UTF8.GetBytes(_encryptionSetting.Key);
            var initialization_vector = Encoding.UTF8.GetBytes(_encryptionSetting.IV);
            aes.Key = secret_key;
            aes.IV = initialization_vector;
            return aes;
        }

        private Stream DecryptStream(Stream cipherStream)
        {
            Aes aes = GetEncryptionAlgorithm();
            FromBase64Transform base64Transform = new FromBase64Transform(FromBase64TransformMode.IgnoreWhiteSpaces);
            CryptoStream base64DecodedStream = new CryptoStream(cipherStream, base64Transform, CryptoStreamMode.Read);
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            CryptoStream decryptedStream = new CryptoStream(base64DecodedStream, decryptor, CryptoStreamMode.Read);
            return decryptedStream;
        }

        private string DecryptString(string cipherText)
        {
            Aes aes = GetEncryptionAlgorithm();
            byte[] buffer = Convert.FromBase64String(cipherText);
            MemoryStream memoryStream = new MemoryStream(buffer);
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            StreamReader streamReader = new StreamReader(cryptoStream);
            return streamReader.ReadToEnd();
        }

        #endregion


        // This are excluded URL from encrypt-decrypt
        // we added in angular side and as well as in ASP.NET CORE side.
        private static List<string> GetExcludeURLList()
        {
            List<string> excludeURL = new List<string>();
            excludeURL.Add("/api/Home/AddUpdateEmployee");
            excludeURL.Add("/api/Home/AddUpdateStaff");
            return excludeURL;
        }
    }

}
