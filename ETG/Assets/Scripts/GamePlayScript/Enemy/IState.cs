using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public abstract class IState
    {
        protected StateManager manager;
        public void SetManager(StateManager _manager)
        {
            manager = _manager;
        }
        public abstract void EnterState();

        public void setValue(string hori, string ver)
        {
            switch (hori)
            {
                case "L":
                    manager.animator.SetFloat("hori",-1);
                    break;
                case "R":
                    manager.animator.SetFloat("hori", 1);
                    break;
                default:
                    manager.animator.SetFloat("hori", 0);
                    break;
            }

            switch (ver)
            {
                case "U":
                    manager.animator.SetFloat("ver", 1);
                    break;
                case "D":
                    manager.animator.SetFloat("ver", -1);
                    break;
                default:
                    manager.animator.SetFloat("hori", 0);
                    break;
            }
        }
    }

}
