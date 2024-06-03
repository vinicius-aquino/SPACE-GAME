using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using Cloth;

public class Player : Singleton<Player>  //, IDamageable
{
    public List<Collider> colliders;
    public Animator animator;
    public CharacterController characterController;
    public float speed = 1f;
    public float turnSpeed = 1f;
    public float gravity = -9.8f;
    public float jumpSpeed = 15f;
    //public List<Transform> playerPosition;

    private float vSpeed = 0f;
    private bool _alive = true;
    private PlayerAbilityShoot _abilityShoot;
    private bool _jumping = false;

    public KeyCode jumpKeyCode = KeyCode.Space;
    [Header("Run Setup")]
    public KeyCode keyRun = KeyCode.LeftShift;
    public float speedRun = 1.5f;

    [Header("Flash")]
    public List<FlashColor> flashColors;
  
    [Header("Life")]
    public HealthBase healthBase;

    [Space]
    [SerializeField]private ClothChanger _clothChanger;


    private void OnValidate()
    {
        if (healthBase == null) healthBase = GetComponent<HealthBase>();
        if (_abilityShoot == null) _abilityShoot = GetComponent<PlayerAbilityShoot>();
    }

    protected override void Awake()
    {
        base.Awake();
        OnValidate();
        healthBase.OnDamage += OnDamage;
        healthBase.OnKill += OnKill;
    }

    #region LIFE
    private void OnKill(HealthBase h)
    {
        _alive = false;
        animator.SetTrigger("Death");
        _abilityShoot.DisableAbilityShoot();
        colliders.ForEach(i => i.enabled = false);

        Invoke(nameof(Revive), 3f);
    }

    public void Revive()
    {
        _alive = true;
        healthBase.ResetLife();
        animator.SetTrigger("Revive");
        _abilityShoot.EnableAbilityShoot();
        Respawn();
        Invoke(nameof(TurnOnCollider), .1f);
    }

    private void TurnOnCollider()
    {
        colliders.ForEach(i => i.enabled = true);      
    }

    public void OnDamage(HealthBase h)
    {
        flashColors.ForEach(i => i.Flash());
        EffectsManager.Instance.ChangeVignette();
        ShakeCamera.Instance.ShakeDamage();
    }
    #endregion

    private void Update()
    {
        if (_alive)
        {
            transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);

            var inputAxisVertical = Input.GetAxis("Vertical");
            var speedVector = transform.forward * inputAxisVertical * speed;

            if (characterController.isGrounded)
            {
                if (_jumping)
                {
                    _jumping = false;
                    animator.SetTrigger("Land");
                }

                vSpeed = 0;
                if (Input.GetKeyDown(jumpKeyCode))
                {
                    vSpeed = jumpSpeed;

                    if (!_jumping)
                    {
                        _jumping = true;
                        animator.SetTrigger("Jump");
                    }
                }
            }

            vSpeed -= gravity * Time.deltaTime;
            speedVector.y = vSpeed;

            var isWalking = inputAxisVertical != 0;
            if (isWalking)
            {
                if (Input.GetKey(keyRun))
                {
                    speedVector *= speedRun;
                    animator.speed = speedRun;
                }
                else
                {
                    animator.speed = 1;
                }

            }

            characterController.Move(speedVector * Time.deltaTime);

            animator.SetBool("Run", inputAxisVertical != 0);

            /*if(inputAxisVertical != 0)
            {
                animator.SetBool("Run", true);
            }
            else
            {
                animator.SetBool("Run", false);
            }*/
        }
    }

    [NaughtyAttributes.Button]
    public void Respawn()
    {
        if (CheckpointManager.Instance.HasCheckpoint())
        {
            transform.position = CheckpointManager.Instance.GetPositionFromLastCheckpoint();
        }
    }

    public void ChangeSpeed(float speed, float duration)
    {
        StartCoroutine(ChangeSpeedCoroutine(speed, duration));
    }

    IEnumerator ChangeSpeedCoroutine(float localSpeed, float duration)
    {
        var defaultSpeed = speed;
        speed = localSpeed;
        yield return new WaitForSeconds(duration);
        speed = defaultSpeed;
    }

    public void ChangeTexture(ClothSetup setup, float duration)
    {
        StartCoroutine(ChangeTextureCoroutine(setup, duration));
    }

    IEnumerator ChangeTextureCoroutine(ClothSetup setup, float duration)
    {
        _clothChanger.ChangeTexture(setup);
        yield return new WaitForSeconds(duration);
        _clothChanger.ResetTexture();
    }
}