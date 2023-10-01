using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipOpeningScene : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("Start");
        }
    }
}
