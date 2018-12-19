using System;
using System.Collections.Generic;

namespace Tef.Dominio.Testes
{
    public class AcTefRequisicaoFake : IAcTefRequisicao
    {
        public AcTefRequisicaoFake(ConfigRequisicao configRequisicao)
        {
            
        }

        public event EventHandler<AguardaRespostaEventArgs> AguardandoResposta;
        public event EventHandler<ImprimeViaEventArgs> ImprimirVia;
        public event EventHandler<ExibeMensagemEventArgs> ExibeMensagem;
        public event EventHandler<AutorizaDfeEventArgs> AutorizaDfe;
        public string ArquivoTemporario { get; }
        public string ArquivoRequisicao { get; }
        public string ArquivoResposta { get; }
        public string PastaBackup { get; }
        public string ArquivoSts { get; }
        public int EsperaSleep { get; }
        public int EsperaSts { get; }

        public TefLinhaLista Enviar(TefLinhaLista requisicao)
        {
            if (requisicao.BuscaLinha(AcTefIdentificadorCampos.Comando).Valor == "ATV")
            {
                var resposta = new List<TefLinha>
                {
                    new TefLinha("000-000", requisicao.BuscaLinha(AcTefIdentificadorCampos.Comando).Valor),
                    new TefLinha("001-000", requisicao.BuscaLinha(AcTefIdentificadorCampos.Identificacao).Valor),
                    new TefLinha("999-999", "0")
                };

                return new TefLinhaLista(resposta);
            }

            return null;
        }

        public void OnAguardandoResposta(AguardaRespostaEventArgs e)
        {
            throw new NotImplementedException();
        }

        public TefLinhaLista AguardaRespostaRequisicao()
        {
            throw new NotImplementedException();
        }

        public void OnImprimirVia(ImprimeViaEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void OnExibeMensagem(ExibeMensagemEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void OnAutorizaDfe(AutorizaDfeEventArgs e)
        {
            throw new NotImplementedException();
        }

        public List<string> BuscarCaminhosBackup()
        {
            throw new NotImplementedException();
        }
    }
}