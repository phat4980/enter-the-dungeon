using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public abstract class PathFireBullet
{
    [Header("Bullet position Settings")]
    public Transform target;
    public Transform shootingPoint;

    [Header("Projectile Settings")]
    public GameObject projectilePrefab;
    public float projectileSpeed;

    public GameObject owner;
    public Bullet bullet;           // get from projectilePrefab 
    public bool isFire;

    public void SetValue(Transform target, Transform shootingPoint, GameObject projectilePrefab, float projectileSpeed, string typeBullet, GameObject owner)
    {
        this.target = target;
        this.shootingPoint = shootingPoint;
        this.projectilePrefab = projectilePrefab;
        this.projectileSpeed = projectileSpeed;
        this.bullet = this.projectilePrefab.GetComponent<Bullet>();
        this.bullet.checkType = typeBullet;
        this.bullet.projectileSpeed = this.projectileSpeed;
        this.owner = owner;

    }
    public virtual void SetSpecialValue(float timeBetweenWaves, float timeBetweenShots, int bulletAmount, float bulletAngleStep) { } // variable 
    public virtual void SetSpecialValue(float timeBetweenShots, float timeBetweenWaves, float waveNum, int bulletAmount) { }   //Shotgun kin
    public virtual void SetSpecialValue(float timeBetweenShots, float timeBetweenWaves, float waveNum, int bulletAmount, float startAngle, float engAngle) { }
    public virtual void SetSpecialValue(float timeBetweenWaves, float timeBetweenShots, int bulletAmount, float shootAmplitude, float shootFrequency) { } // boss Lich
    public abstract IEnumerator FireProjectile();

}