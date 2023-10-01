using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunBackDoorWayState : IState
{
    public override void EnterState()
    {
        base.stateManager.animator.Play("RunBackDoorWay");
    }

    public override void UpdateState()
    {
        if(base.stateManager.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            // Load new scene
            base.stateManager.SwitchState(new IdleState());
        }
    }
}
