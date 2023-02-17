using UnityEngine;
using UnityEngine.Events;

namespace Script.PlayersStatistic
{
    public class RespawnObserver : MonoBehaviour, IRespawn, IHealthObserver
    {
        [field: SerializeField] public UnityEvent<GameObject> Respawn { get; private set; } = new UnityEvent<GameObject>();


        public void ChangeHealth(float currentHealth)
        {
            if (currentHealth <= 0)
            {
                Respawn?.Invoke(gameObject);
            }
        }
    }

    public interface IRespawn
    {
        UnityEvent<GameObject> Respawn { get; }
    }
}