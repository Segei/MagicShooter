using System;
using Script.PlayerAction;
using UnityEngine;

namespace Script.Interaction
{
    [RequireComponent(typeof(Rigidbody))]
    public class InteractionItem : MonoBehaviour, IInteraction, IInteractionItem
    {
        private Transform trackingPoint;
        
        public void Interact(IInteractor interactor)
        {
            interactor.InteractionItem = this;
            trackingPoint = interactor.ItemPoint;
        }
        public void Throw()
        {
            
        }
        
        public void Drop()
        {
            
        }
        
        private void Update()
        {
            if (trackingPoint == null)
            {
                return;
            }
            
        }
    }

    public interface IInteraction
    {
        void Interact(IInteractor interactor);
    }

    public interface IInteractionItem
    {
        void Throw();

        void Drop();
    }
}