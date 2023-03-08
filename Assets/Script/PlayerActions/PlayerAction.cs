using Assets.Script.Interfaces;
using Mirror;
using Script.Interfaces;
using Script.Tools;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Script.PlayerAction
{
    public class PlayerAction : MonoBehaviour, IAction
    {
        [SerializeField] private List<GameObject> iInteractor;
        [SerializeField] private List<GameObject> iAttack;
        private List<IInteractor> interactors;
        private List<IAttack> attacks;

        [ServerCallback]
        private void Awake()
        {
            interactors = iInteractor.GetInterfaces<IInteractor>();
            attacks = iAttack.GetInterfaces<IAttack>();
        }

        [ServerCallback]
        public void ActivateAction()
        {
            IEnumerable<IInteractor> withInteractionItems = interactors.Where(e => e.InteractionItem != null).ToList();
            foreach (IInteractor interactor in withInteractionItems)
            {
                interactor.InteractionItem.Throw();
            }
            if (withInteractionItems.Count() > 0)
            {
                return;
            }
            foreach (IAttack attack in attacks)
            {
                attack.Attack();
            }
        }
    }
}