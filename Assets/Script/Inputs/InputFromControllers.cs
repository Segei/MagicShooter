using Mirror;
using NaughtyAttributes;
using Script.Client.GameSettingsHUDTools;
using Script.Tools;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Script.Inputs
{
    public class InputFromControllers : MonoBehaviour
    {
        [SerializeField] private Input input;
        [Inject] private readonly GameSettings settings;
        private InputActions inputActions;
        [ShowNonSerializedField] private GameSettingsHUD settingsHUD;

        [Client]
        public void Start()
        {
            settingsHUD = FindObjectOfType<GameSettingsHUD>(true);
            settingsHUD.OnCloseHUD.AddListener(CloseMenu);

            inputActions = new InputActions();
            inputActions.Player.Move.started += Move;
            inputActions.Player.Move.performed += Move;
            inputActions.Player.Move.canceled += Move;
            inputActions.Player.Look.started += Mouse;
            inputActions.Player.Look.performed += Mouse;
            inputActions.Player.Look.canceled += Mouse;
            inputActions.Player.Fire.started += Attack;
            inputActions.Player.Interact.started += (e) => Interact(true);
            inputActions.Player.Interact.canceled += (e) => Interact(false);
            inputActions.Player.Jump.started += Jump;
            inputActions.Player.Menu.started += Menu;
            inputActions.Enable();

            settings.OnChangeInvertHorizontal.AddListener(e => inputActions.Player.Look.ApplyParameterOverride("InvertVector2:invertX", e));
            inputActions.Player.Look.ApplyParameterOverride("InvertVector2:invertX", settings.InvertHorizontal);
            settings.OnChangeInvertVertical.AddListener(e => inputActions.Player.Look.ApplyParameterOverride("InvertVector2:invertY", e));
            inputActions.Player.Look.ApplyParameterOverride("InvertVector2:invertY", settings.InvertVertical);

        }

        [Client]
        private void OnDestroy()
        {
            inputActions.Disable();
            inputActions.Dispose();
        }

        [Client]
        private void Move(InputAction.CallbackContext context)
        {
            input.Move(context.ReadValue<Vector2>());
        }


        [Client]
        private void Mouse(InputAction.CallbackContext context)
        {
            Vector2 velocity = context.ReadValue<Vector2>();
            velocity.x *= settings.HorizontalSensitivity;
            velocity.y *= settings.VerticalSensitivity;
            input.Mouse(velocity);
        }

        [Client]
        private void Attack(InputAction.CallbackContext context)
        {
            input.Attack();
        }

        [Client]
        private void Interact(bool value)
        {
            input.Interact(value);
        }

        [Client]
        private void Jump(InputAction.CallbackContext context)
        {
            input.Jump();
        }

        [Client]
        private void Menu(InputAction.CallbackContext context)
        {
            settingsHUD.ShowSettings();
            inputActions.Disable();
        }

        [Client]
        private void CloseMenu()
        {
            Cursor.lockState = CursorLockMode.Locked;
            inputActions.Enable();
        }
    }
}