using System;
using UnityEngine;

namespace BootlacesMaster
{
    public class HandleGrabber : MonoBehaviour
    {
        [SerializeField] private InputRouter _input = null;
        [SerializeField] private Camera _camera = null;
        [SerializeField] private float _dumping = 0.1f;
        [SerializeField] private Vector3 _offset = Vector3.up;

        private LaceHandle _grabbedHandle = null;

        public event Action LaceMoved;

        private void OnEnable()
        {
            _input.Pressed += OnPressed;
            _input.Moved += OnMoved;
            _input.Released += OnReleased;
        }
        
        private void OnDisable()
        {
            _input.Pressed -= OnPressed;
            _input.Moved -= OnMoved;
            _input.Released -= OnReleased;
        }

        private void OnPressed()
        {
            Ray ray = _camera.ScreenPointToRay(_input.Position);

            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.TryGetComponent<LaceHandle>(out var laceHandle))
                {
                    _grabbedHandle = laceHandle;
                    _grabbedHandle.Grab();
                    _grabbedHandle.MoveTo(CalculateWorldPosition(_input.Position));
                    LaceMoved?.Invoke();
                }
            }
        }

        private void OnMoved()
        {
            if (_grabbedHandle == null)
            {
                return;
            }

            _grabbedHandle.MoveTo(CalculateWorldPosition(_input.Position));
            LaceMoved?.Invoke();
        }

        private void OnReleased()
        {
            if (_grabbedHandle == null)
            {
                return;
            }

            _grabbedHandle.MoveTo(CalculateWorldPosition(_input.Position));
            _grabbedHandle.UnGrab();
            LaceMoved?.Invoke();

            _grabbedHandle = null;
        }

        private Vector3 CalculateWorldPosition(Vector2 screenPosition)
        {
            Ray ray = _camera.ScreenPointToRay(screenPosition);

            if (new Plane(Vector3.up, Vector3.zero).Raycast(ray, out var distance))
            {
                return ray.GetPoint(distance);
            }
            else
            {
                return Vector3.zero;
            }
        }
    }
}