using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void Load(int i)
    {
        SceneManager.LoadScene(i);
        Unpause();
    }

    public void Load(string g)
    {
        SceneManager.LoadScene(g);
        Unpause();
    }

    public void Unpause()
    {
        Time.timeScale = 1;
    }
}
