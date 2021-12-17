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

        private Tween _attachMovement;
        private Tween _detachMovement;
        private Tween _wanderingMovement;
        
        private Vector3 _lastRequestedMove;
        private int _lastAttachedHole = -1;

        private bool CanMove => Detached && (_detachMovement == null 
                                             || _detachMovement.IsActive() == false 
                                             || _detachMovement.IsPlaying() == false 
                                             || _upAndDownTransform.localPosition.y / _grabHeight > _sequenceShift);

        public Vector3 Position => _upAndDownTransform.position;

        public bool Attached { get; private set; }

        public bool Detached => Attached == false;

        public int AttachedHoleIndex => _lastAttachedHole;

        public void MoveTo(Vector3 position)
        {
            if (Attached)
                throw new InvalidOperationException("You can't move attached things.");

            _lastRequestedMove = position;

            if (CanMove == false)
                return;

            float moveTime = Vector3.Distance(_moveAroundTransform.position, position) / _moveSpeed;

            _attachMovement?.Kill();
            _wanderingMovement?.Kill();

            _wanderingMovement = _moveAroundTransform.DOMove(position, Mathf.Clamp(moveTime, _minMoveTime, _maxMoveTime))
                .SetEase(Ease.OutQuad)
                .SetUpdate(UpdateType.Fixed);
        }

        public void Detach()
        {
            if (Detached)
                throw new InvalidOperationException("You can't detach lace that not attached.");

            Attached = false;

            _attachMovement?.Kill();
            _detachMovement?.Kill();
            _wanderingMovement?.Kill();

            _detachMovement = _upAndDownTransform.DOLocalMoveY(_grabHeight, _detachTime)
                .SetEase(Ease.InOutQuad)
                .SetUpdate(UpdateType.Fixed)
                .OnUpdate(() =>
                {
                    if (CanMove && (_wanderingMovement == null || _wanderingMovement.IsActive() == false || _wanderingMovement.IsPlaying() == false))
                        MoveTo(_lastRequestedMove);
                });
        }

        public void Attach(Hole hole)
        {
            if (Attached)
                throw new InvalidOperationException("You can't attach lace that already attached.");

            bool canFastAttachToSameHole = _lastAttachedHole == hole.Index && CanMove == false;

            float additionalTime = _detachMovement.IsActive()
                ? Mathf.Max(0f, _detachMovement.Duration() * _sequenceShift - _detachMovement.Elapsed())
                : 0f;

            Attached = true;

            _lastAttachedHole = hole.Index;
            _lastRequestedMove = hole.Position;

            float moveTime = Vector3.Distance(_moveAroundTransform.position, hole.Position) / _moveSpeed;

            _wanderingMovement?.Kill();
            _attachMovement?.Kill();

            if (canFastAttachToSameHole)
            {
                _detachMovement?.Kill();
                _attachMovement = _upAndDownTransform
                    .DOLocalMoveY(0f, _attachTime)
                    .SetEase(Ease.InOutQuad)
                    .SetUpdate(UpdateType.Fixed);
            }
            else
            {
                _attachMovement = DOTween.Sequence()
                    .AppendInterval(additionalTime)
                    .Append(_wanderingMovement = _moveAroundTransform
                        .DOMove(hole.Position, Mathf.Clamp(moveTime, _minAttachMoveTime, _maxMoveTime))
                        .SetEase(Ease.OutQuad)
                        .SetUpdate(UpdateType.Fixed))
                    .AppendCallback(() => _detachMovement.Kill())
                    .Append(_upAndDownTransform
                        .DOLocalMoveY(0f, _attachTime)
                        .SetEase(Ease.InOutQuad)
                        .SetUpdate(UpdateType.Fixed))
                    .SetUpdate(UpdateType.Fixed);
            }
        }

        public void AttachInstantly(Hole hole)
        {
            if (Attached)
                throw new InvalidOperationException("You can't attach lace that already attached.");

            Attached = true;

            _lastAttachedHole = hole.Index;
            _lastRequestedMove = hole.Position;

            _upAndDownTransform.localPosition = _upAndDownTransform.localPosition.With(y: 0f);
            _moveAroundTransform.position = hole.Position;
        }
    }
}