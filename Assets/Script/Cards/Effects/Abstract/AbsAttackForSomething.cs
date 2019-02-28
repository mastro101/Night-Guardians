using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbsAttackForSomething : CardEffect
{
	private void Start()
	{
		card.OnField += ApplyEffect;
		card.OnHand += ApplyEffect;
	}

	public override void ApplyEffect()
	{
		base.ApplyEffect();
		card.Attack = card.Data.Attack + (Variable * EffectMightValue());
	}

	/// <summary>
	/// da reimplementare nei figli per cambiare il valore di aggiunta all attacco
	/// </summary>
	/// <returns></returns>
	protected abstract int EffectMightValue();
}
