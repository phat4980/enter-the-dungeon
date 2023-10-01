using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeState : IState
{
    public override void EnterState()
    {
        stateManager.weaponActive = false;
        base.setValueAndPlay("Dodge");
        base.stateManager.isOnFloor = false;
    }

    public override void UpdateState()
    {
        if (base.stateManager.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            base.stateManager.isDodging = false;
            stateManager.weaponActive = true;
            base.stateManager.isOnFloor = true;
            base.stateManager.SwitchState(new RunState());
        }
    }
}
