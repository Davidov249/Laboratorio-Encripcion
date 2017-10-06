using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace LaboratorioEncripcion
{
    public class AES_128
    {
        private List<BloqueAES> Bloques { get; set; }
        private BloqueAES Clave { get; set; }
        private BloqueAES SubClaves { get; set; }

        public AES_128(string archivo, string clave)
        {
            MD5 hashMD5 = MD5.Create();
            string claveHash = Hashear(hashMD5, clave);
            char[] claveChars = new char[16];
            for (int i = 0; i < 16; i++)
            {
                claveChars[i] = clave[i];
            }
            Clave = new BloqueAES(claveChars);
        }

        public string Hashear(MD5 hasher, string entrada)
        {
            byte[] datos = hasher.ComputeHash(Encoding.UTF8.GetBytes(entrada));
            StringBuilder hashed = new StringBuilder();
            for (int i = 0; i < datos.Length; i++)
            {
                hashed.Append(datos[i].ToString("x2"));
            }
            return hashed.ToString();
        }
    }
}
