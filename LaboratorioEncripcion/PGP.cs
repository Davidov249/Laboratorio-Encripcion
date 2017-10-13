using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace LaboratorioEncripcion
{
    public class PGP
    {
        private RSA Claves { get; set; }
        private AES_128 Cifrado { get; set; }
        private Huffman Compresion { get; set; }
        private string Archivo { get; set; }
        private string Salida { get; set; }

        public PGP(string usuario, string password, bool cifrar, string archivo, string salida)
        {
            Archivo = archivo;
            Salida = salida;
            Random rnd = new Random();
            int dato = rnd.Next(13825, 15918);
            string con = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=primos.accdb;";
            OleDbConnection conexion = new OleDbConnection(con);
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM primos WHERE usuario='" + usuario + "' AND contra='" + password + "';");
            conexion.Open();
            cmd.Connection = conexion;
            OleDbDataReader lector = cmd.ExecuteReader();
            if (lector.HasRows)
            {
                conexion.Close();
                throw new FieldAccessException("Ya se han creado claves con estas credeniales. Para cifrar indique su archivo de clave.");
            }
            else
            {
                conexion.Close();
                conexion.Open();
                cmd = new OleDbCommand("UPDATE primos SET usuario='" + usuario + "', contra='" + password + "' WHERE id=" + dato + ";");
                cmd.Connection = conexion;
                cmd.ExecuteNonQuery();
                conexion.Close();
                cmd = new OleDbCommand("SELECT * FROM primos WHERE id=" + dato + ";");
                conexion.Open();
                cmd.Connection = conexion;
                lector = cmd.ExecuteReader();
                int p = 0; int q = 0;
                if (lector.HasRows)
                {
                    var leer = lector.Read();
                    p = Convert.ToInt32(lector.GetValue(1));
                    q = Convert.ToInt32(lector.GetValue(2));
                }
                else
                {
                    throw new Exception("Falló al obtener p y q");
                }
                conexion.Close();
                Claves = new RSA(p, q);
                System.IO.Directory.CreateDirectory(@"c:\cript");
                char[] chars = salida.ToCharArray();
                string salida2 = "";
                for (int i = 2; i < chars.Length; i++)
                {
                    salida2 += chars[i].ToString();
                }
                StreamWriter escritor = new StreamWriter(@"c:\cript\key" + salida2 + ".clp");
                escritor.WriteLine(Claves.llavePrivada());
                escritor.Close();
                Compresion = new Huffman(archivo);
                Cifrado = new AES_128(Compresion.Comprimir(), Claves.llavePrivada(), cifrar);
            }
        }

        public PGP(string archivoDescifrar, string clavePrivada)
        {
            Archivo = archivoDescifrar;
            StreamReader lector = new StreamReader(clavePrivada);
            String privada = lector.ReadLine();
            lector.Close();
            StringBuilder cifrado = new StringBuilder();
            lector = new StreamReader(archivoDescifrar);
            string linea = lector.ReadLine();
            while (linea != null && linea != "")
            {
                cifrado.Append(linea);
                linea = lector.ReadLine();
            }
            lector.Close();
            Cifrado = new AES_128(cifrado.ToString(), privada, false);
        }

        public void Cifrar()
        {
            string[] ubicacion = Archivo.Split('.');
            string final = "";
            for (int i = 0; i < ubicacion.Length; i++)
            {
                if (i == ubicacion.Length - 1)
                    final += Salida + "." + ubicacion[i] + ".cif";
                else
                    final += ubicacion[i] + ".";
            }
            StreamWriter escritor = new StreamWriter(final);
            string cifrado = Cifrado.Cifrar();
            escritor.WriteLine(cifrado);
            escritor.Close();
        }

        public void Descifrar()
        {

            String descifrado = Cifrado.Descifrar();
            Compresion = new Huffman(descifrado);
            descifrado = Compresion.Descomprimir();
            string[] salida = Archivo.Split('.');
            string final = "";
            for (int i = 0; i < salida.Length - 1; i++)
            {
                if (i == salida.Length - 2)
                    final += salida[i];
                else
                    final += salida[i] + ".";
            }
            StreamWriter escritor = new StreamWriter(final);
            escritor.WriteLine(descifrado);
            escritor.Close();
        }
    }
}
