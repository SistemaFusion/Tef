using System;
using System.Text.RegularExpressions;

namespace AppTesteTef
{
    public static class HidratacaoString
    {
        public static string SubstringWithTrim(this string valor, int inicio, int tamanho)
        {
            if (valor == null) return null;

            var valorTemp = valor;

            valor = valorTemp.Length <= tamanho ? valorTemp : valorTemp.Substring(inicio, tamanho).Trim();

            return valor;
        }

        public static string TrimOrEmpty(this string valor)
        {
            return string.IsNullOrEmpty(valor) ? string.Empty : valor.Trim();
        }

        public static string TrimOrNull(this string valor)
        {
            return valor.IsNullOrEmpty() ? null : valor.Trim();
        }

        public static bool IsNullOrEmpty(this string valor)
        {
            return string.IsNullOrEmpty(valor.TrimOrEmpty());
        }

        public static bool IsNotNullOrEmpty(this string valor)
        {
            return !IsNullOrEmpty(valor);
        }

        public static string RemoverTeclaEnter(this string valor)
        {
            return valor.Replace(Environment.NewLine, " ");
        }

        public static string RemoverAcentos(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            input = Regex.Replace(input, "[áàâãª]", "a");
            input = Regex.Replace(input, "[ÁÀÂÃÄ]", "A");
            input = Regex.Replace(input, "[éèêë]", "e");
            input = Regex.Replace(input, "[ÉÈÊË]", "E");
            input = Regex.Replace(input, "[íìîï]", "i");
            input = Regex.Replace(input, "[ÍÌÎÏ]", "I");
            input = Regex.Replace(input, "[óòôõöº]", "o");
            input = Regex.Replace(input, "[ÓÒÔÕÖ]", "O");
            input = Regex.Replace(input, "[úùûü]", "u");
            input = Regex.Replace(input, "[ÚÙÛÜ]", "U");
            input = Regex.Replace(input, "[Ç]", "C");
            input = Regex.Replace(input, "[ç]", "c");

            return input;
        }
    }
}
