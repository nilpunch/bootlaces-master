using System;
using UnityEngine;

namespace BootlacesMaster
{
    public class Hole : MonoBehaviour
    {
        [SerializeField] private Transform _position = null;
        [SerializeField] private int _index = 1;
        [SerializeField] private bool _initiallyLocked = false;
        
        private LaceHandle _attachedHandle;
        private bool _manuallyLocked;

        public event Action Locked;
        public event Action Unlocked;
        
        public bool IsLocked => _attachedHandle != null || _manuallyLocked;
        
        public bool HasManualLock => _manuallyLocked;

        public Vector3 Position => _position.position;

        public int Index => _index;

        private void Start()
        {
            if (_initiallyLocked)
                Lock();
        }

        public void InitialAttach(LaceHandle laceHandle)
        {
            if (IsLocked)
                throw new InvalidOperationException("Hole cant attach handle while been used by other handle.");

            if (laceHandle.Attached)
                throw new InvalidOperationException("Hole cant attach already grabbed handle.");

            _attachedHandle = laceHandle;
            laceHandle.AttachNoAnimation(this);
        }
        
        public void Attach(LaceHandle laceHandle)
        {
            if (IsLocked)
                throw new InvalidOperationException("Hole cant attach handle while been used by other handle.");

            if (laceHandle.Attached)
                throw new InvalidOperationException("Hole cant attach already grabbed handle.");

            _attachedHandle = laceHandle;
            laceHandle.Attach(this);
        }

        public LaceHandle Detach()
        {
            if (IsLocked == false)
                throw new InvalidOperationException("No handles attached to this hole.");

            LaceHandle handle = _attachedHandle;
            
            _attachedHandle.Detach();
            _attachedHandle = null;

            return handle;
        }

        public void Lock()
        {
            _manuallyLocked = true;
            Locked?.Invoke();
        }
        
        public void Unlock()
        {
            _manuallyLocked = false;
            Unlocked?.Invoke();
        }
    }
}