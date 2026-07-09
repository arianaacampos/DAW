using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Criptografia
    {
        public static string EncriptarSHA256(string textoPlano)
        {
            // Instanciamos el algoritmo criptográfico SHA-256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Convertimos el texto a bytes y lo encriptamos
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(textoPlano));

                // Convertimos los bytes encriptados a un string hexadecimal (el hash largo)
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
