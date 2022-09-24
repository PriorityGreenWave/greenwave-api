using System.Security.Cryptography;
using System.Text;

namespace priority_green_wave_api.Utils
{
    public class MD5Utils
    {
        public static string GetHashMD5(string input)
        {
            MD5 md5 = MD5.Create();
            var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder builder = new StringBuilder();
            foreach (var b in bytes)
            {
                builder.Append(b.ToString("X2"));
            }
            return builder.ToString();
        }
    }
}
