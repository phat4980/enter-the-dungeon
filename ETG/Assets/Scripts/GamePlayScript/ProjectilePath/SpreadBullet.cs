using UnityEngine;
using System.Collections;

public class SpreadBullet : PathFireBullet
{
    [Header("Shooting Settings")]
    public float timeBetweenWaves = 1f; // Time between each wave
    public float timeBetweenShots = .005f; // Time between each shot
    public int bulletAmount = 14;
    public float bulletSpreadAngle = 30f; // Maximum angle for bullet spread

    public override void SetSpecialValue(float timeBetweenWaves, float timeBetweenShots, int bulletAmount, float bulletSpreadAngle)
    {
        this.timeBetweenWaves = timeBetweenWaves;
        this.timeBetweenShots = timeBetweenShots;
        this.bulletAmount = bulletAmount;
        this.bulletSpreadAngle = bulletSpreadAngle;
    }

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
                        yield return new WaitForSeconds(timeBetweenShots);

                        // Calculate direction with random offset
                        Vector3 direction = (target.position - shootingPoint.transform.position).normalized;
                        float spreadAngle = Random.Range(-bulletSpreadAngle, bulletSpreadAngle);
                        direction = Quaternion.Euler(0, 0, spreadAngle) * direction;

                        // Spawn bullet
                        GameObject projectile = ObjectPool.SharedInstance.GetPooledObject(projectilePrefab);
                        if (projectile != null)
                        {
                            projectile.transform.position = shootingPoint.transform.position;
                            projectile.transform.rotation = owner.transform.rotation;
                            projectile.SetActive(true);
                            projectile.GetComponent<Bullet>().SetMoveDirection(direction);
                        } AudioManager.instance.PlayOneShot(FMODEvents.instance.bulletShot, shootingPoint.position);
                        yield return new WaitForSeconds(timeBetweenShots);
                    }
                    yield return new WaitForSeconds(timeBetweenWaves);
                    isFire = false;
                }
            }
            yield return null;
        }
    }
}
