using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace TerritoryLocator.Encryptor
{
    public static class ExceligentPasswordEncryptor
    {
        # region Public Methods
        public static String CreateXceligentPassword(char[] password)
        {
            var salt = new byte[4];
            new Random().NextBytes(salt);
            byte[] pass = EncodeXceligentPassword(password, salt);

            var passandsalt = new byte[pass.Length + salt.Length];
            Smudge(passandsalt);
            for (int b = 0; b < pass.Length; b++)
                passandsalt[b] = (byte)pass[b];
            for (int b = 0; b < salt.Length; b++)
                passandsalt[pass.Length + b] = salt[b];

            var encpass = "{SSHA}" + Convert.ToBase64String(passandsalt);

            return encpass;
        }
        # endregion Public Methods

        # region Private Methods
        private static void Smudge(byte[] pwd)
        {
            if (null == pwd) return;
            for (var b = 0; b < pwd.Length; b++)
                pwd[b] = 0;
        }


        private static byte[] EncodeXceligentPassword(char[] password, byte[] salt)
        {
            var hashstr = new byte[password.Length + salt.Length];

            Smudge(hashstr);

            for (var b = 0; b < password.Length; b++)
                hashstr[b] = (byte)password[b];
            for (var b = 0; b < salt.Length; b++)
                hashstr[password.Length + b] = salt[b];

            return CryptPassword(hashstr);
        }

        private static byte[] CryptPassword(byte[] pwd)
        {

            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] password = sha.ComputeHash(pwd);

            return password;
        }

        # endregion Private Methods
    }
}