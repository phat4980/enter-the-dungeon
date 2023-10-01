using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class SpawnFragments : MonoBehaviour
{
    [Header("Fragments Settings")]
    public List<GameObject> fragmentsPrefab;
    public float spawnForce;
    [Header("Spawn Settings")]
    private Animator animator;
    private Collider2D colliderObject;
    [SerializeField] private EventReference soundBreak;
    private void Start() {
        animator = GetComponent<Animator>();
        colliderObject = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            animator.SetTrigger("Trigger");   
            spawnAction();
            AudioManager.instance.PlayOneShot(soundBreak, this.transform.position);
            colliderObject.enabled = false;
        } 
    }

   private void spawnAction()
    { 
        foreach (GameObject fragmentPrefab in fragmentsPrefab)
        {
            GameObject fragment = Instantiate(fragmentPrefab, transform.position, Quaternion.identity);
            Vector2 randomDirection = Random.insideUnitCircle.normalized;                       //Set the direction
            Vector2 randomVelocity = randomDirection * spawnForce * Random.Range(0.1f, 1f);     //Set the velocity

            Rigidbody2D rb = fragment.GetComponent<Rigidbody2D>();
            rb.AddForce(randomVelocity, ForceMode2D.Impulse);
            rb.AddTorque(Random.Range(-50, 50));      //Add a random torque to make the fragment rotate randomly
            StartCoroutine(DisableRigidbody(rb));     //Stop move

        }
    }

    private IEnumerator DisableRigidbody(Rigidbody2D rb)
    {
        yield return new WaitForSeconds(0.5f);
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0.0f;
        rb.simulated = false;
    }
}