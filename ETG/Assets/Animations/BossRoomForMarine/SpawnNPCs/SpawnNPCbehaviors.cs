using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNPCbehaviors : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    float range;
    [SerializeField]
    float maxDistance;

    Vector2 spawnPoint;
    Vector2 wayPoint;

    // Set this variable to the desired location for the NPC to move around
    [SerializeField]
    Vector2 selectedLocation;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = transform.position; // Set the spawn point to the current position of the NPC
        setNewDestination();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, wayPoint, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, wayPoint) < range)
        {
            setNewDestination();
        }
    }

    void setNewDestination()
    {
        // Generate a random waypoint within a range around the selected location
        wayPoint = selectedLocation + new Vector2(Random.Range(-maxDistance, maxDistance), Random.Range(-maxDistance, maxDistance));
    }
}