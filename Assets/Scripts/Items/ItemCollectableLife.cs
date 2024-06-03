using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class ItemCollectableLife : ItemCollectableBase
{
    public Collider collider;
    protected override void onCollect()
    {
        base.onCollect();
        ItemManager.Instance.AddByType(ItemType.LIFE_PACK);
        collider.enabled = false;
        //VFXManager.Instance.PlayVFXByType(VFXManager.VFXType.LIFE, transform.position);
    }
}
