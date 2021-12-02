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
        
        public override IEnumerable<Vector3> Points => GetParticlePositions();

        private IEnumerable<Vector3> GetParticlePositions()
        {
            List<Vector3> positions = new List<Vector3>();
            
            for (int i = 0; i < _obiRope.elements.Count; ++i)
            {
                var element = _obiRope.elements[i];

                positions.Add(_obiRope.GetParticlePosition(element.particle1));
            }
            
            positions.Add(_obiRope.GetParticlePosition(_obiRope.elements[_obiRope.elements.Count - 1].particle2));

            return positions;
        }
    }
}