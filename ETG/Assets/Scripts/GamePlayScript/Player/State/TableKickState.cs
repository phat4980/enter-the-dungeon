using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableKickState : IState
{
    public override void EnterState()
    {
        setValueAndPlay("TableKick");
    }

    public override void UpdateState()
    {
        if(stateManager.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            stateManager.SwitchState(new IdleState());
        }
    }
}
