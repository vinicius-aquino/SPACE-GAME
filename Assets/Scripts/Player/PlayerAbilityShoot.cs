using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    public GunBase gunBase;
    //public Transform gunPosition;
    public FlashColor flashColor;

    private GunBase _currentGun;
   

    protected override void Init()
    {
        base.Init();
        //CreateGun();
        inputs.Gameplay.Shoot.performed += ctx => Startshoot();
        inputs.Gameplay.Shoot.canceled += ctx => Canceledshoot();
    }

    public void DisableAbilityShoot()
    {
        inputs.Gameplay.Shoot.Disable();
    }

    public void EnableAbilityShoot()
    {
        inputs.Gameplay.Shoot.Enable();
    }

    /*private void CreateGun()
    {
        _currentGun = Instantiate(gunBase, gunPosition);

        _currentGun.transform.localPosition = _currentGun.transform.localEulerAngles = Vector3.zero;
    }*/

    private void Startshoot()
    {
        gunBase.StartShoot();
        flashColor?.Flash();
        Debug.Log("start shoot!");
    }

    private void Canceledshoot()
    {
        Debug.Log("canceled shoot!");
        gunBase.StopShoot();
    }
}
