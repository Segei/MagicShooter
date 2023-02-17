using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace Script.Network
{
    public class ClientOnly : NetworkBehaviour
    {
        [SerializeField] private List<GameObject> targetsEnable;
        [SerializeField] private List<GameObject> targetsDisable;
        [SerializeField] private List<Rigidbody> bodies;

        private void OnValidate()
        {
            foreach (var target in targetsEnable)
            {
                target.SetActive(false);
            }

            foreach (var body in bodies)
            {
                body.isKinematic = true;
            }
            foreach (var target in targetsDisable)
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

            foreach (var target in targetsEnable)
            {
                target.SetActive(true);
            }
            foreach (var target in targetsDisable)
            {
                target.SetActive(false);
            }
            foreach (var body in bodies)
            {
                body.isKinematic = false;
            }
        }
    }
}