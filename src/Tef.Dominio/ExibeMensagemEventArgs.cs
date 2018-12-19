using System;

namespace Tef.Dominio
{
    public class ExibeMensagemEventArgs : EventArgs
    {
        public ExibeMensagemEventArgs(TefLinhaLista tefLinhaLista)
        {
            Mensagem = tefLinhaLista.BuscaLinha(030)?.Valor ?? string.Empty;
        }

        public string Mensagem { get; }
    }
}