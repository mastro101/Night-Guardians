using UnityEngine;
using System.Collections;

//checked
public class LifeForEgg : CardEffect
{
	private void Start()
	{
		card.OnField += ApplyEffect;
		card.OnHand += ApplyEffect;
	}

	public override void ApplyEffect()
	{
		base.ApplyEffect();
		card.Life = card.Data.Life + (Variable * EggEvent.EggInDeck);
	}
}
