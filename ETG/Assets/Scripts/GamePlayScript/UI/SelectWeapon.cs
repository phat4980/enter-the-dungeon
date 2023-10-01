using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectWeapon : MonoBehaviour
{
    public PlayerController playerController;
    public List<GameObject> weapons;
    public int selectedWeapon = 0;
    public int previousWeapon;
    // Start is called before the first frame update
    void Start()
    {
        weapons = new List<GameObject>();
        GetCurrentWeapons();
    }
    // Update is called once per frame
    public void ChooseWeapon(float mouseWheelValue)
    {
        previousWeapon = selectedWeapon;
        if (mouseWheelValue > 0f)
        {
            if (selectedWeapon >= playerController.hand.transform.childCount - 1) selectedWeapon = 0;
            else selectedWeapon++;
        }
        if (mouseWheelValue < 0f)
        {
            if (selectedWeapon <= 0) selectedWeapon = playerController.hand.transform.childCount - 1;
            else selectedWeapon--;
        }
        if (previousWeapon != selectedWeapon) IdentifyWeapon();
    }
    private void IdentifyWeapon()
    {
        int i = 0;
        foreach (GameObject weapon in weapons)
        {
            if (i == selectedWeapon) weapon.SetActive(true);
            else weapon.SetActive(false);
            i++;
        }
    }
    public void GetCurrentWeapons()
    {
        if (weapons.Count != 0)
        {
            AddWeapon();
        }
        else
        {
            foreach (GameObject w in playerController.player.weapons)
            {
                GameObject weapon = new GameObject(w.name, typeof(Image));
                weapon.transform.SetParent(transform);
                weapon.transform.localPosition = Vector3.zero;
                weapon.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                weapon.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 40);
                Image weaponImage = weapon.GetComponent<Image>();
                weaponImage.sprite = w.GetComponent<SpriteRenderer>().sprite;
                weapon.SetActive(false);
                weapons.Add(weapon);
            }
            weapons[selectedWeapon].SetActive(true);
        }
    }

    public void AddWeapon()
    {
        GameObject weapon = new GameObject(playerController.player.weapons[playerController.player.weapons.Count - 1].name, typeof(Image));
        if (!weapons.Contains(weapon))
        {
            weapon.transform.SetParent(transform);
            weapon.transform.localPosition = Vector3.zero;
            weapon.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            weapon.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 40);
            Image weaponImage = weapon.GetComponent<Image>();
            weaponImage.sprite = playerController.player.weapons[playerController.player.weapons.Count - 1].GetComponent<SpriteRenderer>().sprite;
            weapon.SetActive(false);
            weapons.Add(weapon);
        }
        else return ;
    }
    private void Check()
    {

        for (int i = 0; i < weapons.Count; i++)
        {
            for (int j = 0; j < weapons.Count; j++)
            {
                if (weapons[i].name == weapons[j].name && i != j)
                {
                    GameObject tmp = weapons[j];
                    weapons.Remove(tmp);
                    Destroy(tmp);
                }
            }
        }
    }
}
