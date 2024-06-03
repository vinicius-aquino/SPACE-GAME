using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Ebac.StateMachine
{
    public class StateBase
    {
        public virtual void onStateEnter(params object[] objs)
        {
            //Debug.Log("onStateEnter");
        }
        public virtual void onStateStay()
        {
            //Debug.Log("onStateStay");
        }

        public virtual void onStateExit()
        {
            //Debug.Log("onStateExit");
        }
    }
}