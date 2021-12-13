using System;
using System.Linq;
using UnityEngine;

namespace BootlacesMaster
{
    public class Grabber : MonoBehaviour
    {
        [SerializeField] private CrossPlatformPointerInput _input = null;
        [SerializeField] private Camera _camera = null;
        [SerializeField] private LayerMask _inputLayer = new LayerMask();
        [SerializeField] private float _grabRange = 3f;
        
        private LaceHandle _grabbedHandle = null;
        private Hole _lastDetachedHole = null;
        private Hole[] _holes;

        private void Awake()
        {
            _holes = FindObjectsOfType<Hole>();
        }

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
            if (CalculateWorldPosition(_input.Position, out var worldPosition) == false)
                return;

            Hole nearbyHole = _holes.Where(hole => hole.HasHandle)
                .OrderBy(hole => Vector3.Distance(hole.Position, worldPosition))
                .FirstOrDefault();

            if (nearbyHole != null && Vector3.Distance(nearbyHole.Position, worldPosition) < _grabRange)
            {
                _lastDetachedHole = nearbyHole;
                _grabbedHandle = nearbyHole.Detach();
                _grabbedHandle.MoveTo(worldPosition);
            }
        }

        private void OnMoved()
        {
            if (_grabbedHandle == null)
                return;

            if (CalculateWorldPosition(_input.Position, out var worldPosition))
                _grabbedHandle.MoveTo(worldPosition);
        }

        private void OnReleased()
        {
            if (_grabbedHandle == null)
                return;

            LaceHandle handle = _grabbedHandle;
            _grabbedHandle = null;

            if (CalculateWorldPosition(_input.Position, out var worldPosition) == false)
            {
                _lastDetachedHole.Attach(handle);
                return;
            }

            Hole nearbyHole = _holes.Where(hole => hole.HasHandle == false)
                .OrderBy(hole => Vector3.Distance(hole.Position, worldPosition))
                .FirstOrDefault();

            if (nearbyHole != null)
            {
                nearbyHole.Attach(handle);
                return;
            }

            _lastDetachedHole.Attach(handle);
        }

        private bool CalculateWorldPosition(Vector2 screenPosition, out Vector3 worldPosition)
        {
            Ray ray = _camera.ScreenPointToRay(screenPosition);

            if (Physics.Raycast(ray, out var hit, float.MaxValue, _inputLayer))
            {
                worldPosition = hit.point;
                return true;
            }
            
            worldPosition = Vector3.zero;
            return false;
        }
    }
}