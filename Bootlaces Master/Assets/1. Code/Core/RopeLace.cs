using System;
using System.Collections.Generic;
using System.Linq;
using Obi;
using UnityEngine;

namespace BootlacesMaster
{
    public class RopeLace : Lace
    {
        [SerializeField] private ObiRopeCursor _obiRopeCursor = null;
        [SerializeField] private ObiRope _obiRope = null;
        [SerializeField] private LaceHandle _start = null;
        [SerializeField] private LaceHandle _end = null;
        
        [Header("Dynamic Length Change Settings")]
        [SerializeField] private float _tensionLongingChange = 5f;
        [SerializeField] private float _tensionShorteningChange = 5f;
        [SerializeField] private float _tensionLongingTreshold = 0.07f;
        [SerializeField] private float _tensionRestingTreshold = 0.03f;
        [SerializeField] private bool _debugTension = false;

        private float _startLenght;

        private float _lastLength;

        private float _tensionLenght;
        
        public override IEnumerable<Vector3> Points => _start.Position.Yield().Concat(_end.Position.Yield());

        private void Awake()
        {
            _startLenght = Vector3.Distance(_start.Position, _end.Position);
            _lastLength = _startLenght;
        }

        private void FixedUpdate()
        {
            float lengthFromDistance = _lastLength;// Mathf.Max(Vector3.Distance(_start.Position, _end.Position), _startLenght);

            float delta = lengthFromDistance - _lastLength;
            
            float tension = _obiRope.CalculateLength() / _obiRope.restLength - 1f;

            if (_debugTension)
                Debug.Log(tension);
            
            if (tension > _tensionLongingTreshold)
            {
                float tensionDelta = (tension - _tensionLongingTreshold) * _tensionLongingChange * Time.deltaTime;
                delta += tensionDelta;
                _tensionLenght += tensionDelta;
            } 
            else if (tension < _tensionRestingTreshold)
            {
                float tensionDelta = (tension - _tensionRestingTreshold) * _tensionShorteningChange * Time.deltaTime;
                delta += tensionDelta;
                _tensionLenght += tensionDelta;
            }
            
            if (Math.Abs(delta) >= 0.001f)
            {
                _lastLength = Mathf.Max(_lastLength + delta, _startLenght);
                _obiRopeCursor.ChangeLength(_lastLength);
            }
        }
    }
}