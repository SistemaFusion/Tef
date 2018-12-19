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
            throw new NotImplementedException();
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