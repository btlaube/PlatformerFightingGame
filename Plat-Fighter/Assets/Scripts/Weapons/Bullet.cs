using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{

    public void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}
