using UnityEngine;

namespace Script.PlayerAction
{
    public class PlayerAction : MonoBehaviour, IAction
    {
        public void ActivateAction()
        {
            throw new System.NotImplementedException();
        }
    }

    public interface IAction
    {
        void ActivateAction();
    }
}