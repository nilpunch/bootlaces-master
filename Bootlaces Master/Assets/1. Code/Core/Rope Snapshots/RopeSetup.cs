using System;
using System.Collections;
using Obi;
using UnityEngine;

namespace BootlacesMaster
{
    public class RopeSetup : MonoBehaviour
    {
        [SerializeField] private ObiRope _actor = null;
        [SerializeField] private ObiRopeCursor _cursor = null;
        [SerializeField] private RopeLace _ropeLace = null;
        [SerializeField] private RopeTensionLimiter _tensionLimiter = null;
        private RopeSnapshot _ropeSnapshot;
        private Holes _holes;

        private void OnDestroy()
        {
            _cursor.OnRopeUpdated -= OnRopeUpdated;
        }

        public void Init(ObiSolver solver, Holes holes, RopeSnapshot ropeSnapshot)
        {
            _holes = holes;
            _ropeSnapshot = ropeSnapshot;
            
            _cursor.OnRopeUpdated += OnRopeUpdated;
            
            transform.SetParent(solver.transform, true);
        }

        private void OnRopeUpdated()
        {
            if (_ropeSnapshot == null)
            {
                _tensionLimiter.Enable();
                return;
            }
            
            _cursor.OnRopeUpdated -= OnRopeUpdated;
            
            StartCoroutine(InittingRope());
        }

        private IEnumerator InittingRope()
        {
            foreach (var lengthChange in _ropeSnapshot.LengthChanges)
            {
                _cursor.ChangeLength(lengthChange);
            }
            
            _ropeLace.Init(_ropeSnapshot.Color, 
                _holes.GetHole(_ropeSnapshot.FirstHole),
                _holes.GetHole(_ropeSnapshot.SecondHole));

            for (int i = 0; i < _ropeSnapshot.ParticlePositions.Count; ++i)
            {
                int solverIndex = _actor.solverIndices[i];
                _actor.solver.positions[solverIndex] = _ropeSnapshot.ParticlePositions[i];
                _actor.solver.velocities[solverIndex] = Vector3.zero;  //_ropeSnapshot.ParticleVelocities[i];
            }

            for (int i = 0; i < 5; ++i)
                yield return null;

            _tensionLimiter.Enable();
        }
    }
}