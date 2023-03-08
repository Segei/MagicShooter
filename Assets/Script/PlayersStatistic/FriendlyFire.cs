using Mirror;
using Script.Interfaces;
using UnityEngine;

namespace Script.PlayersStatistic
{
    public class FriendlyFire : MonoBehaviour, ITakeDamage
    {
        [SerializeField] private float partDamageMultiplier =0.5f;
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
    }
}