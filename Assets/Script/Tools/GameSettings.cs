using Script.Server;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Script.Tools
{
    [Serializable]
    public class GameSettings
    {
        private bool invertHorizontal;
        private bool invertVertical;

        public bool InvertHorizontal
        {
            get => invertHorizontal;
            set
            {
                invertHorizontal = value;
                OnChangeInvertHorizontal.Invoke(value);
            }
        }

        public bool InvertVertical
        {
            get => invertVertical;
            set
            {
                invertVertical = value;
                OnChangeInvertVertical.Invoke(value);
            }
        }

        public UnityEvent<bool> OnChangeInvertHorizontal;
        public UnityEvent<bool> OnChangeInvertVertical;

        [Range(0.01f, 10)] public float HorizontalSensitivity = 1f;
        [Range(0.01f, 10)] public float VerticalSensitivity = 1f;

        public void SetHorizontalSensitivity(float value)
        {
            HorizontalSensitivity = value;
            OnChangeHorizontalSensitivity.Invoke(value);
        }

        public void SetVerticalSensitivity(float value) {             
            VerticalSensitivity = value; 
            OnChangeVerticalSensitivity?.Invoke(value);
        }

        public UnityEvent<float> OnChangeHorizontalSensitivity;
        public UnityEvent<float> OnChangeVerticalSensitivity;

        public PlayerInstanceFactory PrefabPlayer;
    }
}