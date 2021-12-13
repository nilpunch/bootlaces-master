using System;
using UnityEngine;

namespace BootlacesMaster
{
    public class Hole : MonoBehaviour
    {
        [SerializeField] private int _index = 1;
        
        private LaceHandle _attachedHandle;
        
        public bool HasHandle => _attachedHandle != null;

        public Vector3 Position => transform.position;

        public int Index => _index;

        public void InitialAttach(LaceHandle laceHandle)
        {
            if (HasHandle)
                throw new InvalidOperationException("Hole cant attach handle while been used by other handle.");

            if (laceHandle.Attached)
                throw new InvalidOperationException("Hole cant attach already grabbed handle.");

            _attachedHandle = laceHandle;
            laceHandle.AttachNoAnimation(this);
        }
        
        public void Attach(LaceHandle laceHandle)
        {
            if (HasHandle)
                throw new InvalidOperationException("Hole cant attach handle while been used by other handle.");

            if (laceHandle.Attached)
                throw new InvalidOperationException("Hole cant attach already grabbed handle.");

            _attachedHandle = laceHandle;
            laceHandle.Attach(this);
        }

        public LaceHandle Detach()
        {
            if (HasHandle == false)
                throw new InvalidOperationException("No handles attached to this hole.");

            LaceHandle handle = _attachedHandle;
            
            _attachedHandle.Detach();
            _attachedHandle = null;

            return handle;
        }
    }
}