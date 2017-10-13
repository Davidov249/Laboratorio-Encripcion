using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioEncripcion
{
    public class Hexadecimal
    {
        private String ValorHex { get; set; }

        private int ValorDec { get; set; }

        private int ValorBit { get; set; }

        public Hexadecimal(int valorDecimal)
        {
            if (valorDecimal == 65533)
                valorDecimal = 32;
            ValorDec = valorDecimal;
            string c1 = Convert.ToString(Convert.ToInt32(ValorDec), 16).ToLower();
            ValorHex = c1;
        }

        public Hexadecimal(String valorHexadecimal)
        {
            if (valorHexadecimal.ToLower() == "fffd")
                valorHexadecimal = "20";
            ValorHex = valorHexadecimal;
            ValorDec = Convert.ToInt32(valorHexadecimal, 16);
        }

        public int getDecimal()
        {
            return Convert.ToInt32(ValorHex, 16);
        }

        public String getHexadecimal()
        {
            ValorHex = Convert.ToString(ValorDec, 16).ToLower();
            if (ValorHex.Length == 1)
            {
                ValorHex = "0" + ValorHex;
            }
            return ValorHex;
        }

        public BitArray getBits()
        {
            getHexadecimal();
            byte[] datos = new byte[2];
            for (int i = 0; i < ValorHex.Length; i++)
            {
                datos[i] = Convert.ToByte(ValorHex[i]);
            }
            if (ValorHex.Length < 2)
            {
                datos[1] = datos[0];
                datos[0] = 0;
            }
            return new BitArray(datos);
        }

        public String getString()
        {
            int dato = getDecimal();
            String datoS = ((char)dato).ToString();
            return datoS;
        }
    }
}
