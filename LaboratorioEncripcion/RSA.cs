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
            Obtenern();
            Obtenerfi();
            Obtenere(3);
            Obtenerd();
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
            e = Fi - 1;
            //Random azar = new Random();
            //e = azar.Next(1, Convert.ToInt32(Fi));
            //double g = Euclids(e, Fi);
            //while (g != 1)
            //{
            //    e = azar.Next(1, Convert.ToInt32(Fi));
            //    g = Euclids(e, Fi);
            //}

        }

        public void Obtenerd()
        {
            d = INV(e, Fi);
            //while ((e * d) % Fi != 1)
            //{
            //    d++;
            //}
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

        public double INV(double e, double fi)
        {
            double d = 0;
            double x1 = 0;
            double x2 = 1;
            double y1 = 1;
            double fitemp = fi;

            while (e > 0)
            {
                double temp1 = fitemp / e;
                double temp2 = fitemp - temp1 * e;
                fitemp = e;
                e = temp2;
                double x = x2 - temp1 * x1;
                double y = d - temp1 * y1;
                x2 = x1;
                x1 = x;
                d = y1;
                y1 = y;
            }

            if (fitemp == 1)
            {
                return d + fi;
            }
            else
            {
                return 0;
            }

        }

        public double Euclids(double a, double b)
        {
            while (b != 0)
            {
                a = b % b;
                b = a % b;
            }
            return a;
        }
    }
}