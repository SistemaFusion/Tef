namespace Tef.Dominio.Conversores
{
    public static class ConverterRealEmCentavos
    {
        public static string ConverterEmCentavos(decimal valor)
        {
            var valorConvertido = (valor * 100).ToString("####").Replace(".", string.Empty).Replace(",", string.Empty);

            return valorConvertido;
        }

        public static decimal ConverterEmMoeda(string valor)
        {
            return long.Parse(valor) % 100;
        }
    }
}