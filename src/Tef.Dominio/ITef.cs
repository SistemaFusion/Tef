using System;

namespace Tef.Dominio
{
    public interface ITef : IRequisicaoAtv
    {
        RespostaAtv Atv();
        RespostaAdm Adm();
        RespostaCnc Cnc(string rede, string nsu, DateTime transacaoEm, decimal valor);
        RespostaCrt Crt(decimal valor, string documentoVinculado);

        void Cnf(
            string redeAdquirente,
            string codigoControle,
            string nomeAutomacao,
            string versaoAutomacao,
            string registroCertificacao
        );

        void Ncn(string redeAdquirente, string codigoControle);
        void Inicializa();
    }
}