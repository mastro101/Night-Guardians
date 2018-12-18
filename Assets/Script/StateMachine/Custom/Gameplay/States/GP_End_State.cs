using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.Gameplay
{
    public class GP_End_State : GP_Base_State
    {
        protected override string stateName
        {
            get
            {
                return "End";
            }
        }
    }
}