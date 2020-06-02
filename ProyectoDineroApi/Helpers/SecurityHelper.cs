using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoDineroApi.Helpers
{
    public class SecurityHelper
    {
        public static String GenerateSalt()
        {
            Random rnd = new Random();
            String salt = "";
            for (int i = 1; i <= 20; i++)
            {
                int random = rnd.Next(1, 255);
                char letter = Convert.ToChar(random);
                salt += letter;
            }
            return salt;
        }

        public static String CreateHash(String pass, String salt)
        {
            SHA256Managed sha = new SHA256Managed();
            byte[] output;
            output = Encoding.UTF8.GetBytes(pass + salt);
            //HACEMOS EL CIFRADO n VECES
            for (int i = 1; i <= 10; i++)
            {
                output = sha.ComputeHash(output);
            }
            return Encoding.UTF8.GetString(output);
        }
    }
}
