using Mirror;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Client
{
    public class ClientOnly : NetworkBehaviour
    {
        [SerializeField] private List<GameObject> targetsEnable;
        [SerializeField] private List<GameObject> targetsDisable;
        [SerializeField] private List<Rigidbody> bodies;

        private void OnValidate()
        {
            foreach (GameObject target in targetsEnable)
            {
                target.SetActive(false);
            }

            foreach (Rigidbody body in bodies)
            {
                body.isKinematic = false;
            }
            foreach (GameObject target in targetsDisable)
            {
                target.SetActive(true);
            }
        }

        public void Start()
        {
            if (!isLocalPlayer)
            {
                return;
            }

            foreach (GameObject target in targetsEnable)
            {
                target.SetActive(true);
            }
            foreach (GameObject target in targetsDisable)
            {
                target.SetActive(false);
            }
            foreach (Rigidbody body in bodies)
            {
                body.isKinematic = true;
            }
        }
    }
}