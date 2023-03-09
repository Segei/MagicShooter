using Assets.Script.Interfaces;
using Mirror;
using Script.Interfaces;
using Script.PlayersStatistic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Script.DamageAbility
{
    public class DamageAbility : MonoBehaviour, IDamageDealer
    {
        [SerializeField] private List<DamageDebuff> damageDebuffs = new List<DamageDebuff>();
        [SerializeField] private float waitTimeToDestroy;
        [field: SerializeField] public float Damage { get; set; }
        public UnityEvent OnDestroyThis;


        [Server]
        private void Start()
        {
            if (waitTimeToDestroy != 0)
            {
                _ = StartCoroutine(DestroyWaitTime());
            }
        }

        [Server]
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.name, other.gameObject);
            ITakeDamage[] takeDamages = other.GetComponents<ITakeDamage>();

            if(takeDamages.Length == 0)
            {
                DestroyThis();
                return;
            }

            if (other.tag == gameObject.tag)
            {
                foreach (ITakeDamage damage in takeDamages)
                {
                    if (damage is FriendlyFire)
                    {
                        damage.TakeDamage(Damage);
                        DestroyThis();
                        return;
                    }
                }
                DestroyThis();
                return;
            }
            else
            {
                foreach (ITakeDamage damage in takeDamages)
                {
                    if (damage is DamageObserver)
                    {
                        damage.TakeDamage(Damage);
                        break;
                    }
                }
            }

            foreach (DamageDebuff damage in damageDebuffs)
            {
                _ = Instantiate(damage, other.transform);
            }

            DestroyThis();
        }

        [Server]
        private void DestroyThis()
        {
            OnDestroyThis?.Invoke();
            Destroy(this);
        }

        [Server]
        private void OnCollisionEnter(Collision collision)
        {
            OnTriggerEnter(collision.collider);
        }

        [Server]
        private IEnumerator DestroyWaitTime()
        {
            yield return new WaitForSecondsRealtime(waitTimeToDestroy);
            DestroyThis();
            yield return null;
        }
    }
}
