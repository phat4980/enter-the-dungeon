using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : IState
{
    public override void EnterState()
    {
        stateManager.animator.Play("Death");
    }

    public override void UpdateState()
    {
        if(stateManager.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            stateManager.clock.SetActive(true);
            if (stateManager.clock.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f) stateManager.animator.Play("DeathShot");
        }
        
    }
}
