using UnityEngine.UI;
using UnityEngine;

namespace Enemy.Health
{
    public class HealthController : MonoBehaviour
    {
        [Header("Health Settings")]
        [SerializeField] private Image healthbar;
        [SerializeField] private float fullHealth = 100.0f;
        private float currentHealth;
        [SerializeField] private float decreaseHealthAmount = 100;
        [SerializeField] private bool[] currentWeapon;

        [Header("Score Settings")]
        public Points score;


        void Start()
        {
            currentHealth = fullHealth;  // initialize current health
            UpdateHealthBar();
            if (currentWeapon != null && currentWeapon.Length > 0)
            {
                currentWeapon[0] = true;
            }

            score = FindObjectOfType<Points>();

        }

        void OnTriggerEnter(Collider collider)
        {
            if (collider.transform.gameObject.tag == "bullet")
            {
                DecreaseHealth(decreaseHealthAmount);
            }
        }

        private void DecreaseHealth(float amount)
        {
            currentHealth -= amount;
            score.AddPoints(50);
            score.UpdatePointsText();
            
            // Add points when health decreases, e.g., 50 points (or whatever you desire)
            
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
            if (currentWeapon != null && currentWeapon.Length > 0 && currentWeapon[0])
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
