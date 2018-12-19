using System;
using System.IO;
using System.Reflection;

namespace Tef.Dominio
{
    public class ConfigRequisicao
    {
        public ConfigRequisicao() { }

        public ConfigRequisicao(
            int esperaSts, 
            int esperaSleep, 
            string arquivoTemporario, 
            string arquivoRequisicao, 
            string arquivoResposta, 
            string arquivoSts,
            string pastaBackup
            )
        {
            EsperaSts = esperaSts;
            EsperaSleep = esperaSleep;
            ArquivoTemporario = arquivoTemporario;
            ArquivoRequisicao = arquivoRequisicao;
            ArquivoResposta = arquivoResposta;
            ArquivoSts = arquivoSts;
            PastaBackup = pastaBackup;
        }

        public int EsperaSts { get; } = 7;
        public int EsperaSleep { get; } = 250;
        public string ArquivoTemporario { get; } = @"C:\TEF_DIAL\req\intpos.tmp";
        public string ArquivoRequisicao { get; } = @"C:\TEF_DIAL\req\intpos.001";
        public string ArquivoResposta { get; } = @"C:\TEF_DIAL\resp\intpos.001";
        public string ArquivoSts { get; } = @"C:\TEF_DIAL\resp\intpos.sts";
        public string PastaBackup { get; } =
            $"{Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path))}\\TEF";
    }
}