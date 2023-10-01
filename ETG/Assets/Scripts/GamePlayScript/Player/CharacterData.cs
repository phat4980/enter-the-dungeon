using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Object/Character",order = 4)]
[System.Serializable]
public class CharacterData : ScriptableObject
{
    public int HP;
    public int Shield;
    public int Blank;
    public int SilverKey;
    public int GoldKey;
    public int Coin;
    public List<GameObject> weapons;
    public List<GameObject> items;
}
