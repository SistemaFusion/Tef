namespace Tef.Dominio
{
    public class RespostaCnc
    {
        public TefLinhaLista TefResposta { get; }
        public TefLinhaLista Resposta { get; }

        public RespostaCnc(TefLinhaLista tefResposta, TefLinhaLista resposta)
        {
            TefResposta = tefResposta;
            Resposta = resposta;
        }
    }
}