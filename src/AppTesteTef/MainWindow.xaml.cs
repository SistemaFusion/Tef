using System;
using System.Windows;
using Tef.Dominio;
using Tef.Dominio.Enums;
using Tef.Infra;

namespace AppTesteTef
{
    public partial class MainWindow
    {
        private readonly Operadora _operadora;

        public MainWindow()
        {
            InitializeComponent();
            _operadora = Operadora.Cappta;
        }

        private ITef FabricaOperadora()
        {
            var requisicao = new AcTefRequisicao(new ConfigRequisicao());

            requisicao.AguardandoResposta += AguardandoResposta;
            requisicao.ExibeMensagem += ExibeMensagemAction;
            requisicao.ImprimirVia += ImprimirViaAction;
            //requisicao.AutorizaDfe += AutorizaDfe;

            var configuracao = new ConfigAcTefDial(
                "teste",
                "versaoTeste",
                "nomeTesteAutomacao",
                "83838",
                false, false, false, false
            );

            var iTef = Tef.Dominio.FabricaOperadora.RetornaOperadora(_operadora, requisicao, configuracao);

            iTef.Inicializa();

            return iTef;
        }

        private void Atv_OnClick(object sender, RoutedEventArgs e)
        {
            var acTefDial = FabricaOperadora();
            var respostaAtv = acTefDial.Atv();
        }

        private void ExibeMensagemAction(object sender, ExibeMensagemEventArgs e)
        {
            Console.Out.WriteLine(e.Mensagem);
        }

        private void AguardandoResposta(object sender, AguardaRespostaEventArgs e)
        {
            Console.Out.WriteLine(e.ArquivoSts);
            Console.Out.WriteLine($"Segundos: {e.Segundos}");
        }

        private void Adm_OnClick(object sender, RoutedEventArgs e)
        {
            var acTefDial = FabricaOperadora();

            var respostaAdm = acTefDial.Adm();
        }

        private void ImprimirViaAction(object sender, IImprimeViaEventArgs e)
        {
            if (e.IsTemViaUnica)
            {
                Console.Out.WriteLine(string.Empty);
                Console.Out.WriteLine(string.Empty);
                Console.Out.WriteLine("Via Única");
                foreach (var imagemComprovante in e.ViaUnica)
                {
                    Console.Out.WriteLine(imagemComprovante);
                }
            }

            if (e.IsTemViaCliente)
            {
                Console.Out.WriteLine(string.Empty);
                Console.Out.WriteLine(string.Empty);
                Console.Out.WriteLine("Via Cliente");
                foreach (var imagemComprovante in e.ViaCliente)
                {
                    Console.Out.WriteLine(imagemComprovante);
                }
            }

            if (e.IsTemViaEstabelecimento)
            {
                Console.Out.WriteLine(string.Empty);
                Console.Out.WriteLine(string.Empty);
                Console.Out.WriteLine("Via Estabelecimento");
                foreach (var imagemComprovante in e.ViaEstabelecimento)
                {
                    Console.Out.WriteLine(imagemComprovante);
                }
            }


            if (e.IsTemViaReduzida)
            {
                Console.Out.WriteLine(string.Empty);
                Console.Out.WriteLine(string.Empty);
                Console.Out.WriteLine("Via Reduzida");
                foreach (var imagemComprovante in e.ViaReduzida)
                {
                    Console.Out.WriteLine(imagemComprovante);
                }
            }


            if (e.IsTemVia1)
            {
                Console.Out.WriteLine(string.Empty);
                Console.Out.WriteLine(string.Empty);
                Console.Out.WriteLine(string.Empty);
                Console.Out.WriteLine(string.Empty);
                Console.Out.WriteLine("Via Via1");
                foreach (var imagemComprovante in e.Via1)
                {
                    Console.Out.WriteLine(imagemComprovante);
                }
            }

            if (e.IsTemVia2)
            {
                Console.Out.WriteLine(string.Empty);
                Console.Out.WriteLine(string.Empty);
                Console.Out.WriteLine("Via Via2");
                foreach (var imagemComprovante in e.Via2)
                {
                    Console.Out.WriteLine(imagemComprovante);
                }
            }
        }

        private void Cnc_OnClick(object sender, RoutedEventArgs e)
        {
            var acTefDial = FabricaOperadora();

            var respostaAdm = acTefDial.Cnc("REDECARD", "17230215595", new DateTime(2018, 12, 04, 17, 23, 02), 50);
        }

        private void Crt_OnClick(object sender, RoutedEventArgs e)
        {
            var acTefDial = FabricaOperadora();

            var respostaAdm = acTefDial.Crt(10m, "98393");
        }

        private void AutorizaDfe(object sender, AutorizaDfeEventArgs e)
        {
            e.AutorizadoDfe();
        }
    }
}
