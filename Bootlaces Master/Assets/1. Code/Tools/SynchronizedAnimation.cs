using System;
using UnityEngine;

namespace HighwayRage
{
    [RequireComponent(typeof(Animation))]
    public class SynchronizedAnimation : MonoBehaviour
    {
        [SerializeField] private float _offsetByDistance = 0f;
        [SerializeField, Range(0.0f, 1.0f)] private float _offset = 0f;

        private Animation _animationComponent;
        private AnimationClip _animationClip;

        private Vector3 _lastPosition;
        
        private void Awake()
        {
            _animationComponent = GetComponent<Animation>();
            _animationClip = _animationComponent.clip;
            _animationComponent[_animationClip.name].weight = 1f;

            _lastPosition = transform.position;
        }

        private void OnEnable()
        {
            if (_animationComponent.isPlaying == false)
                _animationComponent.Play();

            UpdateOffset();
        }

        private void Update()
        {
            if (_lastPosition != transform.position)
            {
                _lastPosition = transform.position;
                UpdateOffset();
            }
        }

        private void UpdateOffset()
        {
            float offset = _offset + _offsetByDistance * transform.position.z;
            
            var seconds = Time.time;
            var clipLength = _animationComponent[_animationClip.name].length;
            _animationComponent[_animationClip.name].normalizedTime = (seconds % clipLength / clipLength + offset) % 1f;
        }
    }
}