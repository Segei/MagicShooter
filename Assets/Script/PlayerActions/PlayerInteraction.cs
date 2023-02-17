using Script.Interaction;
using UnityEngine;

namespace Script.PlayerAction
{
    public class PlayerInteraction : MonoBehaviour, IInteractor
    {
        [field: SerializeField] public IInteractionItem InteractionItem { get; set; }
        public Transform ItemPoint { get; set; }


        public void Interact()
        {
            throw new System.NotImplementedException();
        }
    }

    public interface IInteractor : IInteractionItemPoint
    {
        IInteractionItem InteractionItem { get; set; }
        void Interact();
    }

    public interface IInteractionItemPoint
    {
        Transform ItemPoint { get; set; }
    }
}