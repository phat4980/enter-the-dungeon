using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using FMOD.Studio;
using Enemy;

public class StateManager : MonoBehaviour
{
    public static StateManager SInstance { get; private set; }
    IState currentState;
    public IState previousState;

    public GameObject weapon;
    public GameObject hand;

    public PlayerController controller;
    public Animator animator;
    public GameObject clock;
    public SpriteRenderer spriteRenderer;
    public float ver, hori, speed = 5f;
    public Rigidbody2D rb;
    public bool weaponActive = false;
    public bool isDodging = false;
    public bool isInChamber = false;
    public bool isOnFloor;
    public Vector3 lastPos;
    public Vector3 mousePos;
    public bool allowToMove = false;
    public GameObject loading;
    private EventInstance playerFootsteps;
    public bool die = false;

    private void Awake()
    {
        if (SInstance != null && SInstance != this) DestroyImmediate(gameObject);
        else SInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        // Initialization Context in State pattern
        currentState = new SelectState();
        previousState = currentState;
        currentState.SetSide("S", "S");
        currentState.SetManager(this);
        currentState.EnterState();
        playerFootsteps = AudioManager.instance.CreateInstance(FMODEvents.instance.playerFootsteps);
    }

    // Update is called once per frame
    private void Update()
    {
        if (controller.enabled == true && die == false)
        {
            if(SceneManager.GetActiveScene().name != "TheBreach") isInChamber = true;
            else isInChamber = false;
            if (allowToMove == true)
            {
                ver = Input.GetAxis("Vertical");
                hori = Input.GetAxis("Horizontal");
                rb.velocity = new Vector2(hori, ver) * speed;

                UpdateSound();
            }
            else rb.velocity = new Vector2(0, 0) * speed;

            if (Input.GetMouseButtonDown(1) && isDodging == false && rb.velocity != Vector2.zero) isDodging = true;

            currentState.UpdateState();
            if (isOnFloor) lastPos = transform.position;

            if (weaponActive && isInChamber && die != true && controller.win == false) 
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                hand.SetActive(true);
                GetMousePos(mousePos);
            }
            else if(!weaponActive && isInChamber) hand.SetActive(false);
            if (controller.healthSystem.Die() && die == false) 
            {
                die = true;
                weaponActive = false;
                SwitchState(new DeathState());
            }
        }
    }

    public void SwitchState(IState newState)
    {
        if (previousState != currentState) previousState = currentState;
        currentState = newState;
        currentState.SetSide(previousState.previous_side_hori, previousState.previous_side_ver);
        currentState.SetManager(this);
        currentState.EnterState();
    }
    #region Just in Chamber
    private void GetMousePos (Vector3 mousePos) 
    {
        mousePos.z = -10;
        WeaponLookAtPLayer(mousePos);
        if (mousePos.x > transform.position.x + 0.5f) ChangeSideHand(1);

        if (mousePos.x < transform.position.x - 0.5f) ChangeSideHand(0);

        if (mousePos.x <= transform.position.x + 0.5f && mousePos.x >= transform.position.x - 0.5f) ChangeSideHand(1);
    }

    private void WeaponLookAtPLayer(Vector3 mousePos)
    {
        if (weapon != null)
        {
            Vector3 vectorToMousePos = mousePos - hand.transform.position;
            float angle = Mathf.Atan2(vectorToMousePos.y, vectorToMousePos.x) * Mathf.Rad2Deg;
            hand.GetComponent<Rigidbody2D>().rotation = angle;
        }
    }
    private void ChangeSideHand(int i) // Just 0 to turn to left and 1 to turn to right
    {
        if (i == 0) // Right - rotation to left
        {
            hand.transform.localScale = new Vector3(0.8f, -0.8f, 1);
            hand.transform.localPosition = new Vector3(-0.5f, -0.6f, 0);
        }
        if (i == 1) // Left  - rotation to right
        {
            hand.transform.localScale = new Vector3(0.8f, 0.8f, 1);
            hand.transform.localPosition = new Vector3(0.5f, -0.6f, 0);
        }
    }

    #endregion

    #region Collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hole" && isDodging == false && GetComponent<InputManager>().isBlanking == false)
        {
            isOnFloor = false;
            allowToMove = false;
            SwitchState(new PitfallState());
        }
        if (collision.tag == "Floor") isOnFloor = true;
        if (collision.gameObject.tag == "Item") SwitchState(new GetItemState());
        if (collision.gameObject.tag == "Door")
        {
            if (collision.gameObject.transform.parent.name == "Elevator")
            {
                if (collision.gameObject.transform.parent.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
                {
                    string sceneN = collision.gameObject.transform.parent.GetComponent<Elevator>().sceneName;
                    StartCoroutine("LoadScene", sceneN);
                }
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Hole" && isDodging == false && GetComponent<InputManager>().isBlanking == false)
        {
            allowToMove = false;
            SwitchState(new PitfallState());
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            SwitchState(new RunBackDoorWayState());
            StartCoroutine("LoadScene", collision.gameObject.name);
            if (collision.gameObject.name == "Chamber1") weaponActive = true;
        }
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        
        if (collision.collider.tag == "Table" && Input.GetKeyDown(KeyCode.E))
        {
            if (collision.gameObject.GetComponent<Table>().fliped == false)
            {
                collision.gameObject.GetComponent<Table>().fliped = true;
                collision.gameObject.GetComponent<Animator>().SetBool("fliped",true);
                SwitchState(new TableKickState());
            }
        }
    }
    #endregion

    #region Another function
    private IEnumerator LoadScene(string name)
    {
        string levelName = name;

        if(SceneManager.GetActiveScene().name == "Tutorial" && name == "TheBreach") levelName = "TutorialToBreach";

        var scene = SceneManager.LoadSceneAsync(name,LoadSceneMode.Single);

        while (scene.progress < 0.9f) loading.SetActive(true);
        
        foreach (LevelData l in LevelManager.levelManager.levelDatas)
        {
            if (l.name == levelName)
            {
                transform.position = new Vector3(l.posX,l.posY,transform.position.z);
                break;
            }
        }

        yield return new WaitForSeconds(2.5f);
        yield return new WaitForEndOfFrame();
        loading.SetActive(false);
        SwitchState(new IdleState());
    }
    private void UpdateSound()
    {
        if (loading.activeInHierarchy == false && rb.velocity != Vector2.zero && allowToMove == true && die != true)
        {
            PLAYBACK_STATE pLAYBACK_STATE;      // Variable to store the playback state of the sound event
            playerFootsteps.getPlaybackState(out pLAYBACK_STATE);    // Get the playback state of the sound event
            if (pLAYBACK_STATE.Equals(PLAYBACK_STATE.STOPPED))        // If the sound event is stopped, start it
            {
                playerFootsteps.start();
            }
            else playerFootsteps.stop(STOP_MODE.ALLOWFADEOUT);       // If the sound event is already playing, stop it with fadeout
        }
    }
    #endregion
}
