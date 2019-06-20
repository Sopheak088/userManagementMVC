using System;

namespace iThinking.Common.Helpers
{
    public class NumberHelpers
    {
        public static bool IsNumeric(object Expression)
        {
            double retNum;

            bool isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }

        public static string ToRomanNumber(int number)
        {
            try
            {
                string result = string.Empty;
                Boolean flag = true;
                string[] ArrayLaMa = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
                int[] ArrayNumber = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
                int i = 0;
                while (flag)
                {
                    while (number >= ArrayNumber[i])
                    {
                        number -= ArrayNumber[i];
                        result += ArrayLaMa[i];
                        if (number < 1)
                            flag = false;
                    }
                    i++;
                }
                return result;
            }
            catch
            {
                return "";
            }
        }
    }
}