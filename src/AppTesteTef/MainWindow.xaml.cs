using System;
using System.Windows;
using Microsoft.Win32;
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
            _operadora = Operadora.PayGo;
        }

        private void Atv_OnClick(object sender, RoutedEventArgs e)
        {
            var acTefDial = FabricaOperadora();
            var respostaAtv = acTefDial.Atv();
        }

        private void Adm_OnClick(object sender, RoutedEventArgs e)
        {
            var acTefDial = FabricaOperadora();

            var respostaAdm = acTefDial.Adm();
        }

        private void Cnc_OnClick(object sender, RoutedEventArgs e)
        {
            var acTefDial = FabricaOperadora();

            var respostaAdm = acTefDial.Cnc("REDECARD", "17230215595", new DateTime(2018, 12, 04, 17, 23, 02), 50);
        }

        private void Crt_OnClick(object sender, RoutedEventArgs e)
        {
            var acTefDial = FabricaOperadora();

            var respostaAdm = acTefDial.Crt(10.00m, "98393");
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
                true, true, true, true
            );

            var iTef = Tef.Dominio.FabricaOperadora.RetornaOperadora(_operadora, requisicao, configuracao);

            iTef.Inicializa();

            return iTef;
        }

        private void AguardandoResposta(object sender, AguardaRespostaEventArgs e)
        {
            Console.Out.WriteLine(e.ArquivoSts);
            Console.Out.WriteLine($"Segundos: {e.Segundos}");
        }

        private void ExibeMensagemAction(object sender, ExibeMensagemEventArgs e)
        {
            Console.Out.WriteLine(e.Mensagem);
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


                new ImprimirViaTef(e.ViaUnica, NomeImpressora.Text).Imprimir();
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

                new ImprimirViaTef(e.ViaCliente, NomeImpressora.Text).Imprimir();
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

                new ImprimirViaTef(e.ViaEstabelecimento, NomeImpressora.Text).Imprimir();
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

                new ImprimirViaTef(e.ViaReduzida, NomeImpressora.Text).Imprimir();
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

                new ImprimirViaTef(e.Via1, NomeImpressora.Text).Imprimir();
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

                new ImprimirViaTef(e.Via2, NomeImpressora.Text).Imprimir();
            }
        }

        private void AutorizaDfe(object sender, AutorizaDfeEventArgs e)
        {
            e.AutorizadoDfe();
        }

        private void ImprimirArquivoDeRequisicaoTefManualmente_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var arquivoTef = string.Empty;

                var janelaArquivo = new OpenFileDialog
                {
                    Filter = "Arquivo tef(*.tef)|*.tef"
                };

                if (janelaArquivo.ShowDialog() == true)
                {
                    arquivoTef = janelaArquivo.FileName;
                }

                if (janelaArquivo.FileName.IsNullOrEmpty())
                {
                    MessageBox.Show("Selecione um arquivo tef", "Selecione um arquivo tef", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }


                var teflinha = TefLinhaLista.LoadArquivo(arquivoTef);

                if (_operadora == Operadora.TefExpress)
                {
                    var imprimir = new ImprimeViaEventArgs(teflinha);

                    ImprimirViaAction(null, imprimir);
                }

                if (_operadora != Operadora.TefExpress)
                {
                    var imprimir = new ImprimeViaEventArgs(teflinha);

                    ImprimirViaAction(null, imprimir);
                }
            }
            catch (Exception exception)
            {
                RegistarLog.Istancia.RegistrarException(exception);
                MessageBox.Show(exception?.Message, "EXCEPTION", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
