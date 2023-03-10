using Newtonsoft.Json;
using Script.Server;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Script.Tools
{
    [Serializable]
    public class GameSettings
    {
        [JsonProperty, SerializeField] private bool invertHorizontal = false;
        [JsonProperty, SerializeField] private bool invertVertical = false;

        [JsonIgnore]
        public bool InvertHorizontal
        {
            get => invertHorizontal;
            set
            {
                invertHorizontal = value;
                OnChangeInvertHorizontal.Invoke(value);
                OnChangeGameSettings?.Invoke();
            }
        }

        [JsonIgnore]
        public bool InvertVertical
        {
            get => invertVertical;
            set
            {
                invertVertical = value;
                OnChangeInvertVertical.Invoke(value);
                OnChangeGameSettings?.Invoke();
            }
        }

        [Range(0.01f, 10)] public float HorizontalSensitivity = 1f;
        [Range(0.01f, 10)] public float VerticalSensitivity = 1f;

        [JsonIgnore] public PlayerInstanceFactory PrefabPlayer;

        [JsonIgnore] public UnityEvent<bool> OnChangeInvertHorizontal = new UnityEvent<bool>();
        [JsonIgnore] public UnityEvent<bool> OnChangeInvertVertical = new UnityEvent<bool>();
        [JsonIgnore] public UnityEvent<float> OnChangeHorizontalSensitivity = new UnityEvent<float>();
        [JsonIgnore] public UnityEvent<float> OnChangeVerticalSensitivity = new UnityEvent<float>();
        [JsonIgnore] public UnityEvent OnChangeGameSettings = new UnityEvent();



        public void SetHorizontalSensitivity(float value)
        {
            HorizontalSensitivity = value;
            OnChangeHorizontalSensitivity.Invoke(value);
            OnChangeGameSettings?.Invoke();
        }

        public void SetVerticalSensitivity(float value)
        {
            VerticalSensitivity = value;
            OnChangeVerticalSensitivity?.Invoke(value);
            OnChangeGameSettings?.Invoke();
        }

        public void Load(GameSettings settings)
        {
            invertHorizontal = settings.InvertHorizontal;
            invertVertical = settings.InvertVertical;
            SetHorizontalSensitivity(settings.HorizontalSensitivity);
            SetVerticalSensitivity(settings.VerticalSensitivity);
        }
    }
}