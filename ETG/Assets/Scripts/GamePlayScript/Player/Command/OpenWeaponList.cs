using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWeaponList : ICommand
{
    public GameObject slowMotionObj;
    public OpenWeaponList(GameObject _slowMotionObj)
    {
        slowMotionObj = _slowMotionObj;
    }
    public void Execute()
    {
        slowMotionObj.GetComponent<SlowMotion>().TurnToSlowMotion();
    }
}
