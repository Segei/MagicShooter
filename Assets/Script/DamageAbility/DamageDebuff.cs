using Mirror;
using Script.Interfaces;
using System.Collections;
using UnityEngine;

namespace Assets.Script.DamageAbility
{
    public class DamageDebuff : MonoBehaviour
    {
        IHealth health;
        [SerializeField] private float damagePerSecond;
        [SerializeField] private int secondDamage;

        [Server]
        private void Start()
        {
            health = gameObject.GetComponentInParent<IHealth>();
            StartCoroutine(DamageEverySecond());
        }

        [Server]
        public IEnumerator DamageEverySecond()
        {
            for (int i = 0; i < secondDamage; i++)
            {
                yield return new WaitForSecondsRealtime(1f);
                health.TakeDamage(damagePerSecond);
            }
            yield return null;
        }
    }
}
