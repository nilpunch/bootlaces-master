using System;
using System.Collections.Generic;
using System.Linq;
using Obi;
using UnityEngine;

namespace BootlacesMaster
{
    public class RopeSnapshotRecorder : MonoBehaviour
    {
        [SerializeField] private ObiRope _actor = null;
        [SerializeField] private RopeLace _ropeLace = null;
        [SerializeField] private RopeTensionLimiter _tensionLimiter = null;

        private List<float> _lengthChanges;
        private float _lastChangeSing = 1f;
        
        private void Awake()
        {
            _lengthChanges = new List<float>();
        }

        private void OnEnable()
        {
            _tensionLimiter.CursorLengthChanged += OnCursorLengthChanged;
        }
        
        private void OnDisable()
        {
            _tensionLimiter.CursorLengthChanged -= OnCursorLengthChanged;
        }

        public RopeSnapshot MakeSnapshot()
        {
            RopeSnapshot ropeSnapshot = new RopeSnapshot();
            ropeSnapshot.Color = _ropeLace.Color;
            ropeSnapshot.FirstHole = _ropeLace.FirstHole;
            ropeSnapshot.SecondHole = _ropeLace.SecondHole;
            ropeSnapshot.LengthChanges = _lengthChanges.ToList();
            ropeSnapshot.ParticleVelocities = new List<Vector3>();
            ropeSnapshot.ParticlePositions = new List<Vector3>();
            
            foreach (var index in Enumerable.Range(0, _actor.activeParticleCount))
            {
                var solverIndex = _actor.solverIndices[index];
                
                var position = _actor.solver.positions[solverIndex];
                var velocity =  _actor.solver.velocities[solverIndex];
                
                ropeSnapshot.ParticleVelocities.Add(velocity);
                ropeSnapshot.ParticlePositions.Add(position);
            }
            
            return ropeSnapshot;
        }

        private void OnCursorLengthChanged(float lengthChange)
        {
            float lastValueChange = _lengthChanges.LastOrDefault();

            float delta = lengthChange - lastValueChange;

            if (Mathf.Sign(delta) == _lastChangeSing && _lengthChanges.Count != 0)
                _lengthChanges[_lengthChanges.Count - 1] = lengthChange;
            else
                _lengthChanges.Add(lengthChange);

            _lastChangeSing = Mathf.Sign(delta);
        }
    }
}