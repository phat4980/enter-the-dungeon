using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerController : Publisher
{
    public GameObject conditionToWin;
    public Player player;
    public bool onChange = false;
    public bool isFinishedTutorial = false;
    public HeartHealthSystem healthSystem;
    public bool damaged = false;
    public float timer;
    public GameObject hand;
    public int currentWeapon = 0;
    public int kills = 0;
    public float timerBeginWhenPlayerEnterChamber = 0.0f;
    public bool win;
    public GameObject deathUI;
    public Texture2D screenShot;
    public bool screenShotFinish = false;
    public bool stop = false;
    // Start is called before the first frame update
    
    void Start()
    {
        healthSystem = new HeartHealthSystem(player.health);
        notify("All", "", 0);
        player.weapons[currentWeapon].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        SetWeaponForState();
        if (GetComponent<StateManager>().isInChamber == true && GetComponent<StateManager>().die != true) timerBeginWhenPlayerEnterChamber += Time.deltaTime;
        else if((GetComponent<StateManager>().isInChamber == true && GetComponent<StateManager>().die == true) || win == true) timerBeginWhenPlayerEnterChamber += 0;
        if (damaged == true) timer += Time.deltaTime;
        if(GetComponent<StateManager>().die == true && win != true)
        {
            if (GetComponent<StateManager>().die) Time.timeScale = 0;
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 4, 3); 
            StartCoroutine(Capture());
            screenShotFinish = true;
            StopCoroutine(Capture());
        }
        if (GetComponent<StateManager>().die == true && win != true && stop == false)
        {
            if (screenShotFinish == true) deathUI.GetComponent<DeathUI>().SetForLose(GetTime(), player.shell, kills, "???", player.weapons, screenShot);
            stop = true;
        }
        else if (GetComponent<StateManager>().die != true && win == true && stop == false)
        {
            deathUI.GetComponent<DeathUI>().SetForWin(GetTime(), player.shell, kills, player.weapons);
            stop = true;
        }
        
    }
    private void FixedUpdate()
    {
        if(timer > 1.5f)
        {
            damaged = false;
            timer = 0;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hole" && GetComponent<StateManager>().loading.activeInHierarchy == false && GetComponent<StateManager>().weaponActive == true && GetComponent<InputManager>().isBlanking == false && damaged == false)
        {
            DecreaseShieldOrHeart();

        }
        if ((collision.gameObject.tag == "EnemyBullet") && GetComponent<StateManager>().loading.activeInHierarchy == false && GetComponent<StateManager>().weaponActive == true && GetComponent<InputManager>().isBlanking == false && damaged == false)
        {
            DecreaseShieldOrHeart();
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            DecreaseShieldOrHeart();
        }
    }

    void DecreaseShieldOrHeart()
    {
        damaged = true;
        if (player.shield > 0)
        {
            GetComponent<InputManager>().isBlanking = true;
            player.shield -= 1;
            GetComponent<InputManager>().invoker.OnPress(0);
            notify("Shield", "Sub", 0);
        }
        else if (player.shield == 0)
        {
            healthSystem.Damage();
            notify("HP", "", 0);
        }
    }

    public void AddWeapon(GameObject weapon, bool more)
    {
        weapon.GetComponent<Collider2D>().enabled = false;
        weapon.GetComponent<Rigidbody2D>().simulated = false;
        if (more == false)
        {
            GameObject w = Instantiate(weapon, Vector3.zero, Quaternion.identity) as GameObject;
            w.transform.SetParent(hand.transform);
            w.transform.localPosition = weapon.GetComponent<Weapon>().posInHand;
            player.addInList(w);
            w.SetActive(false);
        }
        if (more == true)
        {
            weapon.transform.SetParent(hand.transform);
            if (weapon.GetComponent<KillThePast>() != null) weapon.transform.localPosition = weapon.GetComponent<KillThePast>().posInHand;
            else weapon.transform.localPosition = weapon.GetComponent<Weapon>().posInHand;
            player.addInList(weapon);
            weapon.SetActive(false);
            notify("WeaponList", "", 0);
        }
    }

    public void SetWeaponForState()
    {
        for (int i = 0; i < player.weapons.Count; i++)
        {
            GameObject w = player.weapons[i];
            if (w.activeInHierarchy == true)
            {
                currentWeapon = i;
                GetComponent<StateManager>().weapon = w;
                return;
            }
        }
        GetComponent<StateManager>().weapon = player.weapons[0];
    }

    public string GetTime()
    {
        string time;
        int min = (Mathf.RoundToInt(timerBeginWhenPlayerEnterChamber) % 3600) / 60;
        int second = Mathf.RoundToInt(timerBeginWhenPlayerEnterChamber) % 60;
        int hour = Mathf.RoundToInt(timerBeginWhenPlayerEnterChamber) / 3600;
        string secondS = (second < 10) ? "0" + second.ToString() : second.ToString();
        string minS = (min < 10) ? "0" + min.ToString() : min.ToString();
        string hourS = (hour < 10) ? "0" + hour.ToString() : hour.ToString();
        time = hourS + ":" + minS + ":" + secondS;
        return time;
    }

    public IEnumerator Capture()
    {
        yield return new WaitForEndOfFrame();
        Texture2D texture = ScreenCapture.CaptureScreenshotAsTexture();
        screenShot = texture;
        yield return null;
    }
}
