using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeapon : ICommand
{
    public PlayerController player;
    public GameObject weaponList;
    public ChangeWeapon(PlayerController _player, GameObject weaponList)
    {
        player = _player;
        this.weaponList = weaponList;
    }
    public void Execute()
    {
        weaponList.GetComponent<SelectWeapon>().ChooseWeapon(Input.GetAxis("ChangeWI"));
        player.player.weapons[weaponList.GetComponent<SelectWeapon>().previousWeapon].SetActive(false);
        player.player.weapons[weaponList.GetComponent<SelectWeapon>().selectedWeapon].SetActive(true);

    }

}
