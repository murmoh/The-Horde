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
        [SerializeField] private float bulletLifespan = 2f;
        private float lastShotTime;
        private int currentMagazineSize;
        private bool isReloading;

        private Camera cam; // Store the reference to the camera once instead of finding it every frame

        void Start()
        {
            currentMagazineSize = maxMagazineSize;
            lastShotTime = -shootingRate; // Initialize to allow an immediate first shot
            cam = Camera.main; // Cache the reference to the main camera
        }

        void Update()
        {
            if (Input.GetButton("Fire1") && Time.time - lastShotTime >= shootingRate)
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
                lastShotTime = Time.time;

                Ray ray = cam.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 hitPoint = hit.point;
                    Vector3 spawnPosition = bulletSpawnPoint.position;

                    GameObject bullet = Instantiate(bulletPrefab, spawnPosition, bulletSpawnPoint.rotation);
                    Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
                    Vector3 bulletDirection = (hitPoint - spawnPosition).normalized; // Calculate direction to hit point
                    bulletRigidbody.velocity = bulletDirection * bulletSpeed;
                    StartCoroutine(DestroyBulletAfterLifespan(bullet));
                    currentMagazineSize--;
                }
            }
        }

        IEnumerator DestroyBulletAfterLifespan(GameObject bullet)
        {
            yield return new WaitForSeconds(bulletLifespan);
            Destroy(bullet);
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
