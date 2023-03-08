using Mirror;
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

        [Client]
        public void Start()
        {
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
            inputActions.Enable();
            Cursor.lockState = CursorLockMode.Locked;
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
            Debug.Log("Move");
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
    }
}