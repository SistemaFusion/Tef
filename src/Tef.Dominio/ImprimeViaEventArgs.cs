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


            if ((ViaEstabelecimento == null || ViaEstabelecimento.Length == 0)
                && (ViaCliente == null || ViaCliente.Length == 0) 
                && ViaUnica != null && ViaUnica.Length != 0)
            {
                Via1 = ViaUnica;
            }


            if (ViaEstabelecimento != null && ViaCliente != null 
                && ViaEstabelecimento.Length != 0 
                && ViaCliente.Length != 0)
            {
                Via1 = viaCliente.GetValores();
                Via2 = viaEstabelecimento.GetValores();
                return;
            }

            if ((ViaEstabelecimento == null || ViaEstabelecimento.Length == 0) 
                && ViaCliente != null && ViaCliente.Length != 0)
            {
                Via1 = ViaCliente;
                Via2 = ViaUnica;
                return;
            }

            if ((ViaCliente == null || ViaCliente.Length == 0)
                && ViaEstabelecimento != null && ViaEstabelecimento.Length != 0)
            {
                Via1 = ViaUnica;
                Via2 = ViaEstabelecimento;
            }

        }


        public string[] Via1 { get; }
        public string[] Via2 { get; }

        public ViaComprovante ViaDeComprovante { get; } = ViaComprovante.NaoHaComprovante;
        public string[] ViaEstabelecimento { get; }
        public string[] ViaCliente { get; }
        public string[] ViaUnica { get; }

        public TefLinhaLista TefLinhaLista { get; }
    }
}