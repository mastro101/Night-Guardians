using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbsLifeForSomething : CardEffect
{
	private void Start()
	{
		card.OnField += ApplyEffect;
		card.OnHand += ApplyEffect;
	}

	public override void ApplyEffect()
	{
		base.ApplyEffect();
		card.Life = card.Data.Life + (Variable * EggUtility.EggInDeck);
	}

	/// <summary>
	/// da reimplementare nei figli per cambiare il valore di aggiunta alla vita
	/// </summary>
	/// <returns></returns>
	protected abstract int EffectMightValue();
}
