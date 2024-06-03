using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class ItemCollectableCoin : ItemCollectableBase
{
    public Collider collider;
    protected override void onCollect()
    {
        base.onCollect();
        ItemManager.Instance.AddByType(ItemType.COIN);
        collider.enabled = false;
        //VFXManager.Instance.PlayVFXByType(VFXManager.VFXType.COIN, transform.position);
    }
}
