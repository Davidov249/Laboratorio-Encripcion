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
            char[] caracteres = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
            BloqueAES bloque = new BloqueAES(caracteres);
            RSA Privada = new RSA(1117, 1123);
            string llave = Privada.llavePrivada();
            Console.ReadKey();
        }
    }
}
