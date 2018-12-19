using System.ComponentModel;

namespace Tef.Dominio.Enums
{
    public enum FormaIdentificacaoCliente
    {
        [Description("F")]
        Cpf = 1,

        [Description("J")]
        Cnpj = 2,

        [Description("X")]
        Outro = 3
    }
}