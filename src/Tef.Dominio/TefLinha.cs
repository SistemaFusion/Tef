using Tef.Dominio.Conversores;

namespace Tef.Dominio
{
    public class TefLinha : Entidade
    {
        public string Chave { get; }
        public string Valor { get; }
        public int Identificacao { get; }
        public int Posicao { get; }

        public TefLinha(string chave, string valor)
        {
            Chave = chave;
            Valor = valor;

            Identificacao = int.Parse(Chave.Split('-')[0]);
            Posicao = int.Parse(Chave.Split('-')[1]);
        }

        public TefLinha(string chave, int valor) : this(chave, valor.ToString()) { }

        public TefLinha(string chave, decimal valor) : this(chave, ConverterRealEmCentavos.ConverterEmCentavos(valor)) { }

        public TefLinha(string linha)
        {
            var linhas = linha.Split(new[] { '=' }, 2);

            Chave = linhas[0].Trim();
            Valor = linhas[1].Trim();

            Identificacao = int.Parse(linhas[0].Split('-')[0]);
            Posicao = int.Parse(linhas[0].Split('-')[1]);
        }

        public override string ToString()
        {
            return $"{Chave} = {Valor}";
        }

        protected override int ReferenciaUnica => Identificacao;
    }
}