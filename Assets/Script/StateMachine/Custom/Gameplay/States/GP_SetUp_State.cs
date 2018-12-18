using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.Gameplay
{
    public class GP_SetUp_State : GP_Base_State
    {
        protected override string stateName
        {
            get
            {
                return "SetUp";
            }
        }
    }
}