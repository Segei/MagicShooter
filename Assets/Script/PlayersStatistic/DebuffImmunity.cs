using Assets.Script.DamageAbility;
using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.PlayersStatistic
{
    public class DebuffImmunity : MonoBehaviour
    {
        private float immunityTime = 1f;

        [Server]
        private void Start()
        {
            StartCoroutine(WaitTimeToDestroy());
        }

        [Server]
        private void FixedUpdate()
        {
            foreach (var debuff in GetComponentsInChildren<DamageDebuff>())
            {
                debuff.StopDebuff();
            }
        }

        [Server]
        private IEnumerator WaitTimeToDestroy()
        {
            yield return new WaitForSeconds(immunityTime);
            Destroy(this);
            yield return null;
        }

    }
}
