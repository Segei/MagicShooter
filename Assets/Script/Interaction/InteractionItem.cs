using System;
using Script.PlayerAction;
using UnityEngine;

namespace Script.Interaction
{
    public class InteractionItem : MonoBehaviour, IInteraction, IInteractionItem
    {
        private Transform trackingPoint;
        
        public void Interact(IInteractor interactor)
        {
            interactor.InteractionItem = this;
            trackingPoint = interactor.ItemPoint;
        }

        public void Chunk()
        {
            throw new System.NotImplementedException();
        }

        public void Throw()
        {
            throw new System.NotImplementedException();
        }

        private void Update()
        {
             
        }
    }

    public interface IInteraction
    {
        void Interact(IInteractor interactor);
    }

    public interface IInteractionItem
    {
        void Chunk();

        void Throw();
    }
}