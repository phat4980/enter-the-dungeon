using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class DeathUI : MonoBehaviour
{
    public Image capturScreen;
    public TMP_Text titleTxt,defeatTxt, timeTxt,moneyTxt,killTxt,reasonTxt,detailOfReasonTxt;
    public GameObject completed;

    public void SetForWin(string timer, int money, int kill, List<GameObject> weapon)
    {
        titleTxt.text = "YOU WON";
        defeatTxt.text = "COMPLETED";
        reasonTxt.text = "DEFEAT";
        detailOfReasonTxt.text = "Your Past";
        timeTxt.text = timer;
        moneyTxt.text = money.ToString();
        killTxt.text = kill.ToString();
        AddWeapon(weapon);
        gameObject.SetActive(true);
    }

    public void SetForLose(string timer, int money, int kill, string detailReason, List<GameObject> weapon, Texture2D captureScreen)
    {
        completed.SetActive(false);
        this.capturScreen.sprite = Sprite.Create(captureScreen, new Rect(0, 0, captureScreen.width, captureScreen.height), new Vector2(0.5f,0.5f),100f);
        titleTxt.text = "YOU DIED";
        defeatTxt.text = "DEFEATED";
        reasonTxt.text = "KILL BY";
        detailOfReasonTxt.text = detailReason;
        timeTxt.text = timer;
        moneyTxt.text = money.ToString();
        killTxt.text = kill.ToString();
        AddWeapon(weapon);
        gameObject.SetActive(true);
    }

    public void AddWeapon(List<GameObject> weapons)
    {
        int row = 0;
        int col = 0 ;
        int colMax = 6;
        int rowMax = 4;
        Vector2 offset = new Vector2(110, -120);
        foreach (GameObject w in weapons)
        {
            GameObject weapon = new GameObject(w.name, typeof(Image));
            weapon.transform.SetParent(transform);
            weapon.transform.localPosition = Vector3.zero;
            weapon.GetComponent<RectTransform>().anchoredPosition = offset;
            weapon.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 50);
            Image weaponImage = weapon.GetComponent<Image>();
            weaponImage.sprite = w.GetComponent<SpriteRenderer>().sprite;
            col++;
            if (col >= colMax)
            {
                row++;
                col = 0;
                offset = new Vector2(110, offset.y);
                offset += new Vector2(0,-65);
            }
            else
            {
                offset += new Vector2(110, 0);
            }
            if (row >= rowMax)
            {
                break;
            }
        }
    }
}
