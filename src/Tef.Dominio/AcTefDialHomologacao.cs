using System;
using Tef.Dominio.Enums;

namespace Tef.Dominio
{
    internal class AcTefDialHomologacao : AcTefDial
    {
        private TefLinhaLista _respostaRequisicaoCrt;
        private TefLinhaLista _tefRespostaCrt;
        private bool _statusTransacao;
        private AcTefStatus _statusTransacaoCrt;

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

        public override RespostaCrt Crt(decimal valor, string documentoVinculado, bool confirmarManual = false)
        {
            VerificaInicializado();
            var requisicao = FabricarRequisicao.MontaRequisicaoCrt(
                IdRequisicao, 
                valor, 
                documentoVinculado,
                NomeAutomacaoComercial,
                RegistroCertificacao,
                _configAcTefDial);


            _tefRespostaCrt = _requisicao.Enviar(requisicao, this);

            _respostaRequisicaoCrt = _requisicao.AguardaRespostaRequisicao();

            _requisicao.OnExibeMensagem(new ExibeMensagemEventArgs(_respostaRequisicaoCrt));

            var autorizaDfeEventArgs = new AutorizaDfeEventArgs(_respostaRequisicaoCrt);
            _requisicao.OnAutorizaDfe(autorizaDfeEventArgs);

            _statusTransacao = ConfereStatus(_respostaRequisicaoCrt);
            _statusTransacaoCrt = _statusTransacao ? AcTefStatus.Sucesso : AcTefStatus.Falha;

            if (confirmarManual)
                return new RespostaCrt(_tefRespostaCrt, _respostaRequisicaoCrt, _statusTransacaoCrt);

            return ConfirmarCrt(autorizaDfeEventArgs);
        }

        public override RespostaCrt ConfirmarCrt(AutorizaDfeEventArgs autorizaDfeEventArgs)
        {
            if (_statusTransacao)
            {
                if (autorizaDfeEventArgs.IsContemRejeicao() || autorizaDfeEventArgs.IsContemErro())
                {
                    if (_respostaRequisicaoCrt.RequerConfirmacao())
                        Ncn(
                            _respostaRequisicaoCrt.BuscaLinha(AcTefIdentificadorCampos.RedeAdquirente).Valor,
                            _respostaRequisicaoCrt.BuscaLinha(AcTefIdentificadorCampos.CodigoControle).Valor
                        );
                }

                if (_respostaRequisicaoCrt.RequerConfirmacao() && autorizaDfeEventArgs.IsAutorizado())
                {
                    Cnf(
                        _respostaRequisicaoCrt.BuscaLinha(AcTefIdentificadorCampos.RedeAdquirente).Valor,
                        _respostaRequisicaoCrt.BuscaLinha(AcTefIdentificadorCampos.CodigoControle).Valor,
                        NomeAplicativoComercial,
                        VersaoAplicativoComercial,
                        RegistroCertificacao
                    );
                }


                ImprimirVias(_respostaRequisicaoCrt);
            }

            return new RespostaCrt(_tefRespostaCrt, _respostaRequisicaoCrt, _statusTransacaoCrt);
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