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
            ValorDec = valorDecimal;
        }

        public Hexadecimal(String valorHexadecimal)
        {
            ValorHex = valorHexadecimal;
        }

        public int getDecimal()
        {
            decimal temp = 0;
            Decimal.TryParse(ValorHex, System.Globalization.NumberStyles.HexNumber, null, out temp);
            return Convert.ToInt32(temp);
        }

        public String getHexadecimal()
        {
            return Convert.ToString(Convert.ToInt32(ValorDec), 16).ToUpper();
        }

        public BitArray getBits()
        {
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
            String datoS = ((char) dato).ToString();
            return datoS;
        }
    }
}
