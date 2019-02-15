namespace Tef.Dominio
{
    internal class PayGo : AcTefDialHomologacao
    {
        public PayGo(IAcTefRequisicao requisicao, IConfigAcTefDial configAcTefDial) : base(requisicao, configAcTefDial)
        {
        }
    }
}