using System.Collections;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    public float slowDownFactor = 0.5f; // adjust as needed
    public float transitionTime = 0.5f; // adjust as needed

    public GameObject slowMotionObj;
    private float originalTimeScale;
    private Coroutine slowMotionCoroutine;

    void Start()
    {
        originalTimeScale = Time.timeScale;
    }

    public void TurnToSlowMotion()
    {
        if (slowMotionCoroutine != null)
        {
            StopCoroutine(slowMotionCoroutine);
        }
        slowMotionCoroutine = StartCoroutine(SlowMotionI());
        slowMotionObj.SetActive(true);
    }

    public void ReturnToNormal()
    {
        if (slowMotionCoroutine != null)
        {
            StopCoroutine(slowMotionCoroutine);
        }
        slowMotionCoroutine = StartCoroutine(ReturnToNormalSpeed());
        slowMotionObj.SetActive(false);
    }
    IEnumerator SlowMotionI()
    {
        float targetTimeScale = originalTimeScale * slowDownFactor;

        while (Time.timeScale > targetTimeScale)
        {
            Time.timeScale -= Time.deltaTime / transitionTime;
            AudioListener.volume = Time.timeScale;
            yield return null;
        }

        Time.timeScale = targetTimeScale;

    }

    IEnumerator ReturnToNormalSpeed()
    {
        float targetTimeScale = originalTimeScale;

        while (Time.timeScale < targetTimeScale)
        {
            Time.timeScale += Time.deltaTime / transitionTime;
            AudioListener.volume = Time.timeScale;
            yield return null;
        }

        Time.timeScale = targetTimeScale;
    }
}