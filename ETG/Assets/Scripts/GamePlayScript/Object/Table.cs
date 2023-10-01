using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    public int dirToFlip; // 1 - east / 2 - north / 3 - south / 4 - west
    public int hp = 5;
    public Animator ani;
    public bool fliped = false;
    public bool isbreak;
    //public GameObject player;
    private void Start()
    {
        ani = GetComponent<Animator>();
    }
    public void OnCollisionStay2D(Collision2D collision)
    {
       if(collision.collider.tag == "Shadow")
       {
            if (collision.contacts[0].point.x > transform.position.x && (collision.contacts[0].point.y > transform.position.y - 0.5f && collision.contacts[0].point.y < transform.position.y + 0.5f)) dirToFlip = 4;
            if (collision.contacts[0].point.x < transform.position.x && (collision.contacts[0].point.y > transform.position.y - 0.5f && collision.contacts[0].point.y < transform.position.y + 0.5f)) dirToFlip = 1;
            if (collision.contacts[0].point.y > transform.position.y && (collision.contacts[0].point.x > transform.position.x - 0.5f && collision.contacts[0].point.x < transform.position.x + 0.5f)) dirToFlip = 3;
            if (collision.contacts[0].point.y < transform.position.y && (collision.contacts[0].point.x > transform.position.x - 0.5f && collision.contacts[0].point.x < transform.position.x + 0.5f)) dirToFlip = 2;
            ani.SetInteger("dir", dirToFlip);
            Debug.Log("Play");
       }
       if(collision.collider.tag == "PLayerBullet" || collision.collider.tag == "EnemyBullet")
       {
            hp -= 1;
            if (hp == 0) 
            { 
                isbreak = true;
                ani.SetBool("break", isbreak);
            }
       }
    }
}
