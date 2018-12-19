using System;
using System.Collections.Generic;

namespace Tef.Dominio
{
    public interface IAcTefRequisicao
    {
        event EventHandler<AguardaRespostaEventArgs> AguardandoResposta;
        event EventHandler<ImprimeViaEventArgs> ImprimirVia;
        event EventHandler<ExibeMensagemEventArgs> ExibeMensagem;
        event EventHandler<AutorizaDfeEventArgs> AutorizaDfe;
        string ArquivoTemporario { get; }
        string ArquivoRequisicao { get; }
        string ArquivoResposta { get; }
        string PastaBackup { get; }
        string ArquivoSts { get; }
        int EsperaSleep { get; }
        int EsperaSts { get; }
        TefLinhaLista Enviar(TefLinhaLista requisicao);
        void OnAguardandoResposta(AguardaRespostaEventArgs e);
        TefLinhaLista AguardaRespostaRequisicao();
        void OnImprimirVia(ImprimeViaEventArgs e);
        void OnExibeMensagem(ExibeMensagemEventArgs e);
        void OnAutorizaDfe(AutorizaDfeEventArgs e);
        List<string> BuscarCaminhosBackup();
    }
}