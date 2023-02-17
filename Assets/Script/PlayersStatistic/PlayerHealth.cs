using System;
using Mirror;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace Script.PlayersStatistic
{
    public class PlayerHealth : NetworkBehaviour, IHealth
    {
        [field: SerializeField, SyncVar(hook = nameof(SyncHealth))]
        public float Health { get; private set; }

        [SerializeField] private float maxHealth;
        public UnityEvent<float> OnChangeHealth;

        private void Awake()
        {
            if (isLocalPlayer)
            {
                return;
            }

            foreach (var observer in gameObject.GetComponentsInChildren<IHealthObserver>())
            {
                OnChangeHealth.RemoveListener(observer.ChangeHealth);
                OnChangeHealth.AddListener(observer.ChangeHealth);
            }
        }

        private void Start()
        {
            if (isLocalPlayer)
                SetHealth(maxHealth);
        }

        [Command]
        private void SetHealth(float value)
        {
            Health = value;
        }

        [Command]
        public void AddHealth(float health)
        {
            if (health <= 0)
            {
                throw new ArgumentException("Can't add a negative amount of health");
            }

            Health += health;
            if (Health > maxHealth)
            {
                Health = maxHealth;
            }
        }

        [Command]
        public void TakeDamage(float damage)
        {
            if (damage <= 0)
            {
                throw new ArgumentException("Can't deal negative damage");
            }

            Health -= damage;
            if (Health <= 0)
            {
                Health = 0;
            }
        }

        private void SyncHealth(float _, float newValue)
        {
            OnChangeHealth.Invoke(newValue);
        }
    }

    public interface IHealth
    {
        void AddHealth(float health);
        void TakeDamage(float damage);
    }

    public interface IHealthObserver
    {
        void ChangeHealth(float currentHealth);
    }
}