using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Enemy
{
    public class Lich : Boss
    {
        [SerializeField] private float timeBetweenShots; // Time between each shot
        [SerializeField] private float timeBetweenWaves; // Time between waves
        [SerializeField] private float waveNum;          // number of wave bullets shoot
        [SerializeField] private int bulletAmount;       // Number of bullets per wave
        [SerializeField] private float startAngle, engAngle;
       
        [Tooltip("Skill 1 attribute")]
        public float bulletSpread;
        [Tooltip("Skill 2 attribute")]
        public float shootAmplitude;
        public float shootFrequency;
        [Tooltip("Skill 3 attribute")]
        public float bulletAngleStep;
       
        public override void SkillOne()
        {
            fire = new RandomBullet();
            fire.SetValue(target.transform, spawnBullet, projectilePrefab, projectileSpeed, typeBullet.ToString(), owner);
            SetRandomSpecialValue();
            fire.SetSpecialValue( timeBetweenWaves, timeBetweenShots, bulletAmount, bulletSpread);

            pathFireManager.SetFirePath(fire);
            pathFireManager.Fire(this);
        }

        public override void SkillTwo() 
        {
            fire = new SinBullet();
            fire.SetValue(target.transform, spawnBullet, projectilePrefab, projectileSpeed, typeBullet.ToString(), owner);
            SetRandomSpecialValue();
            fire.SetSpecialValue(timeBetweenWaves, timeBetweenShots, bulletAmount, shootAmplitude, shootFrequency);

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
            if (fire.GetType() == typeof(RandomBullet)){
                timeBetweenWaves = 1; // Time between waves
                timeBetweenShots = 0.05f; // Time between each shot
                bulletAmount = 250;       // Number of bullets per wave
                bulletSpread = 360; // S2
            }
            if (fire.GetType() == typeof(SinBullet))
            {
                timeBetweenWaves = 2f; // Time between waves
                timeBetweenShots = 0.05f; // Time between each shot
                bulletAmount = 50;       // Number of bullets per wave
                shootAmplitude = 1.5f;
                shootFrequency = 1.5f;    
            }
            if (fire.GetType() == typeof(SquishyBullet)){
                timeBetweenWaves = 1; // Time between waves
                timeBetweenShots = 0.01f; // Time between each shot
                bulletAmount = 100;       // Number of bullets per wave
                bulletAngleStep = 9; // S3 
            }
        }
    }
}
