using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldItem : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.name == "Hunter")
        {
            collision.gameObject.GetComponent<PlayerController>().player.shield += 1;
            collision.gameObject.GetComponent<PlayerController>().notify("Shield", "Add", 0);
            Destroy(gameObject);

        }
    }
}
