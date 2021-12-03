using System;
using System.Collections.Generic;
using System.Linq;
using Obi;
using UnityEngine;

namespace BootlacesMaster
{
    public class RopeLace : Lace
    {
        [SerializeField] private ObiRope _obiRope = null;
        [SerializeField] private LaceHandle _start = null;
        [SerializeField] private LaceHandle _end = null;

        private List<Vector3> _positionsCache = new List<Vector3>();
        
        public override IEnumerable<Vector3> Points => GetParticlePositions();

        private IEnumerable<Vector3> GetParticlePositions()
        {
            foreach (var element in _obiRope.elements)
                yield return _obiRope.GetParticlePosition(element.particle1);

            yield return _obiRope.GetParticlePosition(_obiRope.elements[_obiRope.elements.Count - 1].particle2);
        }
    }
}