using Mirror;
using System.Collections.Generic;
using UnityEngine;

namespace Script.PlayersStatistic
{
    public class PlayerOnGround : MonoBehaviour
    {
        [SerializeField] private PlayerStatus status;
        private List<Collider> colliderInside = new List<Collider>();

        [Server]
        private void Start()
        {
            Collider thisCollider = GetComponent<Collider>();
            foreach (Collider collider in status.GetComponentsInChildren<Collider>())
            {
                if (collider != thisCollider)
                {
                    Physics.IgnoreCollision(collider, thisCollider);
                }
            }
        }

        [Server]
        private void OnTriggerEnter(Collider other)
        {
            TriggerEnter(other);
        }

        [Server]
        private void OnTriggerStay(Collider other)
        {
            if (colliderInside.Contains(other))
            {
                return;
            }

            TriggerEnter(other);
        }

        [Server]
        private void OnTriggerExit(Collider other)
        {
            TriggerExit(other);
        }

        [Server]
        private void TriggerEnter(Collider other)
        {
            Debug.Log(other.name);
            if (colliderInside.Count == 0)
            {
                status.OnGround = true;
            }
            if (!colliderInside.Contains(other))
            {
                colliderInside.Add(other);
            }
        }

        [Server]
        private void TriggerExit(Collider other)
        {
            if (colliderInside.Contains(other))
            {
                _ = colliderInside.Remove(other);
            }

            if (colliderInside.Count == 0)
            {
                status.OnGround = false;
            }
        }
    }
}