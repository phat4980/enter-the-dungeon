using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    public enum state
    {
        NONE,
        OPEN,
        CLOSE,
        END,

    }
    public state CURRENT = state.NONE;
    public Animator ani;
    public Collider2D doorCollider;
    public float y;
    [SerializeField] private bool playerEnterRoom;
    private void Start()
    {
        y = transform.localScale.y;
    }
    // Update is called once per frame
    void Update()
    {
        if(CURRENT == state.OPEN) { }
        switch (CURRENT)
        {
            case state.OPEN:
                if (transform.localPosition.y < 2.85f) transform.localPosition += new Vector3(0, 0.01f, 0);
                else transform.localPosition += Vector3.zero; 
                break;
            case state.CLOSE:
                if (transform.localPosition.y > y) transform.localPosition -= new Vector3(0, 0.01f, 0);
                else transform.localPosition += Vector3.zero;
                break;
            case state.END:
                if (transform.localPosition.y < 2.85f) transform.localPosition += new Vector3(0, 0.01f, 0);
                else transform.localPosition += Vector3.zero;
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerEnterRoom = true;
        }
    }
}
