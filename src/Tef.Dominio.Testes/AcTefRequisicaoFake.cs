using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Tef.Dominio.Testes
{
    public class AcTefRequisicaoFake : IAcTefRequisicao
    {
        private string _arquivoCancelamentoCnc;
        private string _arquivoSaqueAdm;
        private string _arquivoVenda10ReaisCrt;
        private TefLinhaLista _requisicao;

        public AcTefRequisicaoFake(ConfigRequisicao configRequisicao)
        {
            var path =
                $"{Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path))}";

            _arquivoCancelamentoCnc = $"{path}\\Assets\\ArquivoCancelamentoCnc.001";
            _arquivoSaqueAdm = $"{path}\\Assets\\ArquivoCancelamentoCnc.001";
            _arquivoVenda10ReaisCrt = $"{path}\\Assets\\ArquivoVenda10ReaisCrt.001";
        }

        public event EventHandler<AguardaRespostaEventArgs> AguardandoResposta;
        public event EventHandler<IImprimeViaEventArgs> ImprimirVia;
        public event EventHandler<ExibeMensagemEventArgs> ExibeMensagem;
        public event EventHandler<AutorizaDfeEventArgs> AutorizaDfe;
        public event EventHandler<AntesRequisicaoEventArgs> AntesRequisicao;
        public string ArquivoTemporario { get; }
        public string ArquivoRequisicao { get; }
        public string ArquivoResposta { get; }
        public string PastaBackup { get; }
        public string ArquivoSts { get; }
        public int EsperaSleep { get; }
        public int EsperaSts { get; }

        public TefLinhaLista Enviar(TefLinhaLista requisicao, IRequisicaoAtv requisicaoAtv)
        {
            _requisicao = requisicao;
            var resposta = new List<TefLinha>
            {
                new TefLinha("000-000", requisicao.BuscaLinha(AcTefIdentificadorCampos.Comando).Valor),
                new TefLinha("001-000", requisicao.BuscaLinha(AcTefIdentificadorCampos.Identificacao).Valor),
                new TefLinha("999-999", "0")
            };

            return new TefLinhaLista(resposta);
        }

        public void OnAguardandoResposta(AguardaRespostaEventArgs e)
        {
            throw new NotImplementedException();
        }

        public TefLinhaLista AguardaRespostaRequisicao()
        {
            var resposta = new List<TefLinha>();

            if (_requisicao.BuscaLinha(AcTefIdentificadorCampos.Comando).Valor == "CNC")
            {
                AdicionaLinhasDeACordoComRequisicao(resposta, _arquivoCancelamentoCnc);
            }

            if (_requisicao.BuscaLinha(AcTefIdentificadorCampos.Comando).Valor == "ADM")
            {
                AdicionaLinhasDeACordoComRequisicao(resposta, _arquivoSaqueAdm);
            }

            if (_requisicao.BuscaLinha(AcTefIdentificadorCampos.Comando).Valor == "CRT")
            {
                AdicionaLinhasDeACordoComRequisicao(resposta, _arquivoVenda10ReaisCrt);
            }

            return new TefLinhaLista(resposta);
        }

        private void AdicionaLinhasDeACordoComRequisicao(List<TefLinha> resposta, string arquivo)
        {
            using (var sr = new StreamReader(arquivo))
            {
                var linha = string.Empty;

                while ((linha = sr.ReadLine()) != null)
                {
                    resposta.Add(new TefLinha(linha));
                }
            }
        }

        public void OnImprimirVia(IImprimeViaEventArgs e)
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

        public void OnAntesRequisicao(AntesRequisicaoEventArgs antesRequisicaoEventArgs)
        {
            
        }

        public List<string> BuscarCaminhosBackup()
        {
            throw new NotImplementedException();
        }
    }
}