using System;
using Obi;
using UnityEngine;

namespace BootlacesMaster
{
    public class RopeTensionLimiter : MonoBehaviour
    {
        [SerializeField] private ObiRopeCursor _obiRopeCursor = null;
        [SerializeField] private ObiRope _obiRope = null;
        [SerializeField] private float _tensionLongingChange = 400f;
        [SerializeField] private float _tensionShorteningChange = 600f;
        [SerializeField] private float _tensionLongingTreshold = 0.03f;
        [SerializeField] private float _tensionRestingTreshold = 0.005f;
        [SerializeField] private float _minDelta = 0.05f;
        
        private float _startLenght;

        private float _lastLength;

        public event Action<float> CursorLengthChanged;

        private void Awake()
        {
            _startLenght = _obiRope.restLength;
            _lastLength = _startLenght;
        }

        private void Update()
        {
            float lengthFromDistance = _lastLength;// Mathf.Max(Vector3.Distance(_start.Position, _end.Position), _startLenght);

            float delta = lengthFromDistance - _lastLength;
            
            float tension = _obiRope.CalculateLength() / _obiRope.restLength - 1f;

            if (tension > _tensionLongingTreshold)
            {
                float tensionDelta = (tension - _tensionLongingTreshold) * _tensionLongingChange * Time.deltaTime;
                delta += tensionDelta;
            } 
            else if (tension < _tensionRestingTreshold)
            {
                float tensionDelta = (tension - _tensionRestingTreshold) * _tensionShorteningChange * Time.deltaTime;
                delta += tensionDelta;
            }
            
            if (Math.Abs(delta) >= _minDelta)
            {
                _lastLength = Mathf.Max(_lastLength + delta, _startLenght);
                _obiRopeCursor.ChangeLength(_lastLength);
                CursorLengthChanged?.Invoke(_lastLength);
            }
        }
    }
}