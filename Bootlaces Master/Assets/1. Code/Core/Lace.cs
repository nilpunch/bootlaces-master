using System.Collections.Generic;
using UnityEngine;

namespace BootlacesMaster
{
    public abstract class Lace : MonoBehaviour, IConditionLace
    {
        public abstract IEnumerable<Vector3> Points { get; }
        
        public abstract Color Color { get; }
        
        public abstract int FirstHole { get; }
        
        public abstract int SecondHole { get; }
    }
}