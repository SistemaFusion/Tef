using System;

namespace Tef.Dominio
{
    public class AutorizaDfeEventArgs : EventArgs
    {
        public StatusAutorizacaoDfe StatusAutorizacaoDfe { get; private set; } = StatusAutorizacaoDfe.Autorizado;

        public TefLinhaLista TefLinhaLista { get; }

        public AutorizaDfeEventArgs(TefLinhaLista tefLinhaLista)
        {
            TefLinhaLista = tefLinhaLista;
        }

        public void AutorizadoDfe()
        {
            StatusAutorizacaoDfe = StatusAutorizacaoDfe.Autorizado;
        }

        public void RejeicaoDfe()
        {
            StatusAutorizacaoDfe = StatusAutorizacaoDfe.Rejeicao;
        }

        public void ErroDfe()
        {
            StatusAutorizacaoDfe = StatusAutorizacaoDfe.Erro;
        }

        public bool IsAutorizado()
        {
            return StatusAutorizacaoDfe == StatusAutorizacaoDfe.Autorizado;
        }

        public bool IsContemErro()
        {
            return StatusAutorizacaoDfe == StatusAutorizacaoDfe.Erro;
        }

        public bool IsContemRejeicao()
        {
            return StatusAutorizacaoDfe == StatusAutorizacaoDfe.Rejeicao;
        }
    }
}