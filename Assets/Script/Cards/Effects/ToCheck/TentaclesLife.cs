using UnityEngine;
using System.Collections;

public class TentaclesLife : CardEffect
{
    private void Start()
    {
        card.OnField += SubscribeEvent;
        card.OnScarti += UnsubscribeEvent;
        card.OnDeath += UnsubscribeEvent;
        card.OnHand += UnsubscribeEvent;
    }

    public override void SubscribeEvent()
    {
        combatManager.OnEndTurn += ApplyEffect;
    }

    public override void ApplyEffect()
    {
        base.ApplyEffect();
        combatManager.Enemy.Life -= Variable;
        UnsubscribeEvent();
    }

    public override void UnsubscribeEvent()
    {
        base.UnsubscribeEvent();
        combatManager.OnEndTurn -= ApplyEffect;
    }
}
