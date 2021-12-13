using System;
using System.Collections.Generic;
using UnityEngine;

namespace BootlacesMaster
{
    [Serializable]
    public struct PlacementCondition : IEquatable<PlacementCondition>
    {
        public Color Color;
        public int FirstHole;
        public int SecondHole;

        public bool Equals(PlacementCondition other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;

            return Color.Equals(other.Color) 
                   && (FirstHole == other.FirstHole && SecondHole == other.SecondHole 
                       || FirstHole == other.SecondHole && SecondHole == other.FirstHole);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((PlacementCondition)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Color.GetHashCode());
                hashCode = (hashCode * 397) ^ FirstHole;
                hashCode = (hashCode * 397) ^ SecondHole;
                return hashCode;
            }
        }
    }
}