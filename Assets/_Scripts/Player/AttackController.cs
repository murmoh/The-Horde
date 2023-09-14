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
        [SerializeField] private int maxMagazineSize = 30;
        [SerializeField] private float reloadTime = 2f;
        private float lastShotTime;
        private int currentMagazineSize;
        private bool isReloading;

        void Start()
        {
            currentMagazineSize = maxMagazineSize;
        }

        void Update()
        {
            if (Input.GetButton("Fire1"))
            {
                PerformGunAttack();
            }

            // The reloading condition should be outside of the attack button check.
            if (currentMagazineSize == 0 && !isReloading)
            {
                StartCoroutine(Reload());
            }
        }

        void PerformGunAttack()
        {
            if (currentMagazineSize > 0)
            {
                lastShotTime = Time.time * shootingRate;
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
                bulletRigidbody.velocity = bulletSpawnPoint.forward * bulletSpeed;
                currentMagazineSize--;
            }
        }

        IEnumerator Reload()
        {
            isReloading = true;
            yield return new WaitForSeconds(reloadTime);
            currentMagazineSize = maxMagazineSize;
            isReloading = false;
        }
    }
}
