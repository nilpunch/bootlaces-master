using System;
using UnityEngine;

namespace BootlacesMaster
{
    public class Hole : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer = null;
        
        private LaceHandle _attachedHandle;
        
        public bool HasHandle => _attachedHandle != null;

        public Vector3 Position => transform.position;

        private void Awake()
        {
            _meshRenderer.material.color = Color.green - Color.black * _meshRenderer.material.color.a;
        }

        public void Attach(LaceHandle laceHandle)
        {
            if (HasHandle)
                throw new InvalidOperationException("Hole cant attach handle while been used by other handle.");

            if (laceHandle.Attached)
                throw new InvalidOperationException("Hole cant attach already grabbed handle.");

            _attachedHandle = laceHandle;
            laceHandle.Attach();
            laceHandle.MoveTo(Position);
            
            _meshRenderer.material.color = Color.red - Color.black * _meshRenderer.material.color.a;
        }

        public LaceHandle Detach()
        {
            if (HasHandle == false)
                throw new InvalidOperationException("No handles attached to this hole.");

            LaceHandle handle = _attachedHandle;
            
            _attachedHandle.Detach();
            _attachedHandle = null;

            _meshRenderer.material.color = Color.green - Color.black * _meshRenderer.material.color.a;

            return handle;
        }
    }
}