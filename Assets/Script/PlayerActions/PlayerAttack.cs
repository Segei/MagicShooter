using Assets.Script.DamageAbility;
using Assets.Script.Interfaces;
using Mirror;
using Script.Interfaces;
using Script.PlayersStatistic;
using Script.Tools;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.PlayerActions
{
    internal class PlayerAttack : MonoBehaviour, IAttack
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private GameObject prefabAttackAbility;
        [SerializeField] private float force;
        [SerializeField] private EventForAnimations animations;


        [ServerCallback]
        private void Start()
        {
            animations.EndAnimation.AddListener(SpawnFireBall); 
        }

        [ServerCallback]
        public void Attack()
        {
            if (animations.Animation)
            {
                return;
            }

            if (prefabAttackAbility == null || spawnPoint == null)
            {
                Debug.LogError("This player not ready Attack.", gameObject);
            }

            animations.Play();
        }

        [ServerCallback]
        public void SpawnFireBall()
        {
            GameObject instanceAttackObject = Instantiate(prefabAttackAbility);
            instanceAttackObject.transform.position = spawnPoint.position;
            instanceAttackObject.transform.rotation = spawnPoint.rotation;
            NetworkServer.Spawn(instanceAttackObject);
            instanceAttackObject.GetComponent<Rigidbody>().AddForce(instanceAttackObject.transform.forward * force);
        }
    }
}
