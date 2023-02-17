using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Server
{
    [RequireComponent(typeof(SphereCollider))]
    public class PointToSpawn : MonoBehaviour
    {
        [field: SerializeField] public bool Free { get; private set; } = true;
        private List<Collider> collidersInside = new List<Collider>();
        private SphereCollider collider;

        private void Start()
        {
            collider = GetComponent<SphereCollider>();
        }
        
        public float GetMinimalDistance()
        {
            float result = collider.radius;
            float distance;
            foreach (var collider in collidersInside)
            {
                distance = (transform.position - collider.transform.position).magnitude;
                if (distance < result)
                {
                    result = distance;
                }
            }

            return result;
        }
        
        public void MoveToPoint(Transform player)
        {
            player.transform.position = transform.position;
            player.transform.rotation = transform.rotation;
        }

        private void OnTriggerEnter(Collider other)
        {
            TriggerEnter(other);
        }

        private void OnTriggerStay(Collider other)
        {
            TriggerEnter(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!collidersInside.Contains(other))
            {
                return;
            }

            collidersInside.Remove(other);
            if (collidersInside.Count == 0)
            {
                Free = true;
            }
        }

        private void TriggerEnter(Collider other)
        {
            if (collidersInside.Contains(other))
            {
                return;
            }

            if (collidersInside.Count == 0)
            {
                Free = false;
            }
            collidersInside.Add(other);
        }
        
    }
}