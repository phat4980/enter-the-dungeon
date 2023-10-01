using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public int heal;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && collision.name == "Hunter")
        {
            collision.gameObject.GetComponent<PlayerController>().healthSystem.Heal(heal);
            collision.gameObject.GetComponent<PlayerController>().notify("HP","",0);
            Destroy(gameObject);

        }
    }
}
