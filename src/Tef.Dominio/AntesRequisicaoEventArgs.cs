using System;
using System.Linq;

namespace Tef.Dominio
{
    public class AntesRequisicaoEventArgs : EventArgs
    {
        public AntesRequisicaoEventArgs(TefLinhaLista requisicao)
        {
            Requisicao = requisicao;
        }

        public TefLinhaLista Requisicao { get; private set; }

        public void Remover(int identificador)
        {
            var linha = Requisicao.FirstOrDefault(x => x.Identificacao == identificador);

            if (linha != null)
                Requisicao.Remove(linha);
        }

        public void Adicionar(TefLinha linha)
        {
            if (Requisicao.BuscaLinha(linha.Identificacao) == linha)
            {
                return;
            }

            Requisicao.Add(linha);
            Requisicao = new TefLinhaLista(Requisicao.OrderBy(req => req.Identificacao).ToList());
        }
    }
}