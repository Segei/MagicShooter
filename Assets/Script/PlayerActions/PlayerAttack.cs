using Assets.Script.Interfaces;
using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.PlayerActions
{
    internal class PlayerAttack : MonoBehaviour, IAttack
    {
        [SerializeField] private RectTransform spawnPoint;
        [SerializeField] private GameObject prefabAttackAbility;


        [ServerCallback]
        public void Attack()
        {
            Debug.Log("Attack");
        }
    }
}
