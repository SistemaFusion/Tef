using System.Linq;

namespace Tef.Dominio
{
    public static class ExtTefLinhas
    {
        public static bool ConfereStatus(this TefLinhaLista tefLinhaLista)
        {
            var campoStatus = 009;

            var campo009Status = tefLinhaLista.BuscaLinha(campoStatus);

            if (campo009Status == null) return false;

            if (campo009Status.Valor != "0") return false;

            return true;
        }

        public static TefLinha BuscaLinha(this TefLinhaLista tefLinhaLista, int identificacao)
        {
            return tefLinhaLista.FirstOrDefault(resp => resp.Identificacao == identificacao);
        }

        public static bool RequerConfirmacao(this TefLinhaLista tefLinhaLista)
        {
            var statusConfirmacao = tefLinhaLista.FirstOrDefault(resp => resp.Identificacao == 729);

            return statusConfirmacao == null || statusConfirmacao.Valor == "2";
        }

        public static bool NaoRequerConfirmacao(this TefLinhaLista tefLinhaLista)
        {
            var statusConfirmacao = tefLinhaLista.FirstOrDefault(resp => resp.Identificacao == 729);

            if (statusConfirmacao == null) return false;

            return statusConfirmacao.Valor == "1";
        }
    }
}