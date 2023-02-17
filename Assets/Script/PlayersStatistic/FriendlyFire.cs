using UnityEngine;

namespace Script.PlayersStatistic
{
    public class FriendlyFire : MonoBehaviour, ITakeDamage
    {
        [SerializeField] private float partDamageMultiplier =0.5f;
        private IHealth health;
        private void Start()
        {
            health = GetComponentInParent<IHealth>();
        }

        public void TakeDamage(float damage)
        {
            health.TakeDamage(damage * partDamageMultiplier);
        }
    }
}