using Mirror;
using Script.Interfaces;
using Script.Tools;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Inputs
{
    internal class Input : NetworkBehaviour
    {
        [SerializeField] private List<GameObject> iMovables;
        [SerializeField] private List<GameObject> iRotatings;
        [SerializeField] private List<GameObject> iJimpings;
        [SerializeField] private List<GameObject> iInteractor;
        [SerializeField] private List<GameObject> iActions;

        private List<IMovable> movables;
        private List<IRotating> rotatings;
        private List<IJumping> jumpings;
        private List<IInteractor> interactors;
        private List<IAction> actions;

        public void Start()
        {
            if (isLocalPlayer && !isServer)
            {
                rotatings = iRotatings.GetInterfaces<IRotating>();
                return;
            }
            else if(isLocalPlayer && isServer)
            {
                rotatings = iRotatings.GetInterfaces<IRotating>();
            }
            movables = iMovables.GetInterfaces<IMovable>();
            jumpings = iJimpings.GetInterfaces<IJumping>();
            interactors = iInteractor.GetInterfaces<IInteractor>();
            actions = iActions.GetInterfaces<IAction>();
        }

        [Command]
        public void Move(Vector2 vector)
        {
            foreach (IMovable movable in movables)
            {
                movable.Move(vector);
            }
        }

        [Client]
        public void Mouse(Vector2 vector)
        {
            foreach (IRotating rotating in rotatings)
            {
                rotating.Rotate(vector);
            }
        }

        [Command]
        public void Attack()
        {
            foreach (IAction action in actions)
            {
                action.ActivateAction();
            }
        }

        [Command]
        public void Interact(bool value)
        {
            Debug.Log("Interact " + value);
            if (value)
            {
                foreach (IInteractor interactor in interactors)
                {
                    interactor.Interact();
                }
            }
            else
            {
                foreach (IInteractor interactor in interactors)
                {
                    interactor.StopInteract();
                }
            }
        }

        [Command]
        public void Jump()
        {
            foreach (IJumping jumping in jumpings)
            {
                jumping.Jump();
            }
        }
    }
}
