using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using Animation;


namespace Enemy
{
    public class EnemyBase : MonoBehaviour, IDamageable
    {
        public Collider collider;
        public FlashColor flashColor;
        public ParticleSystem particleSystem;
        public float startLife = 10f;
        public bool lookAtPlayer = false;

        [SerializeField]private float _currentLife;

        [Header("Animation")]
        [SerializeField] protected AnimationBase _animationBase;

        [Header("Start Animation")]
        public float startAnimationDuration = .2f;
        public Ease startAnimationEase = Ease.OutBack;
        public bool startWithBornAnimation = true;

        [Header("Events")]
        public UnityEvent onKillEvent;
        
        protected Player _player;

        private void Awake()
        {

            Init();
        }

        private void Start()
        {
            _player = GameObject.FindObjectOfType<Player>();
        }

        protected virtual void ResetLife()
        {
            _currentLife = startLife;
        }

        protected virtual void Init()
        {
            ResetLife();

            if(startWithBornAnimation)
                BornAnimation();
        }

        protected virtual void Kill()
        {
            onKill();
        }
        protected virtual void onKill() 
        {
            if (collider != null) collider.enabled = false;
            Destroy(gameObject, 3f);
            PlayAnimationByType(AnimationType.DEATH);
            onKillEvent?.Invoke();
        }

        public void onDamage(float f)
        {
            if (flashColor != null) flashColor.Flash();
            if (particleSystem != null) particleSystem.Emit(15);
            transform.position -= transform.forward;
            _currentLife -= f;

            if(_currentLife <= 0)
            {
                Kill();
            }
        }

        #region ANIMATION
        private void BornAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }

        public void PlayAnimationByType(AnimationType animationType)
        {
            _animationBase.PlayAnimationByType(animationType);
        }
        #endregion

        /* Algo ira dar dano, ele vai buscar pra ver se tem este metado DAMAGE(FLOAT F), aplicavel
         * se ele estiver o metado ele vai substituir o floar f pela qtd de dano que deseja.
         * da ai vc aplica seu metodo de calculo.
         * no codigo abaixo a variavel idamage vai entrar no metedo onDamge(),
         * logo o quem estiver buscando a interface idamageable pra aplicar o dano desejado
         * vai colocar no metado da interface a qtd de dano que deseja e ira vir pra ca
         * o idamage, que ira entrar no calculo ondamage().
         */
        public void Damage(float idamage)//este damage é do idamageable.
        {
            Debug.Log("Damage");
            onDamage(idamage);
        }
        public void Damage(float idamage, Vector3 dir)
        {
            Debug.Log("Damage");
            onDamage(idamage);
            transform.DOMove(transform.position - dir, .1f);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Player p = collision.transform.GetComponent<Player>();

            if(p != null)
            {
                p.healthBase.Damage(2);
            }
        }

        public virtual void Update()
        {
            if (_player != null && _currentLife > 0)
            {
                if (lookAtPlayer)
                {
                    transform.LookAt(_player.transform.position);
                }
            }
        }
    }
}