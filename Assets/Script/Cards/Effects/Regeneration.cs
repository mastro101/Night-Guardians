using UnityEngine;
using System.Collections;

public class Regeneration : CardEffect
{


    public override void ApplyEffect()
    {
        if(card.IsAlive)
        {
            card.Life = card.Data.Life;
        }
    }
}
