using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItemState : IState
{
    public override void EnterState()
    {
        stateManager.animator.Play("GetItem");
    }

    public override void UpdateState()
    {
        stateManager.SwitchState(new IdleState());
    }
}
