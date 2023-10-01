using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class HeartHealthSystem
{
    public const int MAX_FRAGMENT_AMOUNT = 2;
    [SerializeField]private List<Heart> heartList;
    public HeartHealthSystem(int amount)
    {
        heartList = new List<Heart>();
        for (int i = 0; i < amount; i++)
        {
            Heart heart = new Heart(MAX_FRAGMENT_AMOUNT);
            heartList.Add(heart);
        }
    }

    public bool Die()
    {
        foreach (Heart heart in heartList)
        {
            if (heart.GetFragmentAmount() > 0) return false;
        }
        return true;
    }
    public void expandHeart()
    {
        Heart heart = new Heart(MAX_FRAGMENT_AMOUNT);
        heartList.Add(heart);
    }
    public List<Heart> GetHeartList()
    {
        return heartList;
    }
    public void Damage()
    {
        for (int i = heartList.Count - 1; i >= 0; i--)
        {
            Heart heart = heartList[i];
            if (heart.GetFragmentAmount() > 0)
            {
                heart.Damage();
                break;
            }
        }
    }

    public void Heal(int value) // 1 - 2
    {
        for (int i = 0; i < heartList.Count; i++)
        {
            
            Heart heart = heartList[i];
            int missingPiece = MAX_FRAGMENT_AMOUNT - heart.GetFragmentAmount();
            if (value > missingPiece)
            {
                value -= missingPiece;
                heart.Heal(missingPiece);
            }
            else
            {
                heart.Heal(value);
                break;
            }
        }
    }
    [System.Serializable]
    public class Heart
    {
        [SerializeField]private int fragments;
        public Heart(int fragments)
        {
            this.fragments = fragments;
        }

        public int GetFragmentAmount()
        {
            return fragments;
        }
        public void SetFragment(int value)
        {
            fragments = value;
        }
        public void Damage() 
        {
            fragments -= 1;
        }

        public void Heal(int value)
        {
            if(value + fragments > MAX_FRAGMENT_AMOUNT)
            {
                fragments = MAX_FRAGMENT_AMOUNT;
            }
            else
            {
                fragments += value;
            }
        }
    }
}
