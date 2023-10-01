using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public Vector3 posInHand;
    // Use for display in UI
    [Header("Display in UI")]
    public Sprite idleSprite;
    public Sprite bulletIdle;

    // Use for fire action
    [Header("Spawn bullet position")]
    public Transform spawnBullet; // previous name : shootingPoint
    public GameObject projectilePrefab;
    public float projectileSpeed;
   
    public float dmg;
    public int bulletAmountPerTime; // ammo
    public Animator animator;
    public int defaultBulletAmount;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bulletAmountPerTime == 0) 
        {
            animator.Play("Reload");
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1) bulletAmountPerTime = defaultBulletAmount;
        }
    }

    public abstract void ShootingBullet();
}
