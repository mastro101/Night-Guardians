using UnityEngine;
using System.Collections;

namespace StateMachine
{
    public abstract class StateBase : StateMachineBehaviour, IState
    {
        public abstract IState SetUp(IStateContext context);

        /// <summary>
        /// Chiamato quando entra nello stato
        /// </summary>
        public virtual void Enter()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Chiamato finchè non si cambia lo stato attivo
        /// </summary>
        public virtual void Tick()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Chiamato all'uscita dello stato
        /// </summary>
        public virtual void Exit()
        {
            throw new System.NotImplementedException();
        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            Enter();
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);
            Tick();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
            Exit();
        }

    }

    public interface IState
    {
        void Enter();

        void Tick();

        void Exit();
    }

    public interface IStateContext
    {

    }
}