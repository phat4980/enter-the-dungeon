using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy {
    public class StateManager
    { 
        private IState state;
        public Animator animator;

        public StateManager(IState _state, Animator ani)
        {
            state = _state;
            animator = ani;
            state.SetManager(this);
            
        }
        // Start is called before the first frame update
        public void SwithcState(IState _state)
        {
            state = _state;
            state.SetManager(this);
            EnterState();
        }

        // Update is called once per frame
        public void EnterState()
        {
            state.EnterState();
        }

        public void setDirection(string hori, string ver)
        {
            state.setValue(hori,ver);
        }
    }
}
