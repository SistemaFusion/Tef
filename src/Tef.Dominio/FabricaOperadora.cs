using System;
using Tef.Dominio.Enums;

namespace Tef.Dominio
{
    public static class FabricaOperadora
    {
        public static ITef RetornaOperadora(Operadora operadora, IAcTefRequisicao requisicao, IConfigAcTefDial configAcTefDial)
        {
            switch (operadora)
            {
                case Operadora.PayGo:
                    return new PayGo(requisicao, configAcTefDial);
                case Operadora.TefExpress:
                    return new TefExpress(requisicao, configAcTefDial);
                case Operadora.Cappta:
                    return new Cappta(requisicao, configAcTefDial);
                case Operadora.Linx:
                    return new Linx(requisicao, configAcTefDial);
                case Operadora.TefDial:
                    return new AcTefDial(requisicao, configAcTefDial);
                case Operadora.TefDialHomologacao:
                    return new AcTefDialHomologacao(requisicao, configAcTefDial);
                default:
                    throw new ArgumentOutOfRangeException(nameof(operadora), operadora, null);
            }
        }
    }
}