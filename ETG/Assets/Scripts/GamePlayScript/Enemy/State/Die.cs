using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Die : IState
    {
        public override void EnterState()
        {
            manager.animator.Play("Die");
        }
    }
}

