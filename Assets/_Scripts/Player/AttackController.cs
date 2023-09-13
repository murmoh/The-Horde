using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enemy.Attack
{
    public class AttackController : MonoBehaviour
    {
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
            if (Input.GetButtonDown("Fire1") && Time.time > lastShotTime + 1 / shootingRate && !isReloading)
            {
                PerformGunAttack();
            }
            if (!isReloading) // New condition to start reloading
            {
                StartCoroutine(Reload());
            }
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
