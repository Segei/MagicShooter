using Mirror;
using NaughtyAttributes;
using Script.Interfaces;
using UnityEngine;

namespace Script.PlayersStatistic
{
    public class DamageObserver : MonoBehaviour, ITakeDamage
    {
        [SerializeField] private float partDamageMultiplier = 1f;
        private IHealth health;

        [Server]
        private void Start()
        {
            health = GetComponentInParent<IHealth>();
        }

        [Server]
        public void TakeDamage(float damage)
        {
            health.TakeDamage(damage * partDamageMultiplier);
        }

        [Button]
        [ContextMenu("Test")]
        [Server]
        private void Test()
        {
            TakeDamage(10);
        }
    }
}