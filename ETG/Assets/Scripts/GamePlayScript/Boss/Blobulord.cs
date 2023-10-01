using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Enemy
{
    public class Blobulord : Boss
    {
        [SerializeField] private float timeBetweenShots; // Time between each shot
        [SerializeField] private float timeBetweenWaves; // Time between waves
        [SerializeField] private float waveNum;          // number of wave bullets shoot
        [SerializeField] private int bulletAmount;       // Number of bullets per wave
        [SerializeField] private float startAngle, engAngle;
        public float bulletSpreadAngle;

        public float bulletAngleStep;
       
        public override void SkillOne()
        {
            fire = new RadialBullet();
            fire.SetValue(target.transform, spawnBullet, projectilePrefab, projectileSpeed, typeBullet.ToString(), owner);
            SetRandomSpecialValue();
            fire.SetSpecialValue(timeBetweenShots, timeBetweenWaves, waveNum, bulletAmount, startAngle, engAngle);

            pathFireManager.SetFirePath(fire);
            pathFireManager.Fire(this);
        }

        public override void SkillTwo() 
        {
            fire = new SpreadBullet();
            fire.SetValue(target.transform, spawnBullet, projectilePrefab, projectileSpeed, typeBullet.ToString(), owner);
            SetRandomSpecialValue();
            fire.SetSpecialValue(timeBetweenWaves, timeBetweenShots, bulletAmount, bulletSpreadAngle);

            pathFireManager.SetFirePath(fire);
            pathFireManager.Fire(this);
        }
        public override void SkillThree()
        {
           
            fire = new SquishyBullet();
            fire.SetValue(target.transform, spawnBullet, projectilePrefab, projectileSpeed, typeBullet.ToString(), owner);
            SetRandomSpecialValue();
            fire.SetSpecialValue(timeBetweenWaves, timeBetweenShots, bulletAmount, bulletAngleStep);

            pathFireManager.SetFirePath(fire);
            pathFireManager.Fire(this);
        }

        //Set attribute for each type of skill
        public override void SetRandomSpecialValue(){
            if (fire.GetType() == typeof(RadialBullet))
            {
                timeBetweenShots = 1; // Time between each shot
                timeBetweenWaves = 1; // Time between waves
                waveNum = 3;          // number of wave bullets shoot
                bulletAmount = 15;       // Number of bullets per wave
                startAngle = 0;
                engAngle = 360;
            }
            if (fire.GetType() == typeof(SpreadBullet)){
                timeBetweenWaves = 1; // Time between waves
                timeBetweenShots = 0.01f; // Time between each shot
                bulletAmount = 25;       // Number of bullets per wave
                bulletSpreadAngle = 25; // S2
            }
           
            if (fire.GetType() == typeof(SquishyBullet)){
                timeBetweenWaves = 1; // Time between waves
                timeBetweenShots = 0.01f; // Time between each shot
                bulletAmount = 100;       // Number of bullets per wave
                bulletAngleStep = 44; // S3 
            }
        }
    }
}
