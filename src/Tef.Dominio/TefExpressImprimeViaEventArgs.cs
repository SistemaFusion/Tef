using System;
using System.Linq;

namespace Tef.Dominio
{
    public class TefExpressImprimeViaEventArgs : IImprimeViaEventArgs
    {
        public TefExpressImprimeViaEventArgs(TefLinhaLista respostaRequisicaoAdm)
        {
            #if DEBUG
                respostaRequisicaoAdm.Add(new TefLinha("028-001", 12));
                respostaRequisicaoAdm = new TefLinhaLista(respostaRequisicaoAdm.OrderBy(x => x.Identificacao).ToList());
            #endif

            var tamanhoPrimeiraVia = respostaRequisicaoAdm.BuscaLinha(AcTefIdentificadorCampos.TamanhoViaUnica, 1);

            
            var viaCliente = new TefLinhaLista(respostaRequisicaoAdm.Where(x => x.Identificacao == AcTefIdentificadorCampos.ViaUnicaComprovante
                                                                              && x.Posicao <= int.Parse(tamanhoPrimeiraVia.Valor)).ToList());

            var viaEstabelecimento = new TefLinhaLista(respostaRequisicaoAdm.Where(x => x.Identificacao == AcTefIdentificadorCampos.ViaUnicaComprovante
                                                                                && x.Posicao > int.Parse(tamanhoPrimeiraVia.Valor)).ToList());

            ViaCliente = viaCliente.GetValores();
            ViaEstabelecimento = viaEstabelecimento.GetValores();

        }

        public TefLinhaLista TefLinhaLista { get; }
        public ViaComprovante ViaDeComprovante { get; }
        public string[] ViaEstabelecimento { get; }
        public string[] ViaCliente { get; }
        public string[] ViaUnica { get; }
        public string[] ViaReduzida { get; }
        public string[] Via1 { get; }
        public string[] Via2 { get; }
        public bool IsTemViaEstabelecimento { get; }
        public bool IsTemViaCliente { get; }
        public bool IsTemViaUnica { get; }
        public bool IsTemViaReduzida { get; }
        public bool IsTemVia1 { get; }
        public bool IsTemVia2 { get; }



    }
}