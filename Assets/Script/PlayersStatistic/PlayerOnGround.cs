using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script.PlayersStatistic
{
    public class PlayerOnGround : MonoBehaviour
    {
        [SerializeField] private PlayerStatus status;
        private List<Collider> colliderInside = new List<Collider>();
        private void Start()
        {
            Collider thisCollider = GetComponent<Collider>();
            foreach (var collider in status.GetComponentsInChildren<Collider>())
            {
                if (collider != thisCollider)
                {
                    Physics.IgnoreCollision(collider, thisCollider);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            TriggerEnter(other);
        }

        private void OnTriggerStay(Collider other)
        {
            if (colliderInside.Contains(other))
            {
                return;
            }

            TriggerEnter(other);
        }

        private void OnTriggerExit(Collider other)
        {
            TriggerExit(other);
        }

        private void TriggerEnter(Collider other)
        {
            if (colliderInside.Count == 0)
            {
                status.OnGround = true;
            }
            if (!colliderInside.Contains(other))
            {
                colliderInside.Add(other);
            }
        }

        private void TriggerExit(Collider other)
        {
            if (colliderInside.Contains(other))
            {
                colliderInside.Remove(other);
            }

            if (colliderInside.Count == 0)
            {
                status.OnGround = false;
            }
        }
    }
}