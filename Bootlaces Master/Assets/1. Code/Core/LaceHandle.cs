using System;
using DG.Tweening;
using UnityEngine;

namespace BootlacesMaster
{
    public class LaceHandle : MonoBehaviour
    {
        [SerializeField] private Lace _lace = null;
        [SerializeField] private float _easeTime = 0.1f;
        [SerializeField] private float _grabTime = 0.5f;
        [SerializeField] private float _grabHeight = 0.5f;

        private float _originalHeight;
        
        public Vector3 Position => transform.position;

        public ILace Lace => _lace;
        
        public bool Attached { get; private set; }

        public bool Detached => Attached == false;

        private void Awake()
        {
            _originalHeight = transform.position.y;
        }

        public void MoveTo(Vector3 position)
        {
            transform.DOMoveX(position.x, _easeTime);
            transform.DOMoveZ(position.z, _easeTime);
        }

        public void Detach()
        {
            if (Detached)
                throw new InvalidOperationException("You can't detach lace that not attached.");
            
            Attached = false;
            transform.DOMoveY(_originalHeight + _grabHeight, _grabTime);
        }
        
        public void Attach(Hole hole)
        {
            if (Attached)
                throw new InvalidOperationException("You can't attach lace that already attached.");

            _originalHeight = hole.Position.y;
            Attached = true;
            transform.DOMoveY(_originalHeight, _grabTime);
        }
    }
}