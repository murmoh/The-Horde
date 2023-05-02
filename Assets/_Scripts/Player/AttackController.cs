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
        private float lastShotTime;

        void Update()
        {
            if (controller._inputManager.Melee)
            {
                StartCoroutine(PerformMeleeAttack());
            }
            if (controller._inputManager.Attack && Time.time > lastShotTime + 1 / shootingRate)
            {
                PerformGunAttack();
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
            lastShotTime = Time.time;
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            bulletRigidbody.velocity = bulletSpawnPoint.forward * bulletSpeed;
        }
    }
}
