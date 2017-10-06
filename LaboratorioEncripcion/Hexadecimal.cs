using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioEncripcion
{
    public class Hexadecimal
    {
        public String ValorHex { get; set; }

        public int ValorDec { get; set; }

        public Hexadecimal(int valorDecimal)
        {
            ValorDec = valorDecimal;
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
    }
}
