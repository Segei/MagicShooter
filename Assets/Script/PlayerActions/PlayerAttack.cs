using Assets.Script.Interfaces;
using Mirror;
using Script.Tools;
using UnityEngine;

namespace Assets.Script.PlayerActions
{
    internal class PlayerAttack : MonoBehaviour, IAttack
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private GameObject prefabAttackAbility;
        [SerializeField] private float force;
        [SerializeField] private EventForAnimations animations;
        [SerializeField] private float secondDelay = 3f;
        private float waitTime = 0;

        

        [ServerCallback]
        public void Attack()
        {
            if (waitTime > 0)
            {
                return;
            }

            waitTime = secondDelay;
            if (prefabAttackAbility == null || spawnPoint == null)
            {
                Debug.LogError("This player not ready Attack.", gameObject);
            }
            animations.Play();
            SpawnFireBall();
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


        [SerializeField]
        private void Update()
        {
            if (waitTime <= 0)
            {
                return;
            }

            waitTime -= Time.unscaledDeltaTime;
        }
    }
}
