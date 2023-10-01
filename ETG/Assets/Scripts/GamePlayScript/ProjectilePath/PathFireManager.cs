using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[System.Serializable]
public class PathFireManager
{
    [SerializeField]private PathFireBullet pathFire;
    public void SetFirePath(PathFireBullet p)
    {
        pathFire = p;
    }
    public void Fire(Enemy.Enemy e)
    {
        e.StartCoroutine(pathFire.FireProjectile());
    }
    public void Fire(Enemy.Boss e)
    {
        e.StartCoroutine(pathFire.FireProjectile());
    }
}
