using Script.Interaction;
using UnityEngine;

namespace Script.PlayerAction
{
    public class PlayerInteractor : MonoBehaviour, IInteractor
    {
        [SerializeField] private float distance = 2f;
        public IInteractionItem InteractionItem { get; set; }
        [field: SerializeField] public Transform ItemPoint { get; set; }


        public void Interact()
        {
            var position = transform.position;
            var raycastAll = Physics.RaycastAll(position, transform.forward.normalized, distance);
            foreach (var raycast in raycastAll)
            {
                if (raycast.collider.TryGetComponent(out IInteraction interaction))
                {
                    interaction.Interact(this);
                }
            }
        }

        public void StopInteract()
        {
            if (InteractionItem != null)
            {
                InteractionItem.Drop();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + (transform.forward * distance));
        }
    }

    public interface IInteractor : IInteractionItemPoint
    {
        IInteractionItem InteractionItem { get; set; }
        void Interact();
        void StopInteract();
    }

    public interface IInteractionItemPoint
    {
        Transform ItemPoint { get; set; }
    }
}