using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Bullet : MonoBehaviour
{
    public Vector2 moveDirection;
    public Transform spawnPoint;
    public float projectileSpeed;
    private float timeDestroy = 12f;
    public string checkType;
    public float rotation = 0;
    public float radius = 2f; // the radius of the circle

    private void OnEnable()
    {
        Invoke("Destroy", timeDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        switch (checkType)
        {
            case "None":
                break;
            case "CircleType":
                CircleRotation();
                break;
        }
        gameObject.transform.Translate(moveDirection * projectileSpeed * Time.deltaTime);
    }

    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void CircleRotation()
    {
        rotation += 40 * Time.deltaTime * projectileSpeed;
        float x = Mathf.Sin(rotation * Mathf.Deg2Rad) * radius;
        float y = Mathf.Cos(rotation * Mathf.Deg2Rad) * radius;
        Vector3 position = spawnPoint.position + new Vector3(x, y, 0);
        gameObject.transform.position = position;
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Wall" || (collider.tag == "Table" && collider.GetComponent<Table>().fliped == true))
        {
            Destroy();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            Destroy();
        }
    }
}