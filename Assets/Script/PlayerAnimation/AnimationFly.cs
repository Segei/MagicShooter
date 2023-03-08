using Mirror;
using Script.Interfaces;
using System;
using UnityEngine;

namespace Assets.Script.PlayerAnimation
{
    public class AnimationFly : MonoBehaviour, IJumping
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Transform pointCheckGround;
        private int waitTick;

        [Server]
        public void Jump()
        {
            animator.SetBool("Jump", true);
            waitTick = 20;
        }

        [Server]
        private void FixedUpdate()
        {
            if(Physics.Raycast(pointCheckGround.position, -pointCheckGround.up, out RaycastHit hit, 5))
            {
                animator.SetFloat("Height Jump", hit.distance);
            }
            
            if(waitTick <= 0)
            {
                animator.SetBool("Jump", false);
                return;
            }
            waitTick--;
        }
    }
}
