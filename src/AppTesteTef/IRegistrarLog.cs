using System;

namespace AppTesteTef
{
    public interface IRegistrarLog
    {
        string Path { get; set; }

        void Registrar(string evento);

        void RegistrarException(Exception ex);
    }
}