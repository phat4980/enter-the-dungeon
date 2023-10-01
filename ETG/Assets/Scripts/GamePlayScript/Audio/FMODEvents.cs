using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Player SFX")]
    [field: SerializeField] public EventReference playerFootsteps { get; private set; }

    [field: Header("Bullet Shot SFX")]
    [field: SerializeField] public EventReference bulletShot { get; private set; }

    [field: Header("Player Bullet Shot SFX")]
    [field: SerializeField] public EventReference MainGun { get; private set; }
    [field: SerializeField] public EventReference CrossBow { get; private set; }
    [field: SerializeField] public EventReference Ak47Gun { get; private set; }
    [field: SerializeField] public EventReference Blasphemy { get; private set; }
    [field: SerializeField] public EventReference BubbleGun { get; private set; }
    [field: SerializeField] public EventReference CactusGun { get; private set; }
    [field: SerializeField] public EventReference CrystalGun { get; private set; }
    [field: SerializeField] public EventReference Siren { get; private set; }

    // [field: SerializeField] public EventReference ElectricRifle { get; private set; }
    // [field: SerializeField] public EventReference FossilizedGun { get; private set; }
    // [field: SerializeField] public EventReference Phoenix { get; private set; }
    // [field: SerializeField] public EventReference SuperSoaker { get; private set; }
    [field: Tooltip("Fexible Gun Shot SFX")]
    [field: SerializeField] public EventReference Shotgun { get; private set; }
    [field: SerializeField] public EventReference ShogunSawOff { get; private set; }
    [field: SerializeField] public EventReference TommyGun { get; private set; }


    public static FMODEvents instance { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
    }
}