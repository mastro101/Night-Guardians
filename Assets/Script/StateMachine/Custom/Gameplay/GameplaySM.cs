using UnityEngine;
using System.Collections;

namespace StateMachine.Gameplay
{
    public class GameplaySM : StateMachineBase
    {
        protected override void Start()
        {
            currentContext = new GameplayContext()
            {

            };
            base.Start();
        }

    }

    public class GameplayContext : IStateContext
    {
        
    }

}