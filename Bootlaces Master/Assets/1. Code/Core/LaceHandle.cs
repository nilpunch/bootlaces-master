using System;
using DG.Tweening;
using UnityEngine;

namespace BootlacesMaster
{
    public class LaceHandle : MonoBehaviour
    {
        [SerializeField] private float _easeTime = 0.1f;
        [SerializeField] private float _grabTime = 0.5f;
        [SerializeField] private float _grabHeight = 0.5f;

        private float _originalHeight;
        
        public Vector3 Position => transform.position;

        private void Awake()
        {
            _originalHeight = transform.position.y;
        }

        public void MoveTo(Vector3 position)
        {
            transform.DOMoveX(position.x, _easeTime);
            transform.DOMoveZ(position.z, _easeTime);
        }

        public void Grab()
        {
            transform.DOMoveY(_originalHeight + _grabHeight, _easeTime);
        }
        
        public void UnGrab()
        {
            transform.DOMoveY(_originalHeight, _easeTime);
        }
    }
}