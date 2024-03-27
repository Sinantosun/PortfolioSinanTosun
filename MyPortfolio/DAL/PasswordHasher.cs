

using System.Security.Cryptography;
using System.Text;

namespace MyPortfolio.DAL
{
    public class PasswordHasher
    {
        public static string HashPassword(string pwd)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(pwd));
            byte[] result = md5.Hash;
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                str.Append(result[i].ToString("x2"));
            }
            return str.ToString();
        }
    }
}