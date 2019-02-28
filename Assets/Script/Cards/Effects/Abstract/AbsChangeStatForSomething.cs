﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbsChangeStatForSomething : CardEffect {
	private void Start()
	{
		card.OnField += ApplyEffect;
		card.OnHand += ApplyEffect;
	}

	public override void ApplyEffect()
	{
		base.ApplyEffect();
		ChangeStat();
	}

	/// <summary>
	/// da reimplementare nei figli per cambiare il valore di aggiunta alla vita
	/// </summary>
	/// <returns></returns>
	protected abstract int EffectMightValue();

	protected abstract void ChangeStat();
}

public abstract class AbsLifeForSomething : AbsChangeStatForSomething
{
	protected override void ChangeStat()
	{
		card.Life = card.Data.Life + (Variable * EggUtility.EggInDeck);
	}
}

public abstract class AbsAttackForSomething : AbsChangeStatForSomething
{
	protected override void ChangeStat()
	{
		card.Attack = card.Data.Attack + (Variable * EffectMightValue());
	}
}