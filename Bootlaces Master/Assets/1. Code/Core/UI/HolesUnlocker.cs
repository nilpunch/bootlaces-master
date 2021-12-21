using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace BootlacesMaster.UI
{
    public class HolesUnlocker : MonoBehaviour
    {
        [SerializeField] private WinConditionChecker _winConditionChecker = null;
        [SerializeField] private Grabber _grabber = null;
        [SerializeField] private Holes _holes = null;
        [SerializeField] private EasingButton _easingButton = null;
        [SerializeField] private bool _inactive = false;
        
        [Space, SerializeField] private float _inactivityDelay = 3f;

        private bool _triggered = false;
        private Tween _delay = null;
        
        private void Awake()
        {
            if (_inactive)
                return;
            
            _easingButton.Clicked += OnButtonClicked;
            _winConditionChecker.Winned += OnWinned;
            _grabber.Grabbed += OnGrabbed;
            _grabber.UnGrabbed += OnUnGrabbed;
        }

        private void OnGrabbed()
        {
            _delay?.Kill();
            _easingButton.Hide();
        }
        
        private void OnUnGrabbed()
        {
            if (_triggered)
                return;
            
            _delay?.Kill();
            _delay = DOTween.Sequence()
                .AppendInterval(_inactivityDelay)
                .AppendCallback(() => _easingButton.Show());
        }

        private void OnWinned()
        {
            _delay?.Kill();
            _triggered = true;
            _easingButton.Hide();
        }

        private void OnDestroy()
        {
            _easingButton.Clicked -= OnButtonClicked;
            _winConditionChecker.Winned -= OnWinned;
        }

        private void OnButtonClicked()
        {
            if (_triggered)
                return;

            _triggered = true;
            
            _easingButton.Hide();

            foreach (var hole in _holes.Collection)
                if (hole.HasManualLock)
                    hole.Unlock();
        }
    }
}