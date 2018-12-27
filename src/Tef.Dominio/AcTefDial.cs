using System;
using Tef.Dominio.Enums;

namespace Tef.Dominio
{
    public class AcTefDial : ITef
    {
        protected virtual string NomeAutomacaoComercial { get; }
        protected virtual string NomeAplicativoComercial { get; }
        protected virtual string VersaoAplicativoComercial { get; }
        protected virtual string RegistroCertificacao { get; }
        protected virtual int IdRequisicao { get; }
        protected readonly IAcTefRequisicao _requisicao;
        protected readonly IConfigAcTefDial _configAcTefDial;
        protected bool Inicializado { get; set; }

        public AcTefDial(
            IAcTefRequisicao requisicao,
            IConfigAcTefDial configAcTefDial
            )
        {
            NomeAplicativoComercial = configAcTefDial.NomeAplicacaoComercial;
            VersaoAplicativoComercial = configAcTefDial.VersaoAplicacaoComercial;
            RegistroCertificacao = configAcTefDial.RegistroCertificado;
            NomeAutomacaoComercial = configAcTefDial.NomeAutomacaoComercial;
            _requisicao = requisicao;
            _configAcTefDial = configAcTefDial;
            IdRequisicao = (int)DateTime.Now.TimeOfDay.TotalSeconds;
        }

        public virtual RespostaAtv Atv()
        {
            VerificaInicializado();
            var tefResposta = _requisicao.Enviar(FabricarRequisicao.MontaRequisicaoAtv(IdRequisicao, _configAcTefDial));

            return new RespostaAtv(tefResposta);
        }

        public virtual RespostaAdm Adm()
        {
            VerificaInicializado();
            var requisicao = FabricarRequisicao.MontaRequisicaoAdm(IdRequisicao, _configAcTefDial);

            var tefResposta = EfetuaRequisicao(requisicao, out var respostaRequisicaoAdm);

            return new RespostaAdm(tefResposta, respostaRequisicaoAdm);
        }

        public virtual RespostaCnc Cnc(string rede, string nsu, DateTime transacaoEm, decimal valor)
        {
            VerificaInicializado();
            var requisicao = FabricarRequisicao.MontaRequisicaoCnc(IdRequisicao, rede, nsu, transacaoEm, valor, _configAcTefDial);

            var tefResposta = EfetuaRequisicao(requisicao, out var respostaRequisicao);

            return new RespostaCnc(tefResposta, respostaRequisicao);
        }

        public virtual RespostaCrt Crt(decimal valor, string documentoVinculado)
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

            var statusTransacao = respostaRequisicao.ConfereStatus();
            var acTefStatus = statusTransacao ? AcTefStatus.Sucesso : AcTefStatus.Falha;

            return new RespostaCrt(tefResposta, respostaRequisicao, acTefStatus);
        }

        protected virtual TefLinhaLista EfetuaRequisicao(TefLinhaLista requisicao, out TefLinhaLista respostaRequisicaoAdm)
        {
            var tefResposta = _requisicao.Enviar(requisicao);

            respostaRequisicaoAdm = _requisicao.AguardaRespostaRequisicao();

            return tefResposta;
        }

        public virtual void Cnf(
            string redeAdquirente,
            string codigoControle,
            string nomeAutomacao,
            string versaoAutomacao,
            string registroCertificacao
        )
        {
            VerificaInicializado();
            _requisicao.Enviar(FabricarRequisicao.MontaRequisicaoCnf(
                IdRequisicao,
                redeAdquirente,
                codigoControle,
                nomeAutomacao,
                versaoAutomacao,
                registroCertificacao
            ));
        }

        public virtual void Ncn(string redeAdquirente, string codigoControle)
        {
            VerificaInicializado();
            _requisicao.Enviar(FabricarRequisicao.MontaRequisicaoNcn(IdRequisicao, redeAdquirente, RegistroCertificacao, codigoControle));
        }


        public virtual void Inicializa()
        {
            Inicializado = true;
            TransacaoPendente();
        }

        protected virtual void TransacaoPendente()
        {
            if (!Arquivo.Existe(_requisicao.ArquivoResposta)) return;

            var listaLinha = TefLinhaLista.LoadArquivo(_requisicao.ArquivoResposta);

            if (listaLinha.ConfereStatus())
                Ncn(
                listaLinha.BuscaLinha(AcTefIdentificadorCampos.RedeAdquirente).Valor,
                listaLinha.BuscaLinha(AcTefIdentificadorCampos.CodigoControle).Valor
                );
        }

        protected virtual void VerificaInicializado()
        {
            AcTefException.Quando(Inicializado == false, "Inicializar AcTef TEF!");
        }
    }
}