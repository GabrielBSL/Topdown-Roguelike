using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStaffWeapon : Weapon
{
    [SerializeField] private float maxDispertionAngle;

    protected override bool Shoot()
    {
        if (!base.Shoot()) return false;

        float angleCorrection = Random.Range(-maxDispertionAngle, maxDispertionAngle);
        GameObject projectile = projectilePool.Dequeue();

        projectile.transform.position = spawnPoint.position; 
        projectile.transform.eulerAngles = transform.eulerAngles
                 + new Vector3(0, 0, angleCorrection);

        projectile.SetActive(true);
        projectile.GetComponent<Projectile>().Redirect();
        projectilePool.Enqueue(projectile);

        return true;
    }
}
