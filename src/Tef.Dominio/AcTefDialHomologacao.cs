using System;
using Tef.Dominio.Enums;

namespace Tef.Dominio
{
    internal class AcTefDialHomologacao : AcTefDial
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

        public override RespostaCnc Cnc(string rede, string nsu, DateTime transacaoEm, decimal valor)
        {
            VerificaInicializado();
            var requisicao = FabricarRequisicao.MontaRequisicaoCnc(IdRequisicao, rede, nsu, transacaoEm, valor, _configAcTefDial);

            var tefResposta = EfetuaRequisicao(requisicao, out var respostaRequisicao);

            return new RespostaCnc(tefResposta, respostaRequisicao);
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


            var tefResposta = _requisicao.Enviar(requisicao, this);

            var respostaRequisicao = _requisicao.AguardaRespostaRequisicao();

            _requisicao.OnExibeMensagem(new ExibeMensagemEventArgs(respostaRequisicao));

            var autorizaDfeEventArgs = new AutorizaDfeEventArgs(respostaRequisicao);
            _requisicao.OnAutorizaDfe(autorizaDfeEventArgs);

            var statusTransacao = ConfereStatus(respostaRequisicao);
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
            var tefResposta = _requisicao.Enviar(requisicao, this);

            respostaRequisicaoAdm = _requisicao.AguardaRespostaRequisicao();


            _requisicao.OnExibeMensagem(new ExibeMensagemEventArgs(respostaRequisicaoAdm));

            if (ConfereStatus(respostaRequisicaoAdm))
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

                ImprimirVias(respostaRequisicaoAdm);
            }

            return tefResposta;
        }

        protected virtual bool ConfereStatus(TefLinhaLista respostaRequisicaoAdm)
        {
            return respostaRequisicaoAdm.ConfereStatus();
        }
    }
}