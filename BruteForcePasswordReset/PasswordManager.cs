using System;
using System.Security.Cryptography;
using System.Text;

namespace BruteForcePasswordReset
{
    public class PasswordManager
    {
        internal static readonly string Salt = "staticSalt"; // Static salt for simplicity
        public string EncryptedPassword { get; private set; } = string.Empty;

        public void CreatePassword(string password)
        {
            EncryptedPassword = EncryptionHelper.EncryptPassword(password, Salt);
        }
    }
}
