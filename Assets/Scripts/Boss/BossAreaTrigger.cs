using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boss
{
    public class BossAreaTrigger : MonoBehaviour
    {
        public BossBase boss;
        public GameObject bossGraphics;
        public GameObject bossCamera;
        public GameObject bossLife;
        public string TagToCheck = "Player";
        public Color gizmoColor = Color.yellow;
        public float TimeToSetOffCamera = 1f;

        private void Awake()
        {
            TurnCameraOff();
            bossLife.SetActive(false);
            bossGraphics.SetActive(false);
        }

        private void Update()
        {
            if (!boss.GetAlive())
            {
                Invoke(nameof(TurnCameraOff), TimeToSetOffCamera);
                bossLife.SetActive(false);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(boss != null)
            {
                if(other.transform.tag == TagToCheck)
                {
                    TurnBossOn();
                    TurnCameraOn();
                    bossLife.SetActive(true);
                }
            }
        }

        private void TurnCameraOn()
        {
            bossCamera.SetActive(true);
        }

        private void TurnCameraOff()
        {
            bossCamera.SetActive(false);
        }

        private void TurnBossOn()
        {
            bossGraphics.SetActive(true);
            boss.StartAction();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawSphere(transform.position , transform.localScale.y);
        }
    }
}
