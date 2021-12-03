using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BootlacesMaster
{
    public class PointToPointLace : Lace
    {
        [SerializeField] private LaceHandle _start = null;
        [SerializeField] private LaceHandle _end = null;

        public override IEnumerable<Vector3> Points => _start.Position.Yield().Concat(_end.Position.Yield());
        public override Color Color => Color.black;
    }
}