using System.IO;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Principal;

namespace AppTesteTef
{
    public class ManipulaPasta
    {
        public ManipulaPasta(string diretorio)
        {
            Diretorio = diretorio;
        }

        public ManipulaPasta Voltar()
        {
            Diretorio = Directory.GetParent(Diretorio).ToString();
            return this;
        }

        public ManipulaPasta CriaPastaSeNaoExistir()
        {
            if (Directory.Exists(Diretorio)) return this;

            Directory.CreateDirectory(Diretorio);
            AdicionaSeguranca();

            return this;
        }

        private void AdicionaSeguranca()
        {
            var dirSec = Directory.GetAccessControl(Diretorio);

            var todos = new SecurityIdentifier(WellKnownSidType.WorldSid, null);

            dirSec.AddAccessRule(new FileSystemAccessRule(todos, FileSystemRights.FullControl, AccessControlType.Allow));

            Directory.SetAccessControl(Diretorio, dirSec);
        }

        public string Diretorio { get; set; }

        public static string LocalSistema()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        public ManipulaPasta IrPara(string pasta)
        {
            Diretorio += $"\\{pasta}";
            return this;
        }
    }
}
