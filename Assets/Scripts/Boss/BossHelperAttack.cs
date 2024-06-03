using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Boss
{
    public class BossHelperAttack : MonoBehaviour
    {
        public BossBase boss;

        public void AttackHelper()
        {
                boss.AttackDamage();
            
        }       
    }
}