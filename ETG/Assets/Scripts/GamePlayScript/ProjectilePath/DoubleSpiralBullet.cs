using UnityEngine;
using System.Collections;

public class DoubleSpiralBullet : PathFireBullet
{
    [Header("Shooting Settings")]
    public float timeBetweenShots = 0.1f; // Time between each shot
    public float timeBetweenWaves = 1f;         // Time between waves
    public float waveNum = 1;            // number of wave bullets shoot
    public int bulletAmount = 5;           // Number of bullets per wave
    public float angle = 0f;


    public override IEnumerator FireProjectile()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenWaves);

            for (int k = 0; k < waveNum; k++) // Fire waves
            {
                for (int i = 0; i < bulletAmount; i++) // Spawn bullets in a wave
                {
                    float angleOffset = k % 2 == 0 ? 0f : 180f;
                    float bulDirx = shootingPoint.position.x + Mathf.Sin(((angle + angleOffset + 180f * i) * Mathf.PI) / 180f);
                    float bulbiry = shootingPoint.position.y + Mathf.Cos(((angle + angleOffset + 180f * i) * Mathf.PI) / 180f);

                    Vector3 bulMoveVector = new Vector3(bulDirx, bulbiry, 0f);
                    Vector2 bulDir = (bulMoveVector - shootingPoint.position).normalized;

                    GameObject projectile = ObjectPool.SharedInstance.GetPooledObject(projectilePrefab);
                    if (projectile != null)
                    {
                        projectile.transform.position = shootingPoint.position;
                        projectile.transform.rotation = owner.transform.rotation; 
                        projectile.SetActive(true);
                        projectile.GetComponent<Bullet>().SetMoveDirection(bulDir);
                    }
                    angle += 10f;
                    if(angle >= 360) angle = 0f;
                }
                yield return new WaitForSeconds(timeBetweenShots);
            }
        }
    }
}