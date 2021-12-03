using System;
using DG.Tweening;
using UnityEngine;

namespace BootlacesMaster
{
    public class LaceHandle : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 0.1f;
        [SerializeField] private float _minMoveTime = 0.2f;
        [SerializeField] private float _grabTime = 0.5f;
        [SerializeField] private float _grabHeight = 0.5f;

        public Vector3 Position => transform.position;
        
        public bool Attached { get; private set; }

        public bool Detached => Attached == false;

        public void MoveTo(Vector3 position)
        {
            Vector3 endPosition = position + Vector3.up * _grabHeight;
            float moveTime = Vector3.Distance(transform.position, endPosition) / _moveSpeed;

            transform.DOKill();
            transform.DOMove(endPosition, Mathf.Max(_minMoveTime, moveTime));
        }

        public void Detach()
        {
            if (Detached)
                throw new InvalidOperationException("You can't detach lace that not attached.");
            
            Attached = false;
        }
        
        public void Attach(Hole hole)
        {
            if (Attached)
                throw new InvalidOperationException("You can't attach lace that already attached.");

            Attached = true;
            
            Vector3 endPosition = hole.Position + Vector3.up * _grabHeight;
            float moveTime = Vector3.Distance(transform.position, endPosition) / _moveSpeed;

            transform.DOKill();
            DOTween.Sequence()
                .Append(transform.DOMove(endPosition, Mathf.Max(_minMoveTime, moveTime)))
                .Append(transform.DOMove(hole.Position, _grabTime))
                .SetTarget(transform);
        }
    }
}