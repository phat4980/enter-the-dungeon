using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Object/EnemyData", order = 1)]
public class EnemyData : ScriptableObject
{
    public GameObject enemy;
    public int[] hp;
    public int[] chamberAppear;
}
