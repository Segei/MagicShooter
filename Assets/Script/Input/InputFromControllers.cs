using System.Collections.Generic;
using System.Linq;
using Script.PlayersMovable;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Input
{
    public class InputFromControllers : MonoBehaviour
    {
        [SerializeField] private List<GameObject> iMovables;
        [SerializeField] private List<GameObject> iRotatings;
        [SerializeField] private List<GameObject> iJimpings;
        
        private List<IMovable> movables;
        private List<IRotating> rotatings;
        private List<IJumping> jumpings;
        private InputActions actions;

        private void Start()
        {
            movables = iMovables.Select(e => e.GetComponent<IMovable>()).ToList();
            rotatings = iRotatings.Select(e => e.GetComponent<IRotating>()).ToList();
            jumpings = iJimpings.Select(e => e.GetComponent<IJumping>()).ToList();
            actions = new InputActions();
            actions.Player.Move.started += Move;
            actions.Player.Move.performed += Move;
            actions.Player.Move.canceled += Move;
            actions.Player.Look.started += Mouse;
            actions.Player.Look.performed += Mouse;
            actions.Player.Look.canceled += Mouse;
            actions.Player.Fire.started += Attack;
            actions.Player.Interact.started += (e) => Interact(true);
            actions.Player.Interact.canceled += (e) => Interact(false);
            actions.Player.Jump.started += Jump;
            actions.Enable();
        }

        private void OnDestroy()
        {
            actions.Disable();
            actions.Dispose();
        }

        private void Move(InputAction.CallbackContext context)
        {
            foreach (var movable in movables)
            {
                movable.Move(context.ReadValue<Vector2>());
            }
        }

        private void Mouse(InputAction.CallbackContext context)
        {
            foreach (var rotating in rotatings)
            {
                rotating.Rotate(context.ReadValue<Vector2>());
            }
        }

        private void Attack(InputAction.CallbackContext context)
        {
        }

        private void Interact(bool value)
        {
            Debug.Log(value);
        }

        private void Jump(InputAction.CallbackContext context)
        {
            foreach (var jumping in jumpings)
            {
                jumping.Jump();
            }
        }
    }
}