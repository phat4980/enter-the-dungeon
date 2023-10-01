using Enemy;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class RoomManager : MonoBehaviour
{
    [Header("Information of Room")]
    public List<GameObject> neighborRoom;
    public bool spawnRoom;
    public GameObject[] doors; // Neighborhood room

    [Header("Challenge room")]
    public GameObject player;
    public List<GameObject> enemyInRoom;
    public bool isClear = false;
    public int spawnRoundMax = 2,spawnRoundMin = 1, spawnRound;
    
    // A number of enemies spawn each time
    private int numEnemyPerTimeSpawnMin;
    public int numEnemyPerTimeSpawnMax;

    
    // ChamberManager to get chamber enemy list
    public GameObject[] enemies;

    // Room Area
    public List<Vector3Int> trackedCells;

    [Header("Boss Room")]
    public bool BossRoom;
    public GameObject Boss;

    public float timer = 0;
    // Start is called before the first frame update
    void Awake()
    {
        if (spawnRoom == false)
        {
            if (trackedCells.Count <= 1000) numEnemyPerTimeSpawnMin = 3; //3
            if (trackedCells.Count > 1000 && trackedCells.Count <= 1600) numEnemyPerTimeSpawnMin = 4; //4
            if (trackedCells.Count > 1600) numEnemyPerTimeSpawnMin = 5; //5
            spawnRound = Random.Range(spawnRoundMin, spawnRoundMax);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (BossRoom == true)
        {
            if (Boss == null)
            {
                foreach (GameObject d in doors)
                {
                    d.SetActive(false);
                }
                isClear = true;
            }
        }
        if (spawnRoom == false)
        {
            for (int i = 0; i < enemyInRoom.Count; i++)
            {
                if (enemyInRoom[i] == null) enemyInRoom.RemoveAt(i);
            }
            if (player != null) 
            {
                if (enemyInRoom.Count < 3 && spawnRound != 0)
                timer += Time.deltaTime;
                RoomCleared();
            }
            if (isClear == true) gameObject.GetComponent<RoomManager>().enabled = false;
        }
        if (isClear == true)
        {
            if (neighborRoom.Count != 0)
            {
                foreach (GameObject n in neighborRoom) n.SetActive(true);
            }
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
        if (spawnRoom == true)
        {
            if (neighborRoom.Count != 0)
            {
                foreach (GameObject n in neighborRoom) n.SetActive(true);
            }
        }
    }

    void Spawn() // Initialization number spawn round for each room, number enemy for each spawn round, spawn position for enemies when enter the chamber
    {
        if (trackedCells.Count > 0)
        {
            int numEnemyPerTimeSpawn = Random.Range(numEnemyPerTimeSpawnMin, numEnemyPerTimeSpawnMax); //A Number of enemies spawn in each round
            for (int i = 0; i < numEnemyPerTimeSpawn; i++)
            {
                // Random spawn position and enemy
                int ran = Random.Range(0, trackedCells.Count - 1);
                int e = Random.Range(0, enemies.Length - 1);
                
                Vector3Int ranPos = trackedCells.ToArray()[ran];

                // Instantiate enemy at random position and set target = player
                GameObject enemy = enemies[e];
                if(enemy.GetComponent<Enemy.Enemy>() != null) enemy.GetComponent<Enemy.Enemy>().target = player;
                GameObject tmp = Instantiate(enemy, new Vector3(ranPos.x, ranPos.y, ranPos.z), Quaternion.identity);
                enemyInRoom.Add(tmp);
            }
            spawnRound -= 1;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.name == "Hunter")
            {
                foreach (GameObject d in doors)
                {
                    if (d.GetComponent<Door>() != null)
                    {
                        d.GetComponent<Door>().roomClear = false;
                        d.GetComponent<Door>().Close();
                    }
                }
                player = collision.gameObject;
                if (spawnRoom == false) Spawn();
                if (BossRoom == true) Boss.GetComponent<Enemy.Boss>().target = player;
            }
        }
    }
    void RoomCleared()
    {
        if (enemyInRoom.Count == 0 && spawnRound == 0)
        {
            foreach (GameObject d in doors)
            {
                d.GetComponent<Door>().roomClear = true;
                d.GetComponent<Door>().Open();
            }
            isClear = true;
        }
        else if (enemyInRoom.Count == 0 && spawnRound != 0)
        {
            if (timer > 8)
            {
                Spawn();
            }
        }
    }
}
