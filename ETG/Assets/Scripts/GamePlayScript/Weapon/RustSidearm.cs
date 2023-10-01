using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RustSidearm : Weapon
{
    public override void ShootingBullet()
    {
        Vector3 tmp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mousePosition = new Vector3(tmp.x,tmp.y,0.0f);
        Vector3 direction =(mousePosition - spawnBullet.transform.position).normalized;
        if (bulletAmountPerTime > 0)
        {
            GameObject projectile = ObjectPool.SharedInstance.GetPooledObject(projectilePrefab);
            if (projectile != null)
            {
                projectile.transform.position = spawnBullet.transform.position;
                projectile.transform.rotation = Quaternion.identity;
                projectile.SetActive(true);
                projectile.GetComponent<PlayerBullet>().SetMoveDirection(direction);
            }
            animator.Play("Shoot");
            bulletAmountPerTime -= 1;
        }
        //AudioManager.instance.PlayOneShot(FMODEvents.instance.bulletShot, spawnBullet.transform.position);
    }
}
