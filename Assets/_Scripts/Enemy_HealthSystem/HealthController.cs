using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace Enemy.Health
{
    public class HealthController : MonoBehaviour
    {
        [Header("Health Settings")]
        [SerializeField] public Image healthbar;
        [SerializeField] private const float fullHealth = 100.0f;
        [SerializeField] private float currentHealth = fullHealth;
        [SerializeField] public float decreaseHealthAmount = 100;
        [SerializeField] private bool[] currentWeapon;

        void Start()
        {
            UpdateHealthBar();
            currentWeapon[0] = true;
        }

        void OnTriggerEnter(Collider collider)
        {
            if (collider.transform.gameObject.tag == "bullet")
            {
                SetDecreaseHealthAmount();
                DecreaseHealth(decreaseHealthAmount);
            }
        }

        private void DecreaseHealth(float amount)
        {
            currentHealth -= amount;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Destroy(gameObject);
            }
            UpdateHealthBar();
        }

        private void UpdateHealthBar()
        {
            healthbar.fillAmount = currentHealth / fullHealth;
        }

        void SetDecreaseHealthAmount()
        {
            if (currentWeapon[0])
            {
                decreaseHealthAmount = 50;
            }
            else
            {
                decreaseHealthAmount = 0;
            }
        }
    }
}