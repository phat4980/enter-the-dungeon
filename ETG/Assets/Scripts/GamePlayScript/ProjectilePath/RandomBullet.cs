using UnityEngine;
using System.Collections;

public class RandomBullet : PathFireBullet
{
    [Header("Shooting Settings")]
    public float timeBetweenWaves; // Time between each wave
    public float timeBetweenShots; // Time between each shot
    public int bulletAmount;
    public float bulletSpread;

    public override void SetSpecialValue(float timeBetweenWaves, float timeBetweenShots, int bulletAmount, float bulletSpread)
    {
        this.timeBetweenWaves = timeBetweenWaves;
        this.timeBetweenShots = timeBetweenShots;
        this.bulletAmount = bulletAmount;
        this.bulletSpread = bulletSpread;
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
                        Vector3 direction = (target.position - shootingPoint.transform.position).normalized;
                        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                        float spread = Random.Range(-bulletSpread, bulletSpread);
                        angle += spread;

                        direction = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
                        GameObject projectile = ObjectPool.SharedInstance.GetPooledObject(projectilePrefab);
                        if (projectile != null)
                        {
                            projectile.transform.position = shootingPoint.transform.position;
                            projectile.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
                            projectile.SetActive(true);
                            projectile.GetComponent<Bullet>().SetMoveDirection((target.position - shootingPoint.transform.position).normalized); // modified this line
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