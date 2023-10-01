using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static HeartHealthSystem;
using static HeartHealthVisual;

public class ShieldBlankKeyCoinVisual : MonoBehaviour
{
    [SerializeField] private Sprite blank;
    [SerializeField] private Sprite shield;
    [SerializeField] private Sprite key;
    [SerializeField] private Sprite coin;
    public List<GameObject> blanks,shields;
    public TMP_Text keyTextBox,coinTextBox;
    public int keyAmount, coinAmount;
    public HeartHealthVisual heartCounter;
    // Start is called before the first frame update
    void Start()
    {
        heartCounter = GetComponent<HeartHealthVisual>();
        blanks = new List<GameObject>();
        shields = new List<GameObject>();
        keyAmount = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().player.silverKey;
        coinAmount = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().player.shell;

        SetUpBlank();
        CreateKeyCoinCounter();
    }
    void SetUpBlank() // Set up when UI active
    {
        Vector2 blankAnchoredPos = new Vector2(50, -120);
        for (int i = 0; i < GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().player.blank; i++)
        {
            CreateBlankImage(blankAnchoredPos);
            blankAnchoredPos += new Vector2(60, 0);
        }

    }

    public void SetUpShield() // Set up when UI active
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().player.shield > 0)
        {
            for (int i = 0; i < GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().player.shield; i++)
            {
                AddShield();
            }
        }
    }
    public void ChangeBlank(string AddOrSub) // Subriber call this method
    {
        if (AddOrSub == "Add") AddBlank();
        if (AddOrSub == "Sub") RemoveBlank();
    }

    public void ChangeShield(string AddOrSub) // Subriber call this method
    {
        if (AddOrSub == "Add") AddShield();
        if (AddOrSub == "Sub") RemoveShield();
    }
    public void ChangeKeyAmount(int amount) // Subcriber call this method
    {
        keyAmount += amount;
        keyTextBox.text = keyAmount.ToString();
    }
    public void ChangeCoinAmount(int amount) // Subcriber call this method
    {
        coinAmount += amount;
        coinTextBox.text = coinAmount.ToString();
    }
    #region Add, Remove Shield, Blank
    private void AddBlank()
    {
        if (blanks.Count != 0)
        {
            CreateBlankImage(blanks[blanks.Count - 1].GetComponent<RectTransform>().anchoredPosition + new Vector2(60, 0));
        }
        else
        {
            CreateBlankImage(new Vector2(50, -120));
        }
        
    }
    private void RemoveBlank()
    {
        GameObject blankR = blanks[blanks.Count - 1];
        Destroy(blankR);
        blanks.RemoveAt(blanks.Count - 1);
    }

    private void RemoveShield()
    {
        GameObject shieldR = shields[shields.Count - 1];
        Destroy(shieldR);
        shields.RemoveAt(shields.Count - 1);
    }
    private void AddShield()
    {
        
        if (shields.Count == 0)
        {
            Vector2 lastHeart = heartCounter.heartImageList[heartCounter.heartImageList.Count - 1].GetHeartImage().GetComponent<RectTransform>().anchoredPosition;
            CreateShieldImage(lastHeart + new Vector2(60, 0));
        }
        else
        {
            CreateShieldImage(shields[shields.Count-1].GetComponent<RectTransform>().anchoredPosition + new Vector2(60, 0));
        }
    }
    #endregion

    #region Instantiate Blank, Shield, Key, Coin
    void CreateBlankImage(Vector2 anchoredPosition) 
    {
        GameObject _blank = new GameObject("Blank",typeof(Image));
        _blank.transform.SetParent(transform);
        _blank.transform.localPosition = Vector3.zero;

        _blank.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
        _blank.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);

        Image blankUI = _blank.GetComponent<Image>();
        blankUI.sprite = blank;

        blanks.Add(_blank);
    }

    void CreateShieldImage(Vector2 anchoredPosition) 
    {
        GameObject _shield = new GameObject("Shield", typeof(Image));
        _shield.transform.SetParent(transform);
        _shield.transform.localPosition = Vector3.zero;

        _shield.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
        _shield.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);

        Image shieldUI = _shield.GetComponent<Image>();
        shieldUI.sprite = this.shield;

        shields.Add(_shield);
    }

    void CreateKeyCoinCounter() 
    {
        keyTextBox.text = keyAmount.ToString();
        coinTextBox.text = coinAmount.ToString();
    }
    #endregion
}
