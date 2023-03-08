using Assets.Script.DamageAbility;
using Mirror;
using Script.Interfaces;
using Script.PlayerAction;
using UnityEngine;

namespace Script.Interaction
{
    [RequireComponent(typeof(Rigidbody))]
    public class InteractionItem : NetworkBehaviour, IInteraction, IInteractionItem
    {

        private new Rigidbody rigidbody;
        private IInteractor interactor;
        [SerializeField] private float forceTrow, damageForThrow;

        [ServerCallback]
        public override void OnStartServer()
        {
            rigidbody = GetComponent<Rigidbody>();
            syncDirection = SyncDirection.ClientToServer;
            base.OnStartServer();
        }

        [ServerCallback]
        public void Interact(IInteractor interactor)
        {
            if (this.interactor != null)
            {
                return;
            }
            this.interactor = interactor;
            interactor.InteractionItem = this;
        }

        [ServerCallback]
        public void Throw()
        {
            StopInteractToInteractor();
            rigidbody.AddForce(rigidbody.transform.forward * forceTrow);
            gameObject.AddComponent<DamageAbility>().Damage = damageForThrow;
        }

        [ServerCallback]
        public void Drop()
        {
            StopInteractToInteractor();
        }

        [ServerCallback]
        private void StopInteractToInteractor()
        {
            interactor.InteractionItem = null;
            interactor = null;
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }


        [ServerCallback]
        private void FixedUpdate()
        {
            if (interactor == null)
            {
                return;
            }

            rigidbody.velocity = (interactor.ItemPoint.position - rigidbody.position) * 20;
            rigidbody.angularVelocity = Vector3.Cross(interactor.ItemPoint.forward, -rigidbody.transform.forward) * 10;
        }
    }
}