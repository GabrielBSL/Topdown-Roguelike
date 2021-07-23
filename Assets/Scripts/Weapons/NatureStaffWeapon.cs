using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NatureStaffWeapon : Weapon
{
    [SerializeField] [Range(0f, 1f)] private float shootChance;

    protected override bool Shoot()
    {
        if (!base.Shoot()) return false;
        if (shootChance - Random.value >= 0) return false;

        GameObject projectile = projectilePool.Dequeue();

        projectile.transform.position = spawnPoint.position;
        projectile.transform.rotation = transform.rotation;

        projectile.SetActive(true);
        projectile.GetComponent<Projectile>().Redirect();
        projectilePool.Enqueue(projectile);

        return true;
    }
}
