using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController gController { get; private set; }
    public GameObject playerObj; // Player Game Obj
    public bool isWin = false;
    [Header("Character Data")]
    public Player playerData; // Data while explore chamber
    [Header("User Data")]
    public UserData userData; // Data storage per time when meet new weapon, item, enemy, boss

    
    private void Awake()
    {
        if (gController != null && gController != this)
        {
            Destroy(this);
        }
        else
        {
            gController = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(playerObj == null) ChoosePlayer();
        if (SceneManager.GetActiveScene().name == "Chamber1") DontDestroyOnLoad(GameObject.Find("ObjPool"));
    }

    void GameOver() //Handle lose or win game event
    {

    }
    public RaycastHit2D hitObj()
    {
        Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero);
        return hit;
    }

    void ChoosePlayer()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = hitObj();
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    playerObj = hit.collider.gameObject;
                    if (playerObj.name == "Hunter")
                    {
                        playerObj.GetComponent<PlayerController>().enabled = true;
                        playerObj.GetComponent<StateManager>().enabled = true;
                        foreach (CharacterData c in GameObject.Find("CharacterData")?.GetComponent<Characters>().characterData)
                        {
                            if(c.name == playerObj.name)
                            {
                                playerData = new Player(c);
                                playerObj.GetComponent<PlayerController>().player = playerData;
                                foreach (GameObject w in c.weapons)
                                {
                                    playerObj.GetComponent<PlayerController>().AddWeapon(w,false);
                                }
                                break;
                            }
                        }
                        DontDestroyOnLoad(playerObj);
                    }
                }
            }
        }
    }
}
