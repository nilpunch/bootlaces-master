using DG.Tweening;
using UnityEngine;

namespace BootlacesMaster
{
    public class WinFx : MonoBehaviour
    {
        [SerializeField] private WinConditionChecker _winConditionChecker = null;
        [SerializeField] private ParticleSystem _fx = null;

        private void Awake()
        {
            _winConditionChecker.Winned += OnWinned;
        }
        
        private void OnDestroy()
        {
            _winConditionChecker.Winned -= OnWinned;
        }

        private void OnWinned()
        {
            DOTween.Sequence()
                .AppendInterval(1f)
                .AppendCallback(() => _fx?.Play(true));

        }
    }
}