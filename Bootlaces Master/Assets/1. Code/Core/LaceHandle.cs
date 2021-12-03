using System;
using DG.Tweening;
using UnityEngine;

namespace BootlacesMaster
{
    public class LaceHandle : MonoBehaviour
    {
        [SerializeField] private Transform _upAndDownTransform = null;
        [SerializeField] private Transform _moveAroundTransform = null;
        
        [Space, SerializeField] private float _moveSpeed = 0.1f;
        [SerializeField] private float _minMoveTime = 0.05f;
        [SerializeField] private float _maxMoveTime = 0.5f;
        [SerializeField] private float _grabTime = 0.5f;
        [SerializeField] private float _grabHeight = 0.5f;

        public Vector3 Position => _upAndDownTransform.position;
        
        public bool Attached { get; private set; }

        public bool Detached => Attached == false;

        public void MoveTo(Vector3 position)
        {
            if (Attached)
                throw new InvalidOperationException("You can't move attached things.");
            
            float moveTime = Vector3.Distance(_moveAroundTransform.position, position) / _moveSpeed;

            _moveAroundTransform.DOKill();
            _moveAroundTransform.DOMove(position, Mathf.Clamp(moveTime, _minMoveTime, _maxMoveTime));
        }

        public void Detach()
        {
            if (Detached)
                throw new InvalidOperationException("You can't detach lace that not attached.");
            
            Attached = false;

            _upAndDownTransform.DOKill();
            _upAndDownTransform.DOLocalMoveY(_grabHeight, _grabTime)
                .SetEase(Ease.OutQuad);
        }
        
        public void Attach(Hole hole)
        {
            if (Attached)
                throw new InvalidOperationException("You can't attach lace that already attached.");

            Attached = true;

            float moveTime = Vector3.Distance(_moveAroundTransform.position, hole.Position) / _moveSpeed;

            _upAndDownTransform.DOKill();
            _moveAroundTransform.DOKill();
            
            DOTween.Sequence()
                .Append(_moveAroundTransform.DOMove(hole.Position, Mathf.Clamp(moveTime, _minMoveTime, _maxMoveTime)))
                .Append(_upAndDownTransform.DOLocalMoveY(0f, _grabTime)
                    .SetEase(Ease.InQuad))
                .SetTarget(transform);
        }
    }
}