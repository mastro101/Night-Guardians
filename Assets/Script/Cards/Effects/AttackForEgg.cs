using UnityEngine;
using System.Collections;

public class AttackForEgg : CardEffect
{
    private void Start()
    {
        EggEvent.AddedEgg += ApplyEffect;
    }

    public override void ApplyEffect()
    {
        base.ApplyEffect();
        card.Attack += Variable * EggEvent.EggInDeck;
    }
}
