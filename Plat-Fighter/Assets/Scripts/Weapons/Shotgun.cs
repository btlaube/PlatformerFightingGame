using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : WeaponBase
{
    [SerializeField] private int bulletsToFire;
    [SerializeField] private float spreadAngle;

    public override void Attack()
    {
        GetComponent<AudioManager>().Play("Fire");

        for (int bulletCount = 0; bulletCount < bulletsToFire; ++bulletCount)
        {
            // Instantiate the projectile at the fire point position and rotation
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            // Generate a random spread angle between -spreadAngle and spreadAngle
            float randomSpread = Random.Range(-spreadAngle, spreadAngle);
            // Apply the random spread to the projectile's rotation
            projectile.transform.Rotate(0, 0, randomSpread);

            // Get the Rigidbody2D component of the projectile
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

            Projectile projectileScript = projectile.GetComponent<Projectile>();
            if (projectileScript != null)
            {
                projectileScript.SetProjectileDamage(projectileDamage);
            }

            // Check if the Rigidbody2D component exists
            if (rb != null)
            {
                // Apply force to the projectile in its forward direction (taking the spread into account)
                rb.AddForce(projectile.transform.right * projectileForce, ForceMode2D.Impulse);
            }
            else
            {
                Debug.LogError("Projectile prefab does not have a Rigidbody2D component.");
            }
        }
    }
}
