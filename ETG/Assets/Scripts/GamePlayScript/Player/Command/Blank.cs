using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Blank : ICommand
{
    public int damage = 2;
    public Effect effect;

    public Blank(GameObject _eff)
    {
        effect = _eff.GetComponent<Effect>();
    }
    
    public void Execute()
    {
        effect.SetMotion(EffectName.Blank);
        effect.PlayAni();
    }
}
