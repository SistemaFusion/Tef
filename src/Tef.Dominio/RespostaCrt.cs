namespace Tef.Dominio
{
    public class RespostaCrt
    {
        public TefLinhaLista TefResposta { get; }
        public TefLinhaLista Resposta { get; }

        public RespostaCrt(TefLinhaLista tefResposta, TefLinhaLista resposta)
        {
            TefResposta = tefResposta;
            Resposta = resposta;
        }
    }
}