using Mirror;
using Script.Interaction;
using Script.Interfaces;
using UnityEngine;

namespace Script.PlayerAction
{
    public class PlayerInteractor : MonoBehaviour, IInteractor
    {
        [SerializeField] private float distance = 2f;
        [SerializeField] private Transform interactor;
        public IInteractionItem InteractionItem { get; set; }
        [field: SerializeField] public Transform ItemPoint { get; set; }
        private new Transform transform => interactor.transform;

        [ServerCallback]
        public void Interact()
        {
            if (!interactor.gameObject.activeInHierarchy)
            {
                return;
            }
            var raycastAll = Physics.RaycastAll(transform.position, transform.forward.normalized, distance);
            foreach (var raycast in raycastAll)
            {
                if (raycast.collider.TryGetComponent(out IInteraction interaction))
                {
                    interaction.Interact(this);
                }
            }
        }

        [ServerCallback]
        public void StopInteract()
        {
            if (InteractionItem != null)
            {
                InteractionItem.Drop();
            }
        }

        [ServerCallback]
        private void OnDrawGizmos()
        {
            if (!interactor.gameObject.activeInHierarchy)
            {
                return;
            }
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + (transform.forward * distance));
        }
    }
}