using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Object/Level", order = 2)]
[System.Serializable]
public class LevelData : ScriptableObject
{
    public float posX;
    public float posY;
}
