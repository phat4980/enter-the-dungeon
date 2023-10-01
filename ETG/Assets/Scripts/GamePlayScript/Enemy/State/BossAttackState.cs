using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class BossAttackState : IState
    {
        public override void EnterState()
        {
            manager.animator.Play("Attack");
        }
    }
}

