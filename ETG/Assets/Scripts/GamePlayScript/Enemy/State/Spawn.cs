using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Spawn : IState
    {
        
        public override void EnterState()
        {
            manager.animator.Play("Spawn");
            if(manager.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                manager.SwithcState(new IdleState());
            }
        }

    }

}
