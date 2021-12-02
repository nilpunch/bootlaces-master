using System.Collections.Generic;
using UnityEngine;

namespace BootlacesMaster
{
    public abstract class Lace : MonoBehaviour, ILace
    {
        public abstract IEnumerable<Vector3> Points { get; }
    }
}