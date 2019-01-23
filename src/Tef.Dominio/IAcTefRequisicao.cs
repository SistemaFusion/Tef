using System;
using System.Collections.Generic;

namespace Tef.Dominio
{
    public interface IAcTefRequisicao
    {
        event EventHandler<AguardaRespostaEventArgs> AguardandoResposta;
        event EventHandler<IImprimeViaEventArgs> ImprimirVia;
        event EventHandler<ExibeMensagemEventArgs> ExibeMensagem;
        event EventHandler<AutorizaDfeEventArgs> AutorizaDfe;
        event EventHandler<AntesRequisicaoEventArgs> AntesRequisicao;
        string ArquivoTemporario { get; }
        string ArquivoRequisicao { get; }
        string ArquivoResposta { get; }
        string PastaBackup { get; }
        string ArquivoSts { get; }
        int EsperaSleep { get; }
        int EsperaSts { get; }
        TefLinhaLista Enviar(TefLinhaLista requisicao, IRequisicaoAtv requisicaoAtv);
        void OnAguardandoResposta(AguardaRespostaEventArgs e);
        TefLinhaLista AguardaRespostaRequisicao();
        void OnImprimirVia(IImprimeViaEventArgs e);
        void OnExibeMensagem(ExibeMensagemEventArgs e);
        void OnAutorizaDfe(AutorizaDfeEventArgs e);
        void OnAntesRequisicao(AntesRequisicaoEventArgs antesRequisicaoEventArgs);
        List<string> BuscarCaminhosBackup();
    }
}