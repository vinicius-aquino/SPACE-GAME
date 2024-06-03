using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void Damage(float f);
    void Damage(float f, Vector3 dir);
}
