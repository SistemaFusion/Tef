using System;

namespace Tef.Dominio
{
    public class AguardaRespostaEventArgs : EventArgs
    {
        private bool _interromper;
        public string ArquivoSts { get; }
        public int Segundos { get; }

        public AguardaRespostaEventArgs(string arquivoSts, DateTime tempoFimDeEspera)
        {
            ArquivoSts = arquivoSts;
            Segundos = DateTime.Now.Subtract(tempoFimDeEspera).Seconds;
        }

        public void InterromperProcesso()
        {
            _interromper = true;
        }

        public bool Interromper => _interromper;
    }
}