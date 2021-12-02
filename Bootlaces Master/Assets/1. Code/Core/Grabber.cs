using System;
using UnityEngine;

namespace BootlacesMaster
{
    public class Grabber : MonoBehaviour
    {
        [SerializeField] private InputRouter _input = null;
        [SerializeField] private Camera _camera = null;
        [SerializeField] private LayerMask _holesLayer = new LayerMask();

        private LaceHandle _grabbedHandle = null;
        private Hole _lastDetachedHole = null;

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

            if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, _holesLayer))
            {
                if (hitInfo.collider.TryGetComponent<Hole>(out var hole) && hole.HasHandle)
                {
                    _lastDetachedHole = hole;
                    _grabbedHandle = hole.Detach();
                    _grabbedHandle.MoveTo(CalculateWorldPosition(_input.Position));
                }
            }
        }

        private void OnMoved()
        {
            if (_grabbedHandle == null)
                return;

            _grabbedHandle.MoveTo(CalculateWorldPosition(_input.Position));
        }

        private void OnReleased()
        {
            if (_grabbedHandle == null)
                return;

            LaceHandle handle = _grabbedHandle;
            _grabbedHandle = null;
            
            Ray ray = _camera.ScreenPointToRay(_input.Position);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, _holesLayer))
            {
                if (hitInfo.collider.TryGetComponent<Hole>(out var hole) && hole.HasHandle == false)
                {
                    hole.Attach(handle);
                    return;
                }
            }
            
            _lastDetachedHole.Attach(handle);
        }

        private Vector3 CalculateWorldPosition(Vector2 screenPosition)
        {
            Ray ray = _camera.ScreenPointToRay(screenPosition);

            if (new Plane(Vector3.up, Vector3.zero).Raycast(ray, out var distance))
                return ray.GetPoint(distance);

            return Vector3.zero;
        }
    }
}