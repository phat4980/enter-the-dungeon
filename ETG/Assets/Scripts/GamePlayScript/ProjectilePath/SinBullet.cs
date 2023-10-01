using UnityEngine;
using System.Collections;

public class SinBullet : PathFireBullet
{
    [Header("Shooting Settings")]
    public float timeBetweenWaves = 2f; // Time between each wave
    public float timeBetweenShots = 1f; // Time between each shot
    public int bulletAmount = 3;
    public float shootAmplitude = 1f;   // Amplitude of the sine wave
    public float shootFrequency = 1f;   // Frequency of the sine wave

    public override void SetSpecialValue(float timeBetweenWaves, float timeBetweenShots, int bulletAmount, float shootAmplitude, float shootFrequency)
    {
        this.timeBetweenWaves = timeBetweenWaves;
        this.timeBetweenShots = timeBetweenShots;
        this.bulletAmount = bulletAmount;
        this.shootAmplitude = shootAmplitude;
        this.shootFrequency = shootFrequency;
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

                        // Calculate the direction of the bullet using a sine wave
                        float time = Time.time * shootFrequency;
                        Vector3 direction = (target.position - shootingPoint.transform.position).normalized;
                        // Calculate the player's position relative to the shooting point
                        Vector3 relativePos = target.position - shootingPoint.position;
                        if (Mathf.Abs(relativePos.x) < Mathf.Abs(relativePos.y))
                        {
                            direction += Vector3.right * Mathf.Sin(time) * shootAmplitude * Mathf.Sign(relativePos.y);
                        }
                        else
                        {
                            direction += Vector3.up * Mathf.Sin(time) * shootAmplitude * Mathf.Sign(relativePos.x);
                        }
                        GameObject projectile = ObjectPool.SharedInstance.GetPooledObject(projectilePrefab);
                        if (projectile != null)
                        {
                            projectile.transform.position = shootingPoint.transform.position;
                            projectile.transform.rotation = owner.transform.rotation;
                            projectile.SetActive(true);
                            projectile.GetComponent<Bullet>().SetMoveDirection(direction);
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
