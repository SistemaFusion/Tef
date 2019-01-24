using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Tef.Dominio;

namespace Tef.Infra
{
    public class AcTefRequisicao : IAcTefRequisicao
    {
        public event EventHandler<AguardaRespostaEventArgs> AguardandoResposta;
        public event EventHandler<IImprimeViaEventArgs> ImprimirVia;
        public event EventHandler<ExibeMensagemEventArgs> ExibeMensagem;
        public event EventHandler<AutorizaDfeEventArgs> AutorizaDfe;

        public string ArquivoTemporario { get; }
        public string ArquivoRequisicao { get; }
        public string ArquivoResposta { get; }
        public string PastaBackup { get; }
        public string ArquivoSts { get; }
        public int EsperaSleep { get; }
        public int EsperaSts { get; }


        public AcTefRequisicao(ConfigRequisicao configuracao)
        {
            EsperaSleep = configuracao.EsperaSleep;
            EsperaSts = configuracao.EsperaSts;

            ArquivoTemporario = configuracao.ArquivoTemporario;
            ArquivoRequisicao = configuracao.ArquivoRequisicao;
            ArquivoResposta = configuracao.ArquivoResposta;
            PastaBackup = configuracao.PastaBackup;
            ArquivoSts = configuracao.ArquivoSts;
        }

        public TefLinhaLista Enviar(TefLinhaLista requisicao)
        {
            CriaRequisicao(requisicao);

            EfetuaRequisicao();

            AguardarRequisicao();

            var tefListaDeLinha = TefLinhaLista.LoadArquivo(ArquivoSts);

            Arquivo.Deletar(ArquivoTemporario);
            Arquivo.Deletar(ArquivoSts);

            return tefListaDeLinha;
        }

        public TefLinhaLista AguardaRespostaRequisicao()
        {
            var tempoInicioEspera = DateTime.Now;

            bool interromper;
            bool existeArquivoResposta;

            do
            {
                Thread.Sleep(EsperaSleep);

                var aguardandoResposta = new AguardaRespostaEventArgs(ArquivoResposta, tempoInicioEspera);
                OnAguardandoResposta(aguardandoResposta);

                interromper = aguardandoResposta.Interromper;
                existeArquivoResposta = Arquivo.Existe(ArquivoResposta);

            } while (!existeArquivoResposta && !interromper);


            var tefLinhas = TefLinhaLista.LoadArquivo(ArquivoResposta);

            EfetuarBackup(tefLinhas);
            Arquivo.Deletar(ArquivoResposta);

            return tefLinhas;
        }

        private void CriaRequisicao(TefLinhaLista requisicao)
        {
            Arquivo.Deletar(ArquivoTemporario);
            Arquivo.Deletar(ArquivoRequisicao);
            Arquivo.Deletar(ArquivoResposta);
            Arquivo.Deletar(ArquivoSts);

            Arquivo.EscrevaTodasLinhas(ArquivoTemporario, requisicao.GetRequisicao());
        }

        private void EfetuaRequisicao()
        {
            Arquivo.Mover(ArquivoTemporario, ArquivoRequisicao);
        }

        private void AguardarRequisicao()
        {
            var tempoFimEspera = DateTime.Now.AddSeconds(EsperaSts);

            bool interromper;
            bool existeArquivoSts;
            bool timeOut;

            do
            {
                Thread.Sleep(EsperaSleep);

                var aguardandoResposta = new AguardaRespostaEventArgs(ArquivoSts, tempoFimEspera);
                OnAguardandoResposta(aguardandoResposta);

                interromper = aguardandoResposta.Interromper;
                existeArquivoSts = Arquivo.Existe(ArquivoSts);
                timeOut = DateTime.Now > tempoFimEspera;

            } while (!existeArquivoSts && !interromper && !timeOut);

            AcTefException.Quando(!Arquivo.Existe(ArquivoSts), "O gerenciador padrão TEFDial não está ativo!");
        }

        public void OnAguardandoResposta(AguardaRespostaEventArgs e)
        {
            AguardandoResposta?.Invoke(this, e);
        }

        public virtual void OnImprimirVia(IImprimeViaEventArgs e)
        {
            ImprimirVia?.Invoke(this, e);
        }

        public void OnExibeMensagem(ExibeMensagemEventArgs e)
        {
            ExibeMensagem?.Invoke(this, e);
        }

        public void OnAutorizaDfe(AutorizaDfeEventArgs e)
        {
            AutorizaDfe?.Invoke(this, e);
        }

        private void EfetuarBackup(TefLinhaLista tefLinhaLista)
        {
            if (!Directory.Exists(PastaBackup))
            {
                Directory.CreateDirectory(PastaBackup);
            }

            Arquivo.EfetuarBackupResposta(PastaBackup, tefLinhaLista);
        }

        public List<string> BuscarCaminhosBackup()
        {
            var arquivosVerificar = new List<string>();

            var searchPattern = "AcTef_*.tef";
            arquivosVerificar.AddRange(Directory.GetFiles(PastaBackup, searchPattern));

            return arquivosVerificar;
        }
    }
}