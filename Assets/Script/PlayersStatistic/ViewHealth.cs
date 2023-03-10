using Script.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.PlayersStatistic
{
    public class ViewHealth : MonoBehaviour, IHealthObserver
    {
        [SerializeField] private TMP_Text healthView;
        [SerializeField] private Image image;
        [SerializeField] private Gradient gradient;
        [SerializeField] private bool viewMaxHealth;
        public void ChangeHealth(float currentHealth, float maxHealth)
        {
            float result = currentHealth / maxHealth;
            result = Mathf.Clamp01(result);

            if (healthView != null)
            {
                healthView.text = viewMaxHealth? currentHealth+" / "+maxHealth : currentHealth.ToString();
            }

            if (image != null)
            {
                image.fillAmount = result;
                image.color = gradient.Evaluate(result);
            }
        }
    }
}