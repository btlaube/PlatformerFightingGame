using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Abstract class for all information related to weapons the players use.
public abstract class WeaponBase : MonoBehaviour
{
    public GameObject projectilePrefab; // The prefab of the projectile to be shot
    [SerializeField] protected float projectileDamage;
    public Transform firePoint; // The position where the projectile will be instantiated
    public float projectileForce = 20f; // The force at which the projectile will be shot
    [SerializeField] private Cooldown cooldown;
    [SerializeField] bool canHoldToFire;
    [SerializeField] bool useAmmo;
    [SerializeField] int maxAmmo;
    public int ammoRemaining;

    // Start is called before the first frame update
    void Start()
    {
        ammoRemaining = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        Aim(); // Call the Aim method to rotate the gun towards the mouse cursor

        //Checks if the left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            if (!canFire()) return;
            Attack(); // Call the attack
            if (useAmmo) ammoRemaining--;

            cooldown.StartCooldown();
        }

        
        // Check if the left mouse button is held. If it is held, continue firing
        if (Input.GetMouseButton(0) && canHoldToFire)
        {
            if (!canFire()) return;
            Attack(); // Call the attack
            if (useAmmo) ammoRemaining--;

            cooldown.StartCooldown(); //Start cooldown

        }
    }

    bool canFire()
    {
        if (cooldown.IsCoolingDown) return false;
        if (useAmmo)
        {
            if (ammoRemaining <= 0) return false;
        }
        return true;

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
        float newAngle = angle;
        if (90.0f < angle && angle < 180.0f)
        {
            transform.localScale = new Vector3(transform.parent.parent.localScale.x, -1.0f, 1.0f);
        }
        else if (-180.0 < angle && angle < -90.0f)
        {
            transform.localScale = new Vector3(transform.parent.parent.localScale.x, -1.0f, 1.0f);
        }
        else
        {
            transform.localScale = new Vector3(transform.parent.parent.localScale.x, 1.0f, 1.0f);
        }
        transform.rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
    }

    public abstract void Attack();

}
