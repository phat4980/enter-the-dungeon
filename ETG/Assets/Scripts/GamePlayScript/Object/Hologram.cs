using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hologram : MonoBehaviour
{
    public float wait = 5f;
    private Animator ani;
    public enum state
    {
        Fight,
        Rival,
        Poker,
        Intro,
        questing
    }
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        ani.Play(state.Intro.ToString());
        StartCoroutine(PlayAni());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PlayAni()
    {
        while (true)
        {
            int ran = Random.Range(0, 4);
            yield return new WaitForSeconds(wait);
            switch (ran)
            {
                case 0:
                    ani.Play(state.Intro.ToString());
                    break;
                case 1:
                    ani.Play(state.Rival.ToString());
                    break;
                case 2:
                    ani.Play(state.Fight.ToString());
                    break;
                case 3:
                    ani.Play(state.Poker.ToString());
                    break;
                case 4:
                    ani.Play(state.questing.ToString());
                    break;
            }
            yield return new WaitForSeconds(wait);
        }
    }
}
