using Mirror;
using Script.Interfaces;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Script.PlayersStatistic
{
    public class PlayerHealth : NetworkBehaviour, IHealth
    {
        [field: SerializeField, SyncVar(hook = nameof(SyncHealth))]
        public float Health { get; private set; }

        [SerializeField] private float maxHealth;
        public UnityEvent<float, float> OnChangeHealth;


        private void Awake()
        {
            if (isLocalPlayer)
            {
                return;
            }

            Debug.Log("PlayerHealth Awake");

            foreach (IHealthObserver observer in gameObject.GetComponentsInChildren<IHealthObserver>())
            {
                OnChangeHealth.AddListener(observer.ChangeHealth);
            }
        }

        [Client]
        private void Start()
        {
            if (isLocalPlayer)
            {
                UpdateHealth();
            }
        }

        [Server]
        public void SetHealth(float value)
        {
            Health = value;
            OnChangeHealth.Invoke(Health, maxHealth);
        }

        [Command]
        private void UpdateHealth()
        {
            SetHealth(maxHealth);
        }

        [Server]
        void IHealth.UpdateHealth()
        {
            SetHealth(maxHealth);
        }

        [Server]
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
            OnChangeHealth.Invoke(Health, maxHealth);
        }

        [Server]
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
            OnChangeHealth.Invoke(Health, maxHealth);
        }

        [Client]
        private void SyncHealth(float _, float newValue)
        {
            OnChangeHealth.Invoke(newValue, maxHealth);
        }

        
    }
}