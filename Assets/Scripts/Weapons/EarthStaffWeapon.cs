using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthStaffWeapon : Weapon
{
    [SerializeField] private int projectilesPerBlast;
    [SerializeField] private float maxDispertionAngle;

    protected override bool Shoot()
    {
        if (!base.Shoot()) return false;
        if (projectilesPerBlast <= 1) return false;

        for (int i = 0; i < projectilesPerBlast; i++)
        {
            float angleCorrection = Mathf.Lerp(-maxDispertionAngle, 
                maxDispertionAngle, 
                (float)i / (projectilesPerBlast - 1));

            GameObject projectile = projectilePool.Dequeue();
            projectile.transform.position = spawnPoint.position;

            projectile.transform.eulerAngles = transform.eulerAngles
                + new Vector3(0, 0, angleCorrection);

            projectile.SetActive(true);
            projectile.GetComponent<Projectile>().Redirect();
            projectilePool.Enqueue(projectile);
        }
        return true;
    }
}
