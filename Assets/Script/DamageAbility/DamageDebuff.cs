using Mirror;
using Script.Interfaces;
using System.Collections;
using UnityEngine;

namespace Assets.Script.DamageAbility
{
    public class DamageDebuff : MonoBehaviour
    {
        private IHealth health;
        private Coroutine coroutine;
        [SerializeField] private float damagePerSecond;
        [SerializeField] private int secondDamage;


        [Server]
        private void Start()
        {
            Debug.Log("StartDebuff");
            health = gameObject.GetComponentInParent<IHealth>();
            coroutine = StartCoroutine(DamageEverySecond());
        }

        [Server]
        public IEnumerator DamageEverySecond()
        {
            for (int i = 0; i < secondDamage; i++)
            {
                yield return new WaitForSecondsRealtime(1f);
                health.TakeDamage(damagePerSecond);
            }
            StopDebuff();
            yield return null;
        }

        [Server]
        public void StopDebuff()
        {
            Debug.Log("Stop debuff.");
            StopCoroutine(coroutine);
            coroutine = null;
            Destroy(gameObject);
        }
    }
}
