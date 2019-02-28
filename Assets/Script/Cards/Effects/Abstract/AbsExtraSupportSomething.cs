using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbsExtraSupportSomething : CardEffect {

	private void Start()
	{
		combatManager.OnSupport += ApplyEffect;
	}

	public override void ApplyEffect()
	{
		if (card.positionCard == PositionCard.OnSupport)
		{
			base.ApplyEffect();
			combatManager.CalculateSupport(GetBuffType(), Variable);
		}
	}

	protected abstract Buff GetBuffType();

}
