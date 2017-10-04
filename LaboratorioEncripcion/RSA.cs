using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioEncripcion
{
    class RSA
    {
        public int P { get; private set; }
        public int Q { get; private set; }
        public int N { get; private set; }
        public int Fi { get; private set; }
        public int e { get; private set; }
        public int d { get; private set; }

        public RSA(int p, int q)
        {
            P = p;
            Q = q;
        }

        public void Obtenern()
        {
            N = P * Q;
        }

        public void Obtenerfi()
        {
            Fi = (P - 1) * (Q - 1);
        }

        public void Obtenere(int numero)
        {
            e = numero;
        }



    }
}
