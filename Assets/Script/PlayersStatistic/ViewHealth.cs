using Mirror;
using TMPro;
using UnityEngine;

namespace Script.PlayersStatistic
{
    public class ViewHealth : MonoBehaviour, IHealthObserver
    {
        [SerializeField] private TMP_Text healthView;
        
        public void ChangeHealth(float currentHealth)
        {
            healthView.text = currentHealth.ToString();
        }
    }
}