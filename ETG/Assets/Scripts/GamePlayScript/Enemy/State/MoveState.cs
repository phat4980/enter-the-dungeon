using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class MoveState : IState
    {
        
        public override void EnterState()
        {
            manager.animator.Play("Run");
        }
        
    }
}

