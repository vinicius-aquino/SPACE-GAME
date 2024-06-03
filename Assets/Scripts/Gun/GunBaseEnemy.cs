using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBaseEnemy : MonoBehaviour
{
    public ProjectileBase prefebProjectile;
    public Transform positionToShoot;
    public float timeBetweenShoot = .3f;
    public float speed = 50f;

    private Coroutine _currentCoroutine;

    protected virtual IEnumerator ShootCoroutine()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShoot);

        }
    }

    public virtual void Shoot()
    {
        var projectile = Instantiate(prefebProjectile);
        projectile.transform.position = positionToShoot.position;
        projectile.transform.rotation = positionToShoot.rotation;
        projectile.speed = speed;

        //ShakeCamera.Instance.Shake();
    }

    public void StartShoot()
    {
        StopShoot();
        _currentCoroutine = StartCoroutine(ShootCoroutine());
    }

    public void StopShoot()
    {
        if (_currentCoroutine != null)//quer dizer que esta rodando...a coroutine for null quer dizer que nao esta RODANDO.
            StopCoroutine(_currentCoroutine);
    }
}
