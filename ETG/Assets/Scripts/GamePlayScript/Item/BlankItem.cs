using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlankItem : MonoBehaviour
{
    public void Start()
    {
            
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.name == "Hunter")
        {
            collision.gameObject.GetComponent<PlayerController>().player.blank += 1;
            collision.gameObject.GetComponent<PlayerController>().notify("Blank", "Add", 0);
            Destroy(gameObject);

        }
    }
}
