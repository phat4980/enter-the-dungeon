using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAroundElevator : MonoBehaviour
{
    public GameObject Elevator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {
            Elevator.GetComponent<Animator>().Play("Open");
        }
    }
}
