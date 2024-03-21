namespace MagicSDK.Ext.OAuth.Modules
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using System.Linq;

    internal class OAuthChallenge
    {
        public string State { get; private set; }
        public string Verifier { get; private set; }
        public string Challenge { get; private set; }

        public OAuthChallenge()
        {
            State = CreateRandomString(128);
            Verifier = CreateRandomString(128);
            Challenge = HexToBase64UrlSafe(ComputeSha256Hash(Verifier));
        }

        private static string CreateRandomString(int size)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-._~";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, size).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private static string HexToBase64UrlSafe(string hexString)
        {
            var bytes = new byte[hexString.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }

            var base64 = Convert.ToBase64String(bytes);
            return base64.Replace("+", "-").Replace("/", "_").Replace("=", "");
        }

        private static string ComputeSha256Hash(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }

}