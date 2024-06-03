using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootLimit : GunBase
{
    public List<UIFillUpdater> uIGunUpdates;
    //public UIFillUpdater uIGunUpdates;

    public float maxShoot = 5f;
    public float timeToRecharge = 1f;

    public float _currentShoot;
    private bool _recharging = false;

    private void Awake()
    {
        GetAllUIs();
    }

    protected override IEnumerator ShootCoroutine()
    {

        if (_recharging) yield break;// ele nao vai permitir a coroutine passar enquanto estiver recarregando.

        while (true)
        {
            if(_currentShoot < maxShoot)
            {
                Shoot();
                _currentShoot++;
                CheckRecharge();
                UpdateUI();
                yield return new WaitForSeconds(timeBetweenShoot);
            }
        }
    }

    private void CheckRecharge()
    {
        if (_currentShoot >= maxShoot)
        {
            StopShoot();
            StartRecharge();
        }
    }

    private void StartRecharge()
    {
        _recharging = true;
        StartCoroutine(RechargeCoroutine());
    }

    IEnumerator RechargeCoroutine()
    {
        float time = 0;
        while(time < timeToRecharge)
        {
            time += Time.deltaTime;
            uIGunUpdates.ForEach(i => i.UpdateValue(time / timeToRecharge));
            yield return new WaitForEndOfFrame();
        }
        _currentShoot = 0;
        _recharging = false;
    }

    private void UpdateUI()
    {
        uIGunUpdates.ForEach(i => i.UpdateValue(maxShoot, _currentShoot));
    }

    private void GetAllUIs()
    {
        //uIGunUpdates = GameObject.FindObjectsOfType<UIFillUpdater>().ToList();//este metodo consume muita memoria, NÃO É ACONSELHAVEU.
    }
}
