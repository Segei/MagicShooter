using Mirror;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Script.Tools
{
    public class EventForAnimations : MonoBehaviour
    {
        public bool Animation;
        public UnityEvent RunAnimation;
        public UnityEvent EndAnimation;

        [SerializeField] private string nameAcnimationclip;
        [SerializeField] private Animator animator;
        [SerializeField] private float timeEnd = 1;

        private void OnValidate()
        {
            if (animator == null)
            {
                animator = GetComponent<Animator>();
            }

            if (string.IsNullOrEmpty(nameAcnimationclip))
            {
                return;
            }
            if (!CheckAnimation())
            {
                Debug.LogError(nameAcnimationclip + " animation clip not found in animator", gameObject);
            }
        }


        [ServerCallback]
        private void Start()
        {
            animator = GetComponent<Animator>();
            if (!CheckAnimation())
            {
                Debug.LogError("Animation cannot be started", gameObject);
                return;
            }
            foreach (AnimationClip anim in animator.runtimeAnimatorController.animationClips)
            {
                if (anim.name != nameAcnimationclip)
                {
                    continue;
                }
                AnimationEvent animationStartEvent = new AnimationEvent()
                {
                    time = 0,
                    functionName = "RunAnimationVoid",
                };

                AnimationEvent animationEndEvent = new AnimationEvent()
                {
                    time = timeEnd,
                    functionName = "EndAnimationVoid"
                };

                anim.AddEvent(animationStartEvent);
                anim.AddEvent(animationEndEvent);
                break;
            }
        }

        private bool CheckAnimation()
        {
            if (string.IsNullOrEmpty(nameAcnimationclip))
            {
                return false;
            }

            foreach (AnimationClip anim in animator.runtimeAnimatorController.animationClips)
            {
                if (anim.name == nameAcnimationclip)
                {
                    return true;
                }
            }

            return false;
        }

        [Server]
        public void RunAnimationVoid()
        {
            RunAnimation.Invoke();
        }

        [Server]
        public void EndAnimationVoid()
        {
            Animation = false;
            animator.SetBool(nameAcnimationclip, Animation);
            EndAnimation.Invoke();
        }

        [Server]
        public void Play()
        {
            Animation = true;
            animator.SetBool(nameAcnimationclip, Animation);
            animator.Play(nameAcnimationclip);
        }
    }
}
