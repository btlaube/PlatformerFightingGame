using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwables : WeaponBase
{

    public override void Attack()
    {
        GetComponent<AudioManager>().Play("Fire");

        // Instantiate the projectile at the fire point position and rotation
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Get the Rigidbody2D component of the projectile
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        ThrowableProjectile projectileScript = projectile.GetComponent<ThrowableProjectile>();
        if (projectileScript != null)
        {
            projectileScript.SetProjectileDamage(projectileDamage);
            projectileScript.SetHomeWeapon(gameObject);
        }

        // Check if the Rigidbody2D component exists
        if (rb != null)
        {
            // Apply force to the projectile in the direction of the firePoint's right vector
            rb.AddForce(firePoint.right * projectileForce, ForceMode2D.Impulse);
        }
        else
        {
            Debug.LogError("Projectile prefab does not have a Rigidbody2D component.");
        }
    }
}
