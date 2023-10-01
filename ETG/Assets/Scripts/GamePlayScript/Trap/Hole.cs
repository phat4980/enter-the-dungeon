using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Do something with player
        }else if (collision.gameObject.tag == "Enemy"){
            // Do something with enemy
        }
    }
}
