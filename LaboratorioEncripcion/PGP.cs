﻿using System;
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
                StreamWriter escritor = new StreamWriter(@"c:\cript\key" + salida + ".clp");
                escritor.WriteLine(Claves.llavePrivada());
                escritor.Close();
                Compresion = new Huffman(archivo);
                Cifrado = new AES_128(Compresion.Comprimir(), Claves.llavePrivada(), cifrar);
            }
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
    }
}
