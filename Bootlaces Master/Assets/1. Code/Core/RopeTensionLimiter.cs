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
        [SerializeField] private float _maxDelta = 1f;
        
        private float _startLenght;

        private float _lastLength;

        public event Action<float> CursorLengthChanged;

        private void Awake()
        {
            _startLenght = _obiRope.restLength;
            _lastLength = _startLenght;
        }

        public void Enable()
        {
            enabled = true;
            _lastLength = _obiRope.restLength;
        }
        
        public void Disable()
        {
            enabled = false;
        }

        private void Update()
        {
            float delta = 0f;
            
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
                _lastLength = Mathf.Max(_lastLength + Mathf.Clamp(delta, -_maxDelta, _maxDelta), _startLenght);
                _obiRopeCursor.ChangeLength(_lastLength);
                CursorLengthChanged?.Invoke(_lastLength);
            }
        }
    }
}