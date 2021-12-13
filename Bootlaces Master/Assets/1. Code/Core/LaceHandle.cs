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
        [SerializeField] private float _minAttachMoveTime = 0.05f;
        [SerializeField] private float _maxMoveTime = 0.5f;
        [SerializeField] private float _attachTime = 0.3f;
        [SerializeField] private float _detachTime = 0.2f;
        [SerializeField] private float _grabHeight = 1.5f;

        [Space, SerializeField, Range(0f, 1f)] private float _sequenceShift = 0f;

        private Vector3 _lastRequestedMove;
        private bool _canMove = true;
        private int _lastAttachedHole = -1;
        
        public Vector3 Position => _upAndDownTransform.position;
        
        public bool Attached { get; private set; }

        public bool Detached => Attached == false;

        public int AttachedHoleIndex => _lastAttachedHole;

        public void MoveTo(Vector3 position)
        {
            if (Attached)
                throw new InvalidOperationException("You can't move attached things.");

            _lastRequestedMove = position;

            if (_canMove == false)
                return;
            
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
            _moveAroundTransform.DOKill();

            DOTween.Sequence()
                .AppendInterval(_detachTime * _sequenceShift)
                .AppendCallback(() =>
                {
                    _canMove = true;
                    MoveTo(_lastRequestedMove);
                })
                .SetTarget(_moveAroundTransform);

            _upAndDownTransform.DOLocalMoveY(_grabHeight, _detachTime)
                .SetEase(Ease.InOutQuad);
        }
        
        public void Attach(Hole hole)
        {
            if (Attached)
                throw new InvalidOperationException("You can't attach lace that already attached.");

            Attached = true;
            _canMove = false;

            if (_lastAttachedHole == hole.Index)
                _upAndDownTransform.DOKill();
            
            _lastAttachedHole = hole.Index;
            _lastRequestedMove = hole.Position;

            float moveTime = Vector3.Distance(_moveAroundTransform.position, hole.Position) / _moveSpeed;

            _moveAroundTransform.DOKill();
            transform.DOKill();
            
            DOTween.Sequence()
                .Append(_moveAroundTransform.DOMove(hole.Position, Mathf.Clamp(moveTime, _minAttachMoveTime, _maxMoveTime)))
                .AppendCallback(() => _upAndDownTransform.DOKill())
                .Append(_upAndDownTransform.DOLocalMoveY(0f, _attachTime)
                    .SetEase(Ease.InOutQuad))
                .SetTarget(transform);
        }
        
        public void AttachNoAnimation(Hole hole)
        {
            if (Attached)
                throw new InvalidOperationException("You can't attach lace that already attached.");

            Attached = true;
            _canMove = false;

            _lastAttachedHole = hole.Index;
            _lastRequestedMove = hole.Position;

            _upAndDownTransform.localPosition = _upAndDownTransform.localPosition.With(y: 0f);
            _moveAroundTransform.position = hole.Position;
        }
    }
}