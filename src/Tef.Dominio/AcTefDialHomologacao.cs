using Tef.Dominio.Enums;

namespace Tef.Dominio
{
    public class AcTefDialHomologacao : AcTefDial
    {
        public AcTefDialHomologacao(
            IAcTefRequisicao requisicao,
            IConfigAcTefDial configAcTefDial
            ) : base(requisicao, configAcTefDial)
        {
        }

        public override RespostaAdm Adm()
        {
            VerificaInicializado();
            var requisicao = FabricarRequisicao.MontaRequisicaoAdm(IdRequisicao, _configAcTefDial);

            var tefResposta = EfetuaRequisicao(requisicao, out var respostaRequisicaoAdm);

            return new RespostaAdm(tefResposta, respostaRequisicaoAdm);
        }

        public override RespostaCrt Crt(decimal valor, string documentoVinculado)
        {
            VerificaInicializado();
            var requisicao = FabricarRequisicao.MontaRequisicaoCrt(
                IdRequisicao, 
                valor, 
                documentoVinculado,
                NomeAutomacaoComercial,
                RegistroCertificacao,
                _configAcTefDial);


            var tefResposta = _requisicao.Enviar(requisicao);

            var respostaRequisicao = _requisicao.AguardaRespostaRequisicao();

            _requisicao.OnExibeMensagem(new ExibeMensagemEventArgs(respostaRequisicao));

            var autorizaDfeEventArgs = new AutorizaDfeEventArgs(respostaRequisicao);
            _requisicao.OnAutorizaDfe(autorizaDfeEventArgs);

            var statusTransacao = respostaRequisicao.ConfereStatus();
            var acTefStatus = statusTransacao ? AcTefStatus.Sucesso : AcTefStatus.Falha;

            if (statusTransacao)
            {
                if (autorizaDfeEventArgs.IsContemRejeicao() || autorizaDfeEventArgs.IsContemErro())
                {
                    if (respostaRequisicao.RequerConfirmacao())
                        Ncn(
                            respostaRequisicao.BuscaLinha(AcTefIdentificadorCampos.RedeAdquirente).Valor,
                            respostaRequisicao.BuscaLinha(AcTefIdentificadorCampos.CodigoControle).Valor
                        );
                }

                if (respostaRequisicao.RequerConfirmacao() && autorizaDfeEventArgs.IsAutorizado())
                {
                    Cnf(
                        respostaRequisicao.BuscaLinha(AcTefIdentificadorCampos.RedeAdquirente).Valor,
                        respostaRequisicao.BuscaLinha(AcTefIdentificadorCampos.CodigoControle).Valor,
                        NomeAplicativoComercial,
                        VersaoAplicativoComercial,
                        RegistroCertificacao
                    );
                }


                ImprimirVias(respostaRequisicao);
            }
               
            return new RespostaCrt(tefResposta, respostaRequisicao, acTefStatus);
        }

        protected virtual void ImprimirVias(TefLinhaLista respostaRequisicao)
        {
            _requisicao.OnImprimirVia(new ImprimeViaEventArgs(respostaRequisicao));
        }

        protected override TefLinhaLista EfetuaRequisicao(TefLinhaLista requisicao, out TefLinhaLista respostaRequisicaoAdm)
        {
            var tefResposta = _requisicao.Enviar(requisicao);

            respostaRequisicaoAdm = _requisicao.AguardaRespostaRequisicao();


            _requisicao.OnExibeMensagem(new ExibeMensagemEventArgs(respostaRequisicaoAdm));

            if (respostaRequisicaoAdm.ConfereStatus())
            {
                if (respostaRequisicaoAdm.RequerConfirmacao())
                {
                    Cnf(
                        respostaRequisicaoAdm.BuscaLinha(AcTefIdentificadorCampos.RedeAdquirente).Valor,
                        respostaRequisicaoAdm.BuscaLinha(AcTefIdentificadorCampos.CodigoControle).Valor,
                        NomeAplicativoComercial,
                        VersaoAplicativoComercial,
                        RegistroCertificacao
                    );
                }


                _requisicao.OnImprimirVia(new ImprimeViaEventArgs(respostaRequisicaoAdm));
            }

            return tefResposta;
        }
    }
}