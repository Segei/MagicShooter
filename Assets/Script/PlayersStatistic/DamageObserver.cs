using System;
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
        private void Start()
        {
            health = GetComponentInParent<IHealth>();
        }

        
        public void TakeDamage(float damage)
        {
            health.TakeDamage(damage * partDamageMultiplier);
        }
        
        [Button][ContextMenu("Test")]
        private void Test()
        {
            TakeDamage(10);
        }
    }
}