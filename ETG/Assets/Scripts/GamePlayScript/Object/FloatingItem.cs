using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class FloatingItem : MonoBehaviour
{
    [SerializeField] private float offset = 0.1f;
    public float maxMiny;
    public float speed;
    public float beginPosY;
    public float waitToPlay;
    public bool play = false;
    public float timer;
    // Update is called once per frame
    private void Start()
    {
        beginPosY = transform.position.y;
        waitToPlay = Random.value;
    }
    void Update()
    {
        if (!play) timer += Time.deltaTime;
        if (timer > waitToPlay * 4) play = true;
        if (play)
        {
            if (transform.position.y > beginPosY + maxMiny || transform.position.y < beginPosY - maxMiny) offset *= -1;
            transform.position += new Vector3(0, offset * speed * 0.01f, 0);
        }
    }
}
