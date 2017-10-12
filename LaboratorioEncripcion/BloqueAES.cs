using System;
using System.Collections;
using System.Collections.Generic;
namespace LaboratorioEncripcion
{
    public class BloqueAES
    {
        public Hexadecimal[,] valores { get; set; }

        public BloqueAES(char[] caracteres)
        {
            if (caracteres.Length > 16)
            {
                throw new Exception("Los bloques pueden ser de maximo 16 bytes.");
            }
            else
            {
                valores = new Hexadecimal[4, 4];
                int cont = 0;
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (cont > caracteres.Length)
                        {
                            valores[j, i] = new Hexadecimal((int)' ');
                        }
                        else
                        {
                            valores[j, i] = new Hexadecimal((int)caracteres[cont]);
                            cont++;
                        }
                    }
                }
            }
        }

        public BloqueAES(Hexadecimal[,] matriz)
        {
            valores = matriz;
        }

        public Hexadecimal[] getVector(int columna)
        {
            Hexadecimal[] retorno = new Hexadecimal[4];
            for (int i = 0; i < 4; i++)
            {
                retorno[i] = valores[i, columna];
            }
            return retorno;
        }
    }
}
