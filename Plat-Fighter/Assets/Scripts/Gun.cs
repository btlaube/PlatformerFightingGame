using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject projectilePrefab; // The prefab of the projectile to be shot
    public Transform firePoint; // The position where the projectile will be instantiated
    public float projectileForce = 20f; // The force at which the projectile will be shot

    void Update()
    {
        Aim(); // Call the Aim method to rotate the gun towards the mouse cursor

        // Check if the left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            Fire(); // Call the fire method
        }
    }

    void Aim()
    {
        // Get the position of the mouse cursor in the world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Calculate the direction from the gun's position to the mouse cursor position
        Vector2 direction = (mousePosition - transform.parent.position).normalized; // Adjusted position
        // Calculate the angle in radians
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Rotate the gun towards the mouse cursor
        Debug.Log(angle);
        float newAngle = angle;
        if (90.0f < angle && angle < 180.0f)
        {
            transform.localScale = new Vector3(transform.parent.parent.localScale.x, -1.0f , 1.0f);
        }
        else if (-180.0 < angle && angle < -90.0f)
        {
            transform.localScale = new Vector3(transform.parent.parent.localScale.x, -1.0f , 1.0f);
        }
        else
        {
            transform.localScale = new Vector3(transform.parent.parent.localScale.x, 1.0f , 1.0f);
        }
        transform.rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
    }


    void Fire()
    {

        GetComponent<AudioManager>().Play("Fire");

        // Instantiate the projectile at the fire point position and rotation
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Get the Rigidbody2D component of the projectile
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

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
