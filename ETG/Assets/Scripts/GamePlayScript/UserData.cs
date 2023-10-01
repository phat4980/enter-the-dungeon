using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "UserData",menuName = "Scriptable Object/UserData",order = 4)]
[System.Serializable]
public class UserData: ScriptableObject
{
    //Custom config data

    //User's store data
    public int hegemony;
    [SerializeField] private List<Weapon> weaponCollection;
    [SerializeField] private List<Item> itemCollection;
    [SerializeField] private List<Enemy.Enemy> enemyCollection;
    [SerializeField] private List<Enemy.Boss> bossCollection;
    // Start is called before the first frame update
    public void addInList(object obj)
    {
        switch (obj.GetType().Name)
        {
            case "Item":
                var tmp = (Item)obj;
                if (!itemCollection.Contains(tmp))
                {
                    itemCollection.Add(tmp);
                }
                break;

            case "Weapon":
                var tmp1 = (Weapon)obj;
                if (!weaponCollection.Contains(tmp1))
                {
                    weaponCollection.Add(tmp1);
                }
                break;

            case "Enemy":
                var tmp3 = (Enemy.Enemy)obj;
                if (!enemyCollection.Contains(tmp3))
                {
                    enemyCollection.Add(tmp3);
                }
                break;

            case "Boss":
                var tmp4 = (Enemy.Boss)obj;
                if (!bossCollection.Contains(tmp4))
                {
                    bossCollection.Add(tmp4);
                }
                break;
        }
    }
}
