using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UISub : MonoBehaviour,Subscriber
{
    public GameObject player;
    public GameObject weaponList;
    public GameObject counter;
    public void OnNotify(string eventName, string addOrSub, int amount)
    {
        switch (eventName)
        {
            case "HP":
                counter.GetComponent<HeartHealthVisual>().HeartHealthSystemHaveChange();
                break;
            case "Blank":
                counter.GetComponent<ShieldBlankKeyCoinVisual>().ChangeBlank(addOrSub);
                break ;
            case "Coin":
                counter.GetComponent<ShieldBlankKeyCoinVisual>().ChangeCoinAmount(amount);
                break;
            case "Key":
                counter.GetComponent<ShieldBlankKeyCoinVisual>().ChangeKeyAmount(amount);
                break;
            case "Shield":
                counter.GetComponent<ShieldBlankKeyCoinVisual>().ChangeShield(addOrSub);
                break;
            case "WeaponList":
                weaponList.GetComponent<SelectWeapon>().GetCurrentWeapons();
                break;
            default:
                counter.GetComponent<HeartHealthVisual>().HeartHealthSystemHaveChange();
                counter.GetComponent<ShieldBlankKeyCoinVisual>().ChangeBlank(addOrSub);
                counter.GetComponent<ShieldBlankKeyCoinVisual>().ChangeCoinAmount(amount);
                counter.GetComponent<ShieldBlankKeyCoinVisual>().ChangeKeyAmount(amount);
                counter.GetComponent<ShieldBlankKeyCoinVisual>().ChangeShield(addOrSub);
                weaponList.GetComponent<SelectWeapon>().GetCurrentWeapons();
                break;
        }
    }

    private void OnEnable()
    {
        Publisher.Subscribe(this);
    }

    private void OnDisable()
    {
        Publisher.Unsubscribe(this);
    }
}
