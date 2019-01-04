namespace Tef.Dominio
{
    public class TefExpressImprimeViaEventArgs : IImprimeViaEventArgs
    {
        public TefExpressImprimeViaEventArgs(TefLinhaLista respostaRequisicao)
        {
            
        }

        public TefLinhaLista TefLinhaLista { get; }
        public ViaComprovante ViaDeComprovante { get; }
        public string[] ViaEstabelecimento { get; }
        public string[] ViaCliente { get; }
        public string[] ViaUnica { get; }
        public string[] ViaReduzida { get; }
        public string[] Via1 { get; }
        public string[] Via2 { get; }
        public bool IsTemViaEstabelecimento { get; }
        public bool IsTemViaCliente { get; }
        public bool IsTemViaUnica { get; }
        public bool IsTemViaReduzida { get; }
        public bool IsTemVia1 { get; }
        public bool IsTemVia2 { get; }



    }
}