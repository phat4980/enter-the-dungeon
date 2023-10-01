using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace Enemy
{
    public class BulletMan : Enemy
    {
        public PathFireBullet path;
        #region Special Attributes for fire path if it has
        //
        #endregion

        private void Start()
        {
            path = new BasicBullet();
            
            
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
