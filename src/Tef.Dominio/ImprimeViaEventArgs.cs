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

            var viaEstabelecimento = new TefLinhaLista(respostaRequisicaoAdm.Where(x => x.Identificacao == AcTefIdentificadorCampos.ViaEstabelecimentoComprovante).ToList());
            var viaCliente = new TefLinhaLista(respostaRequisicaoAdm.Where(x => x.Identificacao == AcTefIdentificadorCampos.ViaClienteComprovante).ToList());
            var viaUnica = new TefLinhaLista(respostaRequisicaoAdm.Where(x => x.Identificacao == AcTefIdentificadorCampos.ViaUnicaComprovante).ToList());
            var viaReduzida = new TefLinhaLista(respostaRequisicaoAdm.Where(x => x.Identificacao == AcTefIdentificadorCampos.ViaReduzidaComprovante));


            ViaEstabelecimento = viaEstabelecimento.GetValores();
            ViaCliente = viaCliente.GetValores();
            ViaUnica = viaUnica.GetValores();
            ViaReduzida = viaReduzida.GetValores();
            TefLinhaLista = respostaRequisicaoAdm;
        }

        public TefLinhaLista TefLinhaLista { get; }
        public ViaComprovante ViaDeComprovante { get; } = ViaComprovante.NaoHaComprovante;

        public string[] ViaEstabelecimento { get; }
        public string[] ViaCliente { get; }
        public string[] ViaUnica { get; }
        public string[] ViaReduzida { get; }
        public string[] Via1 => GetVia1();
        public string[] Via2 => GetVia2();

        public bool IsTemViaEstabelecimento => ViaEstabelecimento != null && ViaEstabelecimento.Length != 0;
        public bool IsTemViaCliente => ViaCliente != null && ViaCliente.Length != 0;
        public bool IsTemViaUnica => ViaUnica != null && ViaUnica.Length != 0;
        public bool IsTemViaReduzida => ViaReduzida != null && ViaReduzida.Length != 0;
        public bool IsTemVia1 => Via1 != null && Via1.Length != 0;
        public bool IsTemVia2 => Via2 != null && Via2.Length != 0;

        private string[] GetVia1()
        {
            string[] via1 = null;

            if (IsTemViaCliente)
                via1 = ViaCliente;

            if (IsTemViaCliente == false && IsTemViaUnica)
                via1 = ViaUnica;

            if (IsTemViaCliente == false && IsTemViaEstabelecimento == false && IsTemViaReduzida)
                via1 = ViaReduzida;

            return via1;
        }

        private string[] GetVia2()
        {
            string[] via2 = null;

            if (IsTemViaEstabelecimento)
                via2 = ViaEstabelecimento;

            if (IsTemViaEstabelecimento == false && IsTemViaUnica)
                via2 = ViaUnica;

            return via2;
        }
    }
}