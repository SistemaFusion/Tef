using System.Drawing.Printing;

namespace Tef.Infra
{
    public class ImprimirViaTef
    {
        private readonly string[] _imagem;
        private readonly string _nomeImpressora;
        private readonly string _fontePadrao;
        private int _tamanhoTotal;
        private readonly int _tamanhoImagem;
        private int _posicaoImagem;

        public ImprimirViaTef(string[] imagem, string nomeImpressora, string fontePadrao = "Open Sans Condensed")
        {
            _imagem = imagem;
            _nomeImpressora = nomeImpressora;
            _fontePadrao = fontePadrao;
            _tamanhoImagem = _imagem.Length;
            _posicaoImagem = 0;
        }

        public void Imprimir()
        {
            var p = new PrintDocument();

            if (!string.IsNullOrEmpty(_nomeImpressora))
            {
                p.PrinterSettings.PrinterName = _nomeImpressora;
            }

            p.PrintPage += delegate (object sender1, PrintPageEventArgs e1)
            {
                var tamanhoDeUmaPagina = e1.PageSettings.PrintableArea.Height;

                var graphics = e1.Graphics;
                _tamanhoTotal = 3;

                AdicionarTexto.FontPadrao = new System.Drawing.FontFamily(_fontePadrao);

                var tamanhoPaginaAtual = 3;

                for (; _posicaoImagem < _tamanhoImagem; _posicaoImagem++)
                {
                    var texto = new AdicionarTexto(graphics, _imagem[_posicaoImagem], 9);

                    texto.Desenhar(3, _tamanhoTotal);

                    _tamanhoTotal += texto.Medida.Altura;
                    tamanhoPaginaAtual += 22;

                    e1.HasMorePages = false;

                    if (tamanhoPaginaAtual > tamanhoDeUmaPagina)
                    {
                        e1.HasMorePages = true;
                        return;
                    }
                }
            };

            p.Print();
        }
    }
}