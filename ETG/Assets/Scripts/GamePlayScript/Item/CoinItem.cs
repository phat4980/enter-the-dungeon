using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : MonoBehaviour
{
    [SerializeField] private int amount;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.name == "Hunter")
        {
            collision.gameObject.GetComponent<PlayerController>().player.shell += amount;
            collision.gameObject.GetComponent<PlayerController>().notify("Coin", "", amount);
            Destroy(gameObject);

        }
    }
}
