using Mirror;
using Script.PlayerAction;
using UnityEngine;
using UnityEngine.Events;

namespace Script.Interaction
{
    public class Interaction : MonoBehaviour, IInteraction
    {
        public UnityEvent OnInteractWithThis;
        
        [Client]
        public void Interact(IInteractor interactor)
        {
            OnInteractWithThis?.Invoke();
        }
    }
}