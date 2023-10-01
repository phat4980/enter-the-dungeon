using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public List<GameObject> currentInScene;
    public List<GameObject> prefabDontDestroyOnLoadObj;
     
    public void OnEnable()
    {
        Time.timeScale = 0;
    }
    public void Restart()
    {
        foreach (GameObject go in currentInScene)
        {
            Destroy(go);
        }
        SceneManager.LoadScene(1);
    }
    public void Continue()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void OnDisable()
    {
        Time.timeScale = 1;
    }
}
