using UnityEngine;
using System.Collections;

public class BasicBullet : PathFireBullet
{  
    [Header("Shooting Settings")]
    public float timeBetweenWaves = 5f; // Time between each wave
    public float timeBetweenShots = 5f; // Time between each shot
    public int bulletAmount = 1;

    public override IEnumerator FireProjectile()
    {
        while (true)
        {
            if (target == null) break;
            else
            {
                if (isFire == false)
                {
                    isFire = true;
                    for (int k = 0; k < bulletAmount; k++)
                    {
                        Vector3 direction = (target.position - shootingPoint.transform.position).normalized;
                        GameObject projectile = ObjectPool.SharedInstance.GetPooledObject(projectilePrefab);
                        if (projectile != null)
                        {
                            projectile.transform.position = shootingPoint.transform.position;
                            projectile.transform.rotation = owner.transform.rotation;
                            projectile.SetActive(true);
                            projectile.GetComponent<Bullet>().SetMoveDirection(direction);
                        }
                        //AudioManager.instance.PlayOneShot(FMODEvents.instance.bulletShot, shootingPoint.transform.position);
                        yield return new WaitForSeconds(timeBetweenShots);
                    }

                    isFire = false;
                }
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }
    }
}