using Script.Tools;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace Script.Client.GameSettingsHUDTools
{
    public class GameSettingsHUD : MonoBehaviour
    {
        [Inject] private readonly GameSettings gameSettings;

        [SerializeField] private Toggle invertHorizontal;
        [SerializeField] private Toggle invertVertical;
        [SerializeField] private SensitivityView sensitivityViewHorizontal;
        [SerializeField] private SensitivityView sensitivityViewVertical;
        public UnityEvent OnCloseHUD;



        private void Start()
        {
            invertHorizontal.isOn = gameSettings.InvertHorizontal;
            invertHorizontal.onValueChanged.AddListener(e => gameSettings.InvertHorizontal = e);

            invertVertical.isOn = gameSettings.InvertVertical;
            invertVertical.onValueChanged.AddListener(e => gameSettings.InvertVertical = e);

            sensitivityViewHorizontal.SetValue(gameSettings.HorizontalSensitivity);
            gameSettings.OnChangeHorizontalSensitivity.AddListener(sensitivityViewHorizontal.SetValue);
            sensitivityViewHorizontal.OnValueChange.AddListener(gameSettings.SetHorizontalSensitivity);

            sensitivityViewVertical.SetValue(gameSettings.VerticalSensitivity);
            gameSettings.OnChangeVerticalSensitivity.AddListener(sensitivityViewVertical.SetValue);
            sensitivityViewVertical.OnValueChange.AddListener(gameSettings.SetVerticalSensitivity);
        }


        public void ShowSettings()
        {
            gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
        }

        public void HideSettings()
        {
            gameObject.SetActive(false);
            OnCloseHUD?.Invoke();
        }

    }
}
