using UnityEngine;
using System.Collections;

public class LifeForEgg : CardEffect
{
    private void Start()
    {
        EggEvent.AddedEgg += ApplyEffect;
    }

    public override void ApplyEffect()
    {
        base.ApplyEffect();
        card.Life += EggEvent.EggInDeck;
    }
}
