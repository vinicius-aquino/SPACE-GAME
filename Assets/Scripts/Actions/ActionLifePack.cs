using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class ActionLifePack : MonoBehaviour
{
    public KeyCode keyCode = KeyCode.L;
    public SOInt soInt;
    public float lifePackRecover = 3;
    //public float lifePercentRecover = 30;

    private void Start()
    {
        soInt = ItemManager.Instance.GetItemByType(ItemType.LIFE_PACK).soInt;
    }

    private void RecoverLife()
    {
        if(soInt.value > 0)
        {
            ItemManager.Instance.RemoveByType(ItemType.LIFE_PACK);
            //Player.Instance.healthBase.ResetLife();
            //var recoverPercent = (lifePercentRecover + startlife) / 100
            var totalRecover = lifePackRecover + Player.Instance.healthBase.GetCurrentLife();
            
            if (totalRecover > Player.Instance.healthBase.startLife)
            {
                Player.Instance.healthBase.ResetLife();
            }
            else
            {
                Player.Instance.healthBase.AddCurrentLife(lifePackRecover);
                Debug.Log(Player.Instance.healthBase.GetCurrentLife());
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            RecoverLife();
        }
    }
}