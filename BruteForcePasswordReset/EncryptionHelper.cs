using System.Security.Cryptography;
using System.Text;

namespace BruteForcePasswordReset
{
    public static class EncryptionHelper
    {
        public static string EncryptPassword(string password, string salt)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                var saltedPassword = password + salt;
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
