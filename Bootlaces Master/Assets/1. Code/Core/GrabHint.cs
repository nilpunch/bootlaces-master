using System;
using DG.Tweening;
using UnityEngine;

namespace BootlacesMaster
{
    public class GrabHint : MonoBehaviour
    {
        [SerializeField] private Grabber _grabber = null;

        [Space, SerializeField] private Transform _pivot = null;
        [SerializeField] private Transform _arrow = null;
        [SerializeField] private Transform _circle = null;

        [Space, SerializeField] private float _followMoveTime = 1f;
        [SerializeField] private float _followRotationTime = 1f;
        
        [Space, SerializeField] private float _arrowMovingUp = 0.2f;
        [SerializeField] private float _circleGrow = 0.2f;
        [SerializeField] private float _animationSpeed = 3f;

        private Vector3 _circleOriginalLocalScale;
        private float _arrowOriginalLocalHeight;
        private Hole _lastHole;
        
        private void Awake()
        {
            _circleOriginalLocalScale = _circle.localScale;
            _arrowOriginalLocalHeight = _arrow.localPosition.y;
            _grabber.Aimed += OnAimed;
            _grabber.Grabbed += OnGrabbed;
            _grabber.UnGrabbed += OnUnGrabbed;
            
            _pivot.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _grabber.Aimed -= OnAimed;
            _grabber.Grabbed -= OnGrabbed;
            _grabber.UnGrabbed -= OnUnGrabbed;
        }

        private void OnUnGrabbed()
        {
            _lastHole = null;
            _pivot.DOKill();
            _pivot.gameObject.SetActive(false);
        }

        private void OnGrabbed()
        {
            _pivot.gameObject.SetActive(true);
        }

        private void OnAimed(Hole hole)
        {
            if (_lastHole == hole)
                return;

            if (_lastHole == null)
            {
                _pivot.position = hole.Position;
                _pivot.rotation = hole.Rotation;
            }
            else
            {
                _pivot.position = hole.Position;
                _pivot.rotation = hole.Rotation;
                //_pivot.DOKill();
                //_pivot.DORotateQuaternion(hole.Rotation, _followRotationTime).SetEase(Ease.OutQuad);
            }
            
            _lastHole = hole;
        }

        private void Update()
        {
            float magnitude = (Mathf.Sin(Time.time * _animationSpeed) + 1f) / 2f;
            
            _circle.localScale = _circleOriginalLocalScale * (1f + _circleGrow * magnitude);
            _arrow.localPosition = Quaternion.Inverse(_arrow.rotation) * Vector3.up *
                                   (_arrowOriginalLocalHeight * (1f + _arrowMovingUp * magnitude));
        }
    }
}