using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Publisher
{
    public bool isBlanking = false;
    //Receiver
    public GameObject weaponsUI;
    public GameObject effect;
    public GameObject slowMotion;
    public PlayerController playController;
    public GameObject currentWeapon;
    public GameObject menu;
    //Invoker
    public Invoker invoker;
    
    private void Start()
    {
        playController = GetComponent<PlayerController>();
        invoker = new Invoker();

        ICommand blank = new Blank(effect);
        ICommand openWeaponList = new OpenWeaponList(slowMotion);
        ICommand closeWeaponList = new CloseWeaponList(slowMotion);
        ICommand changeWeapon = new ChangeWeapon(playController, weaponsUI);

        ICommand charge = new Charge();
        ICommand interact = new Interact();
        ICommand openMinimap = new OpenMinimap();
        ICommand openAmmonomicon = new OpenAmmonomicon();

        invoker.SetCommand(blank); // - 0
        invoker.SetCommand(charge); // - 1
        invoker.SetCommand(interact); // - 2
        invoker.SetCommand(openWeaponList); // - 3
        invoker.SetCommand(closeWeaponList); // - 4
        invoker.SetCommand(openMinimap); // - 5
        invoker.SetCommand(openAmmonomicon); // - 6
        invoker.SetCommand(changeWeapon); // - 7
    }
    // Update is called once per frame
    void Update()
    {
        if (GetComponent<StateManager>().die == false && GetComponent<StateManager>().loading.activeInHierarchy == false)
        {
            #region Finished
            if (Input.GetKeyDown(KeyCode.Q) && isBlanking == false && GetComponent<PlayerController>().player.blank > 0)
            {
                isBlanking = true;
                invoker.OnPress(0);
                GetComponent<PlayerController>().player.blank -= 1;
                notify("Blank", "Sub", 0);
            }
            if ((Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))) invoker.OnPress(3);
            if ((Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.RightControl))) invoker.OnPress(4);
            if (Input.GetAxis("ChangeWI") != 0f) invoker.OnPress(7);

            if (Input.GetMouseButtonDown(0) && GetComponent<StateManager>().isInChamber == true)
            {
                if (playController.player.weapons[playController.currentWeapon].name == playController.conditionToWin.name + "(Clone)")
                {
                    playController.player.weapons[playController.currentWeapon].GetComponent<KillThePast>().Fire();
                    playController.win = true;
                }else playController.player.weapons[playController.currentWeapon].GetComponent<Weapon>().ShootingBullet();
            }
            #endregion

            #region Another Action
            if (Input.GetKeyDown(KeyCode.R))
            {
                // Charge
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                // Open ammonomico
            }
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                // Open minimap
            }
            #endregion

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (menu.activeInHierarchy == false) menu.SetActive(true);
                else menu.SetActive(false);
            }
        }
    }

    private void FixedUpdate()
    {
        if (isBlanking == true)
        {
            if (effect.GetComponent<Effect>().animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1) 
            { 
                isBlanking = false;
                effect.GetComponent<Effect>().SetMotion(EffectName.None);
                effect.GetComponent<Effect>().PlayAni();
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            GetComponent<PlayerController>().AddWeapon(collision.gameObject, true);
        }
    }
}
