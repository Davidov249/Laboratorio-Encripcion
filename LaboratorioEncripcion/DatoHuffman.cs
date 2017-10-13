using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioEncripcion
{
    internal class DatoHuffman
    {
        public String Caracter { get; private set; }
        public int Apariciones { get; set; }
        public int TotalTexto { get; set; }
        public double Probabilidad
        {
            get { return (Convert.ToDouble(Apariciones) / Convert.ToDouble(TotalTexto)); }
            private set { }
        }

        public DatoHuffman(String caracter)
        {
            Caracter = caracter;
        }
    }
}
