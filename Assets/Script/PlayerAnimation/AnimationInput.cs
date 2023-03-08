using Mirror;
using Script.Interfaces;
using UnityEngine;

namespace Script.PlayerAnimation
{
    public class AnimationInput : MonoBehaviour, IMovable
    {
        [SerializeField] private Animator animator;


        [ServerCallback]
        public void Move(Vector2 velocity)
        {
            animator.SetFloat("Move Forward", velocity.y);
            animator.SetFloat("Move Right", velocity.x);
        }
    }
}