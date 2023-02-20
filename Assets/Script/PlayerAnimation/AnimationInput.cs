using System;
using Script.PlayersMovable;
using UnityEngine;

namespace Script.PlayerAnimation
{
    public class AnimationInput : MonoBehaviour, IMovable
    {
        [SerializeField]private Animator animator;
        public void Move(Vector2 velocity)
        {
            animator.SetFloat("Move Forward", velocity.y);
            animator.SetFloat("Move Right", velocity.x);
        }
    }
}