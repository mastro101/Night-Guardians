using UnityEngine;
using System.Collections;

//checked
public class AttackForEgg : CardEffect
{
    private void Start()
    {
		card.OnField += ApplyEffect;
		card.OnHand += ApplyEffect;
	}

	public override void ApplyEffect()
    {
        base.ApplyEffect();
        card.Attack = card.Data.Attack + (Variable * EggEvent.EggInDeck);
    }
}
