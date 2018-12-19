using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Tef.Dominio
{
    public class TefLinhaLista : List<TefLinha>
    {
        public TefLinhaLista() { }

        public TefLinhaLista(IEnumerable<TefLinha> lista)
        {
            foreach (var tefLinha in lista)
            {
                Add(tefLinha);
            }
        }

        public string[] GetRequisicao()
        {
            var requisicao = new List<string>();

            foreach (var tefLinha in this.OrderBy(x => x.Identificacao))
            {
                requisicao.Add(tefLinha.ToString());
            }

            return requisicao.ToArray();
        }

        public static TefLinhaLista LoadArquivo(string arquivo)
        {
            var listaDeLinhas = new TefLinhaLista();

            var conteudoArquivoSts = File.ReadAllLines(arquivo);

            foreach (var conteudoArquivoSt in conteudoArquivoSts)
            {
                listaDeLinhas.Add(new TefLinha(conteudoArquivoSt));
            }

            return listaDeLinhas;
        }
    }
}