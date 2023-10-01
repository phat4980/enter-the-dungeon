using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWeaponList : ICommand
{
    public GameObject slowMotionObj;
    public CloseWeaponList(GameObject _slowMotionObj)
    {
        slowMotionObj = _slowMotionObj;
    }
    public void Execute()
    {
        slowMotionObj.GetComponent<SlowMotion>().ReturnToNormal();
    }
}
