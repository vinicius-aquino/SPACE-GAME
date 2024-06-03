using System;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float TimeToReset = 5f;
    public Vector3 dir;

    public string tagToLook = "Enemy";

    public Action onHitTarget;
    
    void Update()
    {
        transform.Translate(dir * Time.deltaTime);
    }

    public void Awake()
    {
        //Destroy(gameObject, timeToDestroy);
        
    }

    public void startProject()
    {
        Invoke(nameof(finishUsage), TimeToReset);
    }

    private void finishUsage()
    {
        gameObject.SetActive(false);
        onHitTarget = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == tagToLook)
        {
            Destroy(collision.gameObject);
            onHitTarget?.Invoke();
            finishUsage();
        }
    }
}
