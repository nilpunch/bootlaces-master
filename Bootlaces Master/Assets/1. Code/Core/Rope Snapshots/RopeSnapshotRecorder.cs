using System;
using System.Collections.Generic;
using System.Linq;
using Obi;
using UnityEngine;

namespace BootlacesMaster
{
    public class RopeSnapshotPlayer : MonoBehaviour
    {
        [SerializeField] private ObiRope _actor = null;
        [SerializeField] private RopeLace _ropeLace = null;
        [SerializeField] private RopeTensionLimiter _tensionLimiter = null;

        private List<float> _lengthChanges;

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
            
            foreach (var solverIndex in _actor.solverIndices)
            {
                var position = _actor.solver.positions[solverIndex];
                var velocity =  _actor.solver.velocities[solverIndex];
                
                ropeSnapshot.ParticleVelocities.Add(velocity);
                ropeSnapshot.ParticlePositions.Add(position);
            }
            
            return ropeSnapshot;
        }

        private void OnCursorLengthChanged(float lengthChange)
        {
            _lengthChanges.Add(lengthChange);
        }
    }
    
    public class RopeSnapshotRecorder : MonoBehaviour
    {
        [SerializeField] private ObiRope _actor = null;
        [SerializeField] private RopeLace _ropeLace = null;
        [SerializeField] private RopeTensionLimiter _tensionLimiter = null;

        private List<float> _lengthChanges;

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
            
            foreach (var solverIndex in _actor.solverIndices)
            {
                var position = _actor.solver.positions[solverIndex];
                var velocity =  _actor.solver.velocities[solverIndex];
                
                ropeSnapshot.ParticleVelocities.Add(velocity);
                ropeSnapshot.ParticlePositions.Add(position);
            }
            
            return ropeSnapshot;
        }

        private void OnCursorLengthChanged(float lengthChange)
        {
            _lengthChanges.Add(lengthChange);
        }
    }
}