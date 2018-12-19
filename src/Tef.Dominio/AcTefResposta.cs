using Tef.Dominio.Enums;

namespace Tef.Dominio
{
    public class AcTefResposta
    {
        public TefLinhaLista TefLinhaLista { get; }

        public AcTefResposta(TefLinhaLista tefLinhaLista)
        {
            TefLinhaLista = tefLinhaLista;
        }

        public string Comando => BuscarValor(000);
        public long Identificacao => long.Parse(BuscarValor(001));
        public long DocumentoFiscal => long.Parse(BuscarValor(002));
        public decimal ValorTotal => BuscarValorTotal();
        public Moeda Moeda => BuscarMoeda();
        public FormaIdentificacaoCliente? EntidadeCliente => BuscarFormaIdentificacaoCliente();
        public string IdentificadorCliente => BuscarValor(007);

        private FormaIdentificacaoCliente? BuscarFormaIdentificacaoCliente()
        {
            var entidadeCliente = BuscarValor(006);

            switch (entidadeCliente)
            {
                case null:
                    return null;
                case "F":
                    return FormaIdentificacaoCliente.Cpf;
                case "J":
                    return FormaIdentificacaoCliente.Cnpj;
                case "X":
                    return FormaIdentificacaoCliente.Outro;
                default:
                    return null;
            }
        }

        private Moeda BuscarMoeda()
        {
            var moeda = BuscarValor(004);

            switch (moeda)
            {
                case null:
                case "0":
                    return Moeda.Real;
                case "1":
                    return Moeda.DolarAmericano;
                default:
                    return moeda == "2" ? Moeda.Euro : Moeda.Real;
            }
        }

        private decimal BuscarValorTotal()
        {
            var valorTotal = BuscarValor(003);

            if (string.IsNullOrEmpty(BuscarValor(003)))
                return decimal.Zero;

            valorTotal.Insert(valorTotal.Length - 2, ".");
            return decimal.Parse(valorTotal);
        }

        private string BuscarValor(int identificador)
        {
            return TefLinhaLista.BuscaLinha(identificador)?.Valor ?? string.Empty;
        }
    }
}