using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using Items;

public class ChestBase : MonoBehaviour
{
    public ItemType itemType;
    public ParticleSystem vfx;
    public KeyCode keyCode = KeyCode.Z;
    public Animator animator;
    public string triggerOpen = "Open";
    
    [Header("Notification")]
    public GameObject notification;
    public float tweenDuration = .2f;
    public Ease tweenEase = Ease.OutBack;
    
    [Space]
    public ChestItemBase chestItem;

    [Header("End Game")]
    public EndGame uiEndgame;

    private float startScale;
    private bool _chestOpened = false;

    void Start()
    {
        HideNotification();
        startScale = notification.transform.localScale.x;
    }

    [NaughtyAttributes.Button]
    private void OpenChest()
    {
        if (_chestOpened) return;
        if(vfx != null) vfx.Stop();
        animator.SetTrigger(triggerOpen);
        _chestOpened = true;
        HideNotification();
        Invoke(nameof(ShowItem), 1f);
    }

    private void ShowItem()
    {
        chestItem.ShowItem();
        Invoke(nameof(CollectItem), 1f);
    }

    private void CollectItem()
    {
        chestItem.Collect();
    }

    public void OnTriggerEnter(Collider other)
    {
        Player p = other.transform.GetComponent<Player>();

        if(p != null)
        {
            if(!_chestOpened)
                ShowNotification();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Player p = other.transform.GetComponent<Player>();

        if (p != null)
        {
            HideNotification();
        }
    }

    private void ShowNotification()
    {
        notification.SetActive(true);
        notification.transform.localScale = Vector3.zero;
        notification.transform.DOScale(startScale, tweenDuration).SetEase(tweenEase);
    }

    private void HideNotification()
    {
        notification.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyCode) && notification.activeSelf)
        {
            OpenChest();
            ItemManager.Instance.AddByType(itemType);
            if (transform.CompareTag("ChestPremium"))
            {
                if (uiEndgame != null) uiEndgame.ShowEndGame();
            }
        }
    }
}