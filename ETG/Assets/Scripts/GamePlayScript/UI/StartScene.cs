using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("TheBreach");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
