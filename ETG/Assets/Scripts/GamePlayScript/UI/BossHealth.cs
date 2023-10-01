using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public Slider healthSlider;

    public void SetValue(float bossHealth)
    {
        healthSlider.maxValue = bossHealth;
        healthSlider.value = bossHealth;
    }

    public void Damage(float dmg)
    {
        healthSlider.value -= dmg;
    }
}
