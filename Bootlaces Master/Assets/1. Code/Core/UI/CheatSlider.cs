using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BootlacesMaster.UI
{
    public class CheatSlider : MonoBehaviour
    {
        [SerializeField] private Slider _slider = null;
        [SerializeField] private TextMeshProUGUI _value = null;

        public event Action ValueChanged;

        public float Value => _slider.value;
        
        private void Awake()
        {
            _slider.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDestroy()
        {
            _slider.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(float value)
        {
            _value.text = value.ToString("0.00");
            ValueChanged?.Invoke();
        }

        public void SetValue(float value)
        {
            _slider.SetValueWithoutNotify(value);
            OnValueChanged(value);
        }
    }
}