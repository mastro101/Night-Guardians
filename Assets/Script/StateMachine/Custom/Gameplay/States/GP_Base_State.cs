using UnityEngine;
using System.Collections;

namespace StateMachine.Gameplay
{
    public abstract class GP_Base_State : StateBase
    {
        protected GameplayContext context;

        protected abstract string stateName { get; }

        public override IState SetUp(IStateContext _context)
        {    
            context = _context as GameplayContext;
            return this;
        }
    

        public override void Enter()
        {
            base.Enter();
            Debug.Log("Enter: " + stateName);
        }

        public override void Tick()
        {
            base.Tick();
            Debug.Log("On: " + stateName);
        }

        public override void Exit()
        {
            base.Exit();
            Debug.Log("Exit: " + stateName);
        }
    }
}