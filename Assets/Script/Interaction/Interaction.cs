using Mirror;
using Script.Interfaces;
using Script.PlayerAction;
using UnityEngine;
using UnityEngine.Events;

namespace Script.Interaction
{
    public class Interaction : MonoBehaviour, IInteraction
    {
        public UnityEvent OnInteractWithThis;

        [ServerCallback]
        public void Interact(IInteractor interactor)
        {
            OnInteractWithThis?.Invoke();
        }
    }
}