using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ItemCollectableBase : MonoBehaviour
    {
        public SFXType sfxType;

        public ItemType itemType;
        public string compareTag = "Player";
        public ParticleSystem particleSystem;
        public float TimeToHide = 3;
        public GameObject graphicItem;
        //public Collider collider;
        //public Collider collider2;
        public List<Collider> colliders;

        private bool _checkCollected = false;

        [Header("Audio source")]
        public AudioSource audioSource;

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.transform.CompareTag(compareTag))
            {
                if(!_checkCollected)
                    Collect();
            }
        }

        private void PlaySFX()
        {
            SFXPool.Instance.Play(sfxType);
        }

        protected virtual void Collect()
        {
            PlaySFX();
            if (colliders != null) colliders.ForEach(i => i.enabled = false);
            //if (collider != null) collider.enabled = false;
            //if (collider2 != null) collider2.enabled = false;
            if (graphicItem != null) graphicItem.SetActive(false);
            Invoke("HideToObject", TimeToHide);
            onCollect();
            _checkCollected = true;
        }

        private void HideToObject()
        {
            gameObject.SetActive(false);
        }

        protected virtual void onCollect()
        {
            //VFXManager.Instance.PlayVFXByType(VFXManager.VFXType.COIN, transform.position);
            if (particleSystem != null) particleSystem.Play();
            if (audioSource != null) audioSource.Play();
            ItemManager.Instance.AddByType(itemType);
        }
    }
}