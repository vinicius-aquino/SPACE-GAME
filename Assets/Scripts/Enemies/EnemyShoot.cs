using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyShoot : EnemyBase
    {
        public GunBaseEnemy gunEnemy;
        private bool _playerAlive = true;

        protected override void Init()
        {
            base.Init();
            gunEnemy.StartShoot();
            
        }

        protected override void onKill()
        {
            base.onKill();
            gunEnemy.StopShoot();
        }

        public override void Update()
        {
            base.Update();
            if(!CheckPlayer())
            {
                //gunEnemy.StopShoot();
            }
            else
            {
                
            }
        }

        public bool CheckPlayer()
        {
            if (_player.healthBase.GetCurrentLife() <= 0)
            {
                _playerAlive = false;
            }
            else
            {
                _playerAlive = true;
            }

            return _playerAlive;
        }
    }
}