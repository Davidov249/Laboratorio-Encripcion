﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioEncripcion
{
    public class Huffman
    {
        private List<DatoHuffman> CaracteresProbabilidades = null;
        private List<NodoHuffman> Nodos = null;
        private List<NodoHuffman> Hojas = null;
        private NodoHuffman _raiz = null;
        private StreamReader Lector = null;
        List<string> textoComprimir = null;
        List<string> textoCompreso = null;

        public Huffman(String Entrada)
        {
            textoComprimir = new List<string>();
            textoCompreso = new List<string>();
            Lector = new StreamReader(Entrada);
            CaracteresProbabilidades = new List<DatoHuffman>();
            String Caracter = LeerSiguiente(); int existe = -1;
            int Total = 0;
            while (Caracter != null && Caracter != "" && Caracter != "\uffff")
            {
                existe = ExisteHuffman(Caracter);
                if (existe != -1)
                {
                    CaracteresProbabilidades[existe].Apariciones++;
                }
                else
                {
                    CaracteresProbabilidades.Add(new DatoHuffman(Caracter));
                    CaracteresProbabilidades[CaracteresProbabilidades.Count - 1].Apariciones++;
                }
                Total++;
                textoComprimir.Add(Caracter);
                Caracter = LeerSiguiente();
            }
            for (int i = 0; i < CaracteresProbabilidades.Count; i++)
            {
                CaracteresProbabilidades[i].TotalTexto = Total;
            }
            /// Crear la lista de nodos
            Nodos = new List<NodoHuffman>();
            for (int i = 0; i < CaracteresProbabilidades.Count; i++)
            {
                Nodos.Add(new NodoHuffman(CaracteresProbabilidades[i], ""));
            }
            /// Armar el árbol
            int nodos = 0; int cantidad = Nodos.Count;
            NodoHuffman n1, n2 = null;
            while (nodos < cantidad)
            {
                OrdenarLista();
                if (Nodos.Count > 1)
                {
                    n1 = Nodos[0]; Nodos.RemoveAt(0); n2 = Nodos[0]; Nodos.RemoveAt(0);
                    Nodos.Add(new NodoHuffman(n1, n2));
                }
                else
                {
                    _raiz = Nodos[0];
                    nodos = 1;
                }
                cantidad = Nodos.Count;
            }
            InOrdenAsignar(_raiz, "");
            Hojas = new List<NodoHuffman>();
            InOrden(_raiz);
        }

        public String Comprimir()
        {
            String CadenaEscribir = "";
            Dictionary<string, string> tabla = new Dictionary<string, string>();
            for (int i = 0; i < Hojas.Count; i++)
            {
                if (i == Hojas.Count - 1)
                {
                    CadenaEscribir += Hojas[i].Dato.Caracter + Hojas[i].Codigo + "//";
                    tabla.Add(Hojas[i].Dato.Caracter, Hojas[i].Codigo.ToString());
                }
                else
                {
                    CadenaEscribir += Hojas[i].Dato.Caracter + Hojas[i].Codigo + " ";
                    tabla.Add(Hojas[i].Dato.Caracter, Hojas[i].Codigo.ToString());
                }
            }
            for (int i = 0; i < textoComprimir.Count; i++)
            {
                textoCompreso.Add(tabla[textoComprimir[i]]);
                CadenaEscribir += tabla[textoComprimir[i]];
            }
            return CadenaEscribir;
        }

        private void InOrdenAsignar(NodoHuffman nodo, String caracter)
        {
            if (nodo != null)
            {
                InOrdenAsignar(nodo.Izquierdo, caracter + "0");
                nodo.Codigo += caracter;
                InOrdenAsignar(nodo.Derecho, caracter + "1");
            }
        }

        private void InOrden(NodoHuffman nodo)
        {
            if (nodo != null)
            {
                InOrden(nodo.Izquierdo);
                if (nodo.esHoja())
                {
                    Hojas.Add(nodo);
                }
                InOrden(nodo.Derecho);
            }
        }

        private int ExisteHuffman(String caracter)
        {
            for (int i = 0; i < CaracteresProbabilidades.Count; i++)
            {
                if (CaracteresProbabilidades[i].Caracter.Equals(caracter))
                {
                    return i;
                }
            }
            return -1;
        }

        private void OrdenarLista()
        {
            /// Ordenar la lista CaracteresProbabilidades por su probabilidad de menor a mayor.
            NodoHuffman Aux = null;
            for (int i = 0; i < Nodos.Count; i++)
            {
                for (int j = 0; j < Nodos.Count; j++)
                {
                    if (Nodos[i].Probabilidad < Nodos[j].Probabilidad)
                    {
                        Aux = Nodos[i];
                        Nodos[i] = Nodos[j];
                        Nodos[j] = Aux;
                    }
                }
            }
        }

        private String LeerSiguiente()
        {
            try
            {
                return ((char)Lector.Read()).ToString();
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException("No se encontró el archivo indicado.");
            }
            catch (Exception)
            {
                throw new Exception("Se produjo un error desconocido al leer el archivo.");
            }
        }
    }
}
