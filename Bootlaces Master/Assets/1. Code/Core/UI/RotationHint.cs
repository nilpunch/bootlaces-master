using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace BootlacesMaster.UI
{
    public class RotationHint : MonoBehaviour
    {
        [SerializeField] private Image[] _hints = null;

        [Space, SerializeField] private float _hideTime = 0.5f;
        [SerializeField] private float _pulsationTime = 0.5f;
        [SerializeField] private float _pulsationMinAlfa = 0.3f;
        [SerializeField] private float _pulsationMaxAlfa = 0.7f;

        public bool Showed { get; private set; }

        private void Awake()
        {
            foreach (var hint in _hints)
                hint.color = hint.color * (Color.white - Color.black);
        }

        public void Show()
        {
            if (Showed)
                return;
            
            Showed = true;
            
            foreach (var hint in _hints)
            {
                hint.DOKill();
                hint.DOFade(_pulsationMaxAlfa, _pulsationTime)
                    .OnComplete(() =>
                    {
                        hint.DOKill();
                        hint.DOFade(_pulsationMinAlfa, _pulsationTime)
                            .SetEase(Ease.InOutQuad)
                            .SetLoops(-1, LoopType.Yoyo);
                    });
            }
        }

        public void Hide()
        {
            if (Showed == false)
                return;
            
            Showed = false;

            foreach (var hint in _hints)
            {
                hint.DOKill();
                hint.DOFade(0f, _hideTime);
            }
        }
    }
}