namespace Tef.Dominio
{
    public class PayGo : AcTefDialHomologacao
    {
        public PayGo(IAcTefRequisicao requisicao, IConfigAcTefDial configAcTefDial) : base(requisicao, configAcTefDial)
        {
        }
    }
}