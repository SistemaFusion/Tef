namespace Tef.Dominio
{
    internal class Cappta : AcTefDialHomologacao
    {
        public Cappta(IAcTefRequisicao requisicao, IConfigAcTefDial configAcTefDial) : base(requisicao, configAcTefDial)
        {
            requisicao.AntesRequisicao += AntesRequisicaoCall;
        }

        private void AntesRequisicaoCall(object sender, AntesRequisicaoEventArgs e)
        {
            e.Remover(AcTefIdentificadorCampos.CapacidadesAutomacao);
            e.Remover(AcTefIdentificadorCampos.RegistroCertificacao);
            e.Remover(AcTefIdentificadorCampos.NomeAutomacao);

            var comando = e.Requisicao.BuscaLinha(AcTefIdentificadorCampos.Comando)?.Valor;

            if (comando == "CRT"
                || comando == "CNF")
            {
                e.Adicionar(new TefLinha("701-000", _configAcTefDial.NomeAplicacaoComercial));
                e.Adicionar(new TefLinha("716-000", _configAcTefDial.NomeAutomacaoComercial));
                e.Adicionar(new TefLinha("736-000", _configAcTefDial.VersaoAplicacaoComercial));
            }
        }
    }
}