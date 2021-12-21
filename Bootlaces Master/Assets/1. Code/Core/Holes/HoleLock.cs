using System;
using UnityEngine;

namespace BootlacesMaster
{
    public class HoleLock : MonoBehaviour
    {
        [SerializeField] private GameObject _activating = null;
        [SerializeField] private ParticleSystem _particleSystem = null;
        [SerializeField] private Hole _hole = null;

        private void Awake()
        {
            _hole.Locked += OnLocked;
            _hole.Unlocked += OnUnlocked;
        }

        private void OnDestroy()
        {
            _hole.Locked -= OnLocked;
            _hole.Unlocked -= OnUnlocked;
        }

        private void OnLocked()
        {
            _particleSystem.Stop();
            _activating.SetActive(false);
        }

        private void OnUnlocked()
        {
            _activating.SetActive(true);
            _particleSystem.Play();
        }
    }
}