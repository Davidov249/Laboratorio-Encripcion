using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioEncripcion
{
    class Program
    {
        static void Main(string[] args)
        {
            string archivo = @"C:\Users\Axel Rodriguez\Downloads\git-commands.txt";
            string salida = "bb";
            PGP pgp = new PGP("EzioA", "pgp123", true, archivo, salida);
            pgp.Cifrar();
        }
    }
}
