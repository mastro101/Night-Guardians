using UnityEngine;
using System.Collections;

public class Paura : CardEffect
{
    private void Start()
    {
        if (card.positionCard == PositionCard.OnField)
            combatManager.OnStartFight += ApplyEffect;
    }

    public override void ApplyEffect()
    {
        base.ApplyEffect();
        Debug.Log("Paura");
    }
}
