using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SelectState : IState
{
    public enum state
    {
        SelectIdle,
        SelectLook,
        SelectObserve,
        SelectWind
    }
    
    state currentState = state.SelectIdle;
    
    public override void EnterState()
    {
        base.stateManager.animator.Play(currentState.ToString());
        base.stateManager.StartCoroutine(delay());
    }

    public override void UpdateState()
    {
        if (base.stateManager.controller.enabled == true)
        {
            base.stateManager.animator.Play("IsSelected");
            if (base.stateManager.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                base.stateManager.allowToMove = true;
                base.stateManager.previousState = new IdleState();
                base.stateManager.SwitchState(new IdleState());
            }
        }
        
    }

    IEnumerator delay()
    {
        while(base.stateManager.controller.enabled == false)
        {
            int index = Random.Range(0, 4);
            yield return new WaitForSeconds(3f);
            currentState = (state)(Enum.GetValues(typeof(state)).GetValue(index));
            base.stateManager.animator.Play(currentState.ToString());
            yield return new WaitForSeconds(2f);
            if (currentState == state.SelectObserve || currentState == state.SelectLook)
            {
                yield return new WaitForSeconds(2f);
                base.stateManager.animator.Play(state.SelectIdle.ToString());
            }
        }
    }
}
