using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.name == "Hunter")
        {
            collision.gameObject.GetComponent<PlayerController>().player.silverKey += 1;
            collision.gameObject.GetComponent<PlayerController>().notify("Key", "", 1);
            Destroy(gameObject);

        }
    }
}
