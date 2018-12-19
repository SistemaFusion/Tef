using System;
using System.Linq;

namespace Tef.Dominio
{
    public class ImprimeViaEventArgs : EventArgs
    {
        public ImprimeViaEventArgs(TefLinhaLista respostaRequisicaoAdm)
        {
            var campo737 = respostaRequisicaoAdm.BuscaLinha(737);
            var campo028 = respostaRequisicaoAdm.BuscaLinha(028);

            if (campo737 != null)
                ViaDeComprovante = (ViaComprovante) int.Parse(campo737.Valor);

            if (ViaDeComprovante == ViaComprovante.NaoHaComprovante && campo028 != null && campo028.Valor != "0")
                ViaDeComprovante = ViaComprovante.ViaClienteIEstabelecimento;

            var viaEstabelecimento = new TefLinhaLista(respostaRequisicaoAdm.Where(x => x.Identificacao == 715).ToList());
            var viaCliente = new TefLinhaLista(respostaRequisicaoAdm.Where(x => x.Identificacao == 713).ToList());
            var viaUnica = new TefLinhaLista(respostaRequisicaoAdm.Where(x => x.Identificacao == 029).ToList());


            ViaEstabelecimento = viaEstabelecimento.GetValores();
            ViaCliente = viaCliente.GetValores();
            ViaUnica = viaUnica.GetValores();
            TefLinhaLista = respostaRequisicaoAdm;
        }

        public ViaComprovante ViaDeComprovante { get; } = ViaComprovante.NaoHaComprovante;
        public string[] ViaEstabelecimento { get; }
        public string[] ViaCliente { get; }
        public string[] ViaUnica { get; }

        public TefLinhaLista TefLinhaLista { get; }
    }
}