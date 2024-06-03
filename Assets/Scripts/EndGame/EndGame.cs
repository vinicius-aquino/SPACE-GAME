using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using DG.Tweening;

public class EndGame : MonoBehaviour
{
    public List <GameObject> endGameObjects;
    public int currentLevel = 1;

    private bool _endGame = false;

    private void Awake()
    {
        endGameObjects.ForEach(i => i.SetActive(false));
    }

    private void OnTriggerEnter(Collider other)
    {
       Player p = other.transform.GetComponent<Player>();

       if (!_endGame && p != null)
       {
          //ShowEndGame(); 
       }
        
    }

    public void Pause()
    {
        Time.timeScale = 0;
    } 

    public void ShowEndGame()
    {
        _endGame = true;
        endGameObjects.ForEach(i => i.SetActive(true));

        foreach (var i in endGameObjects)
        {
            i.SetActive(true);
            i.transform.DOScale(0, .2f).SetEase(Ease.OutBack).From();
            SaveManager.Instance.SaveLastLevel(currentLevel);
            Invoke(nameof(Pause), 3f);
        }
    }
}
