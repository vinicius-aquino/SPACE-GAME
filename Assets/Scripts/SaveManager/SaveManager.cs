using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField]private SaveSetup _saveSetup;

    //normalmente se salva na pasta do jogador(persistenDataPath)...datapath salva em assets no projeto...o streamingassets cria uma pasta no projeto
    private string _path = Application.streamingAssetsPath + "/save.txt";

    public int lastLevel;
    public Action<SaveSetup> FileLoaded;

    public SaveSetup setup
    {
        get { return _saveSetup; }
    }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public void CreateNewSave()
    {
        _saveSetup = new SaveSetup();
        _saveSetup.lastLevel = 0;
        _saveSetup.coins = 0;
        _saveSetup.healthPack = 0;
        _saveSetup.playerName = "cororin";

        Save();  
    }

    private void Start()
    {
        Invoke(nameof(Load), .1f);
    }

    #region SAVE
    [NaughtyAttributes.Button]
    private void Save()
    {
        string setupToJason = JsonUtility.ToJson(_saveSetup, true);
        Debug.Log(setupToJason);
        SaveFile(setupToJason);
    }

    public void SaveItens()
    {
        _saveSetup.coins = Items.ItemManager.Instance.GetItemByType(Items.ItemType.COIN).soInt.value;
        _saveSetup.healthPack = Items.ItemManager.Instance.GetItemByType(Items.ItemType.LIFE_PACK).soInt.value;
        _saveSetup.chest = Items.ItemManager.Instance.GetItemByType(Items.ItemType.CHEST).soInt.value;  
        Save();
    }

    public void SaveLastLevel(int level)
    {
        _saveSetup.lastLevel = level;
        SaveItens();
        Save();
    }

    public void SaveName(string text)
    {
        _saveSetup.playerName = text;
        Save();
    }
    #endregion

    private void SaveFile(string json)
    {
        Debug.Log(_path);
        File.WriteAllText(_path, json);
    }

    [NaughtyAttributes.Button]
    private void Load()
    {
        string fileLoaded = "";

        if (File.Exists(_path))
        {
            fileLoaded = File.ReadAllText(_path);
            _saveSetup = JsonUtility.FromJson<SaveSetup>(fileLoaded);
            lastLevel = _saveSetup.lastLevel;  
        }
        else
        {
            CreateNewSave(); 
            Save();
        }


        FileLoaded.Invoke(_saveSetup);
    }

    [NaughtyAttributes.Button]
    private void SaveLevelOne()
    {
        SaveLastLevel(1);
    }
    [NaughtyAttributes.Button]
    private void SaveLevelFive()
    {
        SaveLastLevel(5);
    }
}

[System.Serializable]
public class SaveSetup
{
    public int lastLevel;
    public float coins;
    public float healthPack;
    public float chest;
    //public float life;
    public string playerName;
}