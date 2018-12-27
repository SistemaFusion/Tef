using Tef.Dominio.Enums;

namespace Tef.Dominio
{
    public class RespostaCrt
    {
        public TefLinhaLista TefResposta { get; }
        public TefLinhaLista Resposta { get; }
        public AcTefStatus TefStatus { get; }

        public RespostaCrt(TefLinhaLista tefResposta, TefLinhaLista resposta, AcTefStatus acTefStatus)
        {
            TefResposta = tefResposta;
            Resposta = resposta;
            TefStatus = acTefStatus;
        }
    }
}