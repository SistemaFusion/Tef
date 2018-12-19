namespace Tef.Dominio
{
    public class ConfigAcTefDial : IConfigAcTefDial
    {
        public ConfigAcTefDial
            (
                string nomeAplicacaoComercial, 
                string versaoAplicacaoComercial, 
                string nomeAutomacaoComercial, 
                string registroCertificado, 
                bool suporteTroco, 
                bool suporteDesconto, 
                bool suporteValorReajustado, 
                bool suporteNsuTamanho40
            )
        {
            NomeAplicacaoComercial = nomeAplicacaoComercial;
            VersaoAplicacaoComercial = versaoAplicacaoComercial;
            NomeAutomacaoComercial = nomeAutomacaoComercial;
            RegistroCertificado = registroCertificado;
            SuporteTroco = suporteTroco;
            SuporteDesconto = suporteDesconto;
            SuporteValorReajustado = suporteValorReajustado;
            SuporteNsuTamanho40 = suporteNsuTamanho40;
        }

        public string NomeAplicacaoComercial { get; }
        public string VersaoAplicacaoComercial { get; }
        public string NomeAutomacaoComercial { get; }
        public string RegistroCertificado { get; }
        public bool SuporteTroco { get; }
        public bool SuporteDesconto { get; }
        public bool SuporteValorReajustado { get; }
        public bool SuporteNsuTamanho40 { get; }
    }
}