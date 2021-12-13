using System;
using System.Collections.Generic;
using System.Linq;
using Obi;
using UnityEngine;

namespace BootlacesMaster
{
    public class RopeLace : Lace
    {
        [SerializeField] private LaceColor _laceColor = null;
        [SerializeField] private MeshRenderer _meshRenderer = null;
        [SerializeField] private ObiRope _obiRope = null;
        [SerializeField] private LaceHandle _firstHandle = null;
        [SerializeField] private LaceHandle _secondHandle = null;

        public override IEnumerable<Vector3> Points => GetParticlePositions();
        
        public override Color Color => _laceColor.Color;
        
        public override int FirstHole => _firstHandle.AttachedHoleIndex;
        
        public override int SecondHole => _secondHandle.AttachedHoleIndex;

        private void Awake()
        {
            _meshRenderer.material.color = Color;
        }

        private IEnumerable<Vector3> GetParticlePositions()
        {
            foreach (var element in _obiRope.elements)
                yield return _obiRope.GetParticlePosition(element.particle1);

            yield return _obiRope.GetParticlePosition(_obiRope.elements[_obiRope.elements.Count - 1].particle2);
        }
    }
}