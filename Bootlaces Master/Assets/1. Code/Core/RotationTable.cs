using System;
using UnityEngine;

namespace BootlacesMaster
{
    public class RotationTable : PointerInputHandler
    {
        [SerializeField] private Transform _rotationPivot = null;
        [SerializeField] private Vector3 _rotationAxis = Vector3.up;
        [SerializeField] private float _sensitivity = 1f;
        [SerializeField] private float _deceleration = 1f;
        [SerializeField] private float _pressedDeceleration = 1f;
        [SerializeField] private float _maxSpeed = 1000f;

        private bool _pressed;
        private Vector2 _lastPosition;
        private Vector2 _speed;

        private void Update()
        {
            float rotation = _speed.x * Time.deltaTime;
            
            if (_pressed)
            {
                _speed -= _speed * (_pressedDeceleration * Time.deltaTime);
            }
            else
            {
                _speed -= _speed * (_deceleration * Time.deltaTime);
            }

            _rotationPivot.Rotate(_rotationAxis, rotation);
        }

        public override bool OnPressed(Vector2 screenPosition)
        {
            _pressed = true;
            _lastPosition = ScreenToNormalized(screenPosition);
            return true;
        }

        public override void OnMoved(Vector2 screenPosition)
        {
            if (_pressed == false)
                return;
            
            Vector2 position = ScreenToNormalized(screenPosition);
            Vector2 delta = position - _lastPosition;
            _lastPosition = position;

            _speed += delta / Time.deltaTime * _sensitivity;
            _speed.x = Mathf.Clamp(_speed.x, -_maxSpeed, _maxSpeed);
            _speed.y = Mathf.Clamp(_speed.y, -_maxSpeed, _maxSpeed);
        }

        public override void OnReleased(Vector2 screenPosition)
        {
            _pressed = false;
        }
        
        private Vector2 ScreenToNormalized(Vector2 point)
        {
            Vector2 viewportPoint = new Vector2(point.x / Screen.width, point.y / Screen.height);

            viewportPoint.x *= (float)Screen.width / Screen.height;
        
            return viewportPoint;
        }
    }
}