using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioEncripcion
{
    public class RSA
    {
        public int P { get; private set; }
        public int Q { get; private set; }
        public int n { get; private set; }
        public double Fi { get; private set; }
        public double e { get; private set; }
        public double d { get; private set; }

        public RSA(int p, int q)
        {
            MD5 hashMD5 = MD5.Create();
            P = p;
            Q = q;
            try
            {
                Obtenern();
                Obtenerfi();
                Obtenere(3);
                Obtenerd();
            }
            catch (Exception)
            {

                throw;
            }
            

        }

        public string llavePublica()
        {
            MD5 hashMD5 = MD5.Create();
            Hexadecimal hex = new Hexadecimal(n);
            Hexadecimal hex2 = new Hexadecimal(Convert.ToInt32(e));
            string llavepublica = hex.getHexadecimal() + hex2.getHexadecimal();

            return Hashear(hashMD5, llavepublica);
        }

        public string llavePrivada()
        {
            MD5 hashMD5 = MD5.Create();
            Hexadecimal hex = new Hexadecimal(n);
            Hexadecimal hex2 = new Hexadecimal(Convert.ToInt32(d));
            string llaveprivada = hex.getHexadecimal() + hex2.getHexadecimal();

            return Hashear(hashMD5, llaveprivada);
        }

        public bool VerificarPrimos(int p, int q)
        {
            int var = 0;
            int var2 = 0;
            for (int i = 1; i < p + 1; i++)
            {
                if (p % i == 0)
                {
                    var++;
                }
            }
            for (int i = 1; i < q + 1; i++)
            {
                if (p % i == 0)
                {
                    var2++;
                }
            }
            if (p == 1 && q == 1)
            {
                return true;
            }
            else if (p == 1 && var2 == 2)
            {
                return true;
            }
            else if (var == 2 && q == 1)
            {
                return true;
            }
            else if (var == 2 && var2 == 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Obtenern()
        {
            n = P * Q;
        }

        public void Obtenerfi()
        {
            Fi = (P - 1) * (Q - 1);
        }

        public void Obtenere(int numero)
        {
            e = numero;
        }

        public void Obtenerd()
        {
            d = 1;
            while ((e * d) % Fi != 1)
            {
                d++;
            }
        }

        public double Encriptar(int N)
        {
            return Math.Pow(N, e) % n;
        }

        public double Desencriptar(int C)
        {
            return Math.Pow(C, d) % n;
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
