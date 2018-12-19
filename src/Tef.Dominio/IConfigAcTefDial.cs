namespace Tef.Dominio
{
    public interface IConfigAcTefDial
    {
        string NomeAplicacaoComercial { get; }
        string VersaoAplicacaoComercial { get; }
        string NomeAutomacaoComercial { get; }
        string RegistroCertificado { get; }
        bool SuporteTroco { get; }
        bool SuporteDesconto { get; }
        bool SuporteValorReajustado { get; }
        bool SuporteNsuTamanho40 { get; }
    }
}