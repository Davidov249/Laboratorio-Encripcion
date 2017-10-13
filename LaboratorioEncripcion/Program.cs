using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioEncripcion
{
    class Program
    {
        public static String Query = "";
        private static int pos = -1;
        private static String Usuario = "";
        private static String Password = "";
        private static PGP Cifrado = null;

        static void Main(string[] args)
        {
            //string archivo = @"C:\Users\Axel Rodriguez\Downloads\git-commands.txt";
            //string salida = "bb";
            //PGP pgp = new PGP("EzioA", "pgp123", true, archivo, salida);
            //pgp.Cifrar();

            Console.WriteLine("Ingrese su usuario");
            Usuario = Console.ReadLine();
            Console.WriteLine("Ingrese su password");
            Password = Console.ReadLine();
            Console.WriteLine("Favor ingresar la acción a realizar.");
            Query = Console.ReadLine();
            string archivo = "";
            string clave = "";
            string salida = "";
            string[] texto = Query.Split(' ');
            if (texto[0] == "-c")
            {
                int max = texto[2].Length;
                char[] ubicacion = texto[2].ToCharArray();
                for (int i = 3; i < max - 1; i++)
                {
                    archivo += ubicacion[i].ToString();
                }
                int maxim = texto[1].Length;
                char[] chars = texto[1].ToCharArray();
                for (int i = 2; i < maxim; i++)
                {
                    salida += chars[i].ToString();
                }
                Cifrado = new PGP(Usuario, Password, true, archivo, salida);
                Console.WriteLine("El archivo se cifró con exito.");
            }
            else if (texto[0] == "-d")
            {
                int max = texto[1].Length;
                char[] ubicacion = texto[1].ToCharArray();
                for (int i = 2; i < max; i++)
                {
                    salida += ubicacion[i].ToString();
                }
                int maxim = texto[2].Length;
                char[] chars = texto[2].ToCharArray();
                for (int i = 3; i < maxim - 1; i++)
                {
                    clave += chars[i].ToString();
                }
                Cifrado = new PGP(salida, clave);
                Console.WriteLine("El archivo se descifró con exito");
            }

            //try
            //{
            //    bool stop = false;
            //    while (!stop)
            //    {
            //        Query = Console.ReadLine();
            //        String ubicacion = "";
            //        String salida = "";
            //        ComerEspacios();
            //        pos--;
            //        String c = leerCaracter();
            //        switch (c)
            //        {
            //            case "-":
            //                switch (leerCaracter())
            //                {
            //                    case "s":
            //                        ComerEspacios();
            //                        pos--;
            //                        c = leerCaracter();
            //                        if (c == "\"")
            //                        {
            //                            c = leerCaracter();
            //                            while (c != "\"")
            //                            {
            //                                salida += c;
            //                                c = leerCaracter();
            //                            }
            //                        }
            //                        ComerEspacios();
            //                        pos--;
            //                        switch (leerCaracter())
            //                        {
            //                            case "-":
            //                                switch (leerCaracter())
            //                                {
            //                                    case "c":
            //                                        ComerEspacios();
            //                                        pos--;
            //                                        switch (leerCaracter())
            //                                        {
            //                                            case "-":
            //                                                switch (leerCaracter())
            //                                                {
            //                                                    case "f":
            //                                                        ComerEspacios();
            //                                                        pos--;
            //                                                        ComerEspacios();
            //                                                        pos--;
            //                                                        c = leerCaracter();
            //                                                        if (c == "\"")
            //                                                        {
            //                                                            c = leerCaracter();
            //                                                            while (c != "\"")
            //                                                            {
            //                                                                ubicacion += c;
            //                                                                c = leerCaracter();
            //                                                            }
            //                                                        }
            //                                                        //ComerEspacios();
            //                                                        //pos--;
            //                                                        PGP Cifrado = new PGP(Usuario, Password, true, ubicacion, salida);
            //                                                        Cifrado.Cifrar();

            //                                                        break;
            //                                                    default:
            //                                                        throw new FormatException("Se esperaba una acción: \r\n\t-c -f\"UbicacionDelArchivo\" -d -f\"UbicacionDelArchivo\" -s\"NombreDeArchivoSalida\"  [Nombre del archivo luego de comprimir]");
            //                                                }
            //                                                break;
            //                                            default:
            //                                                throw new FormatException("Se esperaba una acción: \r\n\t-c -f\"UbicacionDelArchivo\" -d -f\"UbicacionDelArchivo\" -s\"NombreDeArchivoSalida\"  [Nombre del archivo luego de comprimir]");
            //                                        }
            //                                        break;
            //                                    case "d":
            //                                        ComerEspacios();
            //                                        pos--;
            //                                        switch (leerCaracter())
            //                                        {
            //                                            case "-":
            //                                                switch (leerCaracter())
            //                                                {
            //                                                    case "f":
            //                                                        ComerEspacios();
            //                                                        pos--;
            //                                                        c = leerCaracter();
            //                                                        if (c == "\"")
            //                                                        {
            //                                                            c = leerCaracter();
            //                                                            while (c != "\"")
            //                                                            {
            //                                                                ubicacion += c;
            //                                                                c = leerCaracter();
            //                                                            }
            //                                                        }
            //                                                        PGP Cifrado = new PGP(Usuario, Password, true, ubicacion, salida);
            //                                                        Cifrado.Descifrar();
            //                                                        break;
            //                                                    default:
            //                                                        throw new FormatException("Se esperaba una acción: \r\n\t-c -f\"UbicacionDelArchivo\" -d -f\"UbicacionDelArchivo\" -s\"NombreDeArchivoSalida\"  [Nombre del archivo luego de comprimir]");
            //                                                }
            //                                                break;
            //                                            default:
            //                                                throw new FormatException("Se esperaba una acción: \r\n\t-c -f\"UbicacionDelArchivo\" -d -f\"UbicacionDelArchivo\" -s\"NombreDeArchivoSalida\"  [Nombre del archivo luego de comprimir]");
            //                                        }
            //                                        break;
            //                                    case "f":
            //                                        ComerEspacios();
            //                                        pos--;
            //                                        c = leerCaracter();
            //                                        if (c == "\"")
            //                                        {
            //                                            c = leerCaracter();
            //                                            while (c != "\"")
            //                                            {
            //                                                ubicacion += c;
            //                                                c = leerCaracter();
            //                                            }
            //                                        }
            //                                        ComerEspacios();
            //                                        pos--;
            //                                        switch (leerCaracter())
            //                                        {
            //                                            case "c":
            //                                                //ComerEspacios();
            //                                                //pos--;
            //                                                String metodo = leerCaracter();
            //                                                PGP Cifrado = new PGP(Usuario, Password, true, ubicacion, salida);
            //                                                Cifrado.Cifrar();
            //                                                break;
            //                                            case "d":
            //                                                PGP Descifrado = new PGP(Usuario, Password, true, ubicacion, salida);
            //                                                Descifrado.Descifrar();
            //                                                break;
            //                                            default:
            //                                                throw new FormatException("Se esperaba una acción: \r\n\t-c -f\"UbicacionDelArchivo\" -d -f\"UbicacionDelArchivo\" -s\"NombreDeArchivoSalida\"  [Nombre del archivo luego de comprimir]");
            //                                        }
            //                                        break;
            //                                    default:
            //                                        throw new FormatException("Se esperaba una acción: \r\n\t-c -f\"UbicacionDelArchivo\" -d -f\"UbicacionDelArchivo\" -s\"NombreDeArchivoSalida\"  [Nombre del archivo luego de comprimir]");
            //                                }
            //                                break;
            //                            default:
            //                                throw new FormatException("Se esperaba una acción: \r\n\t-c -f\"UbicacionDelArchivo\" -d -f\"UbicacionDelArchivo\" -s\"NombreDeArchivoSalida\"  [Nombre del archivo luego de comprimir]");
            //                        }
            //                        break;
            //                    case "c":
            //                        ComerEspacios();
            //                        pos--;
            //                        switch (leerCaracter())
            //                        {
            //                            case "-":
            //                                switch (leerCaracter())
            //                                {
            //                                    case "s":
            //                                        ComerEspacios();
            //                                        pos--;
            //                                        c = leerCaracter();
            //                                        if (c == "\"")
            //                                        {
            //                                            c = leerCaracter();
            //                                            while (c != "\"")
            //                                            {
            //                                                salida += c;
            //                                                c = leerCaracter();
            //                                            }
            //                                        }
            //                                        ComerEspacios();
            //                                        pos--;
            //                                        switch (leerCaracter())
            //                                        {
            //                                            case "-":
            //                                                switch (leerCaracter())
            //                                                {
            //                                                    case "f":
            //                                                        ComerEspacios();
            //                                                        pos--;
            //                                                        c = leerCaracter();
            //                                                        if (c == "\"")
            //                                                        {
            //                                                            c = leerCaracter();
            //                                                            while (c != "\"")
            //                                                            {
            //                                                                ubicacion += c;
            //                                                                c = leerCaracter();
            //                                                            }
            //                                                        }
            //                                                        //ComerEspacios();
            //                                                        //pos--;
            //                                                        String metodo = leerCaracter();
            //                                                        PGP Cifrado = new PGP(Usuario, Password, true, ubicacion, salida);
            //                                                        Cifrado.Cifrar();
            //                                                        break;
            //                                                    default:
            //                                                        throw new FormatException("Se esperaba una acción: \r\n\t-c -f\"UbicacionDelArchivo\" -d -f\"UbicacionDelArchivo\" -s\"NombreDeArchivoSalida\"  [Nombre del archivo luego de comprimir]");
            //                                                }
            //                                                break;
            //                                            default:
            //                                                throw new FormatException("Se esperaba una acción: \r\n\t-c -f\"UbicacionDelArchivo\" -d -f\"UbicacionDelArchivo\" -s\"NombreDeArchivoSalida\"  [Nombre del archivo luego de comprimir]");
            //                                        }
            //                                        break;
            //                                    case "f":
            //                                        ComerEspacios();
            //                                        pos--;
            //                                        c = leerCaracter();
            //                                        if (c == "\"")
            //                                        {
            //                                            c = leerCaracter();
            //                                            while (c != "\"")
            //                                            {
            //                                                ubicacion += c;
            //                                                c = leerCaracter();
            //                                            }
            //                                        }
            //                                        ComerEspacios();
            //                                        pos--;
            //                                        switch (leerCaracter())
            //                                        {
            //                                            case "-":
            //                                                switch (leerCaracter())
            //                                                {
            //                                                    case "s":
            //                                                        ComerEspacios();
            //                                                        pos--;
            //                                                        c = leerCaracter();
            //                                                        if (c == "\"")
            //                                                        {
            //                                                            c = leerCaracter();
            //                                                            while (c != "\"")
            //                                                            {
            //                                                                salida += c;
            //                                                                c = leerCaracter();
            //                                                            }
            //                                                        }
            //                                                        //ComerEspacios();
            //                                                        //pos--;
            //                                                        String metodo = leerCaracter();
            //                                                        PGP Cifrado = new PGP(Usuario, Password, true, ubicacion, salida);
            //                                                        Cifrado.Cifrar();
            //                                                        break;
            //                                                    default:
            //                                                        throw new FormatException("Se esperaba una acción: \r\n\t-c -f\"UbicacionDelArchivo\" -d -f\"UbicacionDelArchivo\" -s\"NombreDeArchivoSalida\"  [Nombre del archivo luego de comprimir]");
            //                                                }
            //                                                break;
            //                                            default:
            //                                                throw new FormatException("Se esperaba una acción: \r\n\t-c -f\"UbicacionDelArchivo\" -d -f\"UbicacionDelArchivo\" -s\"NombreDeArchivoSalida\"  [Nombre del archivo luego de comprimir]");
            //                                        }
            //                                        break;
            //                                    default:
            //                                        throw new FormatException("Se esperaba una acción: \r\n\t-c -f\"UbicacionDelArchivo\" -d -f\"UbicacionDelArchivo\" -s\"NombreDeArchivoSalida\"  [Nombre del archivo luego de comprimir]");
            //                                }
            //                                break;
            //                            default:
            //                                throw new FormatException("Se esperaba una acción: \r\n\t-c -f\"UbicacionDelArchivo\" -d -f\"UbicacionDelArchivo\" -s\"NombreDeArchivoSalida\"  [Nombre del archivo luego de comprimir]");
            //                        }
            //                        break;
            //                    case "d":
            //                        ComerEspacios();
            //                        pos--;
            //                        switch (leerCaracter())
            //                        {
            //                            case "-":
            //                                switch (leerCaracter())
            //                                {
            //                                    case "s":
            //                                        ComerEspacios();
            //                                        pos--;
            //                                        c = leerCaracter();
            //                                        if (c == "\"")
            //                                        {
            //                                            c = leerCaracter();
            //                                            while (c != "\"")
            //                                            {
            //                                                salida += c;
            //                                                c = leerCaracter();
            //                                            }
            //                                        }
            //                                        ComerEspacios();
            //                                        pos--;
            //                                        switch (leerCaracter())
            //                                        {
            //                                            case "-":
            //                                                switch (leerCaracter())
            //                                                {
            //                                                    case "f":
            //                                                        ComerEspacios();
            //                                                        pos--;
            //                                                        c = leerCaracter();
            //                                                        if (c == "\"")
            //                                                        {
            //                                                            c = leerCaracter();
            //                                                            while (c != "\"")
            //                                                            {
            //                                                                ubicacion += c;
            //                                                                c = leerCaracter();
            //                                                            }
            //                                                        }
            //                                                        PGP Cifrado = new PGP(Usuario, Password, true, ubicacion, salida);
            //                                                        Cifrado.Descifrar();
            //                                                        break;
            //                                                    default:
            //                                                        throw new FormatException("Se esperaba una acción: \r\n\t-c -f\"UbicacionDelArchivo\" -d -f\"UbicacionDelArchivo\" -s\"NombreDeArchivoSalida\"  [Nombre del archivo luego de comprimir]");
            //                                                }
            //                                                break;
            //                                            default:
            //                                                throw new FormatException("Se esperaba una acción: \r\n\t-c -f\"UbicacionDelArchivo\" -d -f\"UbicacionDelArchivo\" -s\"NombreDeArchivoSalida\"  [Nombre del archivo luego de comprimir]");
            //                                        }
            //                                        break;
            //                                    case "f":
            //                                        ComerEspacios();
            //                                        pos--;
            //                                        c = leerCaracter();
            //                                        if (c == "\"")
            //                                        {
            //                                            c = leerCaracter();
            //                                            while (c != "\"")
            //                                            {
            //                                                ubicacion += c;
            //                                                c = leerCaracter();
            //                                            }
            //                                        }
            //                                        ComerEspacios();
            //                                        pos--;
            //                                        switch (leerCaracter())
            //                                        {
            //                                            case "-":
            //                                                switch (leerCaracter())
            //                                                {
            //                                                    case "s":
            //                                                        ComerEspacios();
            //                                                        pos--;
            //                                                        c = leerCaracter();
            //                                                        if (c == "\"")
            //                                                        {
            //                                                            c = leerCaracter();
            //                                                            while (c != "\"")
            //                                                            {
            //                                                                salida += c;
            //                                                                c = leerCaracter();
            //                                                            }
            //                                                        }
            //                                                        PGP Cifrado = new PGP(Usuario, Password, true, ubicacion, salida);
            //                                                        Cifrado.Descifrar();
            //                                                        break;
            //                                                    default:
            //                                                        throw new FormatException("Se esperaba una acción: \r\n\t-c -f\"UbicacionDelArchivo\" -d -f\"UbicacionDelArchivo\" -s\"NombreDeArchivoSalida\"  [Nombre del archivo luego de comprimir]");
            //                                                }
            //                                                break;
            //                                            default:
            //                                                throw new FormatException("Se esperaba una acción: \r\n\t-c -f\"UbicacionDelArchivo\" -d -f\"UbicacionDelArchivo\" -s\"NombreDeArchivoSalida\"  [Nombre del archivo luego de comprimir]");
            //                                        }
            //                                        break;
            //                                    default:
            //                                        throw new FormatException("Se esperaba una acción: \r\n\t-c -f\"UbicacionDelArchivo\" -d -f\"UbicacionDelArchivo\" -s\"NombreDeArchivoSalida\"  [Nombre del archivo luego de comprimir]");
            //                                }
            //                                break;
            //                            default:
            //                                throw new FormatException("Se esperaba una acción: \r\n\t-c -f\"UbicacionDelArchivo\" -d -f\"UbicacionDelArchivo\" -s\"NombreDeArchivoSalida\"  [Nombre del archivo luego de comprimir]");
            //                        }
            //                        break;
            //                    case "f":
            //                        ComerEspacios();
            //                        pos--;
            //                        c = leerCaracter();
            //                        if (c == "\"")
            //                        {
            //                            c = leerCaracter();
            //                            while (c != "\"")
            //                            {
            //                                ubicacion += c;
            //                                c = leerCaracter();
            //                            }
            //                        }
            //                        ComerEspacios();
            //                        pos--;
            //                        switch (leerCaracter())
            //                        {
            //                            case "-":
            //                                switch (leerCaracter())
            //                                {
            //                                    case "c":
            //                                        ComerEspacios();
            //                                        pos--;
            //                                        switch (leerCaracter())
            //                                        {
            //                                            case "-":
            //                                                switch (leerCaracter())
            //                                                {
            //                                                    case "s":
            //                                                        c = leerCaracter();
            //                                                        if (c == "\"")
            //                                                        {
            //                                                            c = leerCaracter();
            //                                                            while (c != "\"")
            //                                                            {
            //                                                                salida += c;
            //                                                                c = leerCaracter();
            //                                                            }
            //                                                        }
            //                                                        //ComerEspacios();
            //                                                        //pos--;
            //                                                        String metodo = leerCaracter();
            //                                                        PGP Cifrado = new PGP(Usuario, Password, true, ubicacion, salida);
            //                                                        Cifrado.Cifrar();
            //                                                        break;
            //                                                    default:
            //                                                        throw new FormatException("Se esperaba orden: \r\n\t -s\"NombreArchivoSalida\" [Nombre del archivo luego de comprimir]");
            //                                                }
            //                                                break;
            //                                            default:
            //                                                throw new FormatException("Se esperaba orden: \r\n\t -s\"NombreArchivoSalida\" [Nombre del archivo luego de comprimir]");
            //                                        }
            //                                        break;
            //                                    case "d":
            //                                        ComerEspacios();
            //                                        pos--;
            //                                        switch (leerCaracter())
            //                                        {
            //                                            case "-":
            //                                                switch (leerCaracter())
            //                                                {
            //                                                    case "s":
            //                                                        c = leerCaracter();
            //                                                        if (c == "\"")
            //                                                        {
            //                                                            c = leerCaracter();
            //                                                            while (c != "\"")
            //                                                            {
            //                                                                salida += c;
            //                                                                c = leerCaracter();
            //                                                            }
            //                                                        }
            //                                                        PGP Cifrado = new PGP(Usuario, Password, true, ubicacion, salida);
            //                                                        Cifrado.Descifrar();
            //                                                        break;
            //                                                    default:
            //                                                        throw new FormatException("Se esperaba orden: \r\n\t -s\"NombreArchivoSalida\" [Nombre del archivo luego de comprimir]");
            //                                                }
            //                                                break;
            //                                            default:
            //                                                throw new FormatException("Se esperaba orden: \r\n\t -s\"NombreArchivoSalida\" [Nombre del archivo luego de comprimir]");
            //                                        }
            //                                        break;
            //                                    case "s":
            //                                        ComerEspacios();
            //                                        pos--;
            //                                        c = leerCaracter();
            //                                        if (c == "\"")
            //                                        {
            //                                            c = leerCaracter();
            //                                            while (c != "\"")
            //                                            {
            //                                                salida += c;
            //                                                c = leerCaracter();
            //                                            }
            //                                        }
            //                                        ComerEspacios();
            //                                        pos--;
            //                                        switch (leerCaracter())
            //                                        {
            //                                            case "c":
            //                                                //ComerEspacios();
            //                                                //pos--;
            //                                                String metodo = leerCaracter();
            //                                                PGP Cifrado = new PGP(Usuario, Password, true, ubicacion, salida);
            //                                                Cifrado.Cifrar();
            //                                                break;
            //                                            case "d":
            //                                                //ComerEspacios();
            //                                                //pos--;
            //                                                PGP Descifrado = new PGP(Usuario, Password, true, ubicacion, salida);
            //                                                Descifrado.Descifrar();
            //                                                break;
            //                                            default:
            //                                                throw new FormatException("Se esperaba orden váida: \r\n\t -c [Comprimir] o -d [Descomprimir]");
            //                                        }
            //                                        break;
            //                                    default:
            //                                        throw new FormatException("Se esperaba orden: \r\n\t -c [Comprimir] o -d [Descomprimir] -s\"NombreArchivoSalida\" [Nombre del archivo luego de comprimir]");
            //                                }
            //                                break;
            //                            default:
            //                                throw new FormatException("Se esperaba orden: \r\n\t -c [Comprimir] o -d [Descomprimir] -s\"NombreArchivoSalida\" [Nombre del archivo luego de comprimir]");
            //                        }
            //                        break;
            //                    default:
            //                        throw new FormatException("Se esperaba una acción: \r\n\t-c [Comprimir] o -d [Descomprimir] o -f\"UbicacionDelArchivo\" [Asignar Archivo] -s\"NombreDeArchivoSalida\"  [Nombre del archivo luego de comprimir]");
            //                }
            //                break;
            //            default:
            //                throw new FormatException("Se esperaba una acción: \r\n\t-c -f\"UbicacionDelArchivo\" -d -f\"UbicacionDelArchivo\" -s\"NombreDeArchivoSalida\"  [Nombre del archivo luego de comprimir]");
            //        }
            //    }
            //    Console.WriteLine("La acción se realizó con éxito.");
            //    Cifrado = null;
            //}
            //catch (Exception ex)
            //{

            //    Console.WriteLine(ex.Message);
            //}
            //Console.ReadKey();
        }

        public static void ComerEspacios()
        {
            while (leerCaracter() == " ") { }
        }

        public static String leerCaracter()
        {
            pos++;
            String Caracter = Query[pos].ToString();
            return Caracter;
        }
    }
}
