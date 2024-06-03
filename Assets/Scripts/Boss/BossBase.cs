using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;
using DG.Tweening; 

namespace Boss
{
    public enum BossAction
    {
        INIT,
        IDLE,
        WALK,
        ATTACK,
        DEATH
    }
    public class BossBase : MonoBehaviour
    {
        [Header("Animation")]
        public Animator animator;
        public float startAnimationDuration = .5f;
        public Ease startAnimationEase = Ease.OutBack;

        [Header("Flash")]
        public List<FlashColor> flashColors;

        [Header("Attack")]
        public int attackAmount = 5;
        public float timeBetweenAttacks = .5f;

        public float speedWalk = 5f;
        public List<Transform> wayPoints;
        public HealthBase healthBase;
        public bool lookAtPlayer = false;
        public Collider collider;

        private StateMachine<BossAction> stateMachine;
        private Player _player;
        private bool _startBorn = false;
        private bool _alive = true;

        public bool GetAlive()
        {
            return _alive;
        }

        private void OnValidate()
        {
            if (healthBase == null) healthBase = GetComponent<HealthBase>();           
        }

        private void Awake()
        {
            Init();
            OnValidate();
            if (_player == null) _player = GameObject.FindObjectOfType<Player>();
            if (healthBase != null)
            {
                healthBase.OnKill += OnBossKill;
                healthBase.OnDamage += onBossDamage;
            }
        }

        private void Init()
        {
            stateMachine = new StateMachine<BossAction>();
            stateMachine.Init();

            stateMachine.RegisterStates(BossAction.INIT,new BossStateInit());
            stateMachine.RegisterStates(BossAction.WALK,new BossStateWalk());
            stateMachine.RegisterStates(BossAction.ATTACK,new BossStateAttack());
            stateMachine.RegisterStates(BossAction.DEATH,new BossStateDeath());
        }

        private void OnBossKill(HealthBase h)
        {
            _alive = false;
            animator.SetTrigger("Death");
            SwitchState(BossAction.DEATH);
            collider.enabled = false;
        }

        private void onBossDamage(HealthBase h)
        {
            flashColors.ForEach(i => i.Flash());
        }

        #region ATTACK
        public void StartAttack(Action endCallback = null)
        {
            StartCoroutine(AttackCoroutine(endCallback));
        }

        IEnumerator AttackCoroutine(Action endCallback = null)
        {
            int attack = 0;
            while (attack < attackAmount)
            {
                attack++;
                animator.SetTrigger("Attack");
                //transform.DOScale(1.1f, .1f).SetLoops(2, LoopType.Yoyo);
                yield return new WaitForSeconds(timeBetweenAttacks);
            }
            endCallback?.Invoke();
        }
        public void AttackDamage()
        {
            if (_player != null)
            {
                _player.healthBase.Damage(10);
            }
        }
        #endregion

        public void StartAction()
        {
            if (!_startBorn && _alive)
            {
                //SwitchInit(); 
                SwitchAttack();
                SwitchWalk();
            }
            
            _startBorn = true;
        }

        private void Update()
        {
            if (_player != null && healthBase.GetCurrentLife() > 0)
            {
                if (lookAtPlayer)
                {
                    transform.LookAt(_player.transform.position);
                }
            }
        }

        #region WALK
        public void GoToRandomPoint(Action onArrive = null)
        {
            StartCoroutine(GoToPointCoroutine(wayPoints[UnityEngine.Random.Range(0, wayPoints.Count)], onArrive));
            //StartCoroutine(GoToPointCoroutine(_player.playerPosition[UnityEngine.Random.Range(0, _player.playerPosition.Count)], onArrive));
        }
        IEnumerator GoToPointCoroutine(Transform t, Action onArrive = null)
        {
             while (Vector3.Distance(transform.position, t.position) > 1f)
             {
                 transform.position = Vector3.MoveTowards(transform.position, t.position, Time.deltaTime * speedWalk);
                 yield return new WaitForEndOfFrame();
             }
             onArrive?.Invoke();
        }
        #endregion

        #region ANIMATION
        public void StartInitAnimation()
        {          
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }
        #endregion

        #region DEBUG
        [NaughtyAttributes.Button]
        private void SwitchInit()
        {
            SwitchState(BossAction.INIT);
        }

        [NaughtyAttributes.Button]
        private void SwitchWalk()
        {
            SwitchState(BossAction.WALK);
        }

        [NaughtyAttributes.Button]
        private void SwitchAttack()
        {
            SwitchState(BossAction.ATTACK);
        }
        #endregion

        #region STATE MACHINE
        public void SwitchState(BossAction state)
        {
            stateMachine.SwitchState(state, this);
        }
        #endregion
    }
}