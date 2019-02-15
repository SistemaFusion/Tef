namespace Tef.Dominio
{
    internal class TefExpress : AcTefDialHomologacao
    {
        public TefExpress(IAcTefRequisicao requisicao, IConfigAcTefDial configAcTefDial) : base(requisicao, configAcTefDial)
        {
        }

        protected override void ImprimirVias(TefLinhaLista respostaRequisicao)
        {
            _requisicao.OnImprimirVia(new TefExpressImprimeViaEventArgs(respostaRequisicao));
        }

        protected override bool ConfereStatus(TefLinhaLista respostaRequisicao)
        {
            var linha028 = respostaRequisicao.BuscaLinha(AcTefIdentificadorCampos.TamanhoViaUnica, 0);

            if (linha028 == null) return false;

            if (int.Parse(linha028.Valor) == 0) return false;

            return true;
        }
    }
}