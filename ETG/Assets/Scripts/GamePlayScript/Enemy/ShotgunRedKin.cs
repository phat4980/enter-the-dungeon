using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class ShotgunRedKin : Enemy
    {
        public PathFireBullet path;
        
        #region Special Attributes for fire path if it has
        [Header("Shooting Settings")]
        public float timeBetweenShots; // Time between each shot
        public float timeBetweenWaves;  // Time between waves
        public float waveNum;            // Number of wave bullets shoot
        public int bulletAmount;
        #endregion
        // Start is called before the first frame update
        private void Start()
        {
            path = new ShotgunBullet();
            path.SetSpecialValue(timeBetweenShots, timeBetweenWaves, waveNum, bulletAmount);
            
        }
        public override void Fire()
        {
            pathFireManager.SetFirePath(path);
            pathFireManager.Fire(this);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player") 
            { 
                path.SetValue(collision.transform, spawnBullet, projectilePrefab, projectileSpeed, typeBullet.ToString(), owner);
                Fire();
            }
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                path.SetValue(collision.transform, spawnBullet, projectilePrefab, projectileSpeed, typeBullet.ToString(), owner);
                Fire();

            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "Player") path.SetValue(null, spawnBullet, projectilePrefab, projectileSpeed, typeBullet.ToString(), owner);
        }
    }

}
