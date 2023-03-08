using Assets.Script.Interfaces;
using Script.Interfaces;
using Script.PlayersStatistic;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Script.DamageAbility
{
    public class DamageAbility : MonoBehaviour, IDamageDealer
    {
        [SerializeField] private List<DamageDebuff> damageDebuffs = new List<DamageDebuff>();

        public float Damage { get; set; }
        public UnityEvent OnDestroyThis;


        private void OnTriggerEnter(Collider other)
        {
            ITakeDamage[] takeDamages = other.GetComponents<ITakeDamage>();
            if(other.tag == gameObject.tag)
            {
                foreach(ITakeDamage damage in takeDamages)
                {
                    if(damage is FriendlyFire)
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


        private void DestroyThis()
        {
            OnDestroyThis?.Invoke();
            Destroy(this);
        }
             

        private void OnCollisionEnter(Collision collision)
        {
            OnTriggerEnter(collision.collider);
        }
    }
}
