using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : IState
{
    public enum Run
    {
        RunNoHand,
        RunWithHand,
        RunWithHands
    }
    public override void EnterState()
    {
        if (base.stateManager.weaponActive == false)
        {
            RunWithNoHand();
        }
        else
        {
            RunWithHand();
        }
    }

    public override void UpdateState()
    {
        if (base.stateManager.weaponActive == false)
        {
            RunWithNoHand();
        }
        else
        {
            RunWithHand();
        }
    }

    void RunWithNoHand()
    {
        if(base.stateManager.isDodging == true) base.stateManager.SwitchState(new DodgeState());
        else base.setValueAndPlay(Run.RunNoHand.ToString());
        if (base.stateManager.ver == 0 && base.stateManager.hori == 0) base.stateManager.SwitchState(new IdleState());
    }
    void RunWithHand()
    {
        if (base.stateManager.isDodging == true) base.stateManager.SwitchState(new DodgeState());
        else base.setValueAndPlay(Run.RunWithHand.ToString());
        if (base.stateManager.ver == 0 && base.stateManager.hori == 0) base.stateManager.SwitchState(new IdleState());
    }
}
