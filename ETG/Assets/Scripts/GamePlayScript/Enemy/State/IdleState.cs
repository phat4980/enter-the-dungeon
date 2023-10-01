using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class IdleState : IState
    {
        
        public override void EnterState()
        {
            manager.animator.Play("Idle");
        }
    }

}
