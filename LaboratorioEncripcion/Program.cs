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
            string archivo = @"C:\Users\Axel Rodriguez\Downloads\git-commandsbb.txt.cif";
            string salida = @"C:\cript\keybb.clp";
            PGP pgp = new PGP(archivo, salida);
            pgp.Descifrar();
        }
    }
}
