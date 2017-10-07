using System;
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
        public int Fi { get; private set; }
        public int e { get; private set; }
        public int d { get; private set; }

        public RSA(int p, int q)
        {
            P = p;
            Q = q;
            Obtenern();
            Obtenerfi();
            Obtenere(p);
            Obtenerd();
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
            while ((e*d)%Fi != 1)
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


    }
}
