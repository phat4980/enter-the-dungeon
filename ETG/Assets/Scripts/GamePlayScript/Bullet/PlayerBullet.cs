using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public Vector2 moveDirection;
    public float projectileSpeed;
    private float timeDestroy = 12f;
    public float damage;
    Rigidbody2D rb;
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("Destroy", timeDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.Translate(moveDirection * projectileSpeed * Time.deltaTime);
        rb.velocity = moveDirection * projectileSpeed;
        
    }
    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
        transform.Rotate(0.0f, 0.0f, Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg);
    }
    public void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Wall" || (collider.tag == "Table" && collider.GetComponent<Table>().fliped == true))
        {
            Destroy();
        }
        if (collider.tag == "Enemy")
        {
            if (collider.GetComponent<Enemy.Enemy>() != null)
            {
                collider.GetComponent<Enemy.Enemy>().HP -= damage;
                if (collider.GetComponent<Enemy.Enemy>().HP == 0) GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().kills += 1;
            }
            if (collider.GetComponent<Enemy.Boss>() != null) 
            { 
                collider.GetComponent<Enemy.Boss>().HP -= damage;
                collider.GetComponent<Enemy.Boss>().GetDmg(damage);
                if (collider.GetComponent<Enemy.Boss>().HP == 0) GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().kills += 1;
            }
            Destroy();
        }
    }
}