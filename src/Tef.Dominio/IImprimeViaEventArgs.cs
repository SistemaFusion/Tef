namespace Tef.Dominio
{
    public interface IImprimeViaEventArgs
    {
        TefLinhaLista TefLinhaLista { get; }
        ViaComprovante ViaDeComprovante { get; }
        string[] ViaEstabelecimento { get; }
        string[] ViaCliente { get; }
        string[] ViaUnica { get; }
        string[] ViaReduzida { get; }
        string[] Via1 { get; }
        string[] Via2 { get; }
        bool IsTemViaEstabelecimento { get; }
        bool IsTemViaCliente { get; }
        bool IsTemViaUnica { get; }
        bool IsTemViaReduzida { get; }
        bool IsTemVia1 { get; }
        bool IsTemVia2 { get; }
    }
}