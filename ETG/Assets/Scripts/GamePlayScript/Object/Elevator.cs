using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Elevator : MonoBehaviour
{
    public Collider2D under;
    public bool comeAndGo;
    public string sceneName;
    private void Update()
    {
        if (comeAndGo == false && UIController.Instance.loading.activeInHierarchy == false)
        {
            GetComponent<Animator>().Play("Open");
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && comeAndGo == true)
        {
            GetComponent<Animator>().Play("Close");
        }
    }
}
