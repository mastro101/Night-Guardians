using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    [RequireComponent(typeof(Animator))]
    public abstract class StateMachineBase : MonoBehaviour
    {
        protected Animator SM;

        protected IStateContext currentContext;

        private void Awake()
        {
            SM = GetComponent<Animator>();
        }

        protected virtual void Start()
        {
            foreach (StateMachineBehaviour smb in SM.GetBehaviours<StateMachineBehaviour>())
            {
                (smb as StateBase).SetUp(currentContext);
            }
        }
    }
}