using System;
using UnityEngine;

namespace BootlacesMaster
{
    [Serializable]
    public struct EntanglementCondition : IEquatable<EntanglementCondition>
    {
        public Color First;
        public Entanglement Entanglement;
        public Color Second;
        public int Times;

        public bool IsPremutationOf(EntanglementCondition other)
        {
            return Entanglement == other.Entanglement && First.Equals(other.First) && Second.Equals(other.Second)
                   || Entanglement != other.Entanglement && Second.Equals(other.First) && First.Equals(other.Second);
        }
        
        public bool Equals(EntanglementCondition other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            
            return IsPremutationOf(other)
                   && Times == other.Times;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((EntanglementCondition)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int)Entanglement;
                hashCode = (hashCode * 397) ^ (First + Second).GetHashCode();
                hashCode = (hashCode * 397) ^ Times;
                return hashCode;
            }
        }
    }
}