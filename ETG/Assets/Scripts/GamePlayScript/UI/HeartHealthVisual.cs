using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartHealthVisual : MonoBehaviour
{
    [SerializeField] private Sprite heartFull;
    [SerializeField] private Sprite heartHalf;
    [SerializeField] private Sprite heartEmpty;

    public List<HeartImage> heartImageList;
    public HeartHealthSystem heartHealthSystem;
    private void Start()
    {
        heartImageList = new List<HeartImage>();
        SetHeartHealthSystem(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().healthSystem);
        GetComponent<ShieldBlankKeyCoinVisual>().SetUpShield();
    }
    

    public void SetHeartHealthSystem(HeartHealthSystem heartHealthSystem)
    {
        this.heartHealthSystem = heartHealthSystem;

        List<HeartHealthSystem.Heart> heartList = heartHealthSystem.GetHeartList();
        Vector2 heartAnchoredPos = new Vector2(50, -50);
        for (int i = 0; i < heartList.Count; i++)
        {
            HeartHealthSystem.Heart heart = heartList[i];
            CreateHeartImage(heartAnchoredPos).SetHeartFragments(heart.GetFragmentAmount());
            heartAnchoredPos += new Vector2(65,0);
        }
    }
    public void ExpandHeartList()
    {
        Vector2 newHeartAnchoredPos = heartImageList[heartImageList.Count - 1]
            .GetHeartImage()
            .GetComponent<RectTransform>().anchoredPosition + new Vector2(65,0);

        CreateHeartImage(newHeartAnchoredPos).SetHeartFragments(
            heartHealthSystem.GetHeartList()[heartHealthSystem.GetHeartList().Count - 1]
            .GetFragmentAmount());
    }
    public void HeartHealthSystemHaveChange()
    {
        List<HeartHealthSystem.Heart> hearts = heartHealthSystem.GetHeartList();
        for(int i = 0; i < heartImageList.Count; i++)
        {
            HeartImage heartUI = heartImageList[i];
            HeartHealthSystem.Heart heart = hearts[i];
            heartUI.SetHeartFragments(heart.GetFragmentAmount());
        }
    }
    private HeartImage CreateHeartImage(Vector2 anchoredPosition)
    {
        GameObject heart = new GameObject("Heart", typeof(Image));
        heart.transform.SetParent(transform);
        heart.transform.localPosition = Vector3.zero;

        heart.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
        heart.GetComponent<RectTransform>().sizeDelta = new Vector2(50,50);

        Image heartUI = heart.GetComponent<Image>();
        heartUI.sprite = heartFull;

        HeartImage heartImage = new HeartImage(this,heartUI);
        heartImageList.Add(heartImage);

        return heartImage;
    } 

    public class HeartImage
    {
        private Image heartImage;
        private HeartHealthVisual visual;
        public HeartImage(HeartHealthVisual heartVisual,Image heartImage)
        {
            this.visual = heartVisual;
            this.heartImage = heartImage;
        }
        public Image GetHeartImage()
        {
            return heartImage;
        }
        public void SetHeartFragments(int fragment)
        {
            switch (fragment)
            {
                case 0:
                    heartImage.sprite = visual.heartEmpty;
                    break;
                case 1:
                    heartImage.sprite = visual.heartHalf;
                    break;
                case 2:
                    heartImage.sprite = visual.heartFull;
                    break;
            }
        }
    }
}
