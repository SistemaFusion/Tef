using System.IO;
using System.Text;

namespace Tef.Dominio
{
    public static class Arquivo
    {
        public static void Deletar(string arquivo)
        {
            if (!File.Exists(arquivo)) return;

            File.Delete(arquivo);

            AcTefException.Quando(File.Exists(arquivo), $"Erro ao apagar arquivo\n{arquivo}");
        }

        public static void EscrevaTodasLinhas(string arquivoTemporario, string[] requisicao)
        {
            File.WriteAllLines(arquivoTemporario, requisicao, Encoding.ASCII);
        }

        public static void Mover(string arquivoTemporario, string arquivoRequisicao)
        {
            File.Move(arquivoTemporario, arquivoRequisicao);
        }

        public static bool Existe(string arquivo)
        {
            return File.Exists(arquivo);
        }

        public static void EfetuarBackupResposta(string pastaBackup, TefLinhaLista tefLinhaLista)
        {
            var idBackup = 0;
            string arquivo;

            do
            {
                idBackup++;
                arquivo = Path.Combine(pastaBackup, $@"AcTef_{idBackup:000}.tef");
            } while (File.Exists(arquivo));

            File.WriteAllLines(arquivo, tefLinhaLista.GetRequisicao());
        }
    }
}