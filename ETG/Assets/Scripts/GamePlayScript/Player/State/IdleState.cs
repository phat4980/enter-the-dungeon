using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class IdleState : IState
{
    public enum Idle
    {
        IdleNoHand,
        IdleWithHand,
        IdleWithHands
    }
    public Idle currentState = Idle.IdleNoHand;
    public override void EnterState()
    {
        if (base.stateManager.ver != 0.0f || base.stateManager.hori != 0.0f)
        {
            base.stateManager.SwitchState(new RunState());
        }
        NotInChamberSetSide();
    }
    public override void UpdateState()
    {
        if (base.stateManager.ver != 0.0f || base.stateManager.hori != 0.0f)
        {
            base.stateManager.SwitchState(new RunState());
        }
    }

    void NotInChamberSetSide()
    {
        setValueAndPlay(Idle.IdleNoHand.ToString());
    }

    void InChamberSetSide()
    {
        setValueAndPlay(Idle.IdleWithHand.ToString());
    }
}
