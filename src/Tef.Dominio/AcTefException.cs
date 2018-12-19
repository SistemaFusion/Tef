using System;

namespace Tef.Dominio
{
    public class AcTefException : Exception
    {
        public AcTefException(string mensagem) : base(mensagem)
        {
            
        }


        public static void Quando(bool naoValido, string mensagem)
        {
            if (naoValido) throw new AcTefException(mensagem);
        }

    }
}