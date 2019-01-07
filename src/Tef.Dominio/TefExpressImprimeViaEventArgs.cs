using System.IO;
using System.Linq;

namespace Tef.Dominio
{
    public class TefExpressImprimeViaEventArgs : IImprimeViaEventArgs
    {
        public TefExpressImprimeViaEventArgs(TefLinhaLista respostaRequisicaoAdm)
        {
            #if DEBUG
                var campo028001 = respostaRequisicaoAdm.BuscaLinha(AcTefIdentificadorCampos.TamanhoViaUnica, 1);
                if (campo028001 == null)
                {
                    respostaRequisicaoAdm.Add(new TefLinha("028-001", 12));
                    respostaRequisicaoAdm = new TefLinhaLista(respostaRequisicaoAdm.OrderBy(x => x.Identificacao).ToList());
                }
            #endif

            var tamanhoPrimeiraVia = respostaRequisicaoAdm.BuscaLinha(AcTefIdentificadorCampos.TamanhoViaUnica, 1);

            
            var viaCliente = new TefLinhaLista(respostaRequisicaoAdm.Where(x => x.Identificacao == AcTefIdentificadorCampos.ViaUnicaComprovante
                                                                              && x.Posicao <= int.Parse(tamanhoPrimeiraVia.Valor)).ToList());

            var viaEstabelecimento = new TefLinhaLista(respostaRequisicaoAdm.Where(x => x.Identificacao == AcTefIdentificadorCampos.ViaUnicaComprovante
                                                                                && x.Posicao > int.Parse(tamanhoPrimeiraVia.Valor)).ToList());

            ViaCliente = viaCliente.GetValores();
            ViaEstabelecimento = viaEstabelecimento.GetValores();

            Via1 = ViaCliente;
            Via2 = ViaEstabelecimento;
        }

        public TefLinhaLista TefLinhaLista { get; }
        public ViaComprovante ViaDeComprovante { get; }
        public string[] ViaEstabelecimento { get; }
        public string[] ViaCliente { get; }
        public string[] ViaUnica { get; }
        public string[] ViaReduzida { get; }
        public string[] Via1 { get; }
        public string[] Via2 { get; }

        public bool IsTemViaEstabelecimento => ViaEstabelecimento != null && ViaEstabelecimento.Length != 0;
        public bool IsTemViaCliente => ViaCliente != null && ViaCliente.Length != 0;
        public bool IsTemViaUnica => ViaUnica != null && ViaUnica.Length != 0;
        public bool IsTemViaReduzida => ViaReduzida != null && ViaReduzida.Length != 0;
        public bool IsTemVia1 => Via1 != null && Via1.Length != 0;
        public bool IsTemVia2 => Via2 != null && Via2.Length != 0;



    }
}