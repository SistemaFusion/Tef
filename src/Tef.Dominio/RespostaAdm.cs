namespace Tef.Dominio
{
    public class RespostaAdm
    {
        public TefLinhaLista TefResposta { get; }
        public TefLinhaLista Resposta { get; }

        public RespostaAdm(TefLinhaLista tefResposta, TefLinhaLista resposta)
        {
            TefResposta = tefResposta;
            Resposta = resposta;
        }
    }
}