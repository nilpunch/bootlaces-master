using System.Collections.Generic;
using UnityEngine;

namespace BootlacesMaster
{
    public interface ILace
    {
        IEnumerable<Vector3> Points { get; }
    }
}