using UnityEngine;

namespace Script.Interfaces
{
    public interface IInteractor : IInteractionItemPoint
    {
        IInteractionItem InteractionItem { get; set; }
        void Interact();
        void StopInteract();
    }
}