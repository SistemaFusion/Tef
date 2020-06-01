using System;

namespace Tef.Dominio
{
    public static class FabricarRequisicao
    {
        public static TefLinhaLista MontaRequisicaoAtv(int idRequisicao, IConfigAcTefDial configAcTefDial)
        {

            var listaRequisicao = new TefLinhaLista
            {
                new TefLinha("000-000", "ATV"),
                new TefLinha("001-000", idRequisicao),
                new TefLinha("738-000", configAcTefDial.RegistroCertificado),
                new TefLinha("999-999", "0")
            };
            return listaRequisicao;
        }

        public static TefLinhaLista MontaRequisicaoAdm(int idRequisicao, IConfigAcTefDial configAcTefDial)
        {
            var listaRequisicao = new TefLinhaLista
            {
                new TefLinha("000-000", "ADM"),
                new TefLinha("001-000", idRequisicao),
                new TefLinha("738-000", configAcTefDial.RegistroCertificado)
            };

            CapacidadesAutomacao(configAcTefDial, listaRequisicao);

            listaRequisicao.Add(new TefLinha("999-999", "0"));

            return listaRequisicao;
        }

        public static TefLinhaLista MontaRequisicaoCnc(
            int idRequisicao,
            string rede,
            string nsu,
            DateTime transacaoEm,
            decimal valor,
            IConfigAcTefDial configAcTefDial
            )
        {
            var listaRequisicao = new TefLinhaLista
            {
                new TefLinha("000-000", "CNC"),
                new TefLinha("001-000", idRequisicao),
                new TefLinha("003-000", valor),
                new TefLinha("010-000", rede),
                new TefLinha("012-000", nsu),
                new TefLinha("022-000", transacaoEm.Date.ToString("ddMMyyyy")),
                new TefLinha("023-000", transacaoEm.TimeOfDay.ToString("hhmmss")),
                new TefLinha("738-000", configAcTefDial.RegistroCertificado)
            };

            CapacidadesAutomacao(configAcTefDial, listaRequisicao);

            listaRequisicao.Add(new TefLinha("999-999", "0"));

            return listaRequisicao;
        }

        public static TefLinhaLista MontaRequisicaoCnf(
            int idRequisicao,
            string redeAdquirente,
            string nsu,
            string codigoControle,
            string nomeAutomacao,
            string versaoAutomacao,
            string registroCertificacao)
        {
            var listaRequisicao = new TefLinhaLista
            {
                new TefLinha("000-000", "CNF"),
                new TefLinha("001-000", idRequisicao),
                new TefLinha("010-000", redeAdquirente),
                new TefLinha("012-000", nsu),
                new TefLinha("027-000", codigoControle),
                new TefLinha("735-000", nomeAutomacao),
                new TefLinha("736-000", versaoAutomacao),
                new TefLinha("738-000", registroCertificacao),
                new TefLinha("999-999", "0")
            };
            return listaRequisicao;
        }

        public static TefLinhaLista MontaRequisicaoCrt(
            int idRequisicao,
            decimal valor,
            string documentoVinculado,
            string nomeAutomacao,
            string registroCertificacao,
            IConfigAcTefDial configAcTefDial
            )
        {
            var listaRequisicao = new TefLinhaLista
            {
                new TefLinha("000-000", "CRT"),
                new TefLinha("001-000", idRequisicao),
                new TefLinha("002-000", documentoVinculado),
                new TefLinha("003-000", valor),
                new TefLinha("004-000", "0"),
                new TefLinha("716-000", nomeAutomacao),
                new TefLinha("738-000", registroCertificacao)
            };

            CapacidadesAutomacao(configAcTefDial, listaRequisicao);

            listaRequisicao.Add(new TefLinha("999-999", "0"));

            return listaRequisicao;
        }

        public static TefLinhaLista MontaRequisicaoNcn(
            int idRequisicao,
            string redeAdquirente,
            string registroCertificacao,
            string codigoControle
            )
        {
            var listaRequisicao = new TefLinhaLista
            {
                new TefLinha("000-000", "NCN"),
                new TefLinha("001-000", idRequisicao),
                new TefLinha("010-000", redeAdquirente),
                new TefLinha("027-000", codigoControle),
                new TefLinha("738-000", registroCertificacao),
                new TefLinha("999-999", "0")
            };
            return listaRequisicao;
        }

        private static void CapacidadesAutomacao(IConfigAcTefDial configAcTefDial, TefLinhaLista listaRequisicao)
        {
            var capacidadesDaAutomacao = 0;

            if (configAcTefDial.SuporteTroco)
            {
                capacidadesDaAutomacao += 1;
            }

            if (configAcTefDial.SuporteDesconto)
            {
                capacidadesDaAutomacao += 2;
            }

            if (configAcTefDial.SuporteValorReajustado)
            {
                capacidadesDaAutomacao += 64;
            }

            if (configAcTefDial.SuporteNsuTamanho40)
            {
                capacidadesDaAutomacao += 128;
            }

            if (capacidadesDaAutomacao > 0)
            {
                listaRequisicao.Add(new TefLinha("706-000", capacidadesDaAutomacao));
            }
        }

        private static void Identificacao(IConfigAcTefDial configAcTefDial, TefLinhaLista listaRequisicao)
        {
            listaRequisicao.Add(new TefLinha("735-000", configAcTefDial.NomeAplicacaoComercial));
            listaRequisicao.Add(new TefLinha("736-000", configAcTefDial.VersaoAplicacaoComercial));
        }
    }
}