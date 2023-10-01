using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitfallState : IState
{
    public override void EnterState()
    {
        base.stateManager.animator.Play("Pitfall");
    }

    public override void UpdateState()
    {
        if (base.stateManager.animator.GetCurrentAnimatorStateInfo(0).IsName("Pitfall") && base.stateManager.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            base.stateManager.transform.position = base.stateManager.lastPos;
            base.stateManager.animator.Play("PitfallReturn");
            base.stateManager.allowToMove = true;
        }
        if (base.stateManager.animator.GetCurrentAnimatorStateInfo(0).IsName("PitfallReturn") && base.stateManager.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            base.stateManager.SwitchState(new IdleState());
        }
    }
}
