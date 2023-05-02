using System.Collections;
using UnityEngine;
using UnityTutorial.PlayerControl;

namespace Enemy.Attack
{
    public class AttackController : MonoBehaviour
    {
        [Header("Melee Attack Settings")]
        [SerializeField] private PlayerController controller;
        [SerializeField] public GameObject fist;
        [SerializeField] private float attackDuration = 0.69f;

        [Header("Gun Attack Settings")]
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private float shootingRate = 0.1f;
        [SerializeField] private float bulletSpeed = 50f;
        [SerializeField] private int maxMagazineSize = 30; // New variable for max magazine size
        [SerializeField] private float reloadTime = 2f; // New variable for reload time
        private float lastShotTime;
        private int currentMagazineSize; // New variable for current magazine size
        private bool isReloading; // New variable to check if the character is currently reloading

        void Start()
        {
            currentMagazineSize = maxMagazineSize;
        }

        void Update()
        {
            if (controller._inputManager.Melee)
            {
                StartCoroutine(PerformMeleeAttack());
            }
            if (controller._inputManager.Attack && Time.time > lastShotTime + 1 / shootingRate && !isReloading)
            {
                PerformGunAttack();
            }
            if (controller._inputManager.Reload && !isReloading) // New condition to start reloading
            {
                StartCoroutine(Reload());
            }
        }

        IEnumerator PerformMeleeAttack()
        {
            fist.SetActive(true);
            yield return new WaitForSeconds(attackDuration);
            fist.SetActive(false);
        }

        void PerformGunAttack()
        {
            if (currentMagazineSize > 0)
            {
                lastShotTime = Time.time;
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
                bulletRigidbody.velocity = bulletSpawnPoint.forward * bulletSpeed;
                currentMagazineSize--; // Decrease the magazine size by 1 after shooting
            }
            else
            {
                StartCoroutine(Reload());
            }
        }

        IEnumerator Reload() // New coroutine to handle reloading
        {
            isReloading = true;
            yield return new WaitForSeconds(reloadTime);
            currentMagazineSize = maxMagazineSize;
            isReloading = false;
        }
    }
}
