using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cloth;

public class HealthBase : MonoBehaviour, IDamageable
{
    public float startLife = 10f;
    public bool destroyOnKill = false;
    [SerializeField] private float _currentLife;
    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;
    public List <UIFillUpdater> uiUpdate;
    public float damageMultiply = 1;

    public void AddCurrentLife(float f)
    {
        _currentLife += f;
        UpdateUI();
    }

    public float GetCurrentLife()
    {
        return _currentLife;
    }
    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        ResetLife();
    }

    public void ResetLife()
    {
        _currentLife = startLife;
        UpdateUI();
    }

    protected virtual void Kill()
    {
        if(destroyOnKill)
            Destroy(gameObject, .5f);

        OnKill?.Invoke(this);
    }

    public void Damage(float f)
    {
        _currentLife -= f * damageMultiply;

        if (_currentLife <= 0)
        {
            Kill();
            _currentLife = 0;
        }
        UpdateUI();
        OnDamage?.Invoke(this);
    }

    public void Damage(float idamage, Vector3 dir)
    {
        Damage(idamage); 
    }

    private void UpdateUI()
    {
        if(uiUpdate != null)
        {
            uiUpdate.ForEach(i => i.UpdateValue((float) _currentLife / startLife));
        }
    }

    public void ChangeDamageTaken(float damage, float duration)
    {
        StartCoroutine(ChangeDamageTakenCoroutine(damage, duration));
    }

    IEnumerator ChangeDamageTakenCoroutine(float damage, float duration)
    {
        damageMultiply = damage;
        yield return new WaitForSeconds(duration);
        damageMultiply = 1;
    }
}