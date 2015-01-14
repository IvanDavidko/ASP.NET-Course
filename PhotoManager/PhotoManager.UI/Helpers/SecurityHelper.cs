using System;
using System.Security.Cryptography;
using Domain.Models;

namespace PhotoManager.UI.Helpers
{
    public static class SecurityHelper
    {
        public const int SALT_BYTE_SIZE = 24;
        public const int HASH_BYTE_SIZE = 24;
        public const int PBKDF2_ITERATIONS = 1000;

        public static User CreateUser(string password)
        {
            var csprng = new RNGCryptoServiceProvider();
            var salt = new byte[SALT_BYTE_SIZE];
            csprng.GetBytes(salt);

            return new User()
            {
                Password = Hash(password, salt),
                PasswordSalt = Convert.ToBase64String(salt)
            };
        }
        
        public static string Hash(string password, byte[] salt)
        {
            // Hash the password and encode the parameters
            var hash = PBKDF2(password, salt, PBKDF2_ITERATIONS, HASH_BYTE_SIZE);
            return Convert.ToBase64String(salt) + Convert.ToBase64String(hash);
        }

        public static bool CheckUserPassword(string loginPassword, string salt, string passwordFromDb)
        {
            var hash = Convert.FromBase64String(passwordFromDb);
            var testHash = salt + Convert.ToBase64String(PBKDF2(loginPassword, Convert.FromBase64String(salt), PBKDF2_ITERATIONS, HASH_BYTE_SIZE));


            return SlowEquals(hash, Convert.FromBase64String(testHash));
        }
        private static bool SlowEquals(byte[] a, byte[] b)
        {
            uint diff = (uint)a.Length ^ (uint)b.Length;
            for (var i = 0; i < a.Length && i < b.Length; i++)
                diff |= (uint)(a[i] ^ b[i]);
            return diff == 0;
        }

        private static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt) { IterationCount = iterations };

            return pbkdf2.GetBytes(outputBytes);
        }
    }
}