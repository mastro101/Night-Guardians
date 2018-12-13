using UnityEngine;
using System.Collections;

public class Paura : CardEffect
{
    private void Start()
    {
        card.OnField += SubscribeEvent;
    }

    public override void SubscribeEvent()
    {
        base.SubscribeEvent();
        combatManager.OnStartFight += ApplyEffect;
    }

    public override void ApplyEffect()
    {
        base.ApplyEffect();
        Debug.Log("Paura");
    }
}
