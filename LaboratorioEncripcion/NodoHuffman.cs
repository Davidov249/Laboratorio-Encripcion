using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioEncripcion
{
    internal class NodoHuffman
    {
        internal NodoHuffman Izquierdo = null;

        internal NodoHuffman Derecho = null;

        internal DatoHuffman Dato = null;

        internal String Codigo = "";

        internal double Probabilidad = 0.0;

        public NodoHuffman(DatoHuffman dato, String concatenar)
        {
            Dato = dato;
            Probabilidad = dato.Probabilidad;
        }

        public NodoHuffman(NodoHuffman iz, NodoHuffman dr)
        {
            Izquierdo = iz;
            Derecho = dr;
            Probabilidad = iz.Probabilidad + dr.Probabilidad;
        }

        public bool esHoja()
        {
            if (Dato != null)
            {
                return true;
            }
            return false;
        }

        public void CalcularProbabilidad()
        {
            Probabilidad = Dato.Probabilidad;
        }
    }
}
