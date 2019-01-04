namespace Tef.Dominio
{
    public class TefExpress : AcTefDialHomologacao
    {
        public TefExpress(IAcTefRequisicao requisicao, IConfigAcTefDial configAcTefDial) : base(requisicao, configAcTefDial)
        {
        }

        protected override void ImprimirVias(TefLinhaLista respostaRequisicao)
        {
            _requisicao.OnImprimirVia(new TefExpressImprimeViaEventArgs(respostaRequisicao));
        }
    }
}