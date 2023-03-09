using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Script.Client.GameSettingsHUDTools
{
    public class SensitivityView : MonoBehaviour
    {
        [SerializeField] private TMP_InputField input;
        [SerializeField] private Slider slider;

        public UnityEvent<float> OnValueChange;

        private void Start()
        {
            input.onEndEdit.AddListener(ChangeValue);
            slider.onValueChanged.AddListener(ChangeValue);
        }

        private void ChangeValue(string value)
        {
            ChangeValue(float.Parse(value));
        }

        private void ChangeValue(float value)
        {
            float result = (float)Math.Round(value, 3);
            result = Mathf.Clamp(result, 0.001f, 10);
            OnValueChange?.Invoke(result);
        }

        public void SetValue(float value)
        {
            slider.SetValueWithoutNotify(value);
            input.SetTextWithoutNotify(value.ToString());
        }
    }
}
