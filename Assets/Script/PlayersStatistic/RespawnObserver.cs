using Mirror;
using Script.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Script.PlayersStatistic
{
    public class RespawnObserver : MonoBehaviour, IRespawn, IHealthObserver
    {
        [field: SerializeField] public UnityEvent<GameObject> Respawn { get; private set; } = new UnityEvent<GameObject>();

        [Server]
        public void ChangeHealth(float currentHealth, float maxHealth)
        {
            Debug.Log("To Respawn " + currentHealth);
            if (currentHealth <= 0)
            {
                Respawn?.Invoke(gameObject);
            }
        }
    }
}