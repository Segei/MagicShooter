using Assets.Script.DamageAbility;
using Assets.Script.PlayersStatistic;
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
                Debug.Log("Respawn");
                Respawn?.Invoke(gameObject); 
                gameObject.AddComponent<DebuffImmunity>();
            }
        }
    }
}