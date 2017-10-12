﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace LaboratorioEncripcion
{
    public class AES_128
    {
        private List<BloqueAES> Bloques { get; set; }
        private BloqueAES Clave { get; set; }
        private List<BloqueAES> SubClaves { get; set; }
        private BloqueAES TransformacionDinamica { get; set; }
        private string[,] sBox =
        {
            { "  ", "00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "0a", "0b", "0c", "0d", "0e", "0f" },
            { "00", "63", "7c", "77", "7b", "f2", "6b", "6f", "c5", "30", "01", "67", "2b", "fe", "d7", "ab", "76" },
            { "10", "ca", "82", "c9", "7d", "fa", "59", "47", "f0", "ad", "d4", "a2", "af", "9c", "a4", "72", "c0" },
            { "20", "b7", "fd", "93", "26", "36", "3f", "f7", "cc", "34", "a5", "e5", "f1", "71", "d8", "31", "15" },
            { "30", "04", "c7", "23", "c3", "18", "96", "05", "9a", "07", "12", "80", "e2", "eb", "27", "b2", "75" },
            { "40", "09", "83", "2c", "1a", "1b", "6e", "5a", "a0", "52", "3b", "d6", "b3", "29", "e3", "2f", "84" },
            { "50", "53", "d1", "00", "ed", "20", "fc", "b1", "5b", "6a", "cb", "be", "39", "4a", "4c", "58", "cf" },
            { "60", "d0", "ef", "aa", "fb", "43", "4d", "33", "85", "45", "f9", "02", "7f", "50", "3c", "9f", "a8" },
            { "70", "51", "a3", "40", "8f", "92", "9d", "38", "f5", "bc", "b6", "da", "21", "10", "ff", "f3", "d2" },
            { "80", "cd", "0c", "13", "ec", "5f", "97", "44", "17", "c4", "a7", "7e", "3d", "64", "5d", "19", "73" },
            { "90", "60", "81", "4f", "dc", "22", "2a", "90", "88", "46", "ee", "b8", "14", "de", "5e", "0b", "db" },
            { "a0", "e0", "32", "3a", "0a", "49", "06", "24", "5c", "c2", "d3", "ac", "62", "91", "95", "e4", "79" },
            { "b0", "e7", "c8", "37", "6d", "8d", "d5", "4e", "a9", "6c", "56", "f4", "ea", "65", "7a", "ae", "08" },
            { "c0", "ba", "78", "25", "2e", "1c", "a6", "b4", "c6", "e8", "dd", "74", "1f", "4b", "bd", "8b", "8a" },
            { "d0", "70", "3e", "b5", "66", "48", "03", "f6", "0e", "61", "35", "57", "b9", "86", "c1", "1d", "9e" },
            { "e0", "e1", "f8", "98", "11", "69", "d9", "8e", "94", "9b", "1e", "87", "e9", "ce", "55", "28", "df" },
            { "f0", "8c", "a1", "89", "0d", "bf", "e6", "42", "68", "41", "99", "2d", "0f", "b0", "54", "bb", "16" }
        };
        private string[,] rcorn =
        {
            { "01", "02", "04", "08", "10", "20", "40", "80", "1b", "36"},
            { "00", "00", "00", "00", "00", "00", "00", "00", "00", "00"},
            { "00", "00", "00", "00", "00", "00", "00", "00", "00", "00"},
            { "00", "00", "00", "00", "00", "00", "00", "00", "00", "00"},
        };

        private string[,] transformacion =
        {
            { "02", "01", "01", "03" },
            { "03", "02", "01", "01" },
            { "01", "03", "02", "01" },
            { "01", "01", "03", "02" }
        };

        public AES_128(string archivo, string clave, bool cifrar)
        {
            MD5 hashMD5 = MD5.Create();
            string claveHash = Hashear(hashMD5, clave);
            char[] claveChars = new char[16];
            for (int i = 0; i < 16; i++)
            {
                claveChars[i] = clave[i];
            }
            Clave = new BloqueAES(claveChars);
            SubClaves = new List<BloqueAES>();
            Bloques = new List<BloqueAES>();
            StreamReader lector = new StreamReader(archivo);
            char caracter = (char)lector.Read();
            int contador = 0;
            char[] caracteres = new char[16];
            while (caracter.ToString() != "\uffff")
            {
                if (contador == 15)
                {
                    contador = -1;
                    Bloques.Add(new BloqueAES(caracteres));
                    caracteres = new char[16];
                }
                else
                {
                    caracteres[contador] = caracter;
                }
                contador++;
                caracter = (char)lector.Read();
            }
            if (contador < 15)
            {
                for (int i = contador; i <= 15; i++)
                {
                    caracteres[i] = ' ';
                }
                Bloques.Add(new BloqueAES(caracteres));
            }
            GenerarSubClaves(Clave, 0, 0);
            inicializarTransformacion();
            for (int i = 0; i < Bloques.Count; i++)
            {
                RondaInicial(Bloques[i], Clave);
            }
        }

        private void inicializarTransformacion()
        {
            Hexadecimal[,] matrizHexa = new Hexadecimal[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    matrizHexa[i, j] = new Hexadecimal(transformacion[i, j]);
                }
            }
            TransformacionDinamica = new BloqueAES(matrizHexa);
        }

        public BitArray PasarBit(String hex)
        {
            byte[] datos = new byte[2];
            for (int i = 0; i < hex.Length; i++)
            {
                datos[i] = Convert.ToByte(hex[i]);
            }
            if (hex.Length < 2)
            {
                datos[1] = datos[0];
                datos[0] = 0;
            }
            return new BitArray(datos);
        }

        public void GenerarSubClaves(BloqueAES ClaveAnterior, int posicion, int subClave)
        {
            // Primera Columna
            Hexadecimal[] vectorRotar = new Hexadecimal[4];
            rotarVector(ref vectorRotar, ClaveAnterior.getVector(3));
            Hexadecimal[,] subclave = new Hexadecimal[4, 4];
            String[] hexTemp = new String[4];
            for (int i = 0; i < 4; i++)
            {
                hexTemp[i] = buscarSBox(vectorRotar[i].getHexadecimal());
                hexTemp[i] = getStringBit(XOR(PasarBit(hexTemp[i]), ClaveAnterior.valores[i, posicion].getBits()));
                subclave[i, 0] = new Hexadecimal(getStringBit(XOR(PasarBit(hexTemp[i]), PasarBit(rcorn[i, subClave]))));
            }
            posicion++;
            // Siguientes Columnas
            int anteriorClaveActual = 0;
            for (int i = 1; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    subclave[j, i] = new Hexadecimal(getStringBit(XOR(ClaveAnterior.valores[j, posicion].getBits(), subclave[j, anteriorClaveActual].getBits())));
                }
                posicion++; anteriorClaveActual++;
            }
            SubClaves.Add(new BloqueAES(subclave));
            if (subClave < 9)
            {
                subClave = subClave + 1;
                GenerarSubClaves(SubClaves[SubClaves.Count - 1], 0, subClave);
            }
        }

        #region Rondas
        private void RondaInicial(BloqueAES bloque, BloqueAES clave)
        {
            bloque = AddRoundKey(bloque, clave);
            RondasEstandar(bloque, 0);
        }

        private void RondasEstandar(BloqueAES bloque, int pasada)
        {
            bloque = SubBytes(bloque);
            bloque = ShiftRows(bloque);
            bloque = MixColumns(bloque);
            bloque = AddRoundKey(bloque, SubClaves[pasada]);
            if (pasada < 9)
            {
                pasada++;
                RondasEstandar(bloque, pasada);
            }
            else
            {
                RondaFinal(bloque);
            }
        }

        private void RondaFinal(BloqueAES bloque)
        {
            bloque = SubBytes(bloque);
            bloque = ShiftRows(bloque);
            bloque = AddRoundKey(bloque, SubClaves[SubClaves.Count - 1]);
        }
        #endregion Rondas

        #region Proceso de las Rondas
        public BloqueAES SubBytes(BloqueAES bloque)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    bloque.valores[i, j] = new Hexadecimal(buscarSBox(bloque.valores[i, j].getHexadecimal()));
                }
            }
            return bloque;
        }

        public BloqueAES ShiftRows(BloqueAES bloque)
        {
            Hexadecimal temp = null;
            int rotaciones = 1;
            // Maneja la fila
            for (int i = 1; i < 4; i++)
            {
                // Controla la cantidad de rotaciones
                for (int k = 0; k < rotaciones; k++)
                {
                    // Se aplica la rotación
                    for (int j = 1; j < 4; j++)
                    {
                        switch (j)
                        {
                            case 0:
                                temp = bloque.valores[i, j];
                                break;
                            case 3:
                                bloque.valores[i, j] = temp;
                                break;
                            default:
                                bloque.valores[i, j - 1] = bloque.valores[i, j];
                                break;
                        }
                    }
                }
                rotaciones++;
            }
            return bloque;
        }

        public BloqueAES MixColumns(BloqueAES bloque)
        {
            int datoCalculado = 0;
            for (int f = 0; f < 4; f++)
            {
                for (int c = 0; c < 4; c++)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            datoCalculado += (bloque.valores[i, j].getDecimal() * TransformacionDinamica.valores[j, i].getDecimal());
                        }
                    }
                    bloque.valores[f, c] = new Hexadecimal(datoCalculado);
                }
            }
            return bloque;
        }

        public BloqueAES AddRoundKey(BloqueAES bloque, BloqueAES clave)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    bloque.valores[i, j] = new Hexadecimal(getStringBit(XOR(bloque.valores[i, j].getBits(), clave.valores[i, j].getBits())));
                }
            }
            return bloque;
        }
        #endregion Proceso de las Rondas

        public void rotarVector(ref Hexadecimal[] vectorFinal, Hexadecimal[] vectorInicial)
        {
            vectorFinal[0] = vectorInicial[1];
            vectorFinal[1] = vectorInicial[2];
            vectorFinal[2] = vectorInicial[3];
            vectorFinal[3] = vectorInicial[0];
        }

        private String buscarSBox(String dato)
        {
            int f = 0; int c = 0;
            dato = dato.ToLower();
            for (int i = 1; i < 17; i++)
            {
                if (sBox[i, 0][0] == dato[0])
                {
                    f = i;
                }
            }
            for (int i = 1; i < 17; i++)
            {
                if (sBox[0, i][1] == dato[1])
                {
                    c = i;
                }
            }
            return sBox[f, c];
        }

        private String getStringBit(BitArray bits)
        {
            int valor = 0;
            for (int i = 0; i < bits.Count; i++)
            {
                if (bits[i])
                    valor += Convert.ToInt32(Math.Pow(2, i));
            }
            return Convert.ToString(valor, 16).ToUpper();
        }

        public BitArray XOR(BitArray dato1, BitArray dato2)
        {
            bool[] bitsSalida = new bool[8];
            for (int i = 0; i < 8; i++)
            {
                if (dato1[i] == dato2[i])
                {
                    bitsSalida[i] = false;
                }
                else
                {
                    bitsSalida[i] = true;
                }
            }
            return new BitArray(bitsSalida);
        }

        public string Hashear(MD5 hasher, string entrada)
        {
            byte[] datos = hasher.ComputeHash(Encoding.UTF8.GetBytes(entrada));
            StringBuilder hashed = new StringBuilder();
            for (int i = 0; i < datos.Length; i++)
            {
                hashed.Append(datos[i].ToString("x2"));
            }
            return hashed.ToString();
        }

        public String Cifrar()
        {
            StringBuilder Cifrado = new StringBuilder();
            for (int i = 0; i < Bloques.Count; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        Cifrado.Append(Bloques[i].valores[j, k].getString());
                    }
                }
            }
            return Cifrado.ToString();
        }
    }
}
