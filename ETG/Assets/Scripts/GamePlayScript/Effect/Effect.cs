using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public string nameMotion;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void SetMotion(EffectName e)
    {
        nameMotion = e.ToString();
    }

    public void PlayAni()
    {
        animator.Play(nameMotion);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "EnemyBullet")
        {
            collision.gameObject.GetComponent<Bullet>().Destroy();
        }
    }
}
