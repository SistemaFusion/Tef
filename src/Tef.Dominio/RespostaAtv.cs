using System;

namespace Tef.Dominio
{
    public class RespostaAtv : EventArgs
    {
        public RespostaAtv(TefLinhaLista tefLinhaLista)
        {
            Header = tefLinhaLista[0].Valor;
            Identificadao = tefLinhaLista[1].Valor;
            TefLinhaLista = tefLinhaLista;
        }

        public string Identificadao { get; }
        public TefLinhaLista TefLinhaLista { get; }
        public string Header { get; }
    }
}