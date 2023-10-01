using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enemy
{
    public class BulletManBandana : Enemy
    {
        public PathFireBullet path;
        
        #region Special Attributes for fire path if it has
        [Header("Shooting Settings")]
        public float timeBetweenWaves; // Time between each wave
        public float timeBetweenShots; // Time between each shot
        public int bulletAmount;
        #endregion
        // Start is called before the first frame update
        private void Start()
        {
            path = new BasicBullet();
            path.SetSpecialValue(timeBetweenWaves, timeBetweenShots, bulletAmount, 0f);
            
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
