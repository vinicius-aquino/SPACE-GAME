using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;

namespace Boss
{
    public class BossStateBase : StateBase
    {
        protected BossBase boss;

        public override void onStateEnter(params object[] objs)
        {
            base.onStateEnter(objs);
            boss = (BossBase)objs[0];
        }
    }

    public class BossStateInit : BossStateBase
    {
        public override void onStateEnter(params object[] objs)
        {
            base.onStateEnter(objs);
            boss.StartInitAnimation();
        }
    }

    public class BossStateWalk : BossStateBase
    {
        public override void onStateEnter(params object[] objs)
        {
            base.onStateEnter(objs);
            boss.GoToRandomPoint(OnArrive);
        }

        private void OnArrive()
        {
            boss.SwitchState(BossAction.ATTACK);
        }

        public override void onStateExit()
        {
            Debug.Log("exit walk");
            base.onStateExit();
            boss.StopAllCoroutines();
        }
    }

    public class BossStateAttack : BossStateBase
    {
        public override void onStateEnter(params object[] objs)
        {
            base.onStateEnter(objs);
            boss.StartAttack(EndAttacks);
        }

        private void EndAttacks()
        {
            boss.SwitchState(BossAction.WALK);
        }

        public override void onStateExit()
        {
            Debug.Log("exit attack");
            base.onStateExit();
            boss.StopAllCoroutines();
        }
    }

    public class BossStateDeath : BossStateBase
    {
        public override void onStateEnter(params object[] objs)
        {
            Debug.Log("enter death");
            base.onStateEnter(objs);
            //boss.transform.localScale = Vector3.one * .2f;
        }
    }
}