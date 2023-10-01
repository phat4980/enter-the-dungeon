using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    //User's playing data everytime user enter chamber
    public string name;
    public int health;
    public int shield;
    public int blank;
    public int silverKey;
    public int goldkey;
    public int shell;
    public List<GameObject> weapons;
    public List<GameObject> activeItems;
    public List<GameObject> passiveItems;
    //public List<Synergy> synergies;

    public Player(CharacterData characterData)
    {
       name = characterData.name;
       health = characterData.HP;
       shield = characterData.Shield;
       blank = characterData.Blank;
       silverKey = characterData.SilverKey;
       goldkey = characterData.GoldKey;
       shell = characterData.Coin;
        weapons = new List<GameObject>();
        activeItems = new List<GameObject>();
        passiveItems = new List<GameObject>();
    }

    public void addInList(GameObject obj)
    {
        switch (obj.tag)
        {
            case "Item":
                if (!activeItems.Contains(obj) || activeItems.Count == 0)
                {
                    activeItems.Add(obj);
                }
                if (!passiveItems.Contains(obj) || passiveItems.Count == 0)
                {
                    passiveItems.Add(obj);
                }
                break;

            case "Weapon":
                if (weapons.Count == 0)
                {
                    weapons.Add(obj);
                }
                else if (weapons.Count > 0 && !weapons.Contains(obj))
                {
                    weapons.Add(obj);
                }
                break;

        }
    }
}
