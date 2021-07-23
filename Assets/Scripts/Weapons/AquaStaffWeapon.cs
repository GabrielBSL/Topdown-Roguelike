using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquaStaffWeapon : Weapon
{
    protected override bool Shoot()
    {
        if (!base.Shoot()) return false;

        GameObject projectile = projectilePool.Dequeue();

        projectile.transform.position = spawnPoint.position;
        projectile.transform.rotation = transform.rotation;

        projectile.SetActive(true);
        projectile.GetComponent<Projectile>().Redirect();
        projectilePool.Enqueue(projectile);

        return true;
    }
}
