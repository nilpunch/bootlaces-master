using System.Collections.Generic;
using UnityEngine;

namespace BootlacesMaster
{
    public abstract class Lace : MonoBehaviour, IColorizedLace
    {
        public abstract IEnumerable<Vector3> Points { get; }
        
        public abstract Color Color { get; }
    }
}