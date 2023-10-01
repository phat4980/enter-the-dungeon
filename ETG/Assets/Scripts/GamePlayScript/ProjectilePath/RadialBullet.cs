using UnityEngine;
using System.Collections;

public class RadialBullet : PathFireBullet
{
    [Header("Shooting Settings")]
    public float timeBetweenShots; // Time between each shot
    public float timeBetweenWaves; // Time between waves
    public float waveNum;          // number of wave bullets shoot
    public int bulletAmount;       // Number of bullets per wave
    public float startAngle, engAngle;
    public override void SetSpecialValue(float timeBetweenShots, float timeBetweenWaves, float waveNum, int bulletAmount, float startAngle, float engAngle)
    {
        this.timeBetweenShots = timeBetweenShots;
        this.timeBetweenWaves = timeBetweenWaves;
        this.waveNum = waveNum;
        this.bulletAmount = bulletAmount;
        this.startAngle = startAngle;
        this.engAngle = engAngle;
    }

    public override IEnumerator FireProjectile()
    {
        float angleStep = (engAngle - startAngle) / bulletAmount;
        float angle = startAngle;

        while (true)
        {
            if (target == null) break;

            if (!isFire)
            {
                isFire = true;
                for (int w = 0; w < waveNum; w++)
                {
                    for (int i = 0; i < bulletAmount; i++)
                    {
                        float bulDirx = owner.transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
                        float bulbiry = owner.transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

                        Vector3 bulMoveVector = new Vector3(bulDirx, bulbiry, 0f);
                        Vector2 bulDir = (bulMoveVector - owner.transform.position).normalized;

                        GameObject projectile = ObjectPool.SharedInstance.GetPooledObject(projectilePrefab);
                        if (projectile != null)
                        {
                            projectile.transform.position = shootingPoint.position;
                            projectile.transform.rotation = owner.transform.rotation; 
                            projectile.SetActive(true);
                            projectile.GetComponent<Bullet>().SetMoveDirection(bulDir);
                        }
                        angle += angleStep;
                    }
                    AudioManager.instance.PlayOneShot(FMODEvents.instance.bulletShot, shootingPoint.position);
                    if (w != waveNum - 1) // Don't wait after the last wave
                    {
                        yield return new WaitForSeconds(timeBetweenShots);
                    }
                }
                yield return new WaitForSeconds(timeBetweenWaves);
                isFire = false;
            }
            yield return null;
        }
    }   
}
