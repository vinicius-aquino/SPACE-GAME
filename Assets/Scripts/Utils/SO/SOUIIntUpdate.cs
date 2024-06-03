using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SOUIIntUpdate : MonoBehaviour
{
    public SOInt soInt;
    public SOInt soLife;
    public SOInt soChest;
    public TextMeshProUGUI uiTextValue;
    public TextMeshProUGUI uiTextLife;
    public TextMeshProUGUI uiTextChest;

    // Start is called before the first frame update
    void Start()
    {
        uiTextValue.text = "x " + soInt.value.ToString();
        uiTextLife.text = "LIFE x " + soLife.value.ToString();
        uiTextChest.text = "CHEST x " + soChest.value.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        uiTextValue.text = "x " + soInt.value.ToString();       
        uiTextLife.text = "LIFE x " + soLife.value.ToString();
        uiTextChest.text = "CHEST x " + soChest.value.ToString();
        
    }
}
