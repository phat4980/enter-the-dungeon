using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    [Header("Fragments Settings")]
    public List<GameObject> itemsList;

    [Header("Spawn Settings")]
    private Animator animator;
    public bool hasOpened = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player") && !hasOpened)
        {
            if (other.gameObject.GetComponent<PlayerController>().player.silverKey > 0)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    animator.Play("open");
                    spawnAction();
                    hasOpened = true;
                    other.gameObject.GetComponent<PlayerController>().player.silverKey -= 1;
                    other.gameObject.GetComponent<PlayerController>().notify("Key", "", -1);
                }
            }
        }
    }
    private void spawnAction()
    { 
        if (itemsList.Count > 1)
        {
            int ran = Random.Range(0, itemsList.Count - 1);
            GameObject item = Instantiate(itemsList[ran], transform.position, Quaternion.identity);
            Vector2 randomDirection = Random.insideUnitCircle.normalized + new Vector2(0.5f,0.5f);                       //Set the direction
            Vector2 randomVelocity = randomDirection * 5 * Random.Range(0.1f, 1f);     //Set the velocity

            Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
            rb.AddForce(randomVelocity, ForceMode2D.Impulse);
            rb.AddTorque(30);      //Add a random torque to make the fragment rotate randomly
            StartCoroutine(DisableMovement(rb));     //Stop move
        }
        else
        {
            GameObject item = Instantiate(itemsList[0], transform.position, Quaternion.identity);
            Vector2 randomDirection = Random.insideUnitCircle.normalized;                       //Set the direction
            Vector2 randomVelocity = randomDirection * 5 * Random.Range(0.1f, 1f);     //Set the velocity

            Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
            rb.AddForce(randomVelocity, ForceMode2D.Impulse);
            rb.AddTorque(30);      //Add a random torque to make the fragment rotate randomly
            StartCoroutine(DisableMovement(rb));     //Stop move
        }
    }

    private IEnumerator DisableMovement(Rigidbody2D rb)
    {
        yield return new WaitForSeconds(0.5f);
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0.0f;
    }

}
