using Assets.Script.DamageAbility;
using Assets.Script.Interfaces;
using Mirror;
using Script.Interfaces;
using Script.PlayersStatistic;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.PlayerActions
{
    internal class PlayerAttack : MonoBehaviour, IAttack
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private GameObject prefabAttackAbility;
        [SerializeField] private float force;


        [ServerCallback]
        public void Attack()
        {
            if (prefabAttackAbility == null || spawnPoint == null)
            {
                Debug.LogError("This player not ready Attack.", gameObject);
            }
            GameObject instanceAttackObject = Instantiate(prefabAttackAbility);
            instanceAttackObject.transform.position = spawnPoint.position;
            instanceAttackObject.transform.rotation = spawnPoint.rotation;
            NetworkServer.Spawn(instanceAttackObject);            
            instanceAttackObject.GetComponent<Rigidbody>().AddForce(instanceAttackObject.transform.forward * force);
        }
    }
}
