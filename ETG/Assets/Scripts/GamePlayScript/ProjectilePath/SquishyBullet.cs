using UnityEngine;
using System.Collections;

public class SquishyBullet :  PathFireBullet
{
    [Header("Shooting Settings")]
    public float timeBetweenWaves = 2f; // Time between each wave
    public float timeBetweenShots = 0.1f; // Time between each shot
    public int bulletAmount = 12;
    public float bulletAngleStep = 40f;
    public override void SetSpecialValue(float timeBetweenWaves, float timeBetweenShots, int bulletAmount, float bulletAngleStep)
    {
        this.timeBetweenWaves = timeBetweenWaves;
        this.timeBetweenShots = timeBetweenShots;
        this.bulletAmount = bulletAmount;
        this.bulletAngleStep = bulletAngleStep;
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
                    float startAngle = -projectileSpeed * ((bulletAmount - 1) / 2f);
                    for (int k = 0; k < bulletAmount; k++)
                    {
                        float angle = startAngle + bulletAngleStep * k;
                        Vector3 direction = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
                        GameObject projectile = ObjectPool.SharedInstance.GetPooledObject(projectilePrefab);
                        if (projectile != null)
                        {
                            projectile.transform.position = shootingPoint.transform.position;
                            projectile.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
                            projectile.SetActive(true);
                            projectile.GetComponent<Bullet>().SetMoveDirection(direction.normalized);
                        }
                        AudioManager.instance.PlayOneShot(FMODEvents.instance.bulletShot, shootingPoint.position);
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