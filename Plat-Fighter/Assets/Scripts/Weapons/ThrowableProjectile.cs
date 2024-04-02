using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableProjectile : Projectile
{
    private GameObject homeWeapon;





    public void SetHomeWeapon(GameObject home)
    {
        this.homeWeapon = home;
    }
}
