using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public List<GameObject> blocks;
    public bool roomClear = true;
    public void Close()
    {
        blocks[0].SetActive(true);
        if(blocks.Count > 1)
        {
            blocks[1].SetActive(true);
            blocks[0].GetComponent<Animator>().Play("Close");
            blocks[1].GetComponent<Animator>().Play("Close");
        } else blocks[0].GetComponent<Animator>().Play("Close");
        gameObject.GetComponent<Animator>().Play("Close");
    }

    public void Open()
    {
        if (blocks.Count > 1)
        {
            blocks[0].GetComponent<Animator>().Play("Open");
            blocks[1].GetComponent<Animator>().Play("Open");
            blocks[1].SetActive(false);
        } else blocks[0].GetComponent<Animator>().Play("Open");
        blocks[0].SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && roomClear == true)
        {
            
            gameObject.GetComponent<Animator>().Play("Open");
        }
    }
}
