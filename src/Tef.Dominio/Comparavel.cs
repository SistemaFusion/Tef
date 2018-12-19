using System;

namespace Tef.Dominio
{
    public abstract class Comparavel : ICloneable
    {
        private string _hash = Md5Helper.ComputaUnique();
        protected abstract int ReferenciaUnica { get; }

        private bool Equals(Comparavel other)
        {
            return GetHashCode() == other.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Comparavel)obj);
        }

        public override int GetHashCode()
        {
            return ReferenciaUnica != 0 ? ReferenciaUnica : _hash.GetHashCode();
        }

        public object Clone()
        {
            var clone = (Comparavel)MemberwiseClone();
            clone._hash = _hash;

            return clone;
        }

        public static bool operator ==(Comparavel left, Comparavel right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Comparavel left, Comparavel right)
        {
            return !Equals(left, right);
        }
    }
}